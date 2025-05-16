using AtmMachine.Models;

namespace AtmMachine.Repository.AdminRepository.Implementation;

class CsvAdminRepository : IAdminRepository
{
    #region PrivateDataMember
    private const string AdminPath = @"..\..\..\Src\Database\Admin.csv";
    #endregion

    #region PublicMethod
    public bool VerifyAdminDetails(Admin admin)
    {
        string[] details;
        try
        {
            details = File.ReadAllLines(AdminPath);
        }
        catch (Exception exception)
        {
            throw new Exception("File error occured", exception);
        }

        foreach (var detail in details)
        {
            var data = detail.Split(',');
            if (Convert.ToInt32(data[0]) == admin.Id && Convert.ToInt32(data[1]) == Convert.ToInt32(admin.Pin))
            {
                return true;
            }
        }

        return false;
    }
    #endregion
}