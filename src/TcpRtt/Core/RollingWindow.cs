namespace TcpRtt.Core;

/// <summary>A fixed-size ring buffer that keeps the most recent values.</summary>
internal sealed class RollingWindow
{
    private readonly double[] _buffer;
    private int _start;

    public RollingWindow(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        _buffer = new double[capacity];
    }

    public int Capacity => _buffer.Length;

    public int Count { get; private set; }

    public double Latest => Count > 0 ? _buffer[(_start + Count - 1) % Capacity] : 0;

    public double Average => Count > 0 ? Values().Average() : 0;

    public double Max => Count > 0 ? Values().Max() : 0;

    public void Add(double value)
    {
        if (Count < Capacity)
        {
            _buffer[(_start + Count) % Capacity] = value;
            Count++;
        }
        else
        {
            _buffer[_start] = value;
            _start = (_start + 1) % Capacity;
        }
    }

    public void Clear()
    {
        _start = 0;
        Count = 0;
    }

    /// <summary>Returns the values from oldest to newest.</summary>
    public double[] ToArray() => [.. Values()];

    private IEnumerable<double> Values()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return _buffer[(_start + i) % Capacity];
        }
    }
}
