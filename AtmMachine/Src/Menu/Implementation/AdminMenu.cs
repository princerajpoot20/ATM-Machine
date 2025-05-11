using AtmMachine.Utils.UtilityFunctions;

namespace AtmMachine.Menu.Implementation;

class AdminMenu : IMenu
{
    #region PublicMethod
    public int Display()
    {
        return MenuOptionSelector.GetChoice(new string[]
        {
            "Update Cash Storage",
            "Exit"
        }, 2);
    }
    #endregion
}