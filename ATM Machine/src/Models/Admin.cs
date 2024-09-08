namespace ATM_Machine.src.Models;

internal class Admin
{
    internal string adminId { get; private set; }
    internal int password { get; private set; }
    // Used property here to make only set as private.
    // This way, it can only be set once i.e from constructor.

    internal Admin(string adminId, int password)
    {
        this.adminId = adminId;
        this.password = password;
    }
}