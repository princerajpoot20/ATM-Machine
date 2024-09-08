using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.Models;
using ATM_Machine.src.Services;
using ATM_Machine.src.Utils;

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
            Screen.DisplayHighlitedText("WWelcome to Admin Panel");
            adminVerification();
            if (_adminServices == null)
            {
                Console.Write("Returning to home in ");
                WaitTimer.Wait(4);
                return;
            }
            Screen.DisplaySuccessMessage("Authentication Successful");
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
                    Screen.DisplayWarningMessage("Invalid Choice. ");
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
                Screen.DisplayWarningMessage("Attempts Remaining: " + attemptsRemaining);
                adminVerification(attemptsRemaining - 1);
            }
            else if (_adminServices == null)
            {
                Screen.DisplayErrorMessage("You have exceeded the maximum number of attempts.");
            }
        }
        public static void takeAdminDetails()
        {
            Screen.DisplayMessage("Enter your admin id:");
            var adminId = Console.ReadLine();
            adminId= adminId?.Trim();
            if (string.IsNullOrEmpty(adminId))
            {
                Screen.DisplayErrorMessage("Admin Id cannot be empty.");
                int retryChoice = InteractiveMenuSelector.InteractiveMenu();
                if (retryChoice == 2) return;
                takeAdminDetails();
            }
            Console.WriteLine("Enter your admin pin:");
            var password = Keypad.ReadSenstiveData();
            bool isPin = int.TryParse(password, out int pin);
            if (!isPin)
            {
                Screen.DisplayErrorMessage("Invalid Pin. Pin should be integral.");
                return;
            }
            if(pin < 1000 || pin > 9999)
            {
                Screen.DisplayErrorMessage("Invalid Pin. Pin should be of 4 digits.");
                return;
            }
            _adminServices = AdminServices.VerifyAdmin(new Admin(adminId, pin));
        }
    }
}
