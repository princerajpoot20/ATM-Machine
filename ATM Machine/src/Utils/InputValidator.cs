using ATM_Machine.HardwareImplementation;

namespace ATM_Machine.src.Utils
{
    internal class InputValidator
    {
        const string _blankLine = "                                                         \n";
        internal static bool ReadInteger(out int input, (int left, int top) currentCursor, int startValue = int.MinValue, int endValue = int.MaxValue, int maxAttempt=2, string message="", bool isFailedOnce=false)
        {
            if(message != "")
            {
                Console.SetCursorPosition(currentCursor.left, currentCursor.top);
                //Console.Write(_blank);
                for(int i = 0; i < 8; i++) Console.Write(_blankLine);
                Console.SetCursorPosition(currentCursor.left, currentCursor.top);
                //Console.SetCursorPosition(currentCursor.left, currentCursor.top);
                if(isFailedOnce)
                    Screen.DisplayWarningMessage($"Attempt Remaining {maxAttempt}");
                Screen.DisplayMessage(message);
            }
            var inputString = Console.ReadLine();
            bool isValid = int.TryParse(inputString, out input);

            if (!isValid)
            {
                Screen.DisplayWarningMessage("Please enter the numeric value.");
                int choice= InteractiveMenuSelector.InteractiveMenu();
                if (choice==2)
                {
                    input = -1;
                    return false;
                }
                else if (choice==1)
                {
                    maxAttempt--;
                    if (maxAttempt == 0)
                    {
                        Screen.DisplayWarningMessage("Maximum attempts reached.");
                        input = -1;
                        return false;
                    }
                    
                    return ReadInteger(out input, currentCursor, startValue, endValue, maxAttempt, message, true);
                }
            }
            else if (input < startValue || input > endValue)
            {
                
                Screen.DisplayWarningMessage($"Please enter the number from the choice given i.e between {startValue} to {endValue}.");
                var choice= InteractiveMenuSelector.InteractiveMenu();
                if (choice==2)
                {
                    input = -1;
                    return false;
                }
                else if (choice==1)
                {
                    maxAttempt--;
                    if (maxAttempt == 0)
                    {
                        Screen.DisplayWarningMessage("Maximum attempts reached.");
                        input = -1;
                        return false;
                    }
                    Screen.DisplayWarningMessage($"Attempt Remaining {maxAttempt}");
                    return ReadInteger(out input, currentCursor, startValue, endValue, maxAttempt, message, true);
                }
            }
            return true;
        }
        
    }
}
