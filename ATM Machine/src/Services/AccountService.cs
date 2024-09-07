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
        private static Account _account;
        private static CashDispenser _cashDispenser;

        private AccountService()
        {
            
        }
        internal static AccountService GetAccountServiceInstance(Card card)
        {
            var accountNumber = CardAccountDetails.GetAccountNumber(card);
            
            if (accountNumber == null)
            {
                Logger.Logger.LogMessage($"{card.CardNumber} Failed!! Card is not linked to any account");
                Screen.DisplayWarningMessage("Card is not linked to any account");
                return null;
            }
            _account = GetAccountDetailsByAccountNumber(accountNumber);
            if (_account == null)
            {
                Logger.Logger.LogMessage($"{card.CardNumber} Failed!! Account details not found");
                Screen.DisplayWarningMessage("Account details not found");
                return null;
            }
            _cashDispenser = new CashDispenser(_account);
            return new AccountService();
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
    
                Console.WriteLine("\nDo you want to check updated balance?0");
                var choice= InteractiveMenuSelector.YesNo();
                
                if (choice==1)
                    Console.WriteLine("Your Updated Balance is: {0}", _account.Balance);
                Logger.Logger.LogMessage($"{_account.AccountNumber} Withdraw {amount} successful");
                Screen.DisplaySuccessMessage("Cash withdraw successfully");
                return true;
            }
            Logger.Logger.LogMessage($"{_account.AccountNumber} Failed! Amount debited but cash does not dispense successful.");
            Screen.DisplayWarningMessage("Amount debited but cash does not dispense");
            return false;
        }
        internal void Deposit()
        {
            int amount= _cashDispenser.ReceiveCash();
            if (amount == -1)
            {
                Screen.DisplayWarningMessage("Cash Deposit Failed");
                Logger.Logger.LogMessage($"{_account.AccountNumber} Amount deposited failed. No cash collected");
                
                return;
            }
            else if (amount == -2)
            {
                return;
            }
            
            _account.Balance += amount;
            CardAccountDetails.UpdateAccount(_account);
            Screen.DisplaySuccessMessage("Cash Deposit Successful");
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