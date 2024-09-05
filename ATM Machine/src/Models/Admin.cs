namespace ATM_Machine.src.Models;

internal class Admin
{
    internal string adminId;
    internal int password;

    public Admin(string adminId, int password)
    {
        this.adminId = adminId;
        this.password = password;
    }
}