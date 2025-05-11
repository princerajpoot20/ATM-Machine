using AtmMachine.Models;

namespace AtmMachine.Repository.AccountRepository;

internal interface IAccountRepository
{
    Account? GetAccountDetailsByAccountNumber(int accountNumber);
    Account? GetAccountDetails(Card card);
    void UpdateAccount(Account account);
}