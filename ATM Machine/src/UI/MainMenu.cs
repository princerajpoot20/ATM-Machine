using System.Threading.Channels;
using ATM_Machine.HardwareInterface;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Services;

namespace ATM_Machine.UI;

public class MainMenu
{
    private readonly ICardReader _card;
    private readonly AccountService _accountService;
    private Account _account;
    public MainMenu(ICardReader card, AccountService accountService)
    {
        _card = card;
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
        Card card = _card.ReadCard();
        bool isVerified = CardAccountDetails.VerifyCardDetails(card);
        if (!isVerified)
        {
            Console.WriteLine("Card authentication failed. :(");
            return;
        }
        var accountNumber = CardAccountDetails.GetAccountNumber(card);
        var _account = CardAccountDetails.GetAccountDetailsByAccountNumber(accountNumber);
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
        Console.WriteLine("1. Change Pin");
        Console.WriteLine("2. Update Mobile Number");
        Console.WriteLine("3. Exit");
        var choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case 1:
                Console.WriteLine("Enter New Pin: ");
                var newPin = Convert.ToInt32(Console.ReadLine());
                CardAccountDetails.UpdatePin(_account, newPin);
                break;
            case 2:
                Console.WriteLine("Enter New Mobile Number: ");
                var newMobileNumber = Console.ReadLine();
                CardAccountDetails.UpdateMobileNumber(_account, newMobileNumber);
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }

    public void AdminMenu()
    {
        Console.WriteLine("1. Refill Cash");
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