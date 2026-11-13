using System.Net;

namespace TcpRtt.Core;

/// <summary>The fields of a captured TCP/IPv4 segment that RTT calculation needs.</summary>
internal readonly record struct TcpSegment(
    IPAddress SourceAddress,
    ushort SourcePort,
    IPAddress DestinationAddress,
    ushort DestinationPort,
    uint SequenceNumber,
    uint AcknowledgmentNumber,
    bool HasAck,
    int PayloadLength);

/// <summary>A TCP connection, always written from the client's point of view.</summary>
internal readonly record struct FlowKey(
    IPAddress ClientAddress,
    ushort ClientPort,
    IPAddress ServerAddress,
    ushort ServerPort);

/// <summary>Counters for segments sent with data and segments that were never acknowledged in time.</summary>
internal readonly record struct RttCounters(long Sent, long TimedOut)
{
    public double TimeoutPercent => Sent > 0 ? 100.0 * TimedOut / Sent : 0;
}
