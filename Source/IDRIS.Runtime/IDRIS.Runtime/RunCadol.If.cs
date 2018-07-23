// RunCadol.If.cs - 07/23/2018

using System;
using System.Text;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        public static void ExecuteIf()
        {
            Console.WriteLine("parse IF here"); // todo

            int saveTokenNum = _tokenNum;

            long numAnswer1 = 0;
            long numAnswer2 = 0;
            string alphaAnswer1 = "";
            string alphaAnswer2 = "";
            string comparitor = "";
            bool result = false;

            try
            {
                numAnswer1 = GetNumericExpression();
                comparitor = _tokens[_tokenNum++];
                numAnswer2 = GetNumericExpression();
                switch (comparitor)
                {
                    case "=":
                        result = (numAnswer1 == numAnswer2);
                        break;
                    case "#":
                        result = (numAnswer1 != numAnswer2);
                        break;
                    case "<":
                        result = (numAnswer1 < numAnswer2);
                        break;
                    case ">":
                        result = (numAnswer1 > numAnswer2);
                        break;
                    case "<=":
                        result = (numAnswer1 <= numAnswer2);
                        break;
                    case ">=":
                        result = (numAnswer1 >= numAnswer2);
                        break;
                    default:
                        throw new SystemException("Unknown comparitor");
                }
            }
            catch (Exception)
            {
                try
                {
                    _tokenNum = saveTokenNum;
                    alphaAnswer1 = GetAlphaExpression();
                    comparitor = _tokens[_tokenNum++];
                    alphaAnswer2 = GetAlphaExpression();
                    switch (comparitor)
                    {
                        case "=":
                            result = (alphaAnswer1 == alphaAnswer2);
                            break;
                        case "#":
                            result = (alphaAnswer1 != alphaAnswer2);
                            break;
                        case "<":
                            result = (alphaAnswer1.CompareTo(alphaAnswer2) < 0);
                            break;
                        case ">":
                            result = (alphaAnswer1.CompareTo(alphaAnswer2) > 0);
                            break;
                        case "<=":
                            result = (alphaAnswer1.CompareTo(alphaAnswer2) <= 0);
                            break;
                        case ">=":
                            result = (alphaAnswer1.CompareTo(alphaAnswer2) >= 0);
                            break;
                        default:
                            throw new SystemException("Unknown comparitor");
                    }
                }
                catch (Exception)
                {
                    throw new SystemException("Error parsing IF expressions");
                }
            }
            if (!result)
            {
                return;
            }
            if (_tokenNum >= _tokenCount)
            {
                throw new SystemException("End of line in IF");
            }
            if (_tokens[_tokenNum++] != "GOTO")
            {
                throw new SystemException("GOTO not found after IF compare");
            }
            // goto xxx
            long tempValue = GetNumericExpression();
            Mem.SetNum(MemPos.progline, 2, tempValue);
        }

        private static string GetExpression()
        {
            StringBuilder result = new StringBuilder();
            //result.Append(NextToken());
            //while (!AfterLastToken() && !EndOfExpression(CurrToken()))
            //{
            //    result.Append("\t");
            //    if (CurrToken() == "(" || CurrToken() == "[")
            //    {
            //        result.Append(NextToken());
            //        result.Append("\t");
            //        result.Append(GetExpression());
            //        result.Append("\t");
            //        result.Append(NextToken());
            //    }
            //    else
            //    {
            //        result.Append(NextToken());
            //    }
            //}
            return result.ToString();
        }

        private static bool EndOfExpression(string value)
        {
            switch (value)
            {
                case ")":
                case "]":
                case ",":
                case "=":
                case "#":
                case "<":
                case ">":
                case "<=":
                case ">=":
                case "IF":
                case "THEN":
                case "AND":
                case "OR":
                case "GOTO":
                    return true;
            }
            return false;
        }

        private static bool IsComparitor(string value)
        {
            switch (value)
            {
                case "=":
                case "#":
                case "<":
                case ">":
                case "<=":
                case ">=":
                    return true;
            }
            return false;
        }
    }
}
