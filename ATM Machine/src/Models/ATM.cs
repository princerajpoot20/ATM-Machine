using ATM_Machine.src.data;

namespace ATM_Machine.src.Models;

public struct ATM
{
    internal int AtmID { get; private set; }
    internal AtmState atmState { get; private set; }
    internal string atmLocation { get; private set; }

    // Used property here to make only set as private.
    // This way, it can only be set once i.e from constructor.

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