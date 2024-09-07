using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.UI;
using System.Reflection.Emit;
using ATM_Machine.src.Utils;

namespace ATM_Machine.src.Services;

internal class AdminServices : AdminDetails
    // AdminServices class is inherited from AdminDetails class.
    // Admin services cannot be operated without admin database connection.
{
    // Made this as readonly and internal so that it can be accessed by other classes for logging activity.
    // At the same time it cannot be changed from outside.
    internal readonly Admin _admin;
    private AdminServices(Admin admin)
    {
        // Object of this class cannot from outside.
        // It can only be create from this class only, after verifying the admin details.
        _admin = admin;
    }
    private static AdminServices getAdminServiceInstance(Admin admin)
    {
         return new AdminServices(admin);
    }

    internal static AdminServices VerifyAdmin(Admin admin)
    {
        bool isVerified = VerifyAdminDetails(admin);
        if (isVerified)
        {
            return AdminServices.getAdminServiceInstance(admin);
        }
        Screen.DisplayWarningMessage("Admin authentication failed. :(");
        return null;
    }

    // Made these method as non-static because it can only be called by the verified admin.
    // This will also needed for loggin admin activity.
    internal void SetAtmState(ATM atm)
    {
        Console.WriteLine("Currently Atm state is now: "+ atm.atmState);
        Console.WriteLine("Do you want to change?");
        Console.WriteLine("Press Enter for YES, otherwise press any key");
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            if(atm.atmState == AtmState.OutOfService)
            {
                atm.atmState = AtmState.InService;
            }
            else
            {
                atm.atmState = AtmState.OutOfService;
            }
            Console.WriteLine("Atm state changed to: "+ atm.atmState);
            AtmDetails.updateAtmDetails(atm);
            Logger.Logger.LogMessage($"{_admin} Changed the Atm State");
        }
    }
    internal void UpdateCashStorage()
    {
        Console.WriteLine("------Cash Storage--------");
        Dictionary<CurrencyDenomination,int>cash = new Dictionary<CurrencyDenomination, int>();
        foreach (CurrencyDenomination denomination in Enum.GetValues(typeof(CurrencyDenomination)))
        {
            Console.WriteLine("Enter the updated quantity of notes of: {0}", denomination);
            bool isVerified = InputValidator.ReadInteger(out int count, 0);
            if(!isVerified)
                return;
            cash[denomination] = count;
        }
        CashDetails.UpdateCashStorage(cash);
        Screen.DisplaySuccessMessage("Cash Storage updated :)");
        Logger.Logger.LogMessage($"{_admin} Updated the Cash Storage");
    }

}