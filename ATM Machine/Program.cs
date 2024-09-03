using ATM_Machine.src.Models;
using ATM_Machine.src.Services;

class Program
{
    static void Main()
    {
        var adminServices = new AdminServices();
        
        adminServices.UpdateCashStorage(CurrencyDenomination.Fifty,10);
        adminServices.UpdateCashStorage(CurrencyDenomination.FiveHundred,10);

        adminServices.SetAtmState(AtmState.OutOfService);
        }
}