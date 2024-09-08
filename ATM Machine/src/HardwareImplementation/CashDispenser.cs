using ATM_Machine.HardwareInterface;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Utils;

namespace ATM_Machine.HardwareImplementation;

internal class CashDispenser : ICashDispenser
{
    private Account _account;
    public CashDispenser(Account account)
    {
        _account = account;
    }
    public bool Dispense(Dictionary<CurrencyDenomination, int> cash)
    {
        Console.WriteLine("\n-----Dispensing Cash-----");
        foreach (var currency in cash)
        {
            Console.WriteLine(currency.Key.ToString() + " of " + currency.Value + " notes.");
        }
        Console.WriteLine("------Dispensing Cash-----\n");

        return true;
    }
    internal bool DispenseCash(int amount)
    {
        // need to fix this. we need to sort this in descending order as we need higher denomination first.
        //Fixed Done. :)
        var denominations = Enum.GetValues(typeof(CurrencyDenomination)).Cast<CurrencyDenomination>().OrderByDescending(v=>(int)v);
        var cash = new Dictionary<CurrencyDenomination, int>();

        foreach (var denomination in denominations)
        {
            int count = amount / (int)denomination;
            int available = CashDetails.GetCashCount(denomination);
            if (count <= available)
            {
                cash[denomination] = count;
                amount -= count * (int)denomination;
            }
            else if (available != 0)
            {
                cash[denomination] = available;
                amount -= available * (int)denomination;
            }
        }
        if (amount > 0)
        {
            Console.WriteLine("Unable to dispense exact amount due to denomination limitation.");
            return false;
        }

        return Dispense(cash);
    }

    internal int ReceiveCash()
    {
        Screen.DisplayHeading("Cash Dispenser");
        Console.WriteLine("Enter Details of notes for deposition");
        Dictionary<CurrencyDenomination, int> cash = new Dictionary<CurrencyDenomination, int>();
        int totalAmount = 0;
        foreach (CurrencyDenomination denomination in Enum.GetValues(typeof(CurrencyDenomination)))
        {
            var temp = $"Enter the quantity of notes {denomination}"; 
            bool isValidInput = InputValidator.ReadInteger(out int count, Console.GetCursorPosition(), 0, 500, 2,temp);
            if (!isValidInput)
            {
                return -1;
            }
            cash[denomination] = count;
            totalAmount = totalAmount + ((int)denomination * count);
        }
        if (totalAmount == 0)
        {
            Console.WriteLine("No cash inserted");
            return -2;
        }
        Console.Write("\nPlease wait validating cash ");
        WaitTimer.ProcessingWait(4);
        Console.WriteLine("\n----------------------------------");
        Console.WriteLine("\nTotal cash inserted in machine: {0}", totalAmount);
        Screen.DisplayHighlitedText("\nPlease confirm the amount calculated");

        int choice= InteractiveMenuSelector.InteractiveMenu(new string[]
        {
            "Yes",
            "Return the cash"
        }, 1,2);

        if (choice==2)
        {
            Screen.DisplayHighlitedText("Returning cash back");
            bool isDispensed= Dispense(cash);
            if (!isDispensed)
            {
                Console.WriteLine("Cash not returned successfully");
                Console.WriteLine("Cash got stuck in the machine.");
                Console.WriteLine("Please contact the branch. Sorry for inconvience.");
                return 0;
            }
            Screen.DisplaySuccessMessage("Cash returned successfully");
            Screen.DisplayMessage("Transaction completed. No cash Deposited");

            return -2; // for cash not deposited
        }
        return totalAmount;
    }
}