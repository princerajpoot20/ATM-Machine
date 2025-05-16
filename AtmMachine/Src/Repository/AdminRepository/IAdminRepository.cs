using AtmMachine.Models;

namespace AtmMachine.Repository.AdminRepository;

internal interface IAdminRepository
{
    bool VerifyAdminDetails(Admin admin);
}

