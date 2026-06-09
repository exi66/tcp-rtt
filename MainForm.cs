using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using PacketDotNet;
using SharpPcap;
using System.Linq;

namespace TcpPingMonitorNet10
{
    public partial class MainForm : Form
    {
        private System.Net.IPAddress? targetSubnetAddress;
        private int targetCidrMask = 24;
        private ushort serverPort = 8889;

        private ILiveDevice? selectedDevice;
        private bool isCapturing = false;

        private readonly Dictionary<uint, long> sentPackets = new Dictionary<uint, long>();
        private long totalPackets = 0;
        private long totalPacketsSent = 0;
        private long lostPackets = 0;

        private double currentThreshold = 0;

        private ScottPlot.Plottables.DataLogger loggerPlot = null!;

        private bool isOverlayEnabled = false;

        private Action<IPv4Packet, TcpPacket, long>? _packetProcessor;
        private Dictionary<string, Action<IPv4Packet, TcpPacket, long>> _availableMethods = new();

        public MainForm()
        {
            InitializeComponent();

            using (MemoryStream ms = new MemoryStream(TCP_RTT.Properties.Resources.favicon))
            {
                this.Icon = new Icon(ms);
            }

            LoadNetworkInterfaces();
            InitCalculationOptions();
            InitGraph();
        }

        private void InitCalculationOptions()
        {
            _availableMethods = new Dictionary<string, Action<IPv4Packet, TcpPacket, long>>
            {
                { "RTT based on sent packets", ProcessLogicRtt },
            };

            foreach (var name in _availableMethods.Keys)
            {
                calculationMethodComboBox.Items.Add(name);
            }
            calculationMethodComboBox.SelectedIndex = 0;
        }

        private void InitGraph()
        {
            if (pingPlot == null) return;

            loggerPlot = pingPlot.Plot.Add.DataLogger();

            pingPlot.UserInputProcessor.IsEnabled = false;

            // pingPlot.Plot.Title("Latency history");
            pingPlot.Plot.XLabel("Packets");
            pingPlot.Plot.YLabel("Time (ms)");

            var slidingAxis = new ScottPlot.AxisLimitManagers.Slide()
            {
                Width = 50
            };

            loggerPlot.AxisManager = slidingAxis;

            pingPlot.Plot.Axes.SetLimits(0, 50, 0, 200);

            pingPlot.Refresh();
        }

