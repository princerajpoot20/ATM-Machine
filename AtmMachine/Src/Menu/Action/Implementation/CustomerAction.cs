using AtmMachine.Hardware.CardReader;
using AtmMachine.Hardware.CashTray;
using AtmMachine.Hardware.CashTray.Implementation;
using AtmMachine.Models;
using AtmMachine.Repository.CardRepository;
using AtmMachine.Repository.CardRepository.Implementation;
using AtmMachine.Services.CustomerServices;

namespace AtmMachine.Menu.Action.Implementation;

class CustomerAction: IMenuAction
{
    #region DataMember
    #region PrivateDataMembers
    private Card? Card { get; set; }
    private readonly ICardReader CardReader;
    private AccountServices? AccountTransaction { get; set; }
    private readonly ICardRepository CardRepository;
    #endregion
    #endregion

    #region Constructor
    internal CustomerAction(ICardReader cardReader)
    {
        CardReader = cardReader;
        CardRepository = new CsvCardRepository();
    }
    #endregion

    #region Methods
    #region PublicMehtod
    public void Execute(int choice)
    {
        if (choice == 5)
        {
            return;
        }
        if (Card==null)
        {
            ProcessCardAuthentication();
            if (Card == null)
            {
                return;
            }
        }

        switch (choice)
        {
            case 1:
                AccountTransaction = new AccountServices(Card, (ICashDispenser)new CashTray());
                AccountTransaction?.Withdraw();
                break;
            case 2:
                AccountTransaction = new AccountServices(Card, (ICashCollector)new CashTray());
                AccountTransaction?.Deposit();
                break;
            case 3:
                AccountTransaction = new AccountServices(Card);
                AccountTransaction?.CheckBalance();
                break;
            case 4:
                CardServices.ChangePin(Card);
                break;
        }
    }
    #endregion

    #region PrivateMethod
    private void ProcessCardAuthentication()
    {
        Card = CardReader.ReadCard();
        if (Card == null)
        {
            return;
        }

        var isVerified = CardRepository.VerifyCard(Card);
        if (isVerified)
        {
            return;
        }

        Card = null;
    }
    #endregion
    #endregion
}