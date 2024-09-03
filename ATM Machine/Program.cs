using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Services;

class Program
{
    static void Main()
    {
        // Admin Testing
        //var adminServices = new AdminServices();
        
        //adminServices.UpdateCashStorage(CurrencyDenomination.Fifty,10);
        //adminServices.UpdateCashStorage(CurrencyDenomination.FiveHundred,10);

        //adminServices.SetAtmState(AtmState.OutOfService);


        // Account Testing
        Card card = new Card("123", 123);
        Card card1 = new Card("1231", 123);
        var account = AuthenticationService.Authenticate(card1);
        if(account != null)
        {
            Console.WriteLine("Account Details: ");
            Console.WriteLine("Account Number: "+account.AccountNumber);
            Console.WriteLine("Name: "+account.Name);
            Console.WriteLine("Mobile Number: "+account.MobileNumber);
            Console.WriteLine("Balance: "+account.Balance);
        }
        else
        {
            Console.WriteLine("Authentication failed");
        }
    }
}