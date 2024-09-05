using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;

namespace ATM_Machine.src.Services;

public class AuthenticationService
{
    public static Account Authenticate(Card card)
    {
        if(!CardAccountDetails.VerifyCardDetails(card))
        {
            Screen.DisplayWarningMessage("Card authentication failed. :(");
            return null;
        }
        var accountNumber = CardAccountDetails.GetAccountNumber(card);
        if(accountNumber== null)
        {
            Screen.DisplayWarningMessage("Card is not linked to any account:(\n Please contact the Bank");
            return null;
        }
        return CardAccountDetails.GetAccountDetailsByAccountNumber(accountNumber);
    }
}