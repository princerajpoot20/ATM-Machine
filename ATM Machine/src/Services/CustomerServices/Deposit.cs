using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;

namespace ATM_Machine.src.Services.CustomerServices;

internal class Deposit : Transaction
{
    internal Deposit(Card card) : base(card)
    {
    }
    internal override void Execute()
    {
        if (account == null) return;
        int amount = CashDispenser.ReceiveCash();
        if (amount == -1)
        {
            AtmScreen.DisplayWarningMessage("Cash Deposit Failed");
            Logger.Logger.LogMessage($"{account.AccountNumber} Amount deposited failed. No cash collected");

            return;
        }
        else if (amount == -2)
        {
            return;
        }

        account.Balance += amount;
        CardAccountDetails.UpdateAccount(account);
        AtmScreen.DisplaySuccessMessage("Cash Deposit Successful");
        Logger.Logger.LogMessage($"{account.AccountNumber} Amount {amount} deposited successful");
    }
}