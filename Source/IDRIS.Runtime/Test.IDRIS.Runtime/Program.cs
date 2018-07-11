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
                Screen.CursorAt(1, 0);
                Screen.SetAttrib(0);
                Screen.Display("hello world!");
                Screen.CursorAt(15, 4);
                Screen.SetAttrib(0);
                Screen.Display("Greetings!");
                Screen.CursorAt(23, 70);
                Screen.SetAttrib(2);
                Screen.Display("This scrolls to next line");
                Screen.CursorAt(0, 0);
                Screen.Tab();
                Console.WriteLine(Screen.ToString());
                Console.WriteLine();
                Screen.CursorAt(0, 0);
                Screen.Clear();
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
