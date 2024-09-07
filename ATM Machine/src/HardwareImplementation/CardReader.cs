using ATM_Machine.HardwareInterface;
using ATM_Machine.src.Models;
using ATM_Machine.src.Utils;
using System.Reflection.Emit;

namespace ATM_Machine.HardwareImplementation;

internal class CardReader: ICardReader
{
    public static Card? ReadCard()
    {
        
        Screen.DisplayHighlitedText("Card Reader");
        int attemptsRemaining = 3;
        start1:
        
        Screen.DisplayMessage("Enter Card Number");
        var cardNumber = Console.ReadLine();
        if(cardNumber.Length != 16 && cardNumber.Length!=3)
        {
            Screen.DisplayErrorMessage("Invalid Card Number");
            attemptsRemaining--;
            if (attemptsRemaining == 0) {
                Screen.DisplayWarningMessage("Attempts Limit Reached.");
                return null;
            }
            int choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 1)
            {
                Screen.DisplayHighlitedText("Attempts Remaining: " + attemptsRemaining);
                goto start1;
            }
            else return null;
        }
        for(int i=0; i<cardNumber.Length; i++)
        {
            if(!char.IsDigit(cardNumber[i]))
            {
                Screen.DisplayErrorMessage("Invalid Card Number");
                attemptsRemaining--;
                if (attemptsRemaining == 0)
                {
                    Screen.DisplayWarningMessage("Attempts Limit Reached.");
                    return null;
                }
                int choice = InteractiveMenuSelector.InteractiveMenu();
                if (choice == 1)
                {
                    Screen.DisplayHighlitedText("Attempts Remaining: " + attemptsRemaining);
                    goto start1;
                }
                else return null;
            }
        }
        long cardNo;
        if(!long.TryParse(cardNumber, out cardNo))
        {
            Screen.DisplayErrorMessage("Invalid Card Number");
            return null;
        }
        attemptsRemaining = 3;
        start2:
        Screen.DisplayMessage("Enter Pin");
        var input= Keypad.ReadSenstiveData();
        int pin;
        if (!Int32.TryParse(input, out pin))
        {
            Screen.DisplayErrorMessage("Invalid Pin");
            attemptsRemaining--;
            if (attemptsRemaining == 0)
            {
                Screen.DisplayWarningMessage("Attempts Limit Reached.");
                return null;
            }
            var choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 1)
            {
                Screen.DisplayHighlitedText("Attempts Remaining: " + attemptsRemaining);
                goto start2;
            }
            else return null;
        }
        if(pin < 1000 || pin > 9999)
        {
            Screen.DisplayErrorMessage("Enter pin between range 1000 to 9999");
            if (attemptsRemaining == 0)
            {
                Screen.DisplayWarningMessage("Attempts Limit Reached.");
                return null;
            }
            var choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 1)
            {
                Screen.DisplayHighlitedText("Attempts Remaining: " + attemptsRemaining);
                goto start2;
            }
            else return null;
        }
        return new Card(cardNumber, pin);

    }
}