// Debug.cs - 07/31/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class Debug
    {
        public static void RaiseProgLine(long prognum, long linenum)
        {
            Console.WriteLine($"{prognum.ToString("000")}:{linenum.ToString("0000")} {ILCode.GetLine(prognum, linenum)}");
        }

        public static void RaiseBreakpoint(long prognum, long linenum)
        {
            Console.Write($"### breakpoint {prognum}, {linenum} ###");
            Console.ReadLine();
        }

        public static void StepOver()
        {
            long prognum = Mem.GetByte(MemPos.prog);
            long linenum = Mem.GetNum(MemPos.progline, 2);
            SetBreakpoint(prognum, linenum + 1, true);
        }

        public static void StepInto()
        {
            _nextLineBreakpoint = true;
        }
    }
}
