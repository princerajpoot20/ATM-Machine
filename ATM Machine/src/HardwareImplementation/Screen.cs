using System.Text;

namespace ATM_Machine.HardwareImplementation;

// Used abstract class here.
// Abstract class can have abstract methods and non abstract methods.
// Abstract class are used to inherit some common functionality.
// And in addition some abstract method that will define
abstract public  class MonitorScreen
{
    public static void DisplayMessage(string s)
    {
        Console.WriteLine(s);
    }

    public static void DisplayConfirmationMessage(string message)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public abstract void DisplaySuccessMessage(string message);
}
public class Screen : MonitorScreen
{
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
    public static void DisplaySuccessMessage(string message)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void DisplayHighlitedText(string message)
    {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}