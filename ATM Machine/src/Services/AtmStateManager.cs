using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Utils;

namespace ATM_Machine.src.Services;

internal class AtmStateManager : Administration
{
    internal AtmStateManager(Admin admin) : base(admin)
    {

    }
    internal override void Execute()
    {
        ATM atm = ATM.getAtmInstance();
        Console.Write("Currently Atm state is now: ");
        AtmScreen.DisplayHighlitedText($"{atm.AtmState}");
        Console.WriteLine("Please confirm, Do you want to change state?");
        var choice = InteractiveMenuSelector.YesNo();
        if (choice == 1)
        {
            if (atm.AtmState == AtmState.OutOfService)
            {
                atm.AtmState = AtmState.InService;
            }
            else
            {
                atm.AtmState = AtmState.OutOfService;
            }
            AtmScreen.DisplayHighlitedText("Atm state changed to: " + atm.AtmState);
            AtmDetails.updateAtmDetails(atm);
            Logger.Logger.LogMessage($"{admin} Changed the Atm State");
            Console.Write("Restarting in ");
            WaitTimer.Wait(4);
            Environment.Exit(0); 
        }
    }
}