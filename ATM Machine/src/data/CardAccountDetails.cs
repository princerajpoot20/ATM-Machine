using ATM_Machine.src.Models;

namespace ATM_Machine.src.data;

public class CardAccountDetails
{
    private const string _cardDetailsPath = @"data/card_details.csv";
    private const string _cardAccountMappingPath = @"data/card_account_mapping.csv";
    private const string _accountDetailsPath = @"data/account_details.csv";

    public bool VerifyCardDetails(string cardNumber, int pin)
    {
        var lines = File.ReadAllLines(_cardDetailsPath);
        foreach (var line in lines)
        {
            var data = line.Split(',');
            if (data[0] == cardNumber && Convert.ToInt32(data[1]) == pin)
            {
                Console.WriteLine("Card Details verified Successfully!! :)");
                return true;
            }
        }

        Console.WriteLine("Verification failed :(");
        return false;
    }
    public string GetAccountNumber(string cardNumber)
    {
        var lines= File.ReadAllLines(_cardAccountMappingPath);
        foreach(var line in lines)
        {
            var data = line.Split(',');
            if(data[0] == cardNumber)
            {
                return data[1];
            }
        }

        return null;
    }
    public Account GetAccountDetailsByAccountNumber(string accountNumber)
    {
        var lines= File.ReadAllLines(_accountDetailsPath);
        foreach(var line in lines)
        {
            var data = line.Split(',');
            if(data[0] == accountNumber)
            {
                return new Account(data[0], data[1], data[2], Convert.ToInt32(data[3]));
            }
        }

        return null;
    }

}