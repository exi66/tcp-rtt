using TcpRtt.Core;

namespace TcpRtt.Tests;

public class RollingWindowTests
{
    [Fact]
    public void Empty_ReturnsZeros()
    {
        var window = new RollingWindow(3);

        Assert.Equal(0, window.Count);
        Assert.Equal(0, window.Latest);
        Assert.Equal(0, window.Average);
        Assert.Equal(0, window.Max);
        Assert.Empty(window.ToArray());
    }

    [Fact]
    public void KeepsOnlyMostRecentValues()
    {
        var window = new RollingWindow(3);

        foreach (double value in new double[] { 1, 2, 3, 4, 5 })
        {
            window.Add(value);
        }

        Assert.Equal([3, 4, 5], window.ToArray());
        Assert.Equal(5, window.Latest);
        Assert.Equal(4, window.Average);
        Assert.Equal(5, window.Max);
    }

    [Fact]
    public void Clear_RemovesAllValues()
    {
        var window = new RollingWindow(3);
        window.Add(1);
        window.Add(2);

        window.Clear();
        window.Add(7);

        Assert.Equal([7], window.ToArray());
    }

    [Fact]
    public void Constructor_RejectsNonPositiveCapacity()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new RollingWindow(0));
    }
}
