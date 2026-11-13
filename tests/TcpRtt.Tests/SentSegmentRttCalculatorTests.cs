using System.Net;
using TcpRtt.Core;

namespace TcpRtt.Tests;

public class SentSegmentRttCalculatorTests
{
    private const ushort ServerPort = 8889;
    private static readonly IPAddress Client = IPAddress.Parse("10.0.0.2");
    private static readonly IPAddress Server = IPAddress.Parse("203.0.113.10");
    private static readonly IPNetwork ServerNetwork = IPNetwork.Parse("203.0.113.0/24");

    private static long Ms(double milliseconds) => TimeSpan.FromMilliseconds(milliseconds).Ticks;

    private static SentSegmentRttCalculator CreateCalculator() => new(ServerNetwork, ServerPort, TimeSpan.FromSeconds(1));

    private static TcpSegment Outgoing(uint seq, int length) =>
        new(Client, 50000, Server, ServerPort, seq, 0, HasAck: true, length);

    private static TcpSegment Incoming(uint ack, IPAddress? from = null, ushort port = ServerPort) =>
        new(from ?? Server, port, Client, 50000, 0, ack, HasAck: true, 0);

    [Fact]
    public void MatchesOutgoingDataWithServerAck()
    {
        var calculator = CreateCalculator();

        Assert.Null(calculator.Process(Outgoing(1000, 100), Ms(0)));
        Assert.Equal(12, calculator.Process(Incoming(1100), Ms(12))!.Value, precision: 6);
    }

    [Fact]
    public void IgnoresAcksFromOtherNetworks()
    {
        var calculator = CreateCalculator();

        calculator.Process(Outgoing(1000, 100), Ms(0));

        Assert.Null(calculator.Process(Incoming(1100, from: IPAddress.Parse("198.51.100.1")), Ms(10)));
    }

    [Fact]
    public void IgnoresAcksFromOtherPorts()
    {
        var calculator = CreateCalculator();

        calculator.Process(Outgoing(1000, 100), Ms(0));

        Assert.Null(calculator.Process(Incoming(1100, port: 443), Ms(10)));
    }

    [Fact]
    public void IgnoresSegmentsWithoutAckFlag()
    {
        var calculator = CreateCalculator();

        calculator.Process(Outgoing(1000, 100), Ms(0));

        Assert.Null(calculator.Process(Incoming(1100) with { HasAck = false }, Ms(10)));
    }
}
