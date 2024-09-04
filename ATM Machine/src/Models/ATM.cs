using ATM_Machine.src.data;

namespace ATM_Machine.src.Models;

public struct ATM
{
    public int AtmID;
    public AtmState atmState;
    public string atmLocation;

    public ATM(int AtmID, AtmState atmState, string atmLocation)
    {
        this.AtmID = AtmID;
        this.atmState = atmState;
        this.atmLocation = atmLocation;
    }

    public static ATM getAtmInstance(int atmId)
    {
        return AtmDetails.getAtmDetails(atmId);
    }


}