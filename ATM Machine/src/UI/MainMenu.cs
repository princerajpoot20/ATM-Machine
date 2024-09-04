using System.Threading.Channels;
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
    public MainMenu(ICardReader cardReader, AccountService accountService)
    {
        _cardReader = cardReader;
        _accountService = accountService;
    }
    public void ShowMainMenu()
    {
        Console.WriteLine("-----------Welcome to ATM Machine-------------");
        Console.WriteLine("           ======================              ");
        Console.WriteLine("\nPress Enter to continue");

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
            Console.WriteLine("Card authentication failed. :(");
            return;
        }
        var accountNumber = CardAccountDetails.GetAccountNumber(_card);
        _account = CardAccountDetails.GetAccountDetailsByAccountNumber(accountNumber);
        Console.Clear();
        Console.WriteLine("----------Welcome {0}!----------", _account.Name);
        Console.WriteLine("1. Withdraw Cash");
        Console.WriteLine("2. Deposit Cash");
        Console.WriteLine("3. Check Balance");
        Console.WriteLine("4. Account Services");
        Console.WriteLine("5. Exit");

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
        Console.WriteLine("11. Refill Cash");
        var choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("----Welcome to Admin Pannel-----");
                Console.WriteLine("Enter your admin id:");
                var adminId = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                var password = Convert.ToInt32(Console.ReadLine());
                AdminServices adminServices = AdminServices.VerifyAdmin(new Admin(adminId, password));
                if(adminServices == null)
                {
                    Console.WriteLine("Authentication failed");
                    return;
                }
                adminServices.UpdateCashStorage();
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
    }
}

}