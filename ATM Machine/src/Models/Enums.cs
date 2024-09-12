namespace ATM_Machine.src.Models;

public enum AtmState
{
    InService,
    OutOfService
}

public enum CurrencyDenomination
{
    One = 1,
    Fifty =50,
    OneHundred= 100,
    TwoHundred= 200,
    FiveHundred=500
}

public enum TransactionType
{
    Debit,
    Credit
}

