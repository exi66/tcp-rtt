using System.Net;
using TcpRtt.Core;

namespace TcpRtt.Tests;

public class NetworkInputTests
{
    [Theory]
    [InlineData("192.168.1.0/24", "192.168.1.0", 24)]
    [InlineData(" 10.0.0.0/8 ", "10.0.0.0", 8)]
    [InlineData("203.0.113.10", "203.0.113.10", 32)]
    [InlineData("0.0.0.0/0", "0.0.0.0", 0)]
    [InlineData("192.168.1.77/24", "192.168.1.77", 24)]
    public void TryParse_AcceptsValidInput(string input, string expectedAddress, int expectedPrefix)
    {
        Assert.True(NetworkInput.TryParse(input, out var address, out int prefix));
        Assert.Equal(IPAddress.Parse(expectedAddress), address);
        Assert.Equal(expectedPrefix, prefix);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("1.2.3")]
    [InlineData("256.1.1.1")]
    [InlineData("1.2.3.4/33")]
    [InlineData("1.2.3.4/-1")]
    [InlineData("1.2.3.4/abc")]
    [InlineData("1.2.3.4/24/1")]
    [InlineData("::1")]
    [InlineData("2001:db8::/32")]
    public void TryParse_RejectsInvalidInput(string? input)
    {
        Assert.False(NetworkInput.TryParse(input, out _, out _));
    }

    [Theory]
    [InlineData("192.168.1.77", 24, "192.168.1.0/24")]
    [InlineData("192.168.1.77", 32, "192.168.1.77/32")]
    [InlineData("10.200.3.4", 12, "10.192.0.0/12")]
    [InlineData("10.200.3.4", 0, "0.0.0.0/0")]
    public void ToNetwork_ClearsHostBits(string address, int prefix, string expected)
    {
        Assert.Equal(IPNetwork.Parse(expected), NetworkInput.ToNetwork(IPAddress.Parse(address), prefix));
    }
}