        private void LoadNetworkInterfaces()
        {
            try
            {
                var devices = CaptureDeviceList.Instance;
                if (devices.Count == 0)
                {
                    MessageBox.Show("Драйвер Npcap не найден в системе!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var dev in devices)
                {
                    networkComboBox.Items.Add(dev.Description);
                }
                if (networkComboBox.Items.Count > 0) networkComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка инициализации Npcap: " + ex.Message);
            }
        }

        private static bool TryParseNetworkInput(string input, out System.Net.IPAddress? address, out int cidr)
        {
            address = null;
            cidr = 24;
            string[] parts = input.Split('/');

            if (System.Net.IPAddress.TryParse(parts[0], out address))
            {
                if (parts.Length == 1) return true;
                if (parts.Length == 2 && int.TryParse(parts[1], out cidr) && cidr >= 0 && cidr <= 32) return true;
            }
            return false;
        }

        private static bool IsNetworkAddress(System.Net.IPAddress address, int cidr)
        {
            byte[] bytes = address.GetAddressBytes();
            byte[] netBytes = GetNetworkAddress(address, cidr).GetAddressBytes();
            return bytes.SequenceEqual(netBytes);
        }

        private static System.Net.IPAddress GetNetworkAddress(System.Net.IPAddress address, int cidr)
        {
            byte[] ipBytes = address.GetAddressBytes();
            byte[] maskBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                int bitsInByte = Math.Max(0, Math.Min(8, cidr - (i * 8)));
                maskBytes[i] = (byte)(bitsInByte == 0 ? 0 : (0xFF << (8 - bitsInByte)));
                ipBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
            }

            return new System.Net.IPAddress(ipBytes);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (isCapturing)
            {
                StopCapture();
            }
            else
            {
                if (!ushort.TryParse(portTextBox.Text, out ushort parsedPort))
                {
                    MessageBox.Show("The port must be a number from 1 to 65535!");
                    return;
                }

                string rawIpInput = ipMaskTextBox.Text.Trim();

                if (!TryParseNetworkInput(rawIpInput, out System.Net.IPAddress? parsedAddress, out int cidr))
                {
                    MessageBox.Show(
                        "Invalid network format.\n\n" +
                        "Use the format: IP/CIDR (for example: 192.168.1.0/24).",
                        "Input error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (parsedAddress != null && !IsNetworkAddress(parsedAddress, cidr))
                {
                    var networkAddress = GetNetworkAddress(parsedAddress, cidr);
                    string message = $"The entered address ({parsedAddress}/{cidr}) is not a network address.\n" +
                                     $"Use {networkAddress}/{cidr} instead?";

                    var result = MessageBox.Show(message, "Input error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        parsedAddress = networkAddress;
                        ipMaskTextBox.Text = $"{networkAddress}/{cidr}";
                    }
                    else
                    {
                        return;
                    }
                }

                targetSubnetAddress = parsedAddress;
                targetCidrMask = cidr;
                serverPort = parsedPort;

                StartCapture();
            }
        }

        private void StartCapture()
        {

            double.TryParse(thresholdTextBox.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out currentThreshold);

            string? selectedMethodName = calculationMethodComboBox.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedMethodName) && _availableMethods.TryGetValue(selectedMethodName, out var processor))
            {
                _packetProcessor = processor;
            }
            else
            {
                MessageBox.Show("Error: The selected calculation method was not found.");
                return;
            }

            int selectedIndex = networkComboBox.SelectedIndex;
            if (selectedIndex < 0) return;

            selectedDevice = CaptureDeviceList.Instance[selectedIndex];
            selectedDevice.Open(DeviceModes.Promiscuous, 100);

            var networkAddress = GetNetworkAddress(targetSubnetAddress!, targetCidrMask);
            string bpfNet = string.Format("{0}/{1}", networkAddress.ToString(), targetCidrMask);
            string filter = string.Format("tcp and net {0} and port {1}", bpfNet, serverPort);
            selectedDevice.Filter = filter;

            selectedDevice.OnPacketArrival += OnPacketArrival;
            selectedDevice.StartCapture();

            isCapturing = true;
            startButton.Text = "Stop";
            networkComboBox.Enabled = false;
            ipMaskTextBox.Enabled = false;
            portTextBox.Enabled = false;
            thresholdTextBox.Enabled = false;
            calculationMethodComboBox.Enabled = false;
        }

        private void StopCapture()
        {
            if (selectedDevice != null)
            {
                selectedDevice.OnPacketArrival -= OnPacketArrival;
                selectedDevice.StopCapture();
                selectedDevice.Close();
            }

            isCapturing = false;
            startButton.Text = "Start";
            networkComboBox.Enabled = true;
            ipMaskTextBox.Enabled = true;
            portTextBox.Enabled = true;
            thresholdTextBox.Enabled = true;
            calculationMethodComboBox.Enabled = true;
            _packetProcessor = null;

            lock (sentPackets) { sentPackets.Clear(); }
        }

        private bool IsInSubnet(System.Net.IPAddress ipAddress)
        {
            if (targetSubnetAddress == null) return false;

            byte[] ipBytes = ipAddress.GetAddressBytes();
            byte[] subnetBytes = targetSubnetAddress.GetAddressBytes();

            if (ipBytes.Length != subnetBytes.Length) return false;

            int maskBytes = targetCidrMask / 8;
            int maskBits = targetCidrMask % 8;

            for (int i = 0; i < maskBytes; i++)
            {
                if (ipBytes[i] != subnetBytes[i]) return false;
            }

            if (maskBits > 0)
            {
                byte bitMask = (byte)(0xFF << (8 - maskBits));
                if ((ipBytes[maskBytes] & bitMask) != (subnetBytes[maskBytes] & bitMask))
                    return false;
            }

            return true;
        }

        private void OnPacketArrival(object sender, PacketCapture e)
        {
            totalPackets++;
            var rawPacket = e.GetPacket();
            var packet = Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);

            var ipPacket = packet.Extract<IPv4Packet>();
            var tcpPacket = packet.Extract<TcpPacket>();

            if (ipPacket == null || tcpPacket == null) return;

            _packetProcessor?.Invoke(ipPacket, tcpPacket, Stopwatch.GetTimestamp());
        }


        private void ProcessLogicRtt(IPv4Packet ipPacket, TcpPacket tcpPacket, long nowTicks)
        {
            int payloadLength = tcpPacket.PayloadData?.Length ?? 0;

            if (IsInSubnet(ipPacket.DestinationAddress) && tcpPacket.DestinationPort == serverPort && payloadLength > 0)
            {
                uint nextSeq = tcpPacket.SequenceNumber + (uint)payloadLength;
                lock (sentPackets)
                {
                    sentPackets[nextSeq] = nowTicks;
                    totalPacketsSent++;
                }
            }

            if (IsInSubnet(ipPacket.SourceAddress) && tcpPacket.SourcePort == serverPort && tcpPacket.Acknowledgment)
            {
                lock (sentPackets)
                {
                    var confirmed = sentPackets.Where(kvp => tcpPacket.AcknowledgmentNumber >= kvp.Key).ToList();

                    foreach (var kvp in confirmed)
                    {
                        double rttMs = (double)(nowTicks - kvp.Value) / Stopwatch.Frequency * 1000;

                        if (currentThreshold <= 0 || rttMs >= currentThreshold)
                        {
                            UpdateUI(rttMs);
                        }

                        sentPackets.Remove(kvp.Key);
                    }
                }
            }

            lock (sentPackets)
            {
                var timeoutTicks = Stopwatch.Frequency * 1;
                var expired = sentPackets.Where(kvp => (nowTicks - kvp.Value) > timeoutTicks).ToList();

                foreach (var kvp in expired)
                {
                    lostPackets++;
                    sentPackets.Remove(kvp.Key);
                }
            }
        }

        private void UpdateUI(double rttMs)
        {
            if (!this.IsHandleCreated) return;

            this.BeginInvoke((MethodInvoker)delegate
            {
                double lossPercent = totalPacketsSent > 0 ? ((double)lostPackets / totalPacketsSent) * 100 : 0;

                latestValueLabel.Text = string.Format("{0:F1} ms", rttMs);
                packetLossValueLabel.Text = string.Format("{0:F1}%", lossPercent);

                loggerPlot.Add(rttMs);

                var lastPoints = loggerPlot.Data.Coordinates;
                int count = lastPoints.Count;
                int takeCount = Math.Min(count, 50);

                if (takeCount > 0)
                {
                    var window = lastPoints.Skip(count - takeCount).Select(p => p.Y).ToList();

                    double avgPing = window.Average();
                    double maxPing = window.Max();

                    avgValueLabel.Text = string.Format("{0:F1} ms", avgPing);
                    peaksValueLabel.Text = string.Format("{0:F1} ms", maxPing);

                    double dynamicYMax = Math.Max(maxPing, 200) + 10;
                    pingPlot.Plot.Axes.SetLimits(0, 50, 0, dynamicYMax);
                    pingPlot.Refresh();
                }
            });
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            lock (sentPackets)
            {
                sentPackets.Clear();
            }

            totalPackets = 0;
            totalPacketsSent = 0;
            lostPackets = 0;

            if (loggerPlot != null)
            {
                loggerPlot.Clear();
                pingPlot.Refresh();
            }

            latestValueLabel.Text = "0.0 ms";
            avgValueLabel.Text = "0.0 ms";
            peaksValueLabel.Text = "0.0 ms";
            packetLossValueLabel.Text = "0.0%";
        }

        private void thresholdTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (char.IsControl(e.KeyChar)) return;

                if (char.IsDigit(e.KeyChar)) return;

                if (e.KeyChar == '.' || e.KeyChar == ',')
                {
                    if (tb.Text.Contains(".") || tb.Text.Contains(","))
                    {
                        e.Handled = true;
                    }
                    return;
                }

                e.Handled = true;
            }
        }

        private void thresholdTextBox_Leave(object sender, EventArgs e)
        {
            if (!double.TryParse(thresholdTextBox.Text, out _))
            {
                thresholdTextBox.Text = "0.1";
            }
        }

        private void openOverlayButton_Click(object sender, EventArgs e)
        {
            if (this.isOverlayEnabled)
            {
                this.TopMost = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.ShowInTaskbar = true;
                this.isOverlayEnabled = false;
            }
            else
            {
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.ShowInTaskbar = false;
                this.isOverlayEnabled = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isCapturing)
            {
                if (selectedDevice != null)
                {
                    selectedDevice.OnPacketArrival -= OnPacketArrival;
                    Task.Run(() =>
                    {
                        try
                        {
                            selectedDevice.StopCapture();
                            selectedDevice.Close();
                        }
                        catch { 

                        }
                    });
                }
            }
        }
    }
}