using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Models;
using ATM_Machine.src.Utils;

namespace ATM_Machine.src.Services
{
    internal class AccountService
     // CardAccountDetails class in inherited to get the account details. 
     // To access any account service: card and account details are needed.
     // it cannot cannot be exist as seperate entity.
    {
        private static Account? _account;
        private static CashDispenser? _cashDispenser;

        private AccountService() { }
        internal static AccountService? GetAccountServiceInstance(Card card)
        {
            var accountNumber = CardAccountDetails.GetAccountNumber(card);
            
            if (accountNumber == null)
            {
                Logger.Logger.LogMessage($"{card.CardNumber} Failed!! Card is not linked to any account");
                AtmScreen.DisplayWarningMessage("Card is not linked to any account");
                return null;
            }
            _account = CardAccountDetails.GetAccountDetailsByAccountNumber(accountNumber);
            if (_account == null)
            {
                Logger.Logger.LogMessage($"{card.CardNumber} Failed!! Account details not found");
                AtmScreen.DisplayWarningMessage("Account details not found");
                return null;
            }
            _cashDispenser = new CashDispenser(_account);
            return new AccountService();
        }
        internal void CheckBalance()
        {
            if (_account == null) return;
            AtmScreen.DisplayMessage("Your Current Balance is: " + _account.Balance);
            Logger.Logger.LogMessage($"{_account.AccountNumber} Checked account balance");
        }

        internal bool Withdraw(int amount)
        {
            if (_account.Balance < amount)
            {
                Console.WriteLine("Insufficient balance");
                return false;
            }

            if (amount <= 0)
            {
                AtmScreen.DisplayErrorMessage("Amount should be greater than 0");
                return false;
            }

            if (_cashDispenser.DispenseCash(amount))
            {
                _account.Balance -= amount;
                CardAccountDetails.UpdateAccount(_account);

                Console.WriteLine("\nDo you want to check updated balance?");
                var choice = InteractiveMenuSelector.YesNo();

                if (choice == 1)
                    Console.WriteLine("Your Updated Balance is: {0}", _account.Balance);
                Logger.Logger.LogMessage($"{_account.AccountNumber} Withdraw {amount} successful");
                AtmScreen.DisplaySuccessMessage("Transaction completed successfully");
                return true;
            }
            
            
            return false;
        }
        internal void Deposit()
        {
            if (_account == null || _cashDispenser==null) return;
            int amount= _cashDispenser.ReceiveCash();
            if (amount == -1)
            {
                AtmScreen.DisplayWarningMessage("Cash Deposit Failed");
                Logger.Logger.LogMessage($"{_account.AccountNumber} Amount deposited failed. No cash collected");
                
                return;
            }
            else if (amount == -2)
            {
                return;
            }
            
            _account.Balance += amount;
            CardAccountDetails.UpdateAccount(_account);
            AtmScreen.DisplaySuccessMessage("Cash Deposit Successful");
            Logger.Logger.LogMessage($"{_account.AccountNumber} Amount {amount} deposited successful");
        }
        internal void PinChange(Card card)
        {
            Console.WriteLine("Please Enter your new Pin");
            bool isValidInput = Keypad.ReadInteger(out int newPin,Console.GetCursorPosition(), 1000, 9999);
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



    class Transaction
    {
        

    }
    class AccountTransaction
    {

    }

    class CardTransaction
    {

    }

}




