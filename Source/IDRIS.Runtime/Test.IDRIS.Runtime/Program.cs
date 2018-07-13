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
                Console.WriteLine(ILCode.GetLine(0, 0));
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
