// Program.cs - 07/10/2018

using IDRIS_Runtime;
using System;

namespace Test.IDRIS_Runtime
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                mem.setnum(mempos.totalmemsize - 6, 6, -42);
                Console.WriteLine(mem.getnum(mempos.totalmemsize - 6, 6).ToString());
                Console.WriteLine();
                Console.WriteLine(mem.getpage(mempos.totalpagecount - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }
    }
}
