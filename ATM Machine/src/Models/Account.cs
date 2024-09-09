namespace ATM_Machine.src.Models;

internal class Account
{
    internal string AccountNumber;
    internal string Name;
    internal string MobileNumber;

    internal int Balance;
    internal Account(string accountNumber, string name, string mobileNumber, int balance)
    {
        AccountNumber = accountNumber;
        Name = name;
        MobileNumber = mobileNumber;
        Balance = balance;
    }
}