namespace ATM_Machine.src.Models;

public class CashStorage
{
    public Dictionary<CurrencyDenomination, int> Cash;
    public CashStorage()
    {
        Cash = new Dictionary<CurrencyDenomination, int>();
        // Need to read storage data from a file. Need to implement this.!!
        foreach(CurrencyDenomination denomination in Enum.GetValues(typeof(CurrencyDenomination)))
        {
            Cash.Add(denomination, 10);
        }
    }
    public void UpdateDenomination(CurrencyDenomination denomination, int count)
    {
        if(Cash.ContainsKey(denomination))
        {
            Cash[denomination] += count;
        }
        else
        {
            Console.WriteLine("Invalid currency");
            throw new Exception("Invalid currency");
        }
    }

}