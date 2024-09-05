namespace ATM_Machine.src.Models;

public class Card
{
    public string CardNumber { get; protected set; }
    public int Pin;
    public Card(string cardNumber, int pin)
    {
        CardNumber = cardNumber;
        Pin = pin;
    }

}