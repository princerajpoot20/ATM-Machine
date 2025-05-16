using AtmMachine.Models;

namespace AtmMachine.Repository.CardLinkedRepository.Implementation;
class CsvCardLinkedAccountRepository : ICardLinkedAccountRepository
{
    #region PrivateDataMember
    private const string CardLinkedAccountPath = @"..\..\..\Src\Database\CardLinkedAccount.csv";
    #endregion

    #region PublicMethod
    public int GetAccountNumber(Card card)
    {
        string[] lines;
        try
        {
            lines = File.ReadAllLines(CardLinkedAccountPath);
        }
        catch (Exception exception)
        {
            throw new Exception("File error occured", exception);
        }

        foreach (var line in lines)
        {
            var data = line.Split(',');
            var isNumeric = int.TryParse(data[0], out var cardNumber);
            if (isNumeric && cardNumber == card.CardNumber)
            {
                return Convert.ToInt32(data[1]);
            }
        }

        return -1;
    }
    #endregion 
}