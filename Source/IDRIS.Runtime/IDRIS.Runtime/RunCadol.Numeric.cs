// RunCadol.Numeric.cs - 07/14/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        private static bool IsNumericTarget()
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
            string currToken = _tokens[_tokenNum];
            if (currToken == "N" || currToken == "F" || currToken == "G")
            {
                return true;
            }
            if (currToken.StartsWith("N") || currToken.StartsWith("F") || currToken.StartsWith("G"))
            {
                if (Functions.IsNumber(currToken.Substring(1)))
                {
                    return true;
                }
            }
            // todo buffers and sysvars
            return false;
        }

        private static void ExecuteNumericAssignment()
        {
            int saveTokenNum = _tokenNum;
            string currToken = _tokens[_tokenNum++];
            if (_tokens[_tokenNum++] != "=")
            {
                _tokenNum = saveTokenNum;
                throw new NotImplementedException();
            }
            int offset;
            long tempValue = GetNumericExpression();
            if (currToken == "N")
            {
                Mem.SetNum(MemPos.n, MemPos.numslotsize, tempValue);
                return;
            }
            if (currToken.StartsWith("N"))
            {
                offset = int.Parse(currToken.Substring(1));
                Mem.SetNum(MemPos.nx(offset), MemPos.numslotsize, tempValue);
                return;
            }
            if (currToken == "F")
            {
                Mem.SetByte(MemPos.f, tempValue);
                return;
            }
            if (currToken.StartsWith("F"))
            {
                offset = int.Parse(currToken.Substring(1));
                Mem.SetByte(MemPos.fx(offset), tempValue);
                return;
            }
            if (currToken == "G")
            {
                Mem.SetNum(MemPos.g, 2, tempValue);
                return;
            }
            if (currToken.StartsWith("G"))
            {
                offset = int.Parse(currToken.Substring(1));
                Mem.SetNum(MemPos.gx(offset), 2, tempValue);
                return;
            }
        }

        private static long GetNumericExpression()
        {
            long result = 0;

            string currToken = _tokens[_tokenNum++];
            if (Functions.IsNumber(currToken))
            {
                result = long.Parse(currToken);
            }
            else if (currToken == "FALSE")
            {
                result = 0;
            }
            else if (currToken == "TRUE")
            {
                result = 1;
            }

            //long tempNum = 0;
            //long unaryminus = 1;

            //if (CurrToken() == ")"
            //    || CurrToken() == "]"
            //    || CurrToken() == ","
            //    || CurrToken() == "="
            //    || CurrToken() == "#"
            //    || CurrToken() == "<"
            //    || CurrToken() == ">"
            //    || CurrToken() == "<="
            //    || CurrToken() == ">="
            //    || CurrToken() == "IF"
            //    || CurrToken() == "THEN"
            //    || CurrToken() == "AND"
            //    || CurrToken() == "OR"
            //    )
            //{
            //    _tokenNum++;
            //    return result.Value;
            //}

            //if (CurrToken() == "-")
            //{
            //    _tokenNum++;
            //    unaryminus = -1;
            //}

            //if (CurrToken() == "(")
            //{
            //    result = unaryminus * GetNumericExpression();
            //    unaryminus = 1;
            //}
            //else if (Functions.IsNumber(CurrToken()))
            //{
            //    result = unaryminus * long.Parse(NextToken());
            //    unaryminus = 1;
            //}

            //if (AfterLastToken())
            //{
            //    return result.Value;
            //}

            //if (CurrToken() == ")" || CurrToken() == "]" || CurrToken() == ",")
            //{
            //    _tokenNum++;
            //    return result.Value;
            //}

            //bool hasOP = false;
            //do
            //{
            //    hasOP = false;
            //    if (CurrToken() == "+")
            //    {
            //        result = result.Value + GetNumericExpression();
            //        hasOP = true;
            //    }
            //    else if (CurrToken() == "-")
            //    {
            //        result = result.Value - GetNumericExpression();
            //        hasOP = true;
            //    }
            //    else if (CurrToken() == "*")
            //    {
            //        result = result.Value * GetNumericExpression();
            //        hasOP = true;
            //    }
            //    else if (CurrToken() == "/")
            //    {
            //        tempNum = GetNumericExpression();
            //        Mem.SetNum(MemPos.rem, MemPos.numslotsize, result.Value % tempNum);
            //        result = result.Value / tempNum;
            //        hasOP = true;
            //    }
            //} while (hasOP);

            return result;
        }
    }
}
