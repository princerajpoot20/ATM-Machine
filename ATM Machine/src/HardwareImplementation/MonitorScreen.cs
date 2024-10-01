namespace ATM_Machine.HardwareImplementation;

public class MonitorScreen
{
    internal static void DisplayMessage(string s)
    {
        Console.WriteLine(s);
    }

    internal static void DisplayHighlitedText(string message)
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}