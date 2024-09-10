using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.Models;
using System.Security.Principal;

namespace ATM_Machine.src.Services.CustomerServices;

internal class BalanceInquiry : Transaction
{
    internal BalanceInquiry(Card card) : base(card)
    {
    }
    internal override void Execute()
    {
        if (account == null) return;
        MonitorScreen.DisplayMessage("Your Current Balance is: " + account.Balance);
        Logger.Logger.LogMessage($"{account.AccountNumber} Checked account balance");
    }
}