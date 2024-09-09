using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Machine.src.Utils
{
    internal class InteractiveMenuSelector
    {

        public static int InteractiveMenu(string[] menu, int start, int end, string message="")
        {
            Console.OutputEncoding = Encoding.UTF8; // for emoji :)
            Console.CursorVisible = false;

            Console.WriteLine("\nUse ⬆️ and ⬇️ to navigate and press \u001b[32mEnter\u001b[0m to select:");
            var cursor = Console.GetCursorPosition();
            var option = 1;
            var decorator = "✅ \u001b[32m";
            ConsoleKeyInfo key;
            bool isSelected = false;

            while (!isSelected)
            {
                Console.SetCursorPosition(cursor.Left, cursor.Top);

                for(int i=0;i < menu.Length;i++)
                {
                    Console.WriteLine($"{(option == i+1 ? decorator : "   ")}{menu[i]}\u001b[0m");
                   
                }

                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option = option == 1 ? end : option - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        option = option == end ? 1 : option + 1;
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
        public static int InteractiveMenu()
        {
            return InteractiveMenu(new string[] { "Retry", "EXIT" }, 1, 2);
        }

        public static int YesNo()
        {
            return InteractiveMenu(new string[] { "Yes", "No" }, 1, 2);
        }
    }
}
