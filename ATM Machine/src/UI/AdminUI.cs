using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.Models;
using ATM_Machine.src.Services;
using ATM_Machine.src.Utils;

namespace ATM_Machine.src.UI
{
    internal class AdminUI
    {
        private static ATM _atm;
        private static AdminServices _adminServices = null;
        internal static void AdminMenu(ATM atm)
        {
            _atm = atm;
            Console.Clear();
            Screen.DisplayHighlitedText("WWelcome to Admin Panel");

            adminVerification();
            if (_adminServices == null) return;

            Screen.DisplaySuccessMessage("Authentication Successful");
            Logger.Logger.LogMessage($"{_adminServices._admin.adminId} (Admin) Logged in Successful.");
            AdminFeatureList();

        }
        internal static void AdminFeatureList()
        {
            Screen.DisplayMessage("1. Refill Cash");
            Screen.DisplayMessage("2. Change ATM Service State");
            Screen.DisplayMessage("3. Exit");
            bool isValidChoice = InputValidator.ReadInteger(out int choice,1,3);
            if (!isValidChoice) return;
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
                Screen.DisplayMessage("Press Escape to EXIT OR Press anykey to Try again");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape) return;
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
            Console.WriteLine("Enter your admin pin:");
            bool isValidPassword = InputValidator.ReadInteger(out int password);
            _adminServices = AdminServices.VerifyAdmin(new Admin(adminId, password));
        }
    }
}
