// RunCadol.If.cs - 07/14/2018

using System;
using System.Text;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        public static void RunIf()
        {
            string answer1 = GetExpression();
            string compOp = NextToken();
            string answer2 = GetExpression();

            // todo look for GOTO xxx

            Console.WriteLine($" - if {answer1.Replace("\t", "")} {compOp} {answer2.Replace("\t", "")}"); // todo

            //bool result = false;
            //switch (compOp)
            //{
            //    case "=":
            //        result = (answer1 == answer2);
            //        break;
            //    case "#":
            //        result = (answer1 != answer2);
            //        break;
            //    case "<":
            //        result = (answer1 < answer2);
            //        break;
            //    case ">":
            //        result = (answer1 > answer2);
            //        break;
            //    case "<=":
            //        result = (answer1 <= answer2);
            //        break;
            //    case ">=":
            //        result = (answer1 >= answer2);
            //        break;
            //}

        }

        private static string GetExpression()
        {
            StringBuilder result = new StringBuilder();
            result.Append(NextToken());
            while (!AfterLastToken() && !EndOfExpression(CurrToken()))
            {
                result.Append("\t");
                if (CurrToken() == "(" || CurrToken() == "[")
                {
                    result.Append(NextToken());
                    result.Append("\t");
                    result.Append(GetExpression());
                    result.Append("\t");
                    result.Append(NextToken());
                }
                else
                {
                    result.Append(NextToken());
                }
            }
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
