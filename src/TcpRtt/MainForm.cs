using System.Collections.Concurrent;
using System.Net;
using SharpPcap;
using TcpRtt.Capture;
using TcpRtt.Core;

namespace TcpRtt;

public partial class MainForm : Form
{
    private const int HistoryLength = 50;
    private const double MinVisibleRttMs = 200;
    private const double YAxisHeadroomMs = 10;
    private static readonly TimeSpan SegmentTimeout = TimeSpan.FromSeconds(1);

    private readonly ConcurrentQueue<double> _pendingSamples = new();
    private readonly RollingWindow _history = new(HistoryLength);
    private AppSettings _settings;

    private PacketCaptureService? _capture;
    private IRttCalculator? _calculator;
    private double _minRttMs;

    private ScottPlot.Plottables.Signal? _signal;

    private bool _isOverlayEnabled;
    private Size _dragOffset;

    public MainForm()
    {
        InitializeComponent();

        Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath) ?? Icon;
        _settings = AppSettings.Load();

        LoadNetworkInterfaces();
        LoadCalculationMethods();
        ApplySettings();
        InitGraph();
        AttachOverlayDrag(this);
        ShowStatistics(new RttCounters());
    }

    private bool IsCapturing => _capture is not null;

    private void LoadNetworkInterfaces()
    {
        IReadOnlyList<ILiveDevice> devices;
        try
        {
            devices = PacketCaptureService.GetDevices();
        }
        catch (Exception ex) when (ex is DllNotFoundException or PcapException or TypeInitializationException)
        {
            ShowError($"Failed to initialize Npcap: {ex.Message}\n\nMake sure Npcap is installed (https://npcap.com).", "Npcap error");
            startButton.Enabled = false;
            return;
        }

        if (devices.Count == 0)
        {
            ShowError("No capture devices found. Make sure Npcap is installed (https://npcap.com).", "Npcap error");
            startButton.Enabled = false;
            return;
        }

        foreach (var device in devices)
        {
            networkComboBox.Items.Add(new DeviceItem(device));
        }

        networkComboBox.SelectedIndex = 0;
    }

    private void LoadCalculationMethods()
    {
        foreach (var method in CalculationMethod.All)
        {
            calculationMethodComboBox.Items.Add(method);
        }

        calculationMethodComboBox.SelectedIndex = 0;
    }

    private void ApplySettings()
    {
        ipMaskTextBox.Text = _settings.Network;
        portNumeric.Value = Math.Clamp(_settings.Port, portNumeric.Minimum, portNumeric.Maximum);
        minRttNumeric.Value = Math.Clamp(_settings.MinRttMs, minRttNumeric.Minimum, minRttNumeric.Maximum);

        SelectItem(networkComboBox, (DeviceItem item) => item.Device.Name == _settings.DeviceName);
        SelectItem(calculationMethodComboBox, (CalculationMethod method) => method.Name == _settings.CalculationMethod);

        static void SelectItem<T>(ComboBox comboBox, Func<T, bool> predicate)
        {
            int index = comboBox.Items.Cast<T>().ToList().FindIndex(item => predicate(item));
            if (index >= 0)
            {
                comboBox.SelectedIndex = index;
            }
        }
    }

    private void SaveSettings()
    {
        _settings = _settings with
        {
            Network = ipMaskTextBox.Text.Trim(),
            Port = (ushort)portNumeric.Value,
            MinRttMs = minRttNumeric.Value,
            DeviceName = (networkComboBox.SelectedItem as DeviceItem)?.Device.Name ?? _settings.DeviceName,
            CalculationMethod = (calculationMethodComboBox.SelectedItem as CalculationMethod)?.Name,
        };
        _settings.Save();
    }

    private void InitGraph()
    {
        pingPlot.UserInputProcessor.IsEnabled = false;
        pingPlot.Plot.XLabel("Packets");
        pingPlot.Plot.YLabel("Time (ms)");
        RedrawPlot();
    }

    private void startButton_Click(object? sender, EventArgs e)
    {
        if (IsCapturing)
        {
            StopCapture();
            return;
        }

        if (networkComboBox.SelectedItem is not DeviceItem device)
        {
            ShowError("Select a network interface.", "Input error");
            return;
        }

        if (calculationMethodComboBox.SelectedItem is not CalculationMethod method || !TryReadNetwork(out var network))
        {
            return;
        }

        StartCapture(device.Device, network, (ushort)portNumeric.Value, method);
    }

    private bool TryReadNetwork(out IPNetwork network)
    {
        network = default;

        if (!NetworkInput.TryParse(ipMaskTextBox.Text, out var address, out int prefixLength))
        {
            MessageBox.Show(
                "Invalid server address.\n\nUse an IPv4 address (203.0.113.10) or a network in CIDR notation (203.0.113.0/24).",
                "Input error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        network = NetworkInput.ToNetwork(address, prefixLength);

        if (!network.BaseAddress.Equals(address))
        {
            var answer = MessageBox.Show(
                $"{address}/{prefixLength} is not a network address.\nUse {network} instead?",
                "Input error",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return false;
            }
        }

        ipMaskTextBox.Text = prefixLength == 32 ? address.ToString() : network.ToString();
        return true;
    }

    private void StartCapture(ILiveDevice device, IPNetwork network, ushort port, CalculationMethod method)
    {
        var calculator = method.Create(network, port, SegmentTimeout);
        var capture = new PacketCaptureService(device);
        capture.SegmentCaptured += (segment, timestamp) =>
        {
            if (calculator.Process(segment, timestamp) is double rtt)
            {
                _pendingSamples.Enqueue(rtt);
            }
        };

        try
        {
            capture.Start($"tcp and net {network} and port {port}");
        }
        catch (Exception ex) when (ex is PcapException or DllNotFoundException or InvalidOperationException)
        {
            ShowError(
                $"Failed to start capture on \"{device.Description}\":\n{ex.Message}\n\n" +
                "If Npcap is installed in admin-only mode, run the application as administrator.",
                "Capture error");
            return;
        }

        _capture = capture;
        _calculator = calculator;
        _minRttMs = (double)minRttNumeric.Value;

        SetInputsEnabled(false);
        startButton.Text = "Stop";
        uiTimer.Start();
        SaveSettings();
    }

    private void StopCapture()
    {
        uiTimer.Stop();

        _capture?.Dispose();
        _capture = null;

        RefreshStatistics();
        _calculator = null;

        SetInputsEnabled(true);
        startButton.Text = "Start";
    }

    private void SetInputsEnabled(bool enabled)
    {
        networkComboBox.Enabled = enabled;
        ipMaskTextBox.Enabled = enabled;
        portNumeric.Enabled = enabled;
        minRttNumeric.Enabled = enabled;
        calculationMethodComboBox.Enabled = enabled;
    }

    private void uiTimer_Tick(object? sender, EventArgs e) => RefreshStatistics();

    /// <summary>Moves samples collected by the capture thread into the UI. Runs on the UI thread.</summary>
    private void RefreshStatistics()
    {
        bool hasNewSamples = false;
        while (_pendingSamples.TryDequeue(out double rtt))
        {
            if (rtt >= _minRttMs)
            {
                _history.Add(rtt);
                hasNewSamples = true;
            }
        }

        _calculator?.ExpireStale(DateTime.UtcNow.Ticks);
        ShowStatistics(_calculator?.Counters ?? new RttCounters());

        if (hasNewSamples)
        {
            RedrawPlot();
        }
    }

    private void ShowStatistics(RttCounters counters)
    {
        latestValueLabel.Text = $"{_history.Latest:F1} ms";
        avgValueLabel.Text = $"{_history.Average:F1} ms";
        maxValueLabel.Text = $"{_history.Max:F1} ms";
        timeoutValueLabel.Text = $"{counters.TimeoutPercent:F1}%";
    }

    private void RedrawPlot()
    {
        if (_signal is not null)
        {
            pingPlot.Plot.Remove(_signal);
            _signal = null;
        }

        if (_history.Count > 0)
        {
            _signal = pingPlot.Plot.Add.Signal(_history.ToArray());
        }

        double yMax = Math.Max(_history.Max, MinVisibleRttMs) + YAxisHeadroomMs;
        pingPlot.Plot.Axes.SetLimits(0, HistoryLength - 1, 0, yMax);
        pingPlot.Refresh();
    }

    private void clearButton_Click(object? sender, EventArgs e)
    {
        _pendingSamples.Clear();
        _history.Clear();
        _calculator?.Reset();

        ShowStatistics(new RttCounters());
        RedrawPlot();
    }

    private void overlayButton_Click(object? sender, EventArgs e)
    {
        _isOverlayEnabled = !_isOverlayEnabled;

        TopMost = _isOverlayEnabled;
        FormBorderStyle = _isOverlayEnabled ? FormBorderStyle.None : FormBorderStyle.Sizable;
        ShowInTaskbar = !_isOverlayEnabled;
    }

    /// <summary>A borderless overlay has no title bar, so let the user drag it by any non-input area.</summary>
    private void AttachOverlayDrag(Control control)
    {
        if (control is TextBoxBase or ButtonBase or ComboBox or UpDownBase)
        {
            return;
        }

        control.MouseDown += OverlayDrag_MouseDown;
        control.MouseMove += OverlayDrag_MouseMove;

        foreach (Control child in control.Controls)
        {
            AttachOverlayDrag(child);
        }
    }

    private void OverlayDrag_MouseDown(object? sender, MouseEventArgs e)
    {
        if (_isOverlayEnabled && e.Button == MouseButtons.Left)
        {
            _dragOffset = new Size(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
        }
    }

    private void OverlayDrag_MouseMove(object? sender, MouseEventArgs e)
    {
        if (_isOverlayEnabled && e.Button == MouseButtons.Left)
        {
            Location = Cursor.Position - _dragOffset;
        }
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (IsCapturing)
        {
            StopCapture();
        }

        SaveSettings();
    }

    private static void ShowError(string message, string caption) =>
        MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

    private sealed record DeviceItem(ILiveDevice Device)
    {
        public override string ToString() =>
            string.IsNullOrWhiteSpace(Device.Description) ? Device.Name : Device.Description;
    }
}
