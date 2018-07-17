// RunCadol.Alpha.cs - 07/14/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        private static bool IsAlphaTarget()
        {
            bool found = false;
            for (int i = _tokenNum; i <= _lastTokenNum; i++)
            {
                if (_tokens[i] == "=")
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                return false;
            }
            switch (_tokens[_tokenNum])
            {
                case "KEY":
                case "DATE":
                case "A":
                case "A1":
                case "A2":
                case "B":
                case "B1":
                case "B2":
                case "C":
                case "C1":
                case "C2":
                case "D":
                case "D1":
                case "D2":
                case "E":
                case "E1":
                case "E2":
                    return true;
            }
            if (_tokens[_tokenNum] == "R"
                || _tokens[_tokenNum] == "Z"
                || _tokens[_tokenNum] == "X"
                || _tokens[_tokenNum] == "Y"
                || _tokens[_tokenNum] == "W"
                || _tokens[_tokenNum] == "S"
                || _tokens[_tokenNum] == "T"
                || _tokens[_tokenNum] == "U"
                || _tokens[_tokenNum] == "V")
            {
                if (_tokenNum + 3 <= _lastTokenNum
                    && _tokens[_tokenNum + 1] == "(" 
                    && _tokens[_tokenNum + 2] == "A" 
                    && _tokens[_tokenNum + 3] == ")")
                {
                    return true;
                }
            }
            return false;
        }

        private static void ExecuteAlphaAssignment()
        {
            //throw new NotImplementedException();
        }
    }
}
