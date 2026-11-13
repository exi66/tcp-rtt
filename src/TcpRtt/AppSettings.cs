using System.Text.Json;

namespace TcpRtt;

/// <summary>User input remembered between runs, stored in %APPDATA%\TcpRtt\settings.json.</summary>
internal sealed record AppSettings
{
    private static readonly string FilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TcpRtt", "settings.json");

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public string Network { get; init; } = "";

    public ushort Port { get; init; } = 8889;

    public decimal MinRttMs { get; init; } = 0.1m;

    public string? DeviceName { get; init; }

    public string? CalculationMethod { get; init; }

    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(FilePath), JsonOptions) ?? new();
            }
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            // Corrupted or unreadable settings: fall back to defaults.
        }

        return new();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            File.WriteAllText(FilePath, JsonSerializer.Serialize(this, JsonOptions));
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // Not being able to remember settings is not worth bothering the user.
        }
    }
}
