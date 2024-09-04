using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.UI;

namespace ATM_Machine.src.Services;

public class AdminServices
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

    }

    public static AdminServices VerifyAdmin(Admin admin)
    {
        bool isVerified = AdminDetails.VerifyAdmin(admin);
        if (isVerified)
        {
            return AdminServices.getAdminServiceInstance();
        }
        return null;
    }
    public void SetAtmState(AtmState state)
    {
        //CurrentAtmState.SetAtmService(state);
        // Need to display a constant screen on the display. Need to implement this!!
        Console.WriteLine("Atm state is now: "+ state);
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
        //CurrencyDenomination denomination = CurrencyDenomination.Fifty;
        //_cashStorage.UpdateDenomination(denomination);
        Console.WriteLine("Cash Storage updated :)");
        
    }

}