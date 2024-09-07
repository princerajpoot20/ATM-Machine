using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.Models;
using System.Text;

namespace ATM_Machine.src.data
{

    internal class CardAccountDetails
    {
        private const string _cardDetailsPath =
            @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\card_details.csv";

        private const string _cardAccountMappingPath =
            @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\card_account_mapping.csv";

        private const string _accountDetailsPath =
            @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\account_details.csv";
        private const string _cashStoragePath =
            @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\cash_storage.csv";

        internal static bool VerifyCardDetails(Card card)
        {
            var lines = File.ReadAllLines(_cardDetailsPath);
            lines = lines.Skip(1).ToArray();
            foreach (var line in lines)
            {
                var data = line.Split(',');
                if (data[0] == card.CardNumber && Convert.ToInt32(data[1]) == card.Pin)
                {
                    Screen.DisplaySuccessMessage("Card Details verified Successfully!! :)");
                    return true;
                }
            }

            Screen.DisplayWarningMessage("Card authentication failed. :(");
            return false;
        }

        protected static string? GetAccountNumber(Card card)
        {
            var lines = File.ReadAllLines(_cardAccountMappingPath);
            foreach (var line in lines)
            {
                var data = line.Split(',');
                if (data[0] == card.CardNumber)
                {
                    return data[1];
                }
            }

            return null;
        }

        protected static Account GetAccountDetailsByAccountNumber(string accountNumber)
        {
            var lines = File.ReadAllLines(_accountDetailsPath);
            foreach (var line in lines)
            {
                var data = line.Split(',');
                if (data[0] == accountNumber)
                {
                    return new Account(data[0], data[1], data[2], Convert.ToInt32(data[3]));
                }
            }

            return null;
        }



        protected static void UpdateAccount(Account account)
        {
            StringBuilder builder = new StringBuilder();

            string line;
            int updatedBalance = 0;
            using (StreamReader reader = new StreamReader(_accountDetailsPath))
            {
                while ((line = reader.ReadLine()) != null)
                {

                    var data = line.Split(',');
                    if (data[0] == account.AccountNumber)
                    {
                        line= account.AccountNumber + "," + account.Name + "," + account.MobileNumber + "," + account.Balance;
                    }
                    builder.AppendLine(line);
                }

            }
            using (StreamWriter writer = new StreamWriter(_accountDetailsPath, false)) // false to overwrite the file
            {
                writer.Write(builder.ToString());
            }
        }

        protected static void UpdateCard(Card card)
        {
            StringBuilder builder = new StringBuilder();

            string line;
            int updatedBalance = 0;
            using (StreamReader reader = new StreamReader(_cardDetailsPath))
            {
                while ((line = reader.ReadLine()) != null)
                {

                    var data = line.Split(',');
                    if (data[0] == card.CardNumber)
                    {
                        line= card.CardNumber + "," + card.Pin;
                    }
                    builder.AppendLine(line);
                }

            }
            using (StreamWriter writer = new StreamWriter(_cardDetailsPath, false)) // false to overwrite the file
            {
                writer.Write(builder.ToString());
            }
        }

        protected static void UpdateMobileNumber(Account account, string newMobileNumber)
        {
            Console.WriteLine("Need to implement this");
        }
    }
    
}