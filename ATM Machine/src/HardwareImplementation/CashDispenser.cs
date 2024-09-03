using ATM_Machine.HardwareInterface;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;

namespace ATM_Machine.HardwareImplementation;

public class CashDispenser : ICashDispenser
{
    public  bool DispenseCash(int amount)
    {
        // need to fix this. we need to sort this in descending order as we need higher denomination first.
        //Fixed Done. :)
        var denominations = Enum.GetValues(typeof(CurrencyDenomination)).Cast<CurrencyDenomination>().OrderByDescending(v=>(int)v);
        var toDispense = new Dictionary<CurrencyDenomination, int>();

        foreach (var denomination in denominations)
        {
            int count= amount / (int)denomination;
            int available = CardAccountDetails.GetCashCount(denomination) ;
            if(count > available)
            {
                Console.WriteLine("Cash not available");
                return false;
            }
            toDispense[denomination]= count;
            amount -= count * (int)denomination;
        }

        if (amount > 0)
        {
            Console.WriteLine("Unable to dispense exact amount due to denomination limitation.");
            return false;
        }

        Console.WriteLine("---Dispensing Cash---");
        foreach (var currency in toDispense)
        {
            // call update cash to update the cash storage csv. Need to implement this !!
            Console.WriteLine(currency.Key.ToString() +" of " + currency.Value +" notes.");
        }
        Console.WriteLine("---Dispensing Cash---");

        return true;
    }

    public  bool ReceiveCash(Dictionary<CurrencyDenomination, int> cashReceived)
    {
        // Need to implement this !!
        return true;
    }
}