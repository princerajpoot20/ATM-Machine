using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;

namespace ATM_Machine.src.Services.CustomerServices;

internal class PinUpdate : CardSecurity
{
    internal PinUpdate(Card card) : base(card)
    {
    }

    internal override void Execute()
    {
        bool isValidInput = Keypad.ReadInteger(out int newPin, Console.GetCursorPosition(), 1000, 9999, 3, "Please Enter your new Pin");
        if (!isValidInput)
        {
            AtmScreen.DisplayErrorMessage("Pin changes failed");
            return;
        }
        card.Pin = newPin;
        CardAccountDetails.UpdateCard(card);
        AtmScreen.DisplaySuccessMessage("Pin Change Successfully");
        Logger.Logger.LogMessage($"{card.CardNumber} Pin change successful");
    }

}