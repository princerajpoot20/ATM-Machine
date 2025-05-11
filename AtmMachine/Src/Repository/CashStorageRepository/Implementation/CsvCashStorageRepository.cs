using System.Text;
using AtmMachine.Utils.Enums;

namespace AtmMachine.Repository.CashStorageRepository.Implementation;

class CsvCashStorageRepository : ICashStorageRepository
{
    #region PrivateDataMember
    private const string CashStoragePath = @"..\..\..\Src\Database\CashStorage.csv";
    #endregion

    #region PublicMethods
    public void UpdateCashStorage(Dictionary<Denomination, int> cash)
    {
        var builder = new StringBuilder();
        foreach (var denomination in cash.Keys)
        {
            var line = denomination + "," + cash[denomination];
            builder.AppendLine(line);
        }

        using var writer = new StreamWriter(CashStoragePath, false);
        writer.Write(builder.ToString());
    }

    public Dictionary<Denomination, int> GetAvailableCash()
    {
        string[] lines;
        try
        {
            lines = File.ReadAllLines(CashStoragePath);
        }
        catch (Exception exception)
        {
            throw new Exception("File error occured", exception);
        }

        var cash = new Dictionary<Denomination, int>();
        foreach (var line in lines)
        {
            var data = line.Split(',');
            if (Enum.TryParse(data[0], out Denomination denomination))
            {
                cash.Add(denomination, int.Parse(data[1]));
            }
        }
        return cash;
    }
    #endregion
}