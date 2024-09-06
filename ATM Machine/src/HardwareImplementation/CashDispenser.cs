using ATM_Machine.HardwareInterface;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Utils;

namespace ATM_Machine.HardwareImplementation;

public class CashDispenser : ICashDispenser
{
    public bool Dispense(Dictionary<CurrencyDenomination, int> cash)
    {
        Console.WriteLine("-----Dispensing Cash-----");
        foreach (var currency in cash)
        {
            // call update cash to update the cash storage csv. Need to implement this !!
            Console.WriteLine(currency.Key.ToString() + " of " + currency.Value + " notes.");
        }
        Console.WriteLine("------Dispensing Cash-----");

        return true;
    }
    public  bool DispenseCash(int amount)
    {
        // need to fix this. we need to sort this in descending order as we need higher denomination first.
        //Fixed Done. :)
        var denominations = Enum.GetValues(typeof(CurrencyDenomination)).Cast<CurrencyDenomination>().OrderByDescending(v=>(int)v);
        var cash = new Dictionary<CurrencyDenomination, int>();

        foreach (var denomination in denominations)
        {
            int count= amount / (int)denomination;
            int available = CardAccountDetails.GetCashCount(denomination) ;
            if(count > available)
            {
                Console.WriteLine("Cash not available");
                return false;
            }
            cash[denomination]= count;
            amount -= count * (int)denomination;
        }

        if (amount > 0)
        {
            Console.WriteLine("Unable to dispense exact amount due to denomination limitation.");
            return false;
        }

        return Dispense(cash);
    }

    public int ReceiveCash()
    {

        // Need to implement this !!
        Console.WriteLine("-------Cash Dispenser Hardware--------");
        Console.WriteLine("-------Enter Details of notes for deposition-----");
        Dictionary<CurrencyDenomination, int> cash = new Dictionary<CurrencyDenomination, int>();
        int totalAmount = 0;
        foreach (CurrencyDenomination denomination in Enum.GetValues(typeof(CurrencyDenomination)))
        {
            Console.WriteLine("Enter the quantity of notes {0}", denomination);
            bool isValidInput = InputValidator.ReadInteger(out int count, 0);
            if (!isValidInput)
            {
                return -1;
            }
            cash[denomination] = count;
            totalAmount = totalAmount + ((int)denomination * count);
        }

        Console.WriteLine("------Please wait validating cash-------");
        Thread.Sleep(10000);
        Console.WriteLine("----Total cash inserted in machine: {0}", totalAmount);

        Console.WriteLine("----Press Enter to CONFIRM!-----");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        if (keyInfo.Key != ConsoleKey.Enter)
        {
            Console.WriteLine("------Returning cash back--------");
            bool isDispensed= Dispense(cash);
            if (!isDispensed)
            {
                Console.WriteLine("Cash not returned successfully");
                Console.WriteLine("Cash got stuck in the machine.");
                Console.WriteLine("Please contact the branch. Sorry for inconvience.");
                return 0;
            }
            Console.WriteLine("Cash returned successfully");
        }
        return totalAmount;

    }
}