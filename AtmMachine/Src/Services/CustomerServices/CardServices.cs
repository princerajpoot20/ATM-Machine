using AtmMachine.Hardware.Screen;
using AtmMachine.LogManager;
using AtmMachine.Models;
using AtmMachine.Repository.CardRepository;
using AtmMachine.Repository.CardRepository.Implementation;
using AtmMachine.Utils.UtilityFunctions;

namespace AtmMachine.Services.CustomerServices;
static class CardServices
{
    #region PrivateDataMember
    private static readonly ICardRepository CardRepository;
    #endregion

    #region Constructor
    static CardServices()
    {
        CardRepository = new CsvCardRepository();
    }
    #endregion

    #region InternalMethod
    internal static void ChangePin(Card card)
    {
        Console.Clear();
        var isVerified = CardRepository.VerifyCard(card);
        if (!isVerified)
        {
            return;
        }

        var isValidInput = InputReader.ReadInteger(out var newPin, Console.GetCursorPosition(), 1000, 9999, 3,
            "Please Enter your new Pin");
        if (!isValidInput)
        {
            AtmScreen.DisplayErrorMessage("Pin changes failed");
            Logger.LogMessage($"{card.CardNumber} : Change pin failed");
            return;
        }

        card.Pin = newPin;
        CardRepository.UpdateCard(card);
        AtmScreen.DisplaySuccessMessage("Pin Change Successfully");
        Logger.LogMessage($"{card.CardNumber} : Change pin successfully");
    }
    #endregion
}