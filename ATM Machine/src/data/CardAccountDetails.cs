using ATM_Machine.src.Models;

namespace ATM_Machine.src.data
{

    public class CardAccountDetails
    {
        private const string _cardDetailsPath =
            @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\card_details.csv";

        private const string _cardAccountMappingPath =
            @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\card_account_mapping.csv";

        private const string _accountDetailsPath =
            @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\account_details.csv";


        public static bool VerifyCardDetails(Card card)
        {
            var lines = File.ReadAllLines(_cardDetailsPath);
            lines = lines.Skip(1).ToArray();
            foreach (var line in lines)
            {
                var data = line.Split(',');
                if (data[0] == card.CardNumber && Convert.ToInt32(data[1]) == card.Pin)
                {
                    Console.WriteLine("Card Details verified Successfully!! :)");
                    return true;
                }
            }

            Console.WriteLine("Verification failed :(");
            return false;
        }

        public static string GetAccountNumber(Card card)
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

        public static Account GetAccountDetailsByAccountNumber(string accountNumber)
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

        public static int GetCashCount(CurrencyDenomination denomination)
        {
            var lines = File.ReadAllLines(_cashStoragePath);
            foreach (var line in lines)
            {
                var data = line.Split(',');
                // we need to convert data[0] to currenydenomination 
                if (Enum.TryParse(data[0], out CurrencyDenomination foundDenomination) &&
                    foundDenomination == denomination)
                {
                    return int.Parse(data[1]);
                }

            }

            return 0;
        }
        public static void UpdateCash(CurrencyDenomination denomination, int count)
        {
            Console.WriteLine("Need to write logic to update the csv for denomination");
            return;
        }

    }
    
}