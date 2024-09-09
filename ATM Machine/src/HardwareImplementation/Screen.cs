using System.Text;

namespace ATM_Machine.HardwareImplementation;

//            Used inheritance here
//  It is a "is a" relation. Every atm screen IS A Monitor Screen
// Inheritance is used to inherit some common functionality.
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
public class AtmScreen : MonitorScreen
{
    public static void DisplaySuccessMessage(string message)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void DisplayHeading(string message)
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void DisplayWarningMessage(string message)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void DisplayErrorMessage(string message)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
    }

}