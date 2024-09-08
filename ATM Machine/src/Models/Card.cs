namespace ATM_Machine.src.Models;

public class Card
{
    internal string CardNumber { get; private set; }
    // used private set to make the property immutable.
    // It can now only be set through constructor 
    internal int Pin { get; private set; }
    public Card(string cardNumber, int pin)
    {
        CardNumber = cardNumber;
        Pin = pin;
    }

}