using AtmMachine.Models;

namespace AtmMachine.Repository.CardLinkedRepository;

interface ICardLinkedAccountRepository
{
    int GetAccountNumber(Card card);
}