using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.UI;

namespace ATM_Machine.src.Services;

internal class AdminServices: AdminDetails
{
    // AtmState and CashStorage will be enum. Need to implement enums.
    
    private static AdminServices _adminServices=null;

    private static AdminServices getAdminServiceInstance()
    {
        if(_adminServices==null) 
            return new AdminServices();
        return _adminServices;
    }
    private AdminServices()
    {
        // Object of this class cannot from outside.
        // It can only be create from this class only, after verifying the admin details.
    }

    public static AdminServices VerifyAdmin(Admin admin)
    {
        bool isVerified = VerifyAdminDetails(admin);
        if (isVerified)
        {
            return AdminServices.getAdminServiceInstance();
        }
        return null;
    }

    // Made these method as non-static because it can only be called by the verified admin.
    // This will also needed for loggin admin activity.
    public void SetAtmState(ATM atm)
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
        }
    }
    public void UpdateCashStorage()
    {
        Console.WriteLine("------Cash Storage--------");
        Dictionary<CurrencyDenomination,int>cash = new Dictionary<CurrencyDenomination, int>();
        foreach (CurrencyDenomination denomination in Enum.GetValues(typeof(CurrencyDenomination)))
        {
            Console.WriteLine("Enter the updated quantity of notes of: {0}", denomination);
            int count = Convert.ToInt32(Console.ReadLine());
            cash[denomination] = count;
        }
        CashDetails.UpdateCashStorage(cash);
        Screen.DisplaySuccessMessage("Cash Storage updated :)"); 
    }

}