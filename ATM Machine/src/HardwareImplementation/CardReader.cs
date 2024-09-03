using ATM_Machine.HardwareInterface;
using ATM_Machine.src.Models;

namespace ATM_Machine.HardwareImplementation;

public class CardReader: ICardReader
{
    public Card ReadCard()
    {
        Console.WriteLine("----Card Reader Hardware----");
        Console.WriteLine("Please insert your card");
        Console.WriteLine("Enter Card Number");
        var cardNumber = Console.ReadLine();
        Console.WriteLine("Enter Pin");
        var pin = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("----Card Reader Hardware----");
        return new Card(cardNumber, pin);

    }
}