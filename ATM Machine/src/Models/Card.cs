namespace ATM_Machine.src.Models;

public class Card
{
    public string CardNumber { get; private set; }
    public int Pin { get; private set; }
    public Card(string cardNumber, int pin)
    {
        CardNumber = cardNumber;
        Pin = pin;
    }

}