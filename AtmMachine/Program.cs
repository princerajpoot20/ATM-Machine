using AtmMachine.Handler;
using AtmMachine.Handler.Implementation;
using AtmMachine.Hardware.CardReader.Implementation;
using AtmMachine.Hardware.Screen;
using AtmMachine.Menu.Action.Implementation;
using AtmMachine.Menu.Implementation;

namespace AtmMachine;
class Program
{
    #region InternalMethod
    internal static void Main()
    {
        while (true)
        {
            Console.Clear();
            AtmScreen.DisplayHeading("                                  Welcome to ATM                                  ");
            AtmScreen.DisplayHighlightedText("\nPress any key to begin!");

            IHandler handler;
            var key = Console.ReadKey();
            if (key.Key != ConsoleKey.Escape)
            {
                handler = new CustomerHandler(new CustomerMenu(), new CustomerAction(new CardReader()));
                handler.HandleFlow();
                continue;
            }

            handler = new AdminHandler(new AdminMenu(), new AdminAction());
            handler.HandleFlow();
        }
    }
    #endregion
}