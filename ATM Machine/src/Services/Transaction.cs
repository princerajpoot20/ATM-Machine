using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;

namespace ATM_Machine.src.Services;

internal abstract class Transaction
// abstract class
// ---------------
// an abstract class contain common functionality and abstract method that child class override.
// Here the execute method is override by different types of transaction
// This will be runtime polymorphism.
// Runtime polymorphism is where the content inside the pointer, the actual object matters.
// Not in any way you can access the parent(also not by upcasting)
// Hence it is called as runtime, as the content inside the pointer is not fixed,
// and will be decided at runtime.
{
    protected Account? account;

    protected Transaction(Card card)
    {
        var accountNumber = CardAccountDetails.GetAccountNumber(card);

        if (accountNumber == null)
        {
            Logger.Logger.LogMessage($"{card.CardNumber} Failed!! Card is not linked to any account");
            AtmScreen.DisplayWarningMessage("Card is not linked to any account");
            throw new System.Exception("Card is not linked to any account");
        }
        account = CardAccountDetails.GetAccountDetailsByAccountNumber(accountNumber);
        if (account == null)
        {
            Logger.Logger.LogMessage($"{card.CardNumber} Failed!! Account details not found");
            AtmScreen.DisplayWarningMessage("Account details not found");
            throw new System.Exception("Account details not found");
        }
    }

    internal static string GetAccountHolderName(Card card)

    {
        var account = CardAccountDetails.GetAccountDetailsByAccountNumber(CardAccountDetails.GetAccountNumber(card));
        return account?.Name ?? ""; // If the account is null, it will return an empty string.
    }

    internal abstract void Execute();

}