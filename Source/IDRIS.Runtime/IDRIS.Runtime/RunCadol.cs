// RunCadol.cs - 07/20/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        private static string _lineText;
        private static string[] _tokens;
        private static int _tokenNum;
        private static int _tokenCount;

        public static void Run()
        {
            while (true)
            {
                _lineText = ILCode.GetLine(Mem.GetByte(MemPos.prog), Mem.GetNum(MemPos.progline, 2));
                Console.WriteLine($"{Mem.GetByte(MemPos.prog).ToString("000")}:{Mem.GetNum(MemPos.progline, 2).ToString("0000")} {_lineText}"); // todo
                _tokens = _lineText.Split('\t');
                _tokenCount = _tokens.GetUpperBound(0) + 1;
                _tokenNum = 0;
                if (Functions.IsNumber(_tokens[_tokenNum]))
                {
                    _tokenNum++;
                }
                Mem.SetNum(MemPos.progline, 2, Mem.GetNum(MemPos.progline, 2) + 1); // move to next line
                try
                {
                    ExecuteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                Console.ReadLine(); // todo
            }
        }

        public static void ExecuteLine()
        {
            if (_tokens[_tokenNum] == "IF")
            {
                _tokenNum++;
                ExecuteIf();
                return;
            }
            if (IsNumericTarget())
            {
                ExecuteNumericAssignment();
                return;
            }
            if (IsAlphaTarget())
            {
                ExecuteAlphaAssignment();
                return;
            }
            ExecuteCommand();
        }
    }
}
