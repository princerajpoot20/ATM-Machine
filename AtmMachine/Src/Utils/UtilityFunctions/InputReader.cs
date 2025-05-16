using AtmMachine.Hardware.Screen;
using AtmMachine.Models;
using System.Text;

namespace AtmMachine.Utils.UtilityFunctions;

static class InputReader
{
    #region InternalMethod
    internal static bool ReadInteger(out int input, (int left, int top) currentCursor, int startValue = int.MinValue, int endValue = int.MaxValue, int maxAttempt = 2, string message = "")
    {
        input = -1;
        while (maxAttempt > 0)
        {
            DisplayInputMessage(currentCursor, message, maxAttempt);
            if (!int.TryParse(Console.ReadLine(), out input))
            {
                AtmScreen.DisplayWarningMessage("Please enter a numeric value.");
                if (!PromptRetry(ref maxAttempt))
                {
                    return false;
                }
                continue;
            }

            if (input >= startValue && input <= endValue)
            {
                return true;
            }

            AtmScreen.DisplayWarningMessage($"Please enter a number between {startValue} and {endValue}.");
            if (!PromptRetry(ref maxAttempt))
            {
                return false;
            }
        }

        AtmScreen.DisplayWarningMessage("Maximum attempts reached.");
        return false;
    }

    internal static string ReadSensitiveData()
    {
        var input = new StringBuilder();
        while (true)
        {
            var x = Console.CursorLeft;
            var y = Console.CursorTop;
            var key = Console.ReadKey(true);
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

    internal static Admin? ReadAdminDetails()
    {
        Console.Clear();
        var cursor = Console.GetCursorPosition();
        var isValid = ReadInteger(out var adminId, cursor, 100, 100000, 2, "EEnter Admin Id: ");
        if (!isValid)
        {
            return null;
        }

        Console.WriteLine("Enter Admin Pin: ");
        var isInteger = ReadInteger(out var pin, Console.GetCursorPosition(), 1000, 9999);
        return !isInteger ? null : new Admin(adminId, pin);
    }
    #endregion

    #region PrivateMethods
    private static void DisplayInputMessage((int left, int top) cursorPosition, string message, int remainingAttempts)
    {
        Console.SetCursorPosition(cursorPosition.left, cursorPosition.top);
        ClearPreviousMessages(cursorPosition);
        if (!string.IsNullOrEmpty(message))
        {
            Console.WriteLine(message);
        }
        if (remainingAttempts < 2)
        {
            AtmScreen.DisplayWarningMessage($"Attempts Remaining: {remainingAttempts}");
        }
    }

    private static void ClearPreviousMessages((int left, int top) cursorPosition)
    {
        for (var i = 0; i < 9; i++)
        {
            Console.WriteLine(new string(' ', 80));
        }
        Console.SetCursorPosition(cursorPosition.left, cursorPosition.top);
    }

    private static bool PromptRetry(ref int attempts)
    {
        var choice = MenuOptionSelector.GetRetryChoice();
        switch (choice)
        {
            case 1:
                attempts--;
                return true;
        }
        return false;
    }
    #endregion
}