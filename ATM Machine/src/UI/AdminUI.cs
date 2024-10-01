using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.Models;
using ATM_Machine.src.Services;
using ATM_Machine.src.Services.AdminServices;
using ATM_Machine.src.Utils;
using ATM_Machine.UI;

namespace ATM_Machine.src.UI
{
    internal class AdminUI
    {
        private static Admin? _admin;
        private static AdminUI _adminUI;

        private AdminUI()
        {
            // Instance cannot be created outside.
        }
        internal static AdminUI GetAdminMenuInstance()
        {
            Console.Clear();
            _admin= Admin.VerifyAdmin();
            if (_admin == null)
            {
                Console.Write("Returning to home in ");
                WaitTimer.Wait(4);
                return null;
            }

            _adminUI = new AdminUI();
            return _adminUI;
            //AdminFeatureList();
        }
        internal void AdminFeatureList()
        // Private method: so that it can only be accessed inside this class.
        {
            Console.Clear();
            AtmScreen.DisplaySuccessMessage($"Welcome {_admin.adminId}");

            Logger.Logger.LogMessage($"{_admin.adminId} (Administration) Logged in Successful.");
            var menu = new string[]
            {
                "Refill Cash",
                "Change ATM Service State",
                "Exit"
            };
            int choice = InteractiveMenuSelector.InteractiveMenu(menu, 1, 3);
            Administration administration=null;
            switch (choice)
            {
                case 1:
                    administration = new CashRefiller(_admin);
                    break;
                case 2:
                    administration = new AtmStateManager(_admin);
                    break;
                case 3:
                    return;
                default:
                    AtmScreen.DisplayWarningMessage("Invalid Choice. ");
                    break;
            }

            administration.Execute();
            // Runtime polymorphism

            AtmScreen.DisplayMessage("Do you want to perform another action?");
            choice = InteractiveMenuSelector.YesNo();
            switch (choice)
            {
                case 1:AdminFeatureList();
                    break;
                case 2: new MainMenu(ATM.getAtmInstance(123)).ShowHomeMenu();
                    break;
            }
        }
    }
}
