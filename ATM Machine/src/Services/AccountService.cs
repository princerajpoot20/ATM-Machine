using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Utils;

namespace ATM_Machine.src.Services
{
    internal class AccountService: CardAccountDetails
     // CardAccountDetails class in inherited to get the account details. 
     // To access any account service, card and account details are needed.
     // it cannot cannot be exist as seperate entity.
    {
        private Account _account;
        private CashDispenser _cashDispenser;

        internal AccountService(Card card)
        {
            var accountNumber = CardAccountDetails.GetAccountNumber(card);
            _account = CardAccountDetails.GetAccountDetailsByAccountNumber(accountNumber);
            if (_account == null)
            {
                Logger.Logger.LogMessage($"{card.CardNumber} Failed!! Card is not linked to any account");
                throw new System.Exception("Card is not linked to any account");
            }
            _cashDispenser= new CashDispenser(_account);
        }
        internal void CheckBalance()
        {
            Screen.DisplayMessage("Your Current Balance is: " + _account.Balance);
            Logger.Logger.LogMessage($"{_account.AccountNumber} Checked account balance");
        }

        internal bool Withdraw(int amount)
        {
            if (_account.Balance < amount)
            {
                Console.WriteLine("Insufficient balance");
                return false;
            }
            if (_cashDispenser.DispenseCash(amount))
            {
                _account.Balance -= amount;
                CardAccountDetails.UpdateAccount(_account);
                Console.WriteLine("----Balance Update Successfully----");
                Console.WriteLine("Press Enter to Display the updated balance");
                Console.WriteLine("Press any key to continue");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                    Console.WriteLine("---Your Updated Balance is: {0}", _account.Balance);
                Logger.Logger.LogMessage($"{_account.AccountNumber} Withdraw {amount} successful");
                return true;
            }
            Logger.Logger.LogMessage($"{_account.AccountNumber} Failed! Amount debited but cash does not dispense successful.");
            return false;
        }
        internal void Deposit()
        {
            int amount= _cashDispenser.ReceiveCash();
            if (amount == -1)
            {
                Screen.DisplayWarningMessage("Cash Deposit Failed");
            }
            
            _account.Balance += amount;
            CardAccountDetails.UpdateAccount(_account);
            Console.WriteLine("---Cash Deposit Successful---");
            Logger.Logger.LogMessage($"{_account.AccountNumber} Amount {amount} deposited successful");
        }
        internal void PinChange(Card card)
        {
            Console.WriteLine("Please Enter your new Pin");
            bool isValidInput = InputValidator.ReadInteger(out int newPin, 1000, 9999);
            if(!isValidInput) return;
            card.Pin = newPin;
            CardAccountDetails.UpdateCard(card);
            Console.WriteLine("Pin Change Successfully");
            Logger.Logger.LogMessage($"{card.CardNumber} Pin change successful");
        }
        internal string GetAccountHolderName()
        {
            return _account.Name;
        }
    }
}