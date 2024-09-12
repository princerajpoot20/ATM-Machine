using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;

namespace ATM_Machine.src.Services.AdminServices;

internal class CashRefiller : Administration
{
    internal CashRefiller(Admin admin) : base(admin)
    {

    }
    internal override void Execute()
    {
        MonitorScreen.DisplayHighlitedText("\nCash Storage");
        Dictionary<CurrencyDenomination, int> cash = new Dictionary<CurrencyDenomination, int>();
        foreach (CurrencyDenomination denomination in Enum.GetValues(typeof(CurrencyDenomination)))
        {
            //Console.WriteLine("Enter the updated quantity of notes of: {0}", denomination);
            bool isValid = Keypad.ReadInteger(out int count, Console.GetCursorPosition(), 0, 500, 2, $"Enter the updated quantity of notes of: {denomination}");

            if (!isValid)
                return;
            cash[denomination] = count;
        }
        CashDetails.UpdateCashStorage(cash);
        AtmScreen.DisplaySuccessMessage("Cash Storage updated :)");
        Logger.Logger.LogMessage($"{admin} Updated the Cash Storage");
    }
}