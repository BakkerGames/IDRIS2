// Program.cs - 07/11/2018

using IDRIS.Runtime;
using System;

namespace Test.IDRIS.Runtime
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ILCode.BasePath = ILCode.BasePath.Replace("$DRIVE$", "D:").Replace("$ENV$", "LOCAL");
                ILCode.OpenLib(0, "PROG_VOL", "TESTLIB");

                Screen.Reset();
                Screen.SetCursor(0, 1);
                Screen.SetAttrib(0);
                Screen.Display("hello world!");
                Screen.SetCursor(4, 15);
                Screen.Display("Greetings!");
                Screen.SetCursor(70, 23);
                Screen.Display("This scrolls to next line");
                Console.WriteLine(Screen.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine();
            Console.Write("Press enter...");
            Console.ReadLine();
        }
    }
}
