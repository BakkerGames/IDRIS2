// RunCadol.Alpha.cs - 07/19/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        private static bool IsAlphaTarget()
        {
            if (MemPos.GetPosAlpha(_tokens[_tokenNum]).HasValue)
            {
                if (_tokenNum < _tokenCount)
                {
                    if (_tokens[_tokenNum + 1] == "="
                        || _tokens[_tokenNum + 1] == "[")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void ExecuteAlphaAssignment()
        {
            long? targetPos = null;
            targetPos = MemPos.GetPosAlpha(_tokens[_tokenNum]);
            if (!targetPos.HasValue)
            {
                throw new SystemException("Cannot parse alpha assignment: Target not found");
            }
            _tokenNum++;
            if (_tokens[_tokenNum] == "[")
            {
                _tokenNum++; // "["
                long offset = GetNumericExpression();
                if (_tokens[_tokenNum++] != "]")
                {
                    throw new SystemException("ExecuteAlphaAssignment: No closing \"]\"");
                }
                targetPos += offset;
            }
            if (_tokens[_tokenNum] != "=")
            {
                throw new SystemException("Cannot parse alpha expression: Equals sign expected");
            }
            _tokenNum++;
            string result = GetAlphaExpression();
            Mem.SetAlpha(targetPos.Value, result);
        }

        private static string GetAlphaExpression()
        {
            if (_tokenNum >= _tokenCount)
            {
                throw new SystemException("Cannot parse alpha expression: Unexpected end of line");
            }
            string result = null;
            if (_tokens[_tokenNum].StartsWith("\"")
                || _tokens[_tokenNum].StartsWith("'")
                || _tokens[_tokenNum].StartsWith("$")
                || _tokens[_tokenNum].StartsWith("%"))
            {
                if (_tokenNum != _tokenCount - 1)
                {
                    throw new SystemException("Cannot parse alpha expression: Tokens after string literal");
                }
                result = _tokens[_tokenNum].Substring(1, _tokens[_tokenNum].Length - 2);
            }
            else
            {
                long? targetPos = null;
                targetPos = MemPos.GetPosAlpha(_tokens[_tokenNum++]);
                if (!targetPos.HasValue)
                {
                    throw new SystemException("Cannot parse alpha assignment: Target not found");
                }
                if (_tokenNum < _tokenCount && _tokens[_tokenNum] == "[")
                {
                    _tokenNum++; // "["
                    long offset = GetNumericExpression();
                    if (_tokens[_tokenNum++] != "]")
                    {
                        throw new SystemException("GetAlphaExpression: No closing \"]\"");
                    }
                    targetPos += offset;
                }
                result = Mem.GetAlpha(targetPos.Value);
            }
            if (result == null)
            {
                throw new SystemException("Cannot parse alpha expression: No result");
            }
            return result;
        }
    }
}
