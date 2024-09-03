using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;

namespace ATM_Machine.src.Services
{

    public class AccountService
    {
        private CashDispenser _cashDispenser= new CashDispenser();


        public int CheckBalance(Account account)
        {
            return account.Balance;
        }

        public bool Withdraw(Account account, int amount)
        {
            if (account.Balance < amount)
            {
                Console.WriteLine("Insufficient balance");
                return false;
            }


            if (_cashDispenser.DispenseCash(amount))
            {
                account.Balance -= amount;
                CardAccountDetails.UpdateAccount(account);
                Console.WriteLine("Withdrawn {0}. Remaining Balance: {1}", amount, account.Balance);
                return true;
            }


            return false;
        }

        public void Deposit(Account account, int depositAmount)
        {
            Console.WriteLine("Need to implement deposit");
        }
    }
}