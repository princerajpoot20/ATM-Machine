using ATM_Machine.HardwareInterface;
using ATM_Machine.src.Models;

namespace ATM_Machine.HardwareImplementation;

public class CashDispenser : ICashDispenser
{
    public bool DispenseCash(int amount)
    {
        var denominations = Enum.GetValues(typeof(CurrencyDenomination)).Cast<CurrencyDenomination>();
        var toDispense = new Dictionary<CurrencyDenomination, int>();

        foreach (var denomination in denominations)
        {
            int count= amount / (int)denomination;
            int available = CashStorage.Cash[denomination];
        }

        return true;
    }

    public bool ReceiveCash(Dictionary<CurrencyDenomination, int> cashReceived)
    {
        throw new NotImplementedException();
    }
}