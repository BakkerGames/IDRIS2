// RunCadol.cs - 07/31/2018

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
            long prognum;
            long linenum;
            while (true)
            {
                // get current program and line
                prognum = Mem.GetByte(MemPos.prog);
                linenum = Mem.GetNum(MemPos.progline, 2);
                // do debug events as needed
                Debug.RaiseProgLine(prognum, linenum);
                if (Debug.HasBreakpoint(prognum, linenum))
                {
                    Debug.RaiseBreakpoint(prognum, linenum);
                }
                // handle current line
                _lineText = ILCode.GetLine(prognum, linenum);
                _tokens = _lineText.Split('\t');
                _tokenCount = _tokens.GetUpperBound(0) + 1;
                _tokenNum = 0;
                if (Functions.IsNumber(_tokens[_tokenNum]))
                {
                    _tokenNum++; // line number
                }
                try
                {
                    // move to next line in preparation, could be changed by command
                    Mem.SetNum(MemPos.progline, 2, linenum + 1);
                    ExecuteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
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
