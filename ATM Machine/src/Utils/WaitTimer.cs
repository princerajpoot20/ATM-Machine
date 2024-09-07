using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Machine.src.Utils
{
    internal class WaitTimer
    {
        internal static void Wait(int seconds)
        {
            var curson = Console.GetCursorPosition();
            for (int i = 0; i <= seconds; i++)
            {
                Thread.Sleep(1000);
                Console.SetCursorPosition(curson.Left, curson.Top);
                Console.Write(seconds-i);
            }
            
        }
        internal static void ProcessingWait(int seconds)
        {
            var curson = Console.GetCursorPosition();
            for (int i = 0; i <= seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.Write(".");
                Thread.Sleep(500);
                Console.SetCursorPosition(curson.Left, curson.Top);
                Console.Write("   ");
                Console.SetCursorPosition(curson.Left, curson.Top);
            }

        }
    }
}
