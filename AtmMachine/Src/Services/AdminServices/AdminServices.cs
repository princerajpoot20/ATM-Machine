using AtmMachine.Hardware.Screen;
using AtmMachine.Repository.CashStorageRepository;
using AtmMachine.Repository.CashStorageRepository.Implementation;
using AtmMachine.Utils.Enums;
using AtmMachine.Utils.UtilityFunctions;

namespace AtmMachine.Services.AdminServices;

class AdminServices
{
    #region PrivateDataMember
    private readonly ICashStorageRepository CashStorageRepository;
    #endregion

    #region Constructor
    internal AdminServices()
    {
        CashStorageRepository = new CsvCashStorageRepository();
    }
    #endregion

    #region InternalMethod
    internal void UpdateCashStorage()
    {
        AtmScreen.DisplayHighlightedText("\nCash Storage");
        var cash = new Dictionary<Denomination, int>();
        foreach (Denomination denomination in Enum.GetValues(typeof(Denomination)))
        {
            var isValid = InputReader.ReadInteger(out var count, Console.GetCursorPosition(), 0, 500, 2, $"Enter the updated quantity of notes of: {denomination}");
            if (!isValid)
            {
                return;
            }
            cash[denomination] = count;
        }

        CashStorageRepository.UpdateCashStorage(cash);
        AtmScreen.DisplaySuccessMessage("Cash Storage updated :)");
    }
    #endregion
}