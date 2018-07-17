// RunCadol.cs - 07/13/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        private static string _lineText;
        private static string[] _tokens;

        private static long TokenNum
        {
            get
            {
                return Mem.GetByte(MemPos.progtoken);
            }
            set
            {
                Mem.SetByte(MemPos.progtoken, value);
            }
        }

        public static void Run()
        {
            long debugcount = 0;
            do
            {
                _lineText = ILCode.GetLine(Mem.GetByte(MemPos.prog), Mem.GetNum(MemPos.progline, 2));
                Mem.SetNum(MemPos.progline, 2, Mem.GetNum(MemPos.progline, 2) + 1);
                Mem.SetByte(MemPos.progtoken, 0);
                _tokens = _lineText.Split('\t');
                if (Functions.IsNumber(_tokens[0]))
                {
                    Mem.SetByte(MemPos.progtoken, 1); // skip line number
                }
                Console.WriteLine(_lineText); // todo
                Execute();
                debugcount++;
                if (debugcount >= 18)
                {
                    Console.Write("Press enter...");
                    Console.ReadLine();
                    Console.Clear();
                    debugcount = 0;
                }
            } while (Mem.GetByte(MemPos.prog)
                     + Mem.GetNum(MemPos.progline, 2)
                     + Mem.GetByte(MemPos.progtoken) > 0);
        }
    }
}
