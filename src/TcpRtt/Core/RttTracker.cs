namespace TcpRtt.Core;

/// <summary>
/// Matches outgoing data segments with the ACKs that confirm them, per TCP connection.
/// </summary>
/// <remarks>
/// <para>Timestamps are <see cref="DateTime.Ticks"/>-compatible values (100 ns units).</para>
/// <para>
/// Measurement rules:
/// <list type="bullet">
/// <item>A cumulative ACK yields a single sample, for the newest segment it confirms:
/// older segments covered by the same ACK waited for it and would overstate the RTT.</item>
/// <item>Segments that were retransmitted are never measured (Karn's algorithm),
/// because it is unknown which copy the ACK belongs to.</item>
/// <item>Segments not acknowledged within the timeout are dropped and counted as timed out.</item>
/// </list>
/// </para>
/// <para>The class is thread-safe: capture and UI threads may call it concurrently.</para>
/// </remarks>
internal sealed class RttTracker(TimeSpan timeout, TimeSpan idleFlowTimeout)
{
    private readonly Lock _lock = new();
    private readonly Dictionary<FlowKey, FlowState> _flows = [];
    private readonly long _timeoutTicks = timeout.Ticks;
    private readonly long _idleFlowTicks = idleFlowTimeout.Ticks;
    private long _sent;
    private long _timedOut;

    public RttTracker(TimeSpan timeout)
        : this(timeout, TimeSpan.FromMinutes(1))
    {
    }

    public RttCounters Counters
    {
        get
        {
            lock (_lock)
            {
                return new RttCounters(_sent, _timedOut);
            }
        }
    }

    public void OnSent(FlowKey flow, uint sequenceNumber, int payloadLength, long timestamp)
    {
        if (payloadLength <= 0)
        {
            return;
        }

        uint endSeq = unchecked(sequenceNumber + (uint)payloadLength);

        lock (_lock)
        {
            if (!_flows.TryGetValue(flow, out var state))
            {
                state = new FlowState { HighestSentEnd = endSeq };
                _flows.Add(flow, state);
            }
            else if (SequenceNumber.IsBefore(sequenceNumber, state.HighestSentEnd))
            {
                MarkRetransmitted(state, sequenceNumber, endSeq, timestamp);
                return;
            }

            state.Pending.Add(new PendingSegment(endSeq, timestamp, Retransmitted: false));
            state.HighestSentEnd = endSeq;
            state.LastActivity = timestamp;
            _sent++;
        }
    }

    /// <returns>The RTT in milliseconds, or <see langword="null"/> when the ACK yields no valid sample.</returns>
    public double? OnAck(FlowKey flow, uint acknowledgmentNumber, long timestamp)
    {
        lock (_lock)
        {
            if (!_flows.TryGetValue(flow, out var state))
            {
                return null;
            }

            state.LastActivity = timestamp;

            var pending = state.Pending;
            int acked = 0;
            while (acked < pending.Count && SequenceNumber.IsBeforeOrEqual(pending[acked].EndSeq, acknowledgmentNumber))
            {
                acked++;
            }

            if (acked == 0)
            {
                return null;
            }

            var newest = pending[acked - 1];
            pending.RemoveRange(0, acked);

            return newest.Retransmitted
                ? null
                : TimeSpan.FromTicks(timestamp - newest.SentAt).TotalMilliseconds;
        }
    }

    /// <summary>Drops segments older than the timeout and forgets idle connections.</summary>
    public void ExpireStale(long now)
    {
        lock (_lock)
        {
            List<FlowKey>? idleFlows = null;

            foreach (var (flow, state) in _flows)
            {
                _timedOut += state.Pending.RemoveAll(p => now - p.SentAt > _timeoutTicks);

                if (state.Pending.Count == 0 && now - state.LastActivity > _idleFlowTicks)
                {
                    (idleFlows ??= []).Add(flow);
                }
            }

            if (idleFlows is not null)
            {
                foreach (var flow in idleFlows)
                {
                    _flows.Remove(flow);
                }
            }
        }
    }

    public void Reset()
    {
        lock (_lock)
        {
            _flows.Clear();
            _sent = 0;
            _timedOut = 0;
        }
    }

    private void MarkRetransmitted(FlowState state, uint sequenceNumber, uint endSeq, long timestamp)
    {
        var pending = state.Pending;
        for (int i = 0; i < pending.Count; i++)
        {
            if (SequenceNumber.IsAfter(pending[i].EndSeq, sequenceNumber))
            {
                pending[i] = pending[i] with { Retransmitted = true };
            }
        }

        // A retransmission may also carry new data past what was sent before.
        if (SequenceNumber.IsAfter(endSeq, state.HighestSentEnd))
        {
            pending.Add(new PendingSegment(endSeq, timestamp, Retransmitted: true));
            state.HighestSentEnd = endSeq;
            _sent++;
        }

        state.LastActivity = timestamp;
    }

    private sealed class FlowState
    {
        // Ordered by EndSeq: new data always extends the sequence space.
        public List<PendingSegment> Pending { get; } = [];

        public uint HighestSentEnd { get; set; }

        public long LastActivity { get; set; }
    }

    private readonly record struct PendingSegment(uint EndSeq, long SentAt, bool Retransmitted);
}
