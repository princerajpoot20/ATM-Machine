using ATM_Machine.src.Models;

namespace ATM_Machine.HardwareInterface;

public interface ICashDispenser
{
     bool DispenseCash(int amount);
     int ReceiveCash();

}