namespace AtmMachine.Models;

class Account
{
    #region InternalDataMembers
    internal readonly int AccountNumber;
    internal readonly string Name;
    internal string MobileNumber { get; set; }
    internal int Balance { get; set; }
    #endregion

    #region Constructor
    internal Account(int accountNumber, string name, string mobileNumber, int balance)
    {
        AccountNumber = accountNumber;
        Name = name;
        MobileNumber = mobileNumber;
        Balance = balance;
    }
    #endregion
}