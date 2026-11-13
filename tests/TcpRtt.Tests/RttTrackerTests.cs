using System.Net;
using TcpRtt.Core;

namespace TcpRtt.Tests;

public class RttTrackerTests
{
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(1);

    private static readonly FlowKey FlowA = new(IPAddress.Parse("10.0.0.2"), 50000, IPAddress.Parse("203.0.113.10"), 8889);
    private static readonly FlowKey FlowB = new(IPAddress.Parse("10.0.0.2"), 50001, IPAddress.Parse("203.0.113.10"), 8889);

    private static long Ms(double milliseconds) => TimeSpan.FromMilliseconds(milliseconds).Ticks;

    [Fact]
    public void Ack_ReturnsTimeSinceSegmentWasSent()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, sequenceNumber: 1000, payloadLength: 100, timestamp: Ms(0));
        double? rtt = tracker.OnAck(FlowA, acknowledgmentNumber: 1100, timestamp: Ms(25));

        Assert.Equal(25, rtt!.Value, precision: 6);
    }

    [Fact]
    public void Ack_ThatDoesNotCoverSegment_ReturnsNull()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, 1000, 100, Ms(0));

        Assert.Null(tracker.OnAck(FlowA, 1050, Ms(10)));
        Assert.NotNull(tracker.OnAck(FlowA, 1100, Ms(20)));
    }

    [Fact]
    public void DuplicateAck_ReturnsNull()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, 1000, 100, Ms(0));
        tracker.OnAck(FlowA, 1100, Ms(20));

        Assert.Null(tracker.OnAck(FlowA, 1100, Ms(30)));
    }

    [Fact]
    public void CumulativeAck_MeasuresOnlyNewestSegment()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, 1000, 100, Ms(0));
        tracker.OnSent(FlowA, 1100, 100, Ms(40));
        double? rtt = tracker.OnAck(FlowA, 1200, Ms(60));

        Assert.Equal(20, rtt!.Value, precision: 6);
    }

    [Fact]
    public void SequenceWraparound_IsHandled()
    {
        var tracker = new RttTracker(Timeout);
        const uint NearMax = uint.MaxValue - 49;

        tracker.OnSent(FlowA, NearMax, 100, Ms(0)); // ends at 50 after wrapping
        double? rtt = tracker.OnAck(FlowA, 50, Ms(15));

        Assert.Equal(15, rtt!.Value, precision: 6);
    }

    [Fact]
    public void AckBeforeWraparound_DoesNotConfirmSegmentAfterIt()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, uint.MaxValue - 49, 100, Ms(0));

        Assert.Null(tracker.OnAck(FlowA, uint.MaxValue - 10, Ms(15)));
    }

    [Fact]
    public void RetransmittedSegment_IsNotMeasured()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, 1000, 100, Ms(0));
        tracker.OnSent(FlowA, 1000, 100, Ms(300)); // retransmission

        Assert.Null(tracker.OnAck(FlowA, 1100, Ms(310)));
        Assert.Equal(new RttCounters(Sent: 1, TimedOut: 0), tracker.Counters);
    }

    [Fact]
    public void SegmentAfterRetransmission_IsMeasuredAgain()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, 1000, 100, Ms(0));
        tracker.OnSent(FlowA, 1000, 100, Ms(300));
        tracker.OnAck(FlowA, 1100, Ms(310));
        tracker.OnSent(FlowA, 1100, 100, Ms(400));

        Assert.Equal(30, tracker.OnAck(FlowA, 1200, Ms(430))!.Value, precision: 6);
    }

    [Fact]
    public void Connections_AreTrackedSeparately()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, 1000, 100, Ms(0));
        tracker.OnSent(FlowB, 5000, 100, Ms(10));

        // FlowB's ACK number would "confirm" FlowA's segment if flows were mixed.
        Assert.Null(tracker.OnAck(FlowB, 1100, Ms(20)));
        Assert.Equal(20, tracker.OnAck(FlowA, 1100, Ms(20))!.Value, precision: 6);
        Assert.Equal(15, tracker.OnAck(FlowB, 5100, Ms(25))!.Value, precision: 6);
    }

    [Fact]
    public void EmptySegment_IsIgnored()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, 1000, 0, Ms(0));

        Assert.Equal(new RttCounters(0, 0), tracker.Counters);
        Assert.Null(tracker.OnAck(FlowA, 1000, Ms(10)));
    }

    [Fact]
    public void UnacknowledgedSegment_TimesOut()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, 1000, 100, Ms(0));
        tracker.OnSent(FlowA, 1100, 100, Ms(500));
        tracker.ExpireStale(Ms(1200));

        Assert.Equal(new RttCounters(Sent: 2, TimedOut: 1), tracker.Counters);
        Assert.Equal(50, tracker.Counters.TimeoutPercent);
        Assert.Equal(700, tracker.OnAck(FlowA, 1200, Ms(1200))!.Value, precision: 6);
    }

    [Fact]
    public void LateAck_AfterTimeout_ReturnsNull()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, 1000, 100, Ms(0));
        tracker.ExpireStale(Ms(1500));

        Assert.Null(tracker.OnAck(FlowA, 1100, Ms(1600)));
    }

    [Fact]
    public void Reset_ClearsCountersAndPendingSegments()
    {
        var tracker = new RttTracker(Timeout);

        tracker.OnSent(FlowA, 1000, 100, Ms(0));
        tracker.Reset();

        Assert.Equal(new RttCounters(0, 0), tracker.Counters);
        Assert.Null(tracker.OnAck(FlowA, 1100, Ms(10)));
    }
}
