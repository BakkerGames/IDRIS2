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

                Mem.SetAlphaBuffer(MemPos.rp, "Hello world!");
                Console.WriteLine(Mem.GetPage(0));
                Console.WriteLine();
                Console.WriteLine(Mem.GetPage(1));
                Console.WriteLine();
                Console.WriteLine(Mem.GetPage(2));
                Console.WriteLine();
                Console.WriteLine(Mem.GetPage(3));
                Console.WriteLine();
                Console.WriteLine(Mem.GetPage(MemPos.rpage));
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
