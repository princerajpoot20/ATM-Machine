using ATM_Machine.HardwareInterface;
using ATM_Machine.src.Models;
using ATM_Machine.src.Utils;
using System.Reflection.Emit;

namespace ATM_Machine.HardwareImplementation;

public class CardReader: ICardReader
{

    public Card ReadCard()
    {
        
        Screen.DisplayHighlitedText("----Card Reader Hardware----");
        
        Screen.DisplayMessage("Please insert your card");
        start1:
        Screen.DisplayMessage("Enter Card Number");
        var cardNumber = Console.ReadLine();
        if(cardNumber.Length != 16 && cardNumber.Length!=3)
        {
            Screen.DisplayErrorMessage("Invalid Card Number");
            int choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 1)
                goto start1;
            else return null;
        }
        for(int i=0; i<cardNumber.Length; i++)
        {
            if(!char.IsDigit(cardNumber[i]))
            {
                Screen.DisplayErrorMessage("Invalid Card Number");
                int choice = InteractiveMenuSelector.InteractiveMenu();
                if (choice == 1)
                    goto start1;
                else return null;
            }
        }
        long cardNo;
        if(!long.TryParse(cardNumber, out cardNo))
        {
            Screen.DisplayErrorMessage("Invalid Card Number");
            return null;
        }
        start2:
        Screen.DisplayMessage("Enter Pin");
        var input= Keypad.ReadSenstiveData();
        int pin;
        if (!Int32.TryParse(input, out pin))
        {
            Screen.DisplayErrorMessage("Invalid Pin");
            var choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 1)
                goto start2;
            else return null;
        }
        if(pin < 100 || pin > 999)
        {
            Screen.DisplayErrorMessage("Enter pin between range 1000 to 9999");
            var choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 1)
                goto start2;
            else return null;
        }
        Screen.DisplayMessage("----Card Reader Hardware----");
        return new Card(cardNumber, pin);

    }
}