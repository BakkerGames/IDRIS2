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
                mem.setalpha(mempos.a, "Hello world!");
                Console.WriteLine(mem.getpage(3));
                Console.WriteLine();
                mem.spoolalpha(mempos.a, 5, "Hi!");
                Console.WriteLine(mem.getpage(3));
                Console.WriteLine();
                Console.WriteLine(mem.getalpha(mempos.a));
                Console.WriteLine($"\"{mem.packalpha(mempos.a, 6)}\"");
                mem.setalpha(mempos.a + 3, "");
                Console.WriteLine(mem.getpage(3));
                Console.WriteLine($"\"{mem.packalpha(mempos.a, 6)}\"");
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
