using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.Models;
using ATM_Machine.src.Services;
using ATM_Machine.src.Utils;
using ATM_Machine.UI;

namespace ATM_Machine.src.UI
{
    internal class AdminUI
    {
        private static ATM _atm;
        private static AdminServices? _adminServices = null;
        internal static void AdminMenu(ATM atm)
        {
            _atm = atm;
            Console.Clear();
            AtmScreen.DisplayHighlitedText("WWelcome to Admin Panel");
            adminVerification();
            if (_adminServices == null)
            {
                Console.Write("Returning to home in ");
                WaitTimer.Wait(4);
                return;
            }
            AtmScreen.DisplaySuccessMessage("Authentication Successful");
            Logger.Logger.LogMessage($"{_adminServices._admin.adminId} (Admin) Logged in Successful.");
            AdminFeatureList();
        }
        internal static void AdminFeatureList()
        {
            if(_adminServices == null) return;
            var menu = new string[]
            {
                "Refil Cash",
                "Change ATM Service State",
                "Exit"
            };
            int choice = InteractiveMenuSelector.InteractiveMenu(menu, 1, 3);
            switch (choice)
            {
                case 1:
                    _adminServices.UpdateCashStorage();
                    break;
                case 2:
                    _adminServices.SetAtmState(_atm);
                    break;
                case 3:
                    return;
                default:
                    AtmScreen.DisplayWarningMessage("Invalid Choice. ");
                    break;
            }

            AtmScreen.DisplayMessage("Do you want to perform another action?");
            choice = InteractiveMenuSelector.YesNo();
            switch (choice)
            {
                case 1:AdminFeatureList();
                    break;
                case 2: MainMenu.ShowHomeMenu();
                    break;
            }

        }
        internal static void adminVerification(int attemptsRemaining=2)
        {
            takeAdminDetails();
            if (_adminServices == null && attemptsRemaining > 0)
            {
                int choice = InteractiveMenuSelector.InteractiveMenu();
                if (choice==2) return;
                AtmScreen.DisplayWarningMessage("Attempts Remaining: " + attemptsRemaining);
                adminVerification(attemptsRemaining - 1);
            }
            else if (_adminServices == null)
            {
                AtmScreen.DisplayErrorMessage("You have exceeded the maximum number of attempts.");
            }
        }
        public static void takeAdminDetails()
        {
            AtmScreen.DisplayMessage("Enter your admin id:");
            var adminId = Console.ReadLine();
            adminId= adminId?.Trim();
            if (string.IsNullOrEmpty(adminId))
            {
                AtmScreen.DisplayErrorMessage("Admin Id cannot be empty.");

                return;
            }

            Console.WriteLine("Enter your admin pin:");
            var password = Keypad.ReadSenstiveData();
            bool isPin = int.TryParse(password, out int pin);
            if (!isPin)
            {
                AtmScreen.DisplayErrorMessage("Invalid Pin. Pin should be integral.");
                return;
            }
            if(pin < 1000 || pin > 9999)
            {
                AtmScreen.DisplayErrorMessage("Invalid Pin. Pin should be of 4 digits.");
                return;
            }
            _adminServices = AdminServices.VerifyAdmin(new Admin(adminId, pin));
        }
    }
}
