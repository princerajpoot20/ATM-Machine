using ATM_Machine.HardwareInterface;
using ATM_Machine.src.Models;
using ATM_Machine.src.Utils;
using System.Reflection.Emit;

namespace ATM_Machine.HardwareImplementation;

internal class CardReader: ICardReader
{
    public static Card? ReadCard()
    {
        int attempRemaining = 3;
        Screen.DisplayHighlitedText("Reading Card");
        start1:
        if (attempRemaining == 0) return null;
        Screen.DisplayMessage("Enter Card Number");
        var cardNumber = Console.ReadLine();
        if(cardNumber.Length != 16 && cardNumber.Length!=3)
        {
            Screen.DisplayErrorMessage("Invalid Card Number");
            int choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 1)
            {
                attempRemaining--;
                Screen.DisplayHighlitedText("Attempts Remaining: " + attempRemaining);
                goto start1;
            }
            else return null;
        }
        for(int i=0; i<cardNumber.Length; i++)
        {
            if(!char.IsDigit(cardNumber[i]))
            {
                Screen.DisplayErrorMessage("Invalid Card Number");
                int choice = InteractiveMenuSelector.InteractiveMenu();
                if (choice == 1)
                {
                    Screen.DisplayHighlitedText("Attempts Remaining: " + attempRemaining);
                    attempRemaining--;
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
        attempRemaining = 3;
        start2:
        if (attempRemaining == 0) return null;
        Console.Write("Enter Pin: ");
        var input= Keypad.ReadSenstiveData();
        int pin;
        if (!Int32.TryParse(input, out pin))
        {
            Screen.DisplayErrorMessage("Invalid Pin");
            var choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 1)
            {
                attempRemaining--;
                Screen.DisplayHighlitedText("Attempts Remaining: " + attempRemaining);
                goto start2;
            }
            else return null;
        }
        if(pin < 1000 || pin > 9999)
        {
            Screen.DisplayErrorMessage("Enter pin between range 1000 to 9999");
            var choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 1)
            {
                if (attempRemaining == 0) return null;
                Screen.DisplayHighlitedText("Attempts Remaining: " + attempRemaining);
                attempRemaining--;
                goto start2;
            }
            else return null;
        }
        return new Card(cardNumber, pin);

    }
}