namespace ATM_Machine.src.Models;

internal class Account
{
    internal string AccountNumber { get; private set; }
    internal string Name { get; private set; }
    internal string MobileNumber { get; private set; }
    internal int Balance { get; private set; }
    // Used property here to make only set as private.
    // This way, it can only be set once i.e from constructor.
    internal Account(string accountNumber, string name, string mobileNumber, int balance)
    {
        AccountNumber = accountNumber;
        Name = name;
        MobileNumber = mobileNumber;
        Balance = balance;
    }
}