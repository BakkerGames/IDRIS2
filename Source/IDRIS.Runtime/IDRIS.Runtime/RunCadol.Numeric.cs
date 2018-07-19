// RunCadol.Numeric.cs - 07/19/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        private static bool IsNumericTarget()
        {
            if (MemPos.GetPosByte(_tokens[_tokenNum]).HasValue
                || MemPos.GetPosNumeric(_tokens[_tokenNum]).HasValue)
            {
                if (_tokenNum < _tokenCount)
                {
                    if (_tokens[_tokenNum + 1] == "="
                        || _tokens[_tokenNum + 1] == "["
                        || _tokens[_tokenNum + 1] == "(")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void ExecuteNumericAssignment()
        {
            //int saveTokenNum = _tokenNum;
            long? targetPos = null;
            long? targetSize = null;
            bool? isTargetByte = null;
            targetPos = MemPos.GetPosByte(_tokens[_tokenNum]);
            if (targetPos.HasValue)
            {
                targetSize = 1;
                isTargetByte = true;
            }
            else
            {
                targetPos = MemPos.GetPosNumeric(_tokens[_tokenNum]);
                if (targetPos.HasValue)
                {
                    targetSize = MemPos.GetSizeNumeric(_tokens[_tokenNum]);
                    isTargetByte = false;
                }
            }
            if (!targetPos.HasValue || !targetSize.HasValue || !isTargetByte.HasValue )
            {
                throw new SystemException("Cannot parse numeric assignment: Target not found");
            }
            _tokenNum++;
            if (_tokens[_tokenNum] == "[")
            {
                _tokenNum++; // "["
                long offset = GetNumericExpression();
                _tokenNum++; // "]"
                targetPos += offset;
            }
            if (_tokens[_tokenNum] == "(")
            {
                throw new SystemException("TODO: Cannot handle buffer lengths yet");
            }
            if (_tokens[_tokenNum] != "=")
            {
                throw new SystemException("Cannot parse numeric expression: Equals sign expected");
            }
            _tokenNum++;
            long result = GetNumericExpression();
            if (isTargetByte.Value)
            {
                Mem.SetByte(targetPos.Value, result);
            }
            else
            {
                Mem.SetNum(targetPos.Value, targetSize.Value, result);
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
