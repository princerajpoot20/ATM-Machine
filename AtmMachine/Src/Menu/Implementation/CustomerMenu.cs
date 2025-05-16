using AtmMachine.Utils.UtilityFunctions;

namespace AtmMachine.Menu.Implementation;

class CustomerMenu : IMenu
{
    #region PublicMethod
    public int Display()
    {
        Console.Clear();
        var menu = new string[]
        {
            "Withdraw Cash",
            "Deposit Cash",
            "Check Balance",
            "Pin Change",
            "Exit"
        };
        return MenuOptionSelector.GetChoice(menu, 5);
    }
    #endregion
}