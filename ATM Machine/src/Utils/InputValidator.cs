using ATM_Machine.HardwareImplementation;

namespace ATM_Machine.src.Utils
{
    internal class InputValidator
    {
        internal static bool ReadInteger(out int input, int startValue = int.MinValue, int endValue = int.MaxValue, int maxAttempt=2)
        {
            if(maxAttempt == 0)
            {
                Screen.DisplayWarningMessage("Maximum attempts reached. Exiting the application.");
                input = -1;
                return false;
            }
            var inputString = Console.ReadLine();
            bool isValid = int.TryParse(inputString, out input);


            if (!isValid)
            {
                Screen.DisplayWarningMessage("Please enter the numeric value.");
                //Console.Write("Press Escape to EXIT OR Press Enter to Try again");
                //var key = Console.ReadKey();
                int choice= InteractiveMenuSelector.InteractiveMenu();
                if (choice==2)
                {
                    input = -1;
                    return false;
                }
                else if (choice==1)
                {
                    Console.WriteLine();
                    return ReadInteger(out input, startValue, endValue, maxAttempt - 1);
                }
            }
            else if (input < startValue || input > endValue)
            {
                Screen.DisplayWarningMessage($"Please enter the number from the choice given i.e between {startValue} to {endValue}.");
                //Console.Write("Press Escape to EXIT OR Press Enter to Try again");
                //var key = Console.ReadKey();
                var choice= InteractiveMenuSelector.InteractiveMenu();
                if (choice==2)
                {
                    input = -1;
                    return false;
                }
                else if (choice==1)
                {
                    Console.WriteLine();
                    return ReadInteger(out input, startValue, endValue, maxAttempt - 1);
                }
            }
            return true;
        }
        
    }
}
