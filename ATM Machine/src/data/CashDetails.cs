using ATM_Machine.src.Models;
using System.Text;

namespace ATM_Machine.src.data;

public class CashDetails
{
    private const string _cashDetailsPath =
        @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\cash_storage.csv";

    public static void UpdateCashStorage(Dictionary<CurrencyDenomination, int> cash)
    {
         StringBuilder builder = new StringBuilder();
        string line= CurrencyDenomination.Fifty.ToString() + "," + cash[CurrencyDenomination.Fifty];
        builder.AppendLine(line);
        line= CurrencyDenomination.OneHundred.ToString() + "," + cash[CurrencyDenomination.OneHundred];
        builder.AppendLine(line);
        line= CurrencyDenomination.TwoHundred.ToString() + "," + cash[CurrencyDenomination.TwoHundred];
        builder.AppendLine(line);
        line= CurrencyDenomination.FiveHundred.ToString() + "," + cash[CurrencyDenomination.FiveHundred];
        builder.AppendLine(line);
        using (StreamWriter writer = new StreamWriter(_cashDetailsPath, false)) // false to overwrite the file
        {
            writer.Write(builder.ToString());
        }

    }
    internal static int GetCashCount(CurrencyDenomination denomination)
    {
        var lines = File.ReadAllLines(_cashDetailsPath);
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
}