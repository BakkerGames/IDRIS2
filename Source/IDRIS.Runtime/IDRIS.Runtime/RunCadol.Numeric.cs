// RunCadol.Numeric.cs - 07/20/2018

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
            // todo must check numeric assignment flavors
            // todo must handle numeric buffers as targets and values
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
            if (!targetPos.HasValue || !targetSize.HasValue || !isTargetByte.HasValue)
            {
                throw new SystemException("Cannot parse numeric assignment: Target not found");
            }
            _tokenNum++;
            if (_tokens[_tokenNum] == "[")
            {
                _tokenNum++; // "["
                long offset = GetNumericExpression();
                if (_tokens[_tokenNum++] != "]")
                {
                    throw new SystemException("GetNumericAssignment: No closing \"]\"");
                }
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
            long result;
            long negative = 1;
            string currToken;
            currToken = _tokens[_tokenNum++];
            if (currToken == "-")
            {
                negative = -1;
                currToken = _tokens[_tokenNum++];
            }
            if (currToken == "(")
            {
                result = negative * GetNumericExpression();
                if (_tokens[_tokenNum++] != ")")
                {
                    throw new SystemException("GetNumericExpression: No closing \")\"");
                }
            }
            else if (Functions.IsNumber(currToken))
            {
                result = negative * long.Parse(currToken);
            }
            else
            {
                _tokenNum--; // move back one
                result = negative * GetNumericValue();
            }
            // see if done with expression
            if (_tokenNum >= _tokenCount)
            {
                return result;
            }
            currToken = _tokens[_tokenNum];
            switch (currToken)
            {
                case "]":
                    return result;
                case ")":
                    return result;
                case "+":
                    _tokenNum++;
                    result += GetNumericExpression();
                    return result;
                case "-":
                    _tokenNum++;
                    result =+ GetNumericExpression();
                    return result;
                case "*":
                    _tokenNum++;
                    result *= GetNumericExpression();
                    return result;
                case "/":
                    _tokenNum++;
                    long tempExpr = GetNumericExpression();
                    Mem.SetNum(MemPos.rem, MemPos.numslotsize, result % tempExpr);
                    return result / tempExpr;
            }
            throw new SystemException("GetNumericExpression error");
        }

        private static long GetNumericValue()
        {
            string currToken = _tokens[_tokenNum++];
            if (Functions.IsNumber(currToken))
            {
                return long.Parse(currToken);
            }
            if (currToken == "FALSE")
            {
                return 0;
            }
            if (currToken == "TRUE")
            {
                return 1;
            }
            long? targetPos = null;
            long? targetSize = null;
            bool? isTargetByte = null;
            targetPos = MemPos.GetPosByte(currToken);
            if (targetPos.HasValue)
            {
                targetSize = 1;
                isTargetByte = true;
            }
            else
            {
                targetPos = MemPos.GetPosNumeric(currToken);
                if (targetPos.HasValue)
                {
                    targetSize = MemPos.GetSizeNumeric(currToken);
                    isTargetByte = false;
                }
            }
            if (!targetPos.HasValue || !targetSize.HasValue || !isTargetByte.HasValue)
            {
                throw new SystemException("GetNumericValue error");
            }
            if (_tokenNum < _tokenCount && _tokens[_tokenNum] == "[")
            {
                _tokenNum++; // "["
                long offset = GetNumericExpression();
                if (_tokens[_tokenNum++] != "]")
                {
                    throw new SystemException("GetNumericValue: No closing \"]\"");
                }
                targetPos += offset;
            }
            if (isTargetByte.Value)
            {
                return Mem.GetByte(targetPos.Value);
            }
            return Mem.GetNum(targetPos.Value, targetSize.Value);
        }
    }
}
