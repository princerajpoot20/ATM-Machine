using AtmMachine.Services.AdminServices;

namespace AtmMachine.Menu.Action.Implementation;

class AdminAction : IMenuAction
{
    #region PublicMethod
    public void Execute(int choice)
    {
        switch (choice)
        {
            case 1:
                var accountServices = new AdminServices();
                accountServices.UpdateCashStorage();
                break;
        }
    }
    #endregion
}