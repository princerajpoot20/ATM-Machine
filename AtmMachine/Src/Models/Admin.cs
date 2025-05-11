namespace AtmMachine.Models;

class Admin
{
    #region InternalDataMember
    internal readonly int Id;
    internal readonly int Pin;
    #endregion

    #region Constructor
    internal Admin(int id, int pin)
    {
        Id = id;
        Pin = pin;
    }
    #endregion
}