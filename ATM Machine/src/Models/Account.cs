namespace ATM_Machine.src.Models;

public class Account
{
    public string AccountNumber;
    public string Name;
    public string MobileNumber;
    public int Balance;
    internal Account(string accountNumber, string name, string mobileNumber, int balance)
    {
        AccountNumber = accountNumber;
        Name = name;
        MobileNumber = mobileNumber;
        Balance = balance;
    }
}