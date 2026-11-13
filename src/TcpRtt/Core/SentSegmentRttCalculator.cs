using System.Net;

namespace TcpRtt.Core;

/// <summary>
/// Measures the time between a data segment sent to the server and the server's ACK for it.
/// </summary>
internal sealed class SentSegmentRttCalculator(IPNetwork serverNetwork, ushort serverPort, TimeSpan timeout)
    : IRttCalculator
{
    private readonly RttTracker _tracker = new(timeout);

    // Capture timestamps come from the Npcap clock, which can drift from the system clock.
    // Remembering the offset lets ExpireStale compare wall-clock time against capture time.
    private long _captureClockOffset;

    public RttCounters Counters => _tracker.Counters;

    public double? Process(in TcpSegment segment, long timestamp)
    {
        Volatile.Write(ref _captureClockOffset, timestamp - DateTime.UtcNow.Ticks);

        if (segment.DestinationPort == serverPort && serverNetwork.Contains(segment.DestinationAddress))
        {
            var flow = new FlowKey(segment.SourceAddress, segment.SourcePort, segment.DestinationAddress, segment.DestinationPort);
            _tracker.OnSent(flow, segment.SequenceNumber, segment.PayloadLength, timestamp);
            return null;
        }

        if (segment.HasAck && segment.SourcePort == serverPort && serverNetwork.Contains(segment.SourceAddress))
        {
            var flow = new FlowKey(segment.DestinationAddress, segment.DestinationPort, segment.SourceAddress, segment.SourcePort);
            return _tracker.OnAck(flow, segment.AcknowledgmentNumber, timestamp);
        }

        return null;
    }

    public void ExpireStale(long utcNow) => _tracker.ExpireStale(utcNow + Volatile.Read(ref _captureClockOffset));

    public void Reset() => _tracker.Reset();
}
