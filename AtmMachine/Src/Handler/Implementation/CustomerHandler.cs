using AtmMachine.Menu;
using AtmMachine.Menu.Action;
using AtmMachine.Menu.Action.Implementation;
using AtmMachine.Menu.Implementation;
using AtmMachine.Utils.UtilityFunctions;

namespace AtmMachine.Handler.Implementation;

class CustomerHandler : IHandler
{
    #region PrivateDataMembers
    private readonly IMenu Menu;
    private readonly IMenuAction MenuAction;
    #endregion

    #region Constructor
    internal CustomerHandler(IMenu menu, IMenuAction menuAction)
    {
        Menu = menu;
        MenuAction = menuAction;
    }
    #endregion

    #region PublicMethod
    public void HandleFlow()
    {
        if (Menu is not CustomerMenu)
        {
            throw new ArgumentException("Invalid menu type supplied. CustomerHandler requires an CustomerMenu.");
        }

        if (MenuAction is not CustomerAction)
        {
            throw new ArgumentException("Invalid menu action type supplied. CustomerHandler requires an CustomerAction.");
        }

        while (true)
        {
            var choice = Menu.Display();
            MenuAction.Execute(choice);
            if (choice == 5)
            {
                return;
            }

            choice = MenuOptionSelector.GetYesNoChoice("Do you want to perform another transaction?");
            if (choice == 2)
            {
                break;
            }
        }
    }
    #endregion
}