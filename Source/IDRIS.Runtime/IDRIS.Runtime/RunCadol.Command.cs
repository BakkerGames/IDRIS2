// RunCadol.Command.cs - 07/14/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class RunCadol
    {
        private static void ExecuteCommand()
        {
            long tempValue;
            switch (_tokens[_tokenNum++])
            {
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
                    // todo
                    break;
                case "CLOSEVOLUME":
                    // todo
                    break;
                case "CONVERT":
                    Console.WriteLine("converting"); // todo handle convert
                    break;
                case "CR":
                    Screen.CursorAt(-1, 0);
                    break;
                case "ESC":
                case "ESCAPE":
                    // todo check for esc event
                    Mem.SetByte(MemPos.prog, 0);
                    Mem.SetNum(MemPos.progline, 2, 0);
                    break;
                case "GOS":
                    tempValue = GetNumericExpression();
                    GosubStack.Push();
                    Mem.SetNum(MemPos.progline, 2, tempValue);
                    break;
                case "GOSUB":
                    tempValue = GetNumericExpression();
                    GosubStack.Push();
                    Mem.SetByte(MemPos.prog, tempValue);
                    Mem.SetNum(MemPos.progline, 2, 0);
                    break;
                case "GOTO":
                    tempValue = GetNumericExpression();
                    Mem.SetNum(MemPos.progline, 2, tempValue);
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
                    // todo
                    break;
                case "KLOCK":
                    Keyboard.KeyLock(true);
                    break;
                case "KFREE":
                    Keyboard.KeyLock(false);
                    break;
                case "LOAD":
                    tempValue = GetNumericExpression();
                    Mem.SetByte(MemPos.prog, tempValue);
                    Mem.SetNum(MemPos.progline, 2, 0);
                    break;
                case "LOCK":
                    Data.LockFlag(true);
                    break;
                case "MERGE":
                    // todo
                    break;
                case "MOVE":
                    // todo handle move
                    Console.WriteLine("moving");
                    break;
                case "NL":
                    if (_tokenNum >= _tokenCount)
                    {
                        tempValue = 1;
                    }
                    else
                    {
                        tempValue = GetNumericExpression();
                    }
                    Screen.NL(tempValue);
                    break;
                case "NOP":
                    break;
                case "PACK":
                    Console.WriteLine("packing"); // todo handle pack
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
                    // todo
                    break;
                case "RETURN":
                    GosubStack.Pop();
                    break;
                case "SPOOL":
                    // todo handle spool
                    Console.WriteLine("spooling");
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
                        tempValue = 1;
                    }
                    else
                    {
                        tempValue = GetNumericExpression();
                    }
                    Screen.Tab(tempValue);
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
                case "WHENCANCEL":
                    Console.WriteLine("whencancel"); // todo handle whencancel
                    break;
                case "WHENERROR":
                    Console.WriteLine("whenerror"); // todo handle whenerror
                    break;
                case "WHENESCAPE":
                    Console.WriteLine("whenescape"); // todo handle whenescape
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
                    Console.WriteLine($"Error: Unknown command {_tokens[_tokenNum]}");
                    break;
            }
        }
    }
}
