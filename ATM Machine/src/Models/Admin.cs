using ATM_Machine.HardwareImplementation;
using ATM_Machine.src.data;
using ATM_Machine.src.Services;
using ATM_Machine.src.Utils;

namespace ATM_Machine.src.Models;

internal class Admin: User
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
    public static Admin GetAdminDetails()
    {
        //Console.SetCursorPosition(cursor.left,cursor.top);
        AtmScreen.DisplayMessage("1 Enter your admin id:");
        var adminId = Console.ReadLine();
        adminId = adminId?.Trim();
        if (string.IsNullOrEmpty(adminId))
        {
            AtmScreen.DisplayErrorMessage("Admin Id cannot be empty.");

            return null;
        }

        Console.WriteLine("Enter your admin pin:");
        var password = Keypad.ReadSenstiveData();
        bool isPin = int.TryParse(password, out int pin);
        if (!isPin)
        {
            AtmScreen.DisplayErrorMessage("Invalid Pin. Pin should be integral.");
            return null;
        }
        if (pin < 1000 || pin > 9999)
        {
            AtmScreen.DisplayErrorMessage("Invalid Pin. Pin should be of 4 digits.");
            return null;
        }

        return new Admin(adminId, pin);
    }
    internal static Admin VerifyAdmin((int left, int top) cursor = default , int attemptsRemaining = 1)
    {
        //Console.Clear();
        var admin=GetAdminDetails();
        if (admin == null && attemptsRemaining > 0)
        {
            int choice = InteractiveMenuSelector.InteractiveMenu();
            if (choice == 2) return null;
            Console.Clear();
            AtmScreen.DisplayWarningMessage("Attempts Remaining: " + attemptsRemaining);
            return VerifyAdmin(cursor, attemptsRemaining - 1);
        }

        else if (admin == null)
        {
            AtmScreen.DisplayErrorMessage("You have exceeded the maximum number of attempts.");
            return null;
        }

        bool isVerified = AdminDetails.VerifyAdminDetails(admin);
        if (!isVerified)
        {
            AtmScreen.DisplayWarningMessage("Admin authentication failed. :(");
            return VerifyAdmin(cursor, attemptsRemaining - 1);
        }
        else
        {
            return admin;
        }
    }
}