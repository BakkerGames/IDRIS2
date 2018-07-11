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

                Mem.SetAlpha(MemPos.dateval, "07/11/2018");
                Mem.SetNum(MemPos.dateval + 10, 4, 20180711);
                Mem.SpoolAlpha(MemPos.key, 20, "this is in key");
                Console.WriteLine(Mem.GetPage(2));
                Console.WriteLine();
                Console.WriteLine(Mem.GetPage(3));
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
