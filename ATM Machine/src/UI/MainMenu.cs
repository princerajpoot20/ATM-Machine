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
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("-----------Welcome to ATM Machine-------------");
        Console.WriteLine("----------------------------------------------");

        Card card= _card.ReadCard();
        bool isVerified = CardAccountDetails.VerifyCardDetails(card);
        if(!isVerified)
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
                AccountService accountService = new AccountService();
                accountService.Withdraw(_account, amount);
                break;
            case 2:
                Console.WriteLine("Enter Amount to deposit: ");
                var depositAmount = Convert.ToInt32(Console.ReadLine());
                AccountService accountService1 = new AccountService();
                accountService1.Deposit(_account, depositAmount);
                break;
            case 3:
                AccountService accountService2 = new AccountService();
                var balance = accountService2.CheckBalance(_account);
                Console.WriteLine("Your Balance is: {0}", balance);
                break;
            case 4:
                Console.WriteLine("Account Services");
                ShowAccountServices();
                break;
            case 9:
                AdminMenu();
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
    }
}