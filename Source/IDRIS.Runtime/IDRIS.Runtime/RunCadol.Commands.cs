// RunCadol.Commands.cs - 07/14/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        public static void Execute()
        {
            // conditionals
            if (CurrToken() == "IF")
            {
                TokenNum++;
                RunIf();
                return;
            }
            
            bool done = false;

            // single token commands
            switch (NextToken())
            {
                case "BACK":
                    Screen.Back();
                    done = true;
                    break;
                case "CANCEL":
                    if (!AfterLastToken())
                    {
                        break;
                    }
                    // todo check for cancel event
                    Mem.SetNum(MemPos.progline, 2, 0);
                    Mem.SetByte(MemPos.progtoken, 0);
                    done = true;
                    break;
                case "CLEAR":
                    Screen.Clear();
                    done = true;
                    break;
                case "CLOSETFA":
                    // todo
                    break;
                case "CLOSEVOLUME":
                    // todo
                    break;
                case "CR":
                    Screen.CursorAt(-1, 0);
                    done = true;
                    break;
                case "ESC":
                    if (!AfterLastToken())
                    {
                        break;
                    }
                    // todo check for esc event
                    Mem.SetByte(MemPos.prog, 0);
                    Mem.SetNum(MemPos.progline, 2, 0);
                    Mem.SetByte(MemPos.progtoken, 0);
                    done = true;
                    break;
                case "GRAPHOFF":
                    Screen.SetGraphics(false);
                    done = true;
                    break;
                case "GRAPHON":
                    Screen.SetGraphics(true);
                    done = true;
                    break;
                case "HOME":
                    Screen.CursorAt(0, 0);
                    done = true;
                    break;
                case "INITFETCH":
                    // todo
                    break;
                case "KLOCK":
                    Keyboard.KeyLock(true);
                    done = true;
                    break;
                case "KFREE":
                    Keyboard.KeyLock(false);
                    done = true;
                    break;
                case "LOCK":
                    Data.LockFlag(true);
                    done = true;
                    break;
                case "MERGE":
                    // todo
                    break;
                case "NOP":
                    done = true;
                    break;
                case "PRINTOFF":
                    Mem.SetBool(MemPos.printon, false);
                    done = true;
                    break;
                case "PRINTON":
                    Mem.SetBool(MemPos.printon, true);
                    done = true;
                    break;
                case "REJECT":
                    Screen.Reject();
                    break;
                case "RESETSCREEN":
                    Screen.Reset();
                    done = true;
                    break;
                case "RELEASEDEVICE":
                    // todo
                    break;
                case "RETURN":
                    GosubStack.Pop();
                    Console.WriteLine($" - return to {Mem.GetByte(MemPos.prog)},{Mem.GetNum(MemPos.progline, 2)}"); // todo
                    done = true;
                    break;
                case "STAY":
                    Screen.SetStay(true);
                    done = true;
                    break;
                case "UNLOCK":
                    Data.LockFlag(false);
                    done = true;
                    break;
                case "TABCANCEL":
                    // todo
                    break;
                case "TABCLEAR":
                    // todo
                    break;
                case "TABSET":
                    // todo
                    break;
                case "WRITEBACK":
                    Data.WriteBack();
                    done = true;
                    break;
                case "ZERO":
                    for (int i = 0; i <= 20; i++)
                    {
                        Mem.SetNum(MemPos.nx(i), MemPos.numslotsize, 0);
                    }
                    done = true;
                    break;
            }
            if (done)
            {
                return;
            }
            // multi-token commands
            TokenNum = TokenNum - 1; // move back one
            long tempValue = 0;
            switch (NextToken())
            {
                case "NL":
                    if (AfterLastToken())
                    {
                        tempValue = 1;
                    }
                    else
                    {
                        tempValue = GetNumericExpression();
                    }
                    Screen.NL(tempValue);
                    done = true;
                    break;
                case "TAB":
                    if (AfterLastToken())
                    {
                        tempValue = 1;
                    }
                    else
                    {
                        tempValue = GetNumericExpression();
                    }
                    Screen.Tab(tempValue);
                    done = true;
                    break;
                case "GOTO":
                    tempValue = GetNumericExpression();
                    Mem.SetNum(MemPos.progline, 2, tempValue);
                    Mem.SetByte(MemPos.progtoken, 0);
                    Console.WriteLine($" - goto {Mem.GetByte(MemPos.prog)},{Mem.GetNum(MemPos.progline, 2)}"); // todo
                    done = true;
                    break;
                case "GOS":
                    tempValue = GetNumericExpression();
                    GosubStack.Push();
                    Mem.SetNum(MemPos.progline, 2, tempValue);
                    Mem.SetByte(MemPos.progtoken, 0);
                    Console.WriteLine($" - gos {Mem.GetByte(MemPos.prog)},{Mem.GetNum(MemPos.progline, 2)}"); // todo
                    done = true;
                    break;
                case "LOAD":
                    tempValue = GetNumericExpression();
                    Mem.SetByte(MemPos.prog, tempValue);
                    Mem.SetNum(MemPos.progline, 2, 0);
                    Mem.SetByte(MemPos.progtoken, 0);
                    Console.WriteLine($" - load {Mem.GetByte(MemPos.prog)},{Mem.GetNum(MemPos.progline, 2)}"); // todo
                    done = true;
                    break;
                case "GOSUB":
                    tempValue = GetNumericExpression();
                    GosubStack.Push();
                    Mem.SetByte(MemPos.prog, tempValue);
                    Mem.SetNum(MemPos.progline, 2, 0);
                    Mem.SetByte(MemPos.progtoken, 0);
                    Console.WriteLine($" - gosub {Mem.GetByte(MemPos.prog)},{Mem.GetNum(MemPos.progline, 2)}"); // todo
                    done = true;
                    break;
            }
            // check if done
            if (done)
            {
                return;
            }
            TokenNum--;
            Console.Write(CurrToken());
            Console.WriteLine(" - command not found");
        }

        public static bool AfterLastToken()
        {
            return (TokenNum > _tokens.GetUpperBound(0));
        }

        public static string CurrToken()
        {
            return _tokens[TokenNum];
        }

        public static string NextToken()
        {
            TokenNum++;
            return _tokens[TokenNum - 1];
        }

        private static long GetNumericExpression()
        {
            long? result = null;
            long tempNum = 0;
            long unaryminus = 1;

            if (CurrToken() == ")" 
                || CurrToken() == "]" 
                || CurrToken() == "," 
                || CurrToken() == "="
                || CurrToken() == "#"
                || CurrToken() == "<"
                || CurrToken() == ">"
                || CurrToken() == "<="
                || CurrToken() == ">="
                || CurrToken() == "IF"
                || CurrToken() == "THEN"
                || CurrToken() == "AND"
                || CurrToken() == "OR"
                )
            {
                TokenNum++;
                return result.Value;
            }

            if (CurrToken() == "-")
            {
                TokenNum++;
                unaryminus = -1;
            }

            if (CurrToken() == "(")
            {
                result = unaryminus * GetNumericExpression();
                unaryminus = 1;
            }
            else if (Functions.IsNumber(CurrToken()))
            {
                result = unaryminus * long.Parse(NextToken());
                unaryminus = 1;
            }

            if (AfterLastToken())
            {
                return result.Value;
            }

            if (CurrToken() == ")" || CurrToken() == "]" || CurrToken() == ",")
            {
                TokenNum++;
                return result.Value;
            }

            bool hasOP = false;
            do
            {
                hasOP = false;
                if (CurrToken() == "+")
                {
                    result = result.Value + GetNumericExpression();
                    hasOP = true;
                }
                else if (CurrToken() == "-")
                {
                    result = result.Value - GetNumericExpression();
                    hasOP = true;
                }
                else if (CurrToken() == "*")
                {
                    result = result.Value * GetNumericExpression();
                    hasOP = true;
                }
                else if (CurrToken() == "/")
                {
                    tempNum = GetNumericExpression();
                    Mem.SetNum(MemPos.rem, MemPos.numslotsize, result.Value % tempNum);
                    result = result.Value / tempNum;
                    hasOP = true;
                }
            } while (hasOP);
            return result.Value;
        }
    }
}
