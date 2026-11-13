namespace TcpRtt.Core;

/// <summary>
/// Comparison of 32-bit TCP sequence numbers modulo 2^32 (RFC 793 / RFC 1982),
/// so that comparisons stay correct when the sequence space wraps around zero.
/// </summary>
internal static class SequenceNumber
{
    public static bool IsBefore(uint a, uint b) => (int)(a - b) < 0;

    public static bool IsBeforeOrEqual(uint a, uint b) => (int)(a - b) <= 0;

    public static bool IsAfter(uint a, uint b) => (int)(a - b) > 0;
}
