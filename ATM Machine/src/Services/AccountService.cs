using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;

namespace ATM_Machine.src.Services
{

    internal class AccountService
    {
        private CashDispenser _cashDispenser= new CashDispenser();
        private Keypad _keypad= new Keypad();


        internal int CheckBalance(Account account)
        {
            return account.Balance;
        }

        internal bool Withdraw(Account account, int amount)
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
                Console.WriteLine("----Balance Update Successfully----");
                Console.WriteLine("Press Enter to Display the updated balance");
                Console.WriteLine("Press any key to continue");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                    Console.WriteLine("---Your Updated Balance is: {0}", account.Balance);
                return true;
            }


            return false;
        }

        internal void Deposit(Account account)
        {
            int amount= _cashDispenser.ReceiveCash();

            if(amount == 0)
            {
                Console.WriteLine("No cash deposited");
                return;
            }
            account.Balance += amount;
            CardAccountDetails.UpdateAccount(account);
            Console.WriteLine("---Cash Deposit Successful---");
        }

        internal void PinChange(Card card)
        {
            Console.WriteLine("Please Enter your new Pin");
            var newPin = Convert.ToInt32(_keypad.ReadKeyPad());
            card.Pin = newPin;
            CardAccountDetails.UpdateCard(card);
            Console.WriteLine("Pin Change Successfully");

        }
        internal void MobileChange(Account account)
        {
            Console.WriteLine("Please Enter your new Mobile Number");
            var newMobile = _keypad.ReadKeyPad();
            account.MobileNumber = newMobile;
            CardAccountDetails.UpdateAccount(account);
            Console.WriteLine("Mobile Number Updated Successfully");
        }
    }
}