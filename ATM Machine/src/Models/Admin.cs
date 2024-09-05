namespace ATM_Machine.src.Models;

public class Admin
{
    public string adminId;
    public int password;

    public Admin(string adminId, int password)
    {
        this.adminId = adminId;
        this.password = password;
    }
}