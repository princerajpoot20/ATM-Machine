using System.Text;
using AtmMachine.Hardware.Screen;
using AtmMachine.Models;

namespace AtmMachine.Repository.CardRepository.Implementation;

class CsvCardRepository : ICardRepository
{
    #region PrivateDataMember
    private const string CardDatabasePath = @"..\..\..\Src\Database\Card.csv";
    #endregion

    #region PublicMethods
    public bool VerifyCard(Card card)
    {
        string[] lines;
        try
        {
            lines = File.ReadAllLines(CardDatabasePath);
        }
        catch (Exception exception)
        {
            throw new Exception("File error occured" + exception);
        }

        foreach (var line in lines)
        {
            var data = line.Split(',');
            if (Convert.ToInt32(data[0]) != card.CardNumber || Convert.ToInt32(data[1]) != card.Pin)
            {
                continue;
            }
            return true;
        }
        AtmScreen.DisplayWarningMessage("Card authentication failed. :(");
        return false;
    }

    public void UpdateCard(Card card)
    {
        var builder = new StringBuilder();
        try
        {
            string? line;
            using var reader = new StreamReader(CardDatabasePath);
            while ((line = reader.ReadLine()) != null)
            {
                var data = line.Split(',');
                if (Convert.ToInt32(data[0]) == card.CardNumber)
                {
                    line = card.CardNumber + "," + card.Pin;
                }
                builder.AppendLine(line);
            }
        }
        catch (Exception exception)
        {
            throw new Exception("File error occured" + exception);
        }

        using var writer = new StreamWriter(CardDatabasePath, false);
        writer.Write(builder.ToString());
    }
    #endregion
}