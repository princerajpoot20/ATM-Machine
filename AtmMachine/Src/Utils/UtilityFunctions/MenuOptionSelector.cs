using System.Text;
using AtmMachine.Hardware.Screen;

namespace AtmMachine.Utils.UtilityFunctions;
static class MenuOptionSelector
{
    #region InternalMethods
    internal static int GetChoice(string[] menu, int totalChoices, string message = "")
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;

        Console.WriteLine("\nUse ⬆️ and ⬇️ to navigate and press \u001b[32mEnter\u001b[0m to select:");
        var cursor = Console.GetCursorPosition();
        var option = 1;
        const string decorator = "✅ \u001b[32m";
        var isSelected = false;

        while (!isSelected)
        {
            Console.SetCursorPosition(cursor.Left, cursor.Top);

            for (var i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($"{(option == i + 1 ? decorator : "   ")}{menu[i]}\u001b[0m");
            }

            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    option = option == 1 ? totalChoices : option - 1;
                    break;
                case ConsoleKey.DownArrow:
                    option = option == totalChoices ? 1 : option + 1;
                    break;
                case ConsoleKey.Enter:
                    isSelected = true;
                    break;
            }
        }

        Console.WriteLine();
        Console.CursorVisible = true;
        Console.ResetColor();
        return option;
    }

    internal static int GetRetryChoice()
    {
        return GetChoice(["Retry", "EXIT"], 2);
    }

    internal static int GetYesNoChoice(string message = "")
    {
        if (!string.IsNullOrEmpty(message))
        {
            AtmScreen.DisplayHeading(message);
        }
        return GetChoice(["Yes", "No"], 2);
    }
    #endregion
}