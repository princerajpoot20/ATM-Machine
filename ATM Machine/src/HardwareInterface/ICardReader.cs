using ATM_Machine.src.Models;

namespace ATM_Machine.HardwareInterface;

public interface ICardReader
{
    Card ReadCard();
}