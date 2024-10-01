using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.Models;
using ATM_Machine.src.Services.CustomerServices;
using ATM_Machine.src.UI;
using ATM_Machine.src.Utils;

namespace ATM_Machine.UI
{
    public class MainMenu
    {

        private static Card? _card;
        private static ATM _atm;
        // atm id will remain same throughout. 
        const int AtmId = 123;
        private AdminUI _adminUI;
        internal MainMenu(ATM atm)
        {
            _atm = atm;
        }
        //static MainMenu()
        //{
        //     // Static constructor to initialize the ATM instance
        //     // will get automatically called when the class is loaded
        //     // This will be used to maintain atm specific tasks/activity
        //     // Like checking if this atm is out of service or not.
            
        //}
        public void ShowHomeMenu()
        {
            Console.Clear();
            AtmScreen.DisplayHeading("                                  Welcome to ATM                                  ");
            if (_atm.AtmState == AtmState.OutOfService)
            {
                AtmScreen.DisplayHighlitedText("Sorry!! ATM is Out of Service");
                AtmScreen.DisplaySuccessMessage("\n\nHope to see you again! :)");
                Console.CursorVisible = false;
                ConsoleKeyInfo adminRedirect = Console.ReadKey();
                if(adminRedirect.Key == ConsoleKey.Escape)
                {
                    _adminUI = AdminUI.GetAdminMenuInstance();
                    if (_adminUI == null)
                    {
                        ShowHomeMenu();
                        return;
                    }
                    _adminUI.AdminFeatureList();
                }
                return;
            }
            AtmScreen.DisplayHighlitedText("\nPress any key to begin!");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                _adminUI = AdminUI.GetAdminMenuInstance();
                if (_adminUI == null)
                {
                    ShowHomeMenu();
                    return;
                }
                _adminUI.AdminFeatureList();
                ShowHomeMenu();// after admin menu return to home
                return;
            }
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.WriteLine("                               ");
            UserMenu();
        }

        internal void UserMenu()
        {
            Console.Clear();
            _card = CardReader.ReadCard();
            bool isVerified = CardSecurity.VerifyCard(_card);
            if (!isVerified)
            {
                Console.Write("\nPlease collect your card ");
                WaitTimer.Wait(5);
                ShowHomeMenu();
                return;
            }
            Console.Clear();
            UserServicesMenu();
        }
        public void UserServicesMenu()
        {
            Console.Clear();
            AtmScreen.DisplayHeading($"                                  Welcome {Transaction.GetAccountHolderName(_card)}!                                  ");
            string[] menu = new string[]
                        {
                "Withdraw Cash",
                "Deposit Cash",
                "Check Balance",
                "Account Services",
                "Exit"
            };
            int choice = InteractiveMenuSelector.InteractiveMenu(menu, 1, 5);
            Transaction transaction;
            switch (choice)
            {
                case 1:
                    transaction= new Withdrawal(_card);
                    transaction.Execute(); //runtime polymorphism
                    break;
                case 2:
                    transaction= new Deposit(_card);
                    transaction.Execute();
                    // this is runtime polymorphism
                    //         ---------------------
                    break;
                case 3:
                    transaction= new BalanceInquiry(_card);
                    transaction.Execute(); //runtime polymorphism
                    break;
                // With the help of runtime polymorphism, we can achieve abstraction.
                // Hiding the implementation details from the user.
                // The user only knows that he is doing a transaction.
                case 4:
                    ShowAccountServices();
                    break;
                //case 5:
                //    //DenominationChecker.();
                //    return;
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
        public void ShowAccountServices()
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
                        CardSecurity cardSecurity = new PinUpdate(_card);
                        cardSecurity.Execute();
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