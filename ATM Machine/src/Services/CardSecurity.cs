using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Utils;

namespace ATM_Machine.src.Services;

internal abstract class CardSecurity
// this cardsecurity can further be used to implement more features
// like card blocking, generate pin change etc.
// the execute method will be overiden.
// This will be runtime polymorphism.
{
    protected Card card;

    internal CardSecurity(Card card)
    {
        this.card = card;
    }
    internal static bool VerifyCard(Card card)
    {
        if (card == null) return false; 
        return CardAccountDetails.VerifyCardDetails(card);
    }

    internal abstract void Execute();
    // this class further can have multiple features like pin generate, card blocking etc.
    // We will make it same way as we have done for transaction.
    // Declaring it abstract, and overriding it in the child class.
}