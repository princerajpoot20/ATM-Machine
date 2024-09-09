using ATM_Machine.HardwareInterface;
using ATM_Machine.src.Utils;
using System.Text;

namespace ATM_Machine.HardwareImplementation;

internal class Keypad: IKeyPad
{
    const string _blankLine = "                                                                                                          \n";
    internal static bool ReadInteger(out int input, (int left, int top) currentCursor, int startValue = int.MinValue, int endValue = int.MaxValue, int maxAttempt = 2, string message = "", bool isFailedOnce = false)
    {
        if (message != "")
        {
            Console.SetCursorPosition(currentCursor.left, currentCursor.top);
            //Console.Write(_blank);
            for (int i = 0; i < 9; i++) Console.Write(_blankLine);
            Console.SetCursorPosition(currentCursor.left, currentCursor.top);
            //Console.SetCursorPosition(currentCursor.left, currentCursor.top);
            if (isFailedOnce)
                AtmScreen.DisplayWarningMessage($"Attempt Remaining {maxAttempt}");
            AtmScreen.DisplayMessage(message);
        }
        var inputString = Console.ReadLine();
        bool isValid = int.TryParse(inputString, out input);

        if (!isValid)
        {
            AtmScreen.DisplayWarningMessage("Please enter the numeric value.");
            int choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 2)
            {
                input = -1;
                return false;
            }
            else if (choice == 1)
            {
                maxAttempt--;
                if (maxAttempt == 0)
                {
                    AtmScreen.DisplayWarningMessage("Maximum attempts reached.");
                    input = -1;
                    return false;
                }

                return ReadInteger(out input, currentCursor, startValue, endValue, maxAttempt, message, true);
            }
        }
        else if (input < startValue || input > endValue)
        {

            AtmScreen.DisplayWarningMessage($"Please enter the number from the choice given i.e between {startValue} to {endValue}.");
            var choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 2)
            {
                input = -1;
                return false;
            }
            else if (choice == 1)
            {
                maxAttempt--;
                if (maxAttempt == 0)
                {
                    AtmScreen.DisplayWarningMessage("Maximum attempts reached.");
                    input = -1;
                    return false;
                }
                AtmScreen.DisplayWarningMessage($"Attempt Remaining {maxAttempt}");
                return ReadInteger(out input, currentCursor, startValue, endValue, maxAttempt, message, true);
            }
        }
        return true;
    }
    public static string ReadSenstiveData()
    {
        StringBuilder input = new StringBuilder();
        while (true)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input.Remove(input.Length - 1, 1);
                Console.SetCursorPosition(x - 1, y);
                Console.Write(" ");
                Console.SetCursorPosition(x - 1, y);
            }
            else if (key.Key != ConsoleKey.Backspace)
            {
                input.Append(key.KeyChar);
                Console.Write("*");
            }
        }
        return input.ToString();
    }
}