using AtmMachine.Models;

namespace AtmMachine.Repository.CardRepository;

internal interface ICardRepository
{
    bool VerifyCard(Card card);
    void UpdateCard(Card card);
}