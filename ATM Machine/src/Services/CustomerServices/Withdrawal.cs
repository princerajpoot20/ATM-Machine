using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Utils;

namespace ATM_Machine.src.Services.CustomerServices;

internal class Withdrawal : Transaction
{
    internal Withdrawal(Card card) : base(card)
    {

    }
    internal override void Execute()
    {
        if (account == null) return;

        bool isValid = Keypad.ReadInteger(out int amount, Console.GetCursorPosition(), 0, 100000, 2, "Enter Amount to withdraw: ");
        if (!isValid)
        {
            return;
        }
        if (account.Balance < amount)
        {
            Console.WriteLine("Insufficient balance");
            return;
        }

        if (amount <= 0)
        {
            AtmScreen.DisplayErrorMessage("Amount should be greater than 0");
            return;
        }

        if (CashDispenser.DispenseCash(amount))
        {
            account.Balance -= amount;
            CardAccountDetails.UpdateAccount(account);

            Console.WriteLine("\nDo you want to check updated balance?");
            var choice = InteractiveMenuSelector.YesNo();

            if (choice == 1)
                Console.WriteLine("Your Updated Balance is: {0}", account.Balance);
            Logger.Logger.LogMessage($"{account.AccountNumber} Withdraw {amount} successful");
            AtmScreen.DisplaySuccessMessage("Transaction completed successfully");
            return;
        }
        return;
    }
}