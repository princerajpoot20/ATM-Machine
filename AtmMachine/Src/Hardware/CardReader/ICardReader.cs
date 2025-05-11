using AtmMachine.Models;

namespace AtmMachine.Hardware.CardReader;

interface ICardReader
{
    Card? ReadCard();
}