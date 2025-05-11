using System.Text;
using AtmMachine.Models;
using AtmMachine.Repository.CardLinkedRepository;
using AtmMachine.Repository.CardLinkedRepository.Implementation;
using AtmMachine.Repository.CardRepository;
using AtmMachine.Repository.CardRepository.Implementation;

namespace AtmMachine.Repository.AccountRepository.Implementation;

class AccountRepository : IAccountRepository
{
    #region PrivateDataMember
    private const string AccountPath = @"..\..\..\Src\Database\Account.csv";
    private readonly ICardRepository CardRepository;
    private readonly ICardLinkedAccountRepository CardLinkedRepository;
    #endregion

    #region Constructor
    internal AccountRepository()
    {
        CardRepository = new CsvCardRepository();
        CardLinkedRepository = new CsvCardLinkedAccountRepository();
    }
    #endregion

    #region PublicMethods
    public Account? GetAccountDetailsByAccountNumber(int accountNumber)
    {
        string[] lines;
        try
        {
            lines = File.ReadAllLines(AccountPath);
        }
        catch (Exception exception)
        {
            throw new Exception("File error occured", exception);
        }

        foreach (var line in lines)
        {
            var data = line.Split(',');
            var isNumeric = int.TryParse(data[0], out var fetchedAccountNumber);
            if (isNumeric && fetchedAccountNumber == accountNumber)
            {
                return new Account(Convert.ToInt32(data[0]), data[1], data[2], Convert.ToInt32(data[3]));
            }
        }

        return null;
    }

    public Account? GetAccountDetails(Card card)
    {
        var isVerified = CardRepository.VerifyCard(card);
        if (!isVerified)
        {
            return null;
        }

        var accountNumber = CardLinkedRepository.GetAccountNumber(card);
        return GetAccountDetailsByAccountNumber(accountNumber);
    }

    public void UpdateAccount(Account account)
    {
        var builder = new StringBuilder();
        try
        {
            using var reader = new StreamReader(AccountPath);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var data = line.Split(',');
                if (Convert.ToInt32(data[0]) == account.AccountNumber)
                {
                    line = account.AccountNumber + "," +
                           account.Name + "," +
                           account.MobileNumber + "," +
                           account.Balance;
                }
                builder.AppendLine(line);
            }
        }
        catch (Exception exception)
        {
            throw new Exception("File error occured", exception);
        }

        using var writer = new StreamWriter(AccountPath, false);
        writer.Write(builder.ToString());
    }
    #endregion
}