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
        // atm id will remain same throught. 
        const int _atmId = 123;
        internal MainMenu(ATM atm)
        {
            _atm = atm;
        }
        static MainMenu()
        {
             // Static constructor to initialize the ATM instance
             // will get automatically called when the class is loaded
             // This will be used to maintain atm specific tasks/activity
             // Like checking if this atm is out of service or not.
            _atm = ATM.getAtmInstance(_atmId);
            
        }
        public static void ShowHomeMenu()
        {
            Console.Clear();
            AtmScreen.DisplayHeading("                                  Welcome to ATM                                  ");
            if (_atm.atmState == AtmState.OutOfService)
            {
                AtmScreen.DisplayHighlitedText("Sorry!! ATM is Out of Service");
                AtmScreen.DisplaySuccessMessage("\n\nHope to see you again! :)");
                Console.CursorVisible = false;
                ConsoleKeyInfo adminRedirect = Console.ReadKey();
                if(adminRedirect.Key == ConsoleKey.Escape)
                {
                    AdminUI.AdminMenu(_atm);
                }
                return;
            }
            AtmScreen.DisplayHighlitedText("\nPress any key to begin!");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                AdminUI.AdminMenu(_atm);
                ShowHomeMenu();
                return;
            }
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.WriteLine("                               ");
            UserMenu();
        }

        internal static void UserMenu()
        {
            Console.Clear();
            _card = CardReader.ReadCard();
            if (_card == null)
            {
                Console.Write("\nPlease collect your card ");
                WaitTimer.Wait(5);
                ShowHomeMenu();
                return;
            }
            bool isVerified = CardAccountDetails.VerifyCardDetails(_card);
            if (!isVerified)
            {
                Console.Write("\nPlease collect your card ");
                WaitTimer.Wait(5);
                ShowHomeMenu();
                return;
            }
            _accountService = AccountService.GetAccountServiceInstance(_card);
            if (_accountService == null ) {
                Console.Write("\nPlease collect your card ");
                WaitTimer.Wait(5);
                ShowHomeMenu();
                return;
            }
            Console.Clear();
            UserServicesMenu();
        }
        public static void UserServicesMenu()
        {
            Console.Clear();
            AtmScreen.DisplayHeading($"                                  Welcome {_accountService.GetAccountHolderName()}!                                  ");
            string[] menu = new string[]
                        {
                "Withdraw Cash",
                "Deposit Cash",
                "Check Balance",
                "Account Services",
                "Exit"
            };
            int choice = InteractiveMenuSelector.InteractiveMenu(menu, 1, 5);
            switch (choice)
            {
                case 1:
                    bool isValid = Keypad.ReadInteger(out int amount, Console.GetCursorPosition(), 0, 100000,2, "Enter Amount to withdraw: ");
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
            Console.WriteLine("\n\nDo you want to perform another transaction?");
            choice = InteractiveMenuSelector.YesNo();
            if (choice == 2)
            {
                Console.WriteLine("Thank you for using our services");
                Console.Write("\nPlease collect your card ");
                WaitTimer.Wait(5);
                ShowHomeMenu();
                return;
            }
            UserServicesMenu();
        }
        public static void ShowAccountServices()
        {
            Console.Clear();
            Console.WriteLine("Account Services");
            int choice = InteractiveMenuSelector.InteractiveMenu(new string[]
            {
                "Change Pin",
                "Exit"
            }, 1,2);
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
        }
 }