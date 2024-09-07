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
        
        Screen.DisplayHeading("<<Welcome to ATM Machine>>");
        //Screen.DisplayMessage("==========================");

    static MainMenu()
    {
         // Static constructor to initialize the ATM instance
         // will get automatically called when the class is loaded
         // This will be used to maintain atm specific tasks/activity
         // Like checking if this atm is out of service or not.
        _atm = ATM.getAtmInstance(123);
    }
    public static void ShowHomeMenu()
    {
        Console.Clear();
        Screen.DisplayHeading("                                  Welcome to ATM                                  ");
            //Screen.DisplayMessage("==========================");
        Screen.DisplayMessage("Please insert your card");
        if (_atm.atmState == AtmState.OutOfService)
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
            Screen.DisplayHighlitedText("\nPress any key to begin!");
            //InteractiveMenuSelector.InteractiveMenu(new string[] { "Press Enter to begin." }, 1, 1);
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                AdminUI.AdminMenu(_atm);
            }
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.WriteLine("                               ");
            UserMenu();
            ShowHomeMenu();
        
    }

    public static void UserMenu()
    {
        Console.Clear();
        _card = CardReader.ReadCard();
            if (_card == null) return;
        bool isVerified = CardAccountDetails.VerifyCardDetails(_card);
        if (!isVerified) return;
        _accountService = new AccountService(_card);
        Console.Clear();
            
        Screen.DisplayHeading($"Welcome {_accountService.GetAccountHolderName()}!");
        string[] menu = new string[]
                    {
        "Withdraw Cash",
        "Deposit Cash",
        "Check Balance",
        "Account Services",
        "Exit"
        };

        //bool isValidInput = InputValidator.ReadInteger(out int choice, 1,5);
        int choice =InteractiveMenuSelector.InteractiveMenu(menu, 1, 5);

        //if(!isValidInput) return;
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
            //Console.WriteLine("1. Change Pin");
            //Console.WriteLine("2. Update Mobile Number");
            //Console.WriteLine("3. Exit");
        int choice = InteractiveMenuSelector.InteractiveMenu(new string[]
        {
            "Change Pin",
            "Exit"
        }, 1,2);
        //bool isValidInput= InputValidator.ReadInteger(out int choice,0);
        //if(!isValidInput) return;
        switch (choice)
        {
            case 1:
                _accountService.PinChange(_card);
                break;
            case 2:
                return;
            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
        private static void DisplayUserServices() {
            Console.Clear();
            Screen.DisplayHeading($"                                  Welcome {_accountService.GetAccountHolderName()}!                                  ");
            string[] menu = new string[]
            {
                "Withdraw Cash",
                "Deposit Cash",
                "Check Balance",
                "Account Services",
                "Exit"
            };

            //bool isValidInput = InputValidator.ReadInteger(out int choice, 1,5);
            int choice = InteractiveMenuSelector.InteractiveMenu(menu, 1, 5);

            //if(!isValidInput) return;
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter Amount to withdraw: ");
                    bool isValid = InputValidator.ReadInteger(out int amount, 0);
                    if (isValid)
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
                case 5:
                    ShowHomeMenu();
                    return;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }

            Console.WriteLine("Do you want to perform another transaction?");
            choice = InteractiveMenuSelector.YesNo();
            if (choice == 2)
            {
                Console.WriteLine("Thank you for using our services");
                return;
            }
            DisplayUserServices();
        }
    }
 }