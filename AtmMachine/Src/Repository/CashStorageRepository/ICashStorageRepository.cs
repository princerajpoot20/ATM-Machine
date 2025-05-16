using AtmMachine.Utils.Enums;

namespace AtmMachine.Repository.CashStorageRepository;

internal interface ICashStorageRepository
{
    void UpdateCashStorage(Dictionary<Denomination, int> cash);
    Dictionary<Denomination, int> GetAvailableCash();
}