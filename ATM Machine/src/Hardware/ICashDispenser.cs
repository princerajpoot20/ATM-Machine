using ATM_Machine.src.Models;

namespace ATM_Machine.HardwareInterface;

internal interface ICashDispenser
{
     static abstract bool DispenseCash(int amount);
     static abstract int ReceiveCash();

}