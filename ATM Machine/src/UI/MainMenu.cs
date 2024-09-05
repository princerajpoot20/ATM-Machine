using System.Threading.Channels;
using ATM_Machine.HardwareImplementation;
using ATM_Machine.HardwareInterface;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Services;

namespace ATM_Machine.UI;

public class MainMenu
{
    private readonly ICardReader _cardReader;
    private  Card _card;
    private readonly AccountService _accountService;
    private Account _account;
    private ATM _atm;
    private AdminServices _adminServices;
    public MainMenu(ICardReader cardReader, AccountService accountService, ATM atm)
    {
        _cardReader = cardReader;
        _accountService = accountService;
        _atm = atm;
    }
    public void ShowMainMenu()
    {
        
        Screen.DisplayMessage("-----------Welcome to ATM Machine-------------");
        Screen.DisplayMessage("           ======================              ");

        if(_atm.atmState == AtmState.OutOfService)
        {
            Screen.DisplayHighlitedText("Sorry!! ATM is Out of Service");
            Screen.DisplayHighlitedText("Hope to see you again! :)");
            Screen.DisplayHighlitedText("Press Any key to exit");
            ConsoleKeyInfo adminRedirect = Console.ReadKey();
            if(adminRedirect.Key == ConsoleKey.Escape)
            {
                AdminMenu();
            }
            return;
        }
        Screen.DisplayHighlitedText("\nPress Enter to continue");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
            UserMenu();
        if(keyInfo.Key == ConsoleKey.Escape)
            AdminMenu();
        
    }

    public void UserMenu()
    {
        _card = _cardReader.ReadCard();
        bool isVerified = CardAccountDetails.VerifyCardDetails(_card);
        if (!isVerified)
        {
            return;
        }
        var accountNumber = CardAccountDetails.GetAccountNumber(_card);
        _account = CardAccountDetails.GetAccountDetailsByAccountNumber(accountNumber);
        Console.Clear();
        Console.WriteLine("----------Welcome {0}!----------", _account.Name);
        Screen.DisplayMessage("1. Withdraw Cash");
        Screen.DisplayMessage("2. Deposit Cash");
        Screen.DisplayMessage("3. Check Balance");
        Screen.DisplayMessage("4. Account Services");
        Screen.DisplayMessage("5. Exit");

        var choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case 1:
                Console.WriteLine("Enter Amount to withdraw: ");
                var amount = Convert.ToInt32(Console.ReadLine());

                _accountService.Withdraw(_account, amount);
                break;
            case 2:
                //Console.WriteLine("Enter Amount to deposit: ");
                //var depositAmount = Convert.ToInt32(Console.ReadLine());
                _accountService.Deposit(_account);
                break;
            case 3:
                var balance = _accountService.CheckBalance(_account);
                Console.WriteLine("Your Balance is: {0}", balance);
                break;
            case 4:
                Console.WriteLine("Account Services");
                ShowAccountServices();
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
    public void ShowAccountServices()
    {
        Console.Clear();
        Console.WriteLine("1. Change Pin");
        Console.WriteLine("2. Update Mobile Number");
        Console.WriteLine("3. Exit");
        var choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case 1:
                _accountService.PinChange(_card);
                break;
            case 2:
                _accountService.MobileChange(_account);
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }

    public void AdminMenu()
    {
        Console.Clear();
        Console.WriteLine("----WWelcome to Admin Panel");
        Console.WriteLine("Enter your admin id:");
        var adminId = Console.ReadLine();
        Console.WriteLine("Enter your password:");
        var password = Convert.ToInt32(Console.ReadLine());
        _adminServices = AdminServices.VerifyAdmin(new Admin(adminId, password));
        if (_adminServices == null)
        {
            Console.WriteLine("Authentication failed");
            return;
        }

        Console.WriteLine("Authentication Successful");
        Console.WriteLine("1. Refill Cash");
        Console.WriteLine("2. Change ATM Service State");
        var choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                
                _adminServices.UpdateCashStorage();
                break;
            case 2:
                _adminServices.SetAtmState(_atm);
                break;

            default:
                Console.WriteLine("Invalid Choice");
                break;
    }
}

}