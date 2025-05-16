using AtmMachine.Hardware.Screen;
using AtmMachine.LogManager;
using AtmMachine.Repository.CashStorageRepository;
using AtmMachine.Repository.CashStorageRepository.Implementation;
using AtmMachine.Utils.Enums;
using AtmMachine.Utils.UtilityFunctions;

namespace AtmMachine.Hardware.CashTray.Implementation;
class CashTray : ICashDispenser, ICashCollector
{
    #region PrivateDataMember
    private readonly ICashStorageRepository CashStorageRepository;
    #endregion

    #region Constructor
    internal CashTray()
    {
        CashStorageRepository = new CsvCashStorageRepository();
    }
    #endregion
    
    #region Methods
    #region PublicMethods
    public bool DispenseCash(int amount)
    {
        var denominations = Enum.GetValues(typeof(Denomination)).Cast<Denomination>().OrderByDescending(v => (int)v);
        var cash = new Dictionary<Denomination, int>();
        var totalCurrencyCount = CashStorageRepository.GetAvailableCash();

        foreach (var denomination in denominations)
        {
            var count = amount / (int)denomination;
            if (!totalCurrencyCount.TryGetValue(denomination, out var available))
            {
                continue;
            }

            if (available >= count)
            {
                cash[denomination] = count;
                amount -= count * (int)denomination;
                totalCurrencyCount[denomination] -= count;
            }
            else if (available != 0)
            {
                cash[denomination] = available;
                amount -= available * (int)denomination;
                totalCurrencyCount[denomination] = 0;
            }
        }

        if (amount == 0)
        {
            CashStorageRepository.UpdateCashStorage(totalCurrencyCount);
            return ShowDispenseDetails(cash);
        }
        Console.WriteLine("Unable to dispense exact amount due to denomination limitation.");
        return false;
    }

    public int CollectCash()
    {
        AtmScreen.DisplayHeading("Enter Details of notes for deposition\n");
        var cash = new Dictionary<Denomination, int>();
        var totalAmount = 0;
        foreach (Denomination denomination in Enum.GetValues(typeof(Denomination)))
        {
            var temp = $"Enter the quantity of notes {denomination}";
            var isValidInput = InputReader.ReadInteger(out var count, Console.GetCursorPosition(), 0, 500, 2, temp);
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

        Console.WriteLine("\n----------------------------------");
        Console.WriteLine("\nTotal cash inserted in machine: {0}", totalAmount);
        AtmScreen.DisplayHighlightedText("\nPlease confirm the amount calculated");

        var choice = MenuOptionSelector.GetChoice(new string[]
        {
            "Yes",
            "Return the cash"
        }, 2);

        if (choice != 2)
        {
            return totalAmount;
        }

        AtmScreen.DisplayHighlightedText("Returning cash back");
        var isDispensed = ShowDispenseDetails(cash);
        if (!isDispensed)
        {
            Console.WriteLine("Cash not returned successfully");
            Console.WriteLine("Cash got stuck in the machine.");
            Console.WriteLine("Please contact the branch. Sorry for inconvenience.");
            Logger.LogMessage("Cash got stuck.");
            return 0;
        }
        
        AtmScreen.DisplaySuccessMessage("Cash returned successfully");
        Console.WriteLine("Transaction completed. No cash Deposited");
        return -2;
    }
    #endregion

    #region PrivateMethod
    private static bool ShowDispenseDetails(Dictionary<Denomination, int> denominationCount)
    {
        foreach (var (denomination, count) in denominationCount)
        {
            Console.WriteLine($"Dispensing {count} of {denomination}");
        }
        return true;
    }
    #endregion
    #endregion
}