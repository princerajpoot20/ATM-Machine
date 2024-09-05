using ATM_Machine.HardwareImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Machine.src.Utils
{
    internal class InputValidator
    {
        internal static bool ReadInteger(out int input, int startValue = int.MinValue, int endValue = int.MaxValue, int maxAttempt=2)
        {
            var inputString = Console.ReadLine();
            bool isValid = int.TryParse(inputString, out input);


            if (!isValid)
            {
                Screen.DisplayWarningMessage("Please enter the numeric value.");
                Console.Write("Press Escape to try again OR Press Enter to Try again");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    input = -1;
                    return false;
                }
                Console.WriteLine();
                return ReadInteger(out input, startValue, endValue, maxAttempt - 1);
            }
            else if (input < startValue || input > endValue)
            {
                Screen.DisplayWarningMessage($"Please enter the number from the choice given i.e between {startValue} to {endValue}.");
                Console.Write("Press Escape to try again OR Press Enter to Try again");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    input = -1;
                    return false;
                }
                Console.WriteLine();
                return ReadInteger(out input, startValue, endValue, maxAttempt - 1);
            }
            else
            {
                return true;
            }
        }
        
    }
}
