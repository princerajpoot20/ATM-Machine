using ATM_Machine.HardwareImplementation;
using ATM_Machine.HardwareInterface;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Services;
using ATM_Machine.src.UI;
using ATM_Machine.src.Utils;
using Microsoft.VisualBasic.CompilerServices;

namespace ATM_Machine.UI { 
public class MainMenu
{
    private static ICardReader _cardReader;
    private static Card _card;
    private static AccountService _accountService;
    private static ATM _atm;
    
    internal MainMenu(ATM atm)
    {
        _cardReader = new CardReader();
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
                AdminUI.AdminMenu(_atm);
            }

            return;
        }
        Screen.DisplayHighlitedText("\nPress Enter to continue");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 2);
                Console.WriteLine("                               ");
                UserMenu();
            }
        if(keyInfo.Key == ConsoleKey.Escape)
            AdminUI.AdminMenu(_atm);
        
    }

    public static void UserMenu()
    {
        _card = _cardReader.ReadCard();
        bool isVerified = CardAccountDetails.VerifyCardDetails(_card);
        if (!isVerified) return;
        _accountService = new AccountService(_card);
        Console.Clear();
        Console.WriteLine("----------Welcome {0}!----------", _accountService.GetAccountHolderName());
        Screen.DisplayMessage("1. Withdraw Cash");
        Screen.DisplayMessage("2. Deposit Cash");
        Screen.DisplayMessage("3. Check Balance");
        Screen.DisplayMessage("4. Account Services");
        Screen.DisplayMessage("5. Exit");

        bool isValidInput = InputValidator.ReadInteger(out int choice, 1,5);
        if(!isValidInput) return;
        switch (choice)
        {
            case 1:
                Console.WriteLine("Enter Amount to withdraw: ");
                bool isValid = InputValidator.ReadInteger(out int amount, 0);
                if(isValid)
                    _accountService.Withdraw(amount);
                break;
            case 2:
                _accountService.Deposit();
                break;
            case 3:
                _accountService.CheckBalance();
                break;
            case 4:
                ShowAccountServices();
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
    public static void ShowAccountServices()
    {
        Console.Clear();
        Console.WriteLine("Account Services");
        Console.WriteLine("1. Change Pin");
        Console.WriteLine("2. Update Mobile Number");
        Console.WriteLine("3. Exit");
        bool isValidInput= InputValidator.ReadInteger(out int choice,0);
        if(!isValidInput) return;
        switch (choice)
        {
            case 1:
                _accountService.PinChange(_card);
                break;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
    }
 }