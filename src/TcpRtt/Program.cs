namespace TcpRtt;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        // High DPI, default font etc.: https://aka.ms/applicationconfiguration
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
