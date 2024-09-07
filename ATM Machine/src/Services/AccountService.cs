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

        private AccountService(Account account)
        {
            _account = account;
            _cashDispenser = new CashDispenser(_account);
        }
        internal static AccountService GetAccountServiceInstance(Card card)
        {
           
            var accountNumber = CardAccountDetails.GetAccountNumber(card);
            if (accountNumber == null)
            {
                Logger.Logger.LogMessage($"{card.CardNumber} Failed!! Card is not linked to any account");
                Screen.DisplayErrorMessage("Card is not linked to any account");
                Screen.DisplayMessage("Please remove your card");
                Console.Write("Redirecting to home in... ");
                WaitTimer.Wait(6);
                return null;
            }
            Account account = CardAccountDetails.GetAccountDetailsByAccountNumber(accountNumber);
            if (account == null)
            {
                Logger.Logger.LogMessage($"{card.CardNumber} Failed!! Not able to locate account");
                Screen.DisplayErrorMessage("Not able to locate account.");
                Screen.DisplayMessage("Please remove your card");
                Console.Write("Redirecting to home in... ");
                WaitTimer.Wait(6);
                return null;
            }
            return new AccountService(account);
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
                Screen.DisplaySuccessMessage("Transaction Successfully");
                Screen.DisplayMessage("Do you want to check your updated balance?");
                int choice = InteractiveMenuSelector.YesNo();
                if (choice == 1)
                    Console.WriteLine("Your Updated Balance is: {0}", _account.Balance);
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
                Logger.Logger.LogMessage($"{_account.AccountNumber} Amount deposited failed. No cash collected");
                
                return;
            }
            if (amount == -2) { 
                Screen.DisplaySuccessMessage("No cash deposited.");
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
            Screen.DisplaySuccessMessage("Pin Change Successfully");
            Logger.Logger.LogMessage($"{card.CardNumber} Pin change successful");
        }
        internal string GetAccountHolderName()
        {
            return _account.Name;
        }
    }
}