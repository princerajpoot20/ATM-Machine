using AtmMachine.Menu;
using AtmMachine.Menu.Action;
using AtmMachine.Menu.Action.Implementation;
using AtmMachine.Menu.Implementation;
using AtmMachine.Repository.AdminRepository;
using AtmMachine.Repository.AdminRepository.Implementation;
using AtmMachine.Utils.UtilityFunctions;

namespace AtmMachine.Handler.Implementation;

class AdminHandler: IHandler
{
    #region PrivateDataMembers
    private readonly IMenu Menu;
    private readonly IMenuAction MenuAction;
    private readonly IAdminRepository AdminRepository;
    #endregion

    #region Constructor
    internal AdminHandler(IMenu menu, IMenuAction menuAction)
    {
        Menu = menu;
        MenuAction = menuAction;
        AdminRepository = new CsvAdminRepository();
    }
    #endregion

    #region PublicMethod
    public void HandleFlow()
    {
        if (Menu is not AdminMenu)
        {
            throw new ArgumentException("Invalid menu type supplied. AdminHandler requires an AdminMenu.");
        }

        if (MenuAction is not AdminAction)
        {
            throw new ArgumentException("Invalid menu action type supplied. AdminHandler requires an AdminAction.");
        }

        var admin = InputReader.ReadAdminDetails();
        if (admin == null)
        {
            return;
        }

        var isVerified = AdminRepository.VerifyAdminDetails(admin);
        if (!isVerified)
        {
            return;
        }

        while(true)
        {
            var choice = Menu.Display();
            MenuAction.Execute(choice);

            choice = MenuOptionSelector.GetYesNoChoice("Do you want to perform another transaction?");
            if (choice == 2)
            {
                break;
            }
        }
    }
    #endregion
}