using System.Buffers.Binary;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace TcpRtt.Core;

/// <summary>Parses the user's "IP/CIDR" input. Only IPv4 is supported.</summary>
internal static class NetworkInput
{
    private const int MaxPrefixLength = 32;

    /// <summary>
    /// Parses <c>a.b.c.d/prefix</c> or a bare <c>a.b.c.d</c> (treated as <c>/32</c>).
    /// Host bits are allowed here; use <see cref="ToNetwork"/> to clear them.
    /// </summary>
    public static bool TryParse(string? input, out IPAddress address, out int prefixLength)
    {
        address = IPAddress.None;
        prefixLength = MaxPrefixLength;

        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        var parts = input.Trim().Split('/');
        if (parts.Length > 2)
        {
            return false;
        }

        // IPAddress.TryParse also accepts shorthand such as "1" (= 0.0.0.1); require four octets.
        if (parts[0].Count(c => c == '.') != 3
            || !IPAddress.TryParse(parts[0], out var parsed)
            || parsed.AddressFamily != AddressFamily.InterNetwork)
        {
            return false;
        }

        if (parts.Length == 2
            && (!int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out prefixLength)
                || prefixLength > MaxPrefixLength))
        {
            return false;
        }

        address = parsed;
        return true;
    }

    /// <summary>Returns the network that contains <paramref name="address"/> (host bits cleared).</summary>
    public static IPNetwork ToNetwork(IPAddress address, int prefixLength)
    {
        uint value = BinaryPrimitives.ReadUInt32BigEndian(address.GetAddressBytes());
        uint mask = prefixLength == 0 ? 0 : uint.MaxValue << (MaxPrefixLength - prefixLength);

        var bytes = new byte[4];
        BinaryPrimitives.WriteUInt32BigEndian(bytes, value & mask);
        return new IPNetwork(new IPAddress(bytes), prefixLength);
    }
}
