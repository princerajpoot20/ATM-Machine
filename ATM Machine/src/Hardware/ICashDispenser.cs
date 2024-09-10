using ATM_Machine.src.Models;

namespace ATM_Machine.HardwareInterface;

internal interface ICashDispenser
{
    // Refer ICardReader for notes on interface
     static abstract bool DispenseCash(int amount);
     static abstract int ReceiveCash();

}