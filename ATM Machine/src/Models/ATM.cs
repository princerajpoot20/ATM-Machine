using ATM_Machine.src.data;

namespace ATM_Machine.src.Models;

public struct ATM
{
    internal int AtmId;
    internal AtmState AtmState;
    internal string AtmLocation;
    private static ATM _ATM;
    internal ATM(int AtmID, AtmState atmState, string atmLocation)
    {
        this.AtmId = AtmID;
        this.AtmState = atmState;
        this.AtmLocation = atmLocation;
    }

    internal static ATM getAtmInstance(int atmId=123)
    {
        _ATM = AtmDetails.getAtmDetails(atmId);
        return _ATM;
    }


}