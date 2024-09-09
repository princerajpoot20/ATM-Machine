using ATM_Machine.src.data;

namespace ATM_Machine.src.Models;

public struct ATM
{
    internal int AtmID;
    internal AtmState atmState;
    internal string atmLocation;
    internal ATM(int AtmID, AtmState atmState, string atmLocation)
    {
        this.AtmID = AtmID;
        this.atmState = atmState;
        this.atmLocation = atmLocation;
    }

    internal static ATM getAtmInstance(int atmId)
    {
        return AtmDetails.getAtmDetails(atmId);
    }


}