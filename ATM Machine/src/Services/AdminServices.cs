using ATM_Machine.src.Models;

namespace ATM_Machine.src.Services;

public class AdminServices
{
    // AtmState and CashStorage will be enum. Need to implement enums.
    private AtmState _atmState;
    private CashStorage _cashStorage;

    public AdminServices()
    {

        _atmState = AtmState.InService; //Default value
        _cashStorage = new CashStorage(); // Changes to be made: in the constructor, of this CashStrorage,
        // We will load data from csv file. Need to implement this.
    }

    public void SetAtmState(AtmState state)
    {
        _atmState = state;
        // Need to display a constant screen on the display. Need to implement this!!
        Console.WriteLine("Atm state is now: "+ state);
    }
    public void UpdateCashStorage(CurrencyDenomination denomination, int count)
    {
        _cashStorage.UpdateDenomination(denomination, count);
        Console.WriteLine("Cash Storage updated :)");
        Console.WriteLine(_cashStorage);
    }

}