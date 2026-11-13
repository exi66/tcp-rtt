using PacketDotNet;
using SharpPcap;
using TcpRtt.Core;

namespace TcpRtt.Capture;

/// <summary>Captures TCP/IPv4 segments from a network device via Npcap.</summary>
internal sealed class PacketCaptureService(ILiveDevice device) : IDisposable
{
    // Packets carry their own capture timestamps, so a short read timeout only affects
    // how quickly samples reach the UI, not the measured RTT.
    private const int ReadTimeoutMs = 10;

    private bool _running;

    /// <summary>Raised on the capture thread. The timestamp is the capture time in UTC <see cref="DateTime.Ticks"/>.</summary>
    public event Action<TcpSegment, long>? SegmentCaptured;

    public static IReadOnlyList<ILiveDevice> GetDevices() => [.. CaptureDeviceList.Instance];

    public void Start(string filter)
    {
        if (_running)
        {
            throw new InvalidOperationException("Capture is already running.");
        }

        device.Open(new DeviceConfiguration { Mode = DeviceModes.MaxResponsiveness, ReadTimeout = ReadTimeoutMs });
        try
        {
            device.Filter = filter;
            device.OnPacketArrival += OnPacketArrival;
            device.StartCapture();
            _running = true;
        }
        catch
        {
            device.OnPacketArrival -= OnPacketArrival;
            device.Close();
            throw;
        }
    }

    public void Dispose()
    {
        if (!_running)
        {
            return;
        }

        _running = false;
        device.OnPacketArrival -= OnPacketArrival;
        try
        {
            device.StopCapture();
        }
        finally
        {
            device.Close();
        }
    }

    private static long ToUtcTicks(PosixTimeval timeval) =>
        DateTime.UnixEpoch.Ticks
        + ((long)timeval.Seconds * TimeSpan.TicksPerSecond)
        + ((long)timeval.MicroSeconds * TimeSpan.TicksPerMicrosecond);

    private void OnPacketArrival(object sender, PacketCapture e)
    {
        var raw = e.GetPacket();
        TcpSegment segment;

        try
        {
            var packet = Packet.ParsePacket(raw.LinkLayerType, raw.Data);
            if (packet.Extract<IPv4Packet>() is not { } ip || packet.Extract<TcpPacket>() is not { } tcp)
            {
                return;
            }

            segment = new TcpSegment(
                ip.SourceAddress,
                tcp.SourcePort,
                ip.DestinationAddress,
                tcp.DestinationPort,
                tcp.SequenceNumber,
                tcp.AcknowledgmentNumber,
                tcp.Acknowledgment,
                tcp.PayloadData?.Length ?? 0);
        }
#pragma warning disable CA1031 // A malformed packet must not kill the capture thread.
        catch (Exception)
#pragma warning restore CA1031
        {
            return;
        }

        SegmentCaptured?.Invoke(segment, ToUtcTicks(raw.Timeval));
    }
}
