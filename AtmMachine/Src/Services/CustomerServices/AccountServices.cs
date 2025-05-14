using AtmMachine.Hardware.CashTray;
using AtmMachine.Hardware.Screen;
using AtmMachine.LogManager;
using AtmMachine.Models;
using AtmMachine.Repository.AccountRepository;
using AtmMachine.Repository.AccountRepository.Implementation;
using AtmMachine.Utils.UtilityFunctions;

namespace AtmMachine.Services.CustomerServices;

class AccountServices
{
    #region PrivateDataMembers

    private readonly IAccountRepository AccountRepository;
    private readonly Account? Account;
    private readonly ICashDispenser? CashDispenser;
    private readonly ICashCollector? CashCollector;
    #endregion

    #region Constructor
    internal AccountServices(Card card)
    {
        AccountRepository = new AccountRepository();
        Account = AccountRepository.GetAccountDetails(card);
        CashCollector = null;
        CashDispenser = null;
    }

    internal AccountServices(Card card, ICashDispenser cashDispenser) :
        this(card)
    {
        CashDispenser = cashDispenser;
    }

    internal AccountServices(Card card, ICashCollector cashCollector) :
        this(card)
    {
        CashCollector = cashCollector;
    }
    #endregion

    #region InternalMethods
    internal void Withdraw()
    {
        Console.Clear();
        if (Account == null)
        {
            return;
        }

        var isValid = InputReader.ReadInteger(out var amount, Console.GetCursorPosition(), 0, 100000, 2, "Enter Amount to withdraw: ");

        if (!isValid)
        {
            return;
        }

        if (Account.Balance < amount)
        {
            Console.WriteLine("Insufficient balance");
            return;
        }

        if (amount <= 0)
        {
            AtmScreen.DisplayErrorMessage("Amount should be greater than 0");
            return;
        }

        if (CashDispenser == null)
        {
            AtmScreen.DisplayWarningMessage("Cash Dispenser is out of service at this moment");
            return;
        }

        if (!CashDispenser.DispenseCash(amount))
        {
            Logger.LogMessage($"{Account.AccountNumber} Failed: Cash Dispense Failed");
            return;
        }

        Account.Balance -= amount;
        AccountRepository.UpdateAccount(Account);

        Console.WriteLine("\nDo you want to check updated balance?");
        var choice = MenuOptionSelector.GetYesNoChoice();
        if (choice == 1)
        {
            Console.WriteLine("Your Updated Balance is: {0}", Account.Balance);
        }

        AtmScreen.DisplaySuccessMessage("Transaction completed successfully");
        Logger.LogMessage($"{Account.AccountNumber} withdraw {amount} successfully.");
    }

    internal void Deposit()
    {
        Console.Clear();
        if (Account == null)
        {
            return;
        }

        if (CashCollector == null)
        {
            AtmScreen.DisplayWarningMessage("Currently cash deposit is out of service.");
            return;
        }

        var amount = CashCollector.CollectCash();
        switch (amount)
        {
            case -1:
                AtmScreen.DisplayWarningMessage("Cash Deposit Failed");
                Logger.LogMessage($"{Account.AccountNumber} Failed: Cash Deposit");
                return;
            case -2:
                Logger.LogMessage($"{Account.AccountNumber} Cancelled: Cash Returned");
                return;
        }

        Account.Balance += amount;
        AccountRepository.UpdateAccount(Account);
        AtmScreen.DisplaySuccessMessage("Cash Deposit Successful");
        Logger.LogMessage($"{Account.AccountNumber} Success: Cash Deposit");
    }

    internal void CheckBalance()
    {
        if (Account == null)
        {
            return;
        }

        Console.WriteLine("Your Current Balance is: " + Account.Balance);
        Logger.LogMessage($"{Account.AccountNumber}  Checked balance");
    }
    #endregion
}