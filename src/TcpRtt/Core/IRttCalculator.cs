using System.Net;

namespace TcpRtt.Core;

/// <summary>A strategy that turns captured TCP segments into RTT samples.</summary>
/// <remarks>Implementations must be thread-safe: the capture thread calls <see cref="Process"/>
/// while the UI thread calls the other members.</remarks>
internal interface IRttCalculator
{
    RttCounters Counters { get; }

    /// <param name="segment">A segment captured on the monitored interface.</param>
    /// <param name="timestamp">Capture time of the segment, in <see cref="DateTime.Ticks"/> (UTC).</param>
    /// <returns>An RTT sample in milliseconds, or <see langword="null"/>.</returns>
    double? Process(in TcpSegment segment, long timestamp);

    /// <param name="utcNow">Current wall-clock time, in <see cref="DateTime.Ticks"/> (UTC).</param>
    void ExpireStale(long utcNow);

    void Reset();
}

/// <summary>An entry of the "Calculation method" list.</summary>
internal sealed record CalculationMethod(string Name, Func<IPNetwork, ushort, TimeSpan, IRttCalculator> Create)
{
    public static IReadOnlyList<CalculationMethod> All { get; } =
    [
        new("RTT based on sent packets", (network, port, timeout) => new SentSegmentRttCalculator(network, port, timeout)),
    ];

    public override string ToString() => Name;
}
