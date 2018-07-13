// RunCadol.Commands.cs - 07/13/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {

        public static void Execute()
        {
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
                    Console.WriteLine($" - return to {Mem.GetByte(MemPos.prog)},{Mem.GetNum(MemPos.progline,2)}"); // todo
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
            TokenNum = TokenNum - 1;
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
            TokenNum = TokenNum + 1;
            return _tokens[TokenNum - 1];
        }

        private static long GetNumericExpression()
        {
            long result = 0;
            if (Functions.IsNumber(CurrToken()))
            {
                result = long.Parse(NextToken());
            }
            else
            {
                throw new SystemException("getnumericexpression parse error");
            }
            if (AfterLastToken())
            {
                return result;
            }
            throw new SystemException("getnumericexpression parse error");
        }
    }
}
