namespace AtmMachine.Models;

class Card
{
    #region InternalDataMember
    internal readonly int CardNumber;
    internal int Pin { get; set; }
    #endregion

    #region Constructor
    internal Card(int cardNumber, int pin)
    {
        CardNumber = cardNumber;
        Pin = pin;
    }
    #endregion
}