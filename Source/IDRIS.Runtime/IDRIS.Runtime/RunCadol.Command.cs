// RunCadol.Command.cs - 09/28/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        private static void ExecuteCommand()
        {
            long tempNum;
            string tempAlpha;
            switch (_tokens[_tokenNum++])
            {
                case "ATT":
                    long att = GetNumericValue();
                    Screen.SetAttrib(att);
                    break;
                case "BACK":
                    Screen.Back();
                    break;
                case "CAN":
                case "CANCEL":
                    // todo check for cancel event
                    Mem.SetNum(MemPos.progline, 2, 0);
                    break;
                case "CLEAR":
                    Screen.Clear();
                    break;
                case "CLOSETFA":
                    Console.WriteLine("### closetfa"); // todo
                    break;
                case "CLOSEVOLUME":
                    Console.WriteLine("### closevolume"); // todo
                    break;
                case "CONVERT":
                    Console.WriteLine("### convert"); // todo
                    break;
                case "CR":
                    Screen.CursorAt(-1, 0);
                    break;
                case "CURSORAT":
                    NumExpr y = BuildNumericExpression();
                    CheckToken(",");
                    NumExpr x = BuildNumericExpression();
                    //long y = GetNumericValue();
                    //if (_tokens[_tokenNum++] != ",")
                    //{
                    //    Console.WriteLine("invalid CURSORAT format");
                    //    break;
                    //}
                    //long x = GetNumericValue();
                    Screen.CursorAt(y.GetValue(), x.GetValue());
                    break;
                case "DCH":
                    Console.WriteLine("### dch");
                    break;
                case "DISPLAY":
                    tempAlpha = GetAlphaExpression();
                    Screen.Display(tempAlpha);
                    break;
                case "ENTERALPHA":
                    if (_tokens[_tokenNum++] != "(")
                    {
                        Console.WriteLine("invalid ENTERALPHA format");
                        break;
                    }
                    tempNum = GetNumericExpression();
                    if (_tokens[_tokenNum++] != ")")
                    {
                        Console.WriteLine("invalid ENTERALPHA format");
                        break;
                    }
                    tempAlpha = Keyboard.GetEnteredString(tempNum);
                    //todo handle entered alpha string
                    break;
                case "ESC":
                case "ESCAPE":
                    // todo check for esc event
                    Mem.SetByte(MemPos.prog, 0);
                    Mem.SetNum(MemPos.progline, 2, 0);
                    break;
                case "GOS":
                    tempNum = GetNumericExpression();
                    GosubStack.Push();
                    Mem.SetNum(MemPos.progline, 2, tempNum);
                    break;
                case "GOSUB":
                    tempNum = GetNumericExpression();
                    GosubStack.Push();
                    Mem.SetByte(MemPos.prog, tempNum);
                    Mem.SetNum(MemPos.progline, 2, 0);
                    break;
                case "GOTO":
                    tempNum = GetNumericExpression();
                    Mem.SetNum(MemPos.progline, 2, tempNum);
                    break;
                case "GRAPHOFF":
                    Screen.SetGraphics(false);
                    break;
                case "GRAPHON":
                    Screen.SetGraphics(true);
                    break;
                case "HOME":
                    Screen.CursorAt(0, 0);
                    break;
                case "INIT":
                    long? ptrPos = MemPos.GetPosBufferPtr(_tokens[_tokenNum]);
                    long? ptrPage = MemPos.GetPosBufferPage(_tokens[_tokenNum]);
                    if (!ptrPos.HasValue || !ptrPage.HasValue)
                    {
                        throw new SystemException("INIT error");
                    }
                    Mem.SetByte(ptrPos.Value + 1, ptrPage.Value);
                    Mem.SetByte(ptrPos.Value, 0);
                    break;
                case "INITFETCH":
                    Console.WriteLine("### initfetch"); // todo
                    break;
                case "KLOCK":
                    Keyboard.KeyLock(true);
                    break;
                case "KFREE":
                    Keyboard.KeyLock(false);
                    break;
                case "LOAD":
                    tempNum = GetNumericExpression();
                    Mem.SetByte(MemPos.prog, tempNum);
                    Mem.SetNum(MemPos.progline, 2, 0);
                    break;
                case "LOCK":
                    Data.LockFlag(true);
                    break;
                case "MERGE":
                    Console.WriteLine("### merge"); // todo
                    break;
                case "MOVE":
                    Console.WriteLine("### move"); // todo
                    break;
                case "NL":
                    if (_tokenNum >= _tokenCount)
                    {
                        tempNum = 1;
                    }
                    else
                    {
                        tempNum = GetNumericExpression();
                    }
                    Screen.NL(tempNum);
                    break;
                case "NOP":
                    break;
                case "PACK":
                    Console.WriteLine("### pack"); // todo
                    break;
                case "PRINTOFF":
                    Mem.SetBool(MemPos.printon, false);
                    break;
                case "PRINTON":
                    Mem.SetBool(MemPos.printon, true);
                    break;
                case "REJECT":
                    Screen.Reject();
                    break;
                case "RESETSCREEN":
                    Screen.Reset();
                    break;
                case "RELEASEDEVICE":
                    Console.WriteLine("### releasedevice"); // todo
                    break;
                case "RETURN":
                    GosubStack.Pop();
                    break;
                case "SPOOL":
                    Console.WriteLine("### spool"); // todo
                    break;
                case "STAY":
                    Screen.SetStay(true);
                    break;
                case "UNLOCK":
                    Data.LockFlag(false);
                    break;
                case "TAB":
                    if (_tokenNum >= _tokenCount)
                    {
                        tempNum = 1;
                    }
                    else
                    {
                        tempNum = GetNumericExpression();
                    }
                    Screen.Tab(tempNum);
                    break;
                case "TABCANCEL":
                    Console.WriteLine("### tabcancel"); // todo
                    break;
                case "TABCLEAR":
                    Console.WriteLine("### tabclear"); // todo
                    break;
                case "TABSET":
                    Console.WriteLine("### tabset"); // todo
                    break;
                case "WHENCANCEL":
                    Console.WriteLine("### whencancel"); // todo
                    break;
                case "WHENERROR":
                    Console.WriteLine("### whenerror"); // todo
                    break;
                case "WHENESCAPE":
                    Console.WriteLine("### whenescape"); // todo
                    break;
                case "WRITEBACK":
                    Data.WriteBack();
                    break;
                case "ZERO":
                    for (int i = 0; i <= 20; i++)
                    {
                        Mem.SetNum(MemPos.nx(i), MemPos.numslotsize, 0);
                    }
                    break;
                default:
                    Console.WriteLine($"Error: Unknown command {_tokens[_tokenNum - 1]}");
                    break;
            }
        }
    }
}
