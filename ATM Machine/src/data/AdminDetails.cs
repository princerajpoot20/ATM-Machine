using ATM_Machine.src.Models;

namespace ATM_Machine.src.data
{

    internal class AdminDetails
    {
        private const string _adminDetailsPath =
            @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\admin_details.csv";

        protected static bool VerifyAdminDetails(Admin admin)
        {
            var details = File.ReadAllLines(_adminDetailsPath);
            foreach (var detail in details)
            {
                var data = detail.Split(',');
                if (data[0] == admin.adminId && Convert.ToInt32(data[1]) == admin.password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}