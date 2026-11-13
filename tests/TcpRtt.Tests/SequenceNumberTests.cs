using TcpRtt.Core;

namespace TcpRtt.Tests;

public class SequenceNumberTests
{
    [Theory]
    [InlineData(1u, 2u, true)]
    [InlineData(2u, 1u, false)]
    [InlineData(5u, 5u, false)]
    [InlineData(uint.MaxValue, 0u, true)] // wraps around
    [InlineData(0u, uint.MaxValue, false)]
    [InlineData(uint.MaxValue - 100, 100u, true)]
    public void IsBefore(uint a, uint b, bool expected) => Assert.Equal(expected, SequenceNumber.IsBefore(a, b));

    [Fact]
    public void IsBeforeOrEqual_IncludesEquality() => Assert.True(SequenceNumber.IsBeforeOrEqual(5, 5));

    [Fact]
    public void IsAfter_HandlesWraparound() => Assert.True(SequenceNumber.IsAfter(10, uint.MaxValue - 10));
}
