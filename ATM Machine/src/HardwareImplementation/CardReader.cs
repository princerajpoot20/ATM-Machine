using ATM_Machine.HardwareInterface;
using ATM_Machine.src.Models;
using System.Reflection.Emit;

namespace ATM_Machine.HardwareImplementation;

public class CardReader: ICardReader
{

    public Card ReadCard()
    {
        Screen.DisplayHighlitedText("----Card Reader Hardware----");
        
        Screen.DisplayMessage("Please insert your card");
        Screen.DisplayMessage("Enter Card Number");
        start1:
        var cardNumber = Console.ReadLine();
        long cardNo;
        if(!long.TryParse(cardNumber, out cardNo))
        {
            Screen.DisplayErrorMessage("Invalid Card Number");
            Screen.DisplayMessage("Try again. Please enter a valid card number");
            goto start1;
        }
        Screen.DisplayMessage("Enter Pin");
        start2:
        var input= Keypad.ReadSenstiveData();
        int pin;
        if (!Int32.TryParse(input, out pin))
        {
            Screen.DisplayErrorMessage("Invalid Pin");
            Screen.DisplayMessage("Try again. Please enter a valid Pin number");
            goto start2;
        }
        Screen.DisplayMessage("----Card Reader Hardware----");
        return new Card(cardNumber, pin);

    }
}