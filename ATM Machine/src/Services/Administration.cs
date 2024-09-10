using ATM_Machine.src.Models;

namespace ATM_Machine.src.Services;

internal abstract class Administration
{
    protected Admin admin;

    protected Administration(Admin admin)
    // Protected constructor
    // It can only be accessed by derived class constructor.
    // Important Note:: Protected constructor cannot be accessed from derived class method

    {
        this.admin= admin;
    }

    internal abstract void Execute();
}