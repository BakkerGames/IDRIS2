// Program.cs - 07/13/2018

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
                ILCode.OpenLib(0, "PROG_VOL", "P96VER3");
                RunCadol.Run();
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
