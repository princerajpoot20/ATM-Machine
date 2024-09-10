using ATM_Machine.UI;

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
        //Card card = new Card("123", 123);
        //Card card1 = new Card("1231", 123);
        //var account = AuthenticationService.Authenticate(card1);
        //if(account != null)
        //{
        //    Console.WriteLine("Account Details: ");
        //    Console.WriteLine("Account Number: "+account.AccountNumber);
        //    Console.WriteLine("Name: "+account.Name);
        //    Console.WriteLine("Mobile Number: "+account.MobileNumber);
        //    Console.WriteLine("Balance: "+account.Balance);
        //}
        //else
        //{
        //    Console.WriteLine("Authentication failed");
        //}




        //ICashDispenser cashDispenser= new CashDispenser();
        //ICardReader cardReader = new CardReader();

        //Card card = cardReader.ReadCard();
        //var account = AuthenticationService.Authenticate(card);
        //if(account != null)
        //{
        //    Console.WriteLine("Welcome {0} :)",account.Name);
        //    Console.WriteLine("Account Details: ");
        //    Console.WriteLine("Account Number: "+account.AccountNumber);
        //    Console.WriteLine("Name: "+account.Name);
        //    Console.WriteLine("Mobile Number: "+account.MobileNumber);
        //    Console.WriteLine("Balance: "+account.Balance);
        //    Console.WriteLine("Enter Amount to withdraw: ");
        //    var amount = Convert.ToInt32(Console.ReadLine());
        //    AccountService accountService = new AccountService();
        //    accountService.Withdraw(account,amount);

        //}
        //else
        //{
        //    Console.WriteLine("Authentication failed");

        //}
        //var menu = new string []{
        //    "1. Withdraw Cash",
        //    "2. Deposit Cash",
        //    "3. Check Balance",
        //    "4. Account Services"
        //};

        //Console.WriteLine(InteractiveMenuSelector.InteractiveMenu(menu, 1,4));
        //(ATM.getAtmInstance(123));
        //Admin.VerifyAdmin();



        MainMenu.ShowHomeMenu();
    }
}