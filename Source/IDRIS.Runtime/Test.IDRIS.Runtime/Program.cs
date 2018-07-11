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
                ILCode.OpenLib(0, "PROG_VOL", "P96VER3");
                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine(ILCode.GetLine(0, i));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine();
            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }
    }
}
