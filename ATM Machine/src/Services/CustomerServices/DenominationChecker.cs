using ATM_Machine.HardwareInterface;

namespace ATM_Machine.src.Services.CustomerServices;

internal class DenominationChecker
{
    internal static void CheckAvailableDenomination(IAvailableDenomination iAvailableDenomination)
    {
        iAvailableDenomination.ShowDenomination();
    }
}