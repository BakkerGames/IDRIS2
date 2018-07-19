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
                    switch (_tokens[_tokenNum])
                    {
                        case "R":
                        case "RP":
                            Mem.SetByte(MemPos.rp2, MemPos.rpage);
                            Mem.SetByte(MemPos.rp, 0);
                            break;
                        case "IR":
                        case "IRP":
                            Mem.SetByte(MemPos.irp2, MemPos.rpage);
                            Mem.SetByte(MemPos.irp, 0);
                            break;
                        case "Z":
                        case "ZP":
                            Mem.SetByte(MemPos.zp2, MemPos.zpage);
                            Mem.SetByte(MemPos.zp, 0);
                            break;
                        case "ZR":
                        case "ZRP":
                            Mem.SetByte(MemPos.izp2, MemPos.zpage);
                            Mem.SetByte(MemPos.izp, 0);
                            break;
                        case "X":
                        case "XP":
                            Mem.SetByte(MemPos.xp2, MemPos.xpage);
                            Mem.SetByte(MemPos.xp, 0);
                            break;
                        case "IX":
                        case "IXP":
                            Mem.SetByte(MemPos.ixp2, MemPos.xpage);
                            Mem.SetByte(MemPos.ixp, 0);
                            break;
                        case "Y":
                        case "YP":
                            Mem.SetByte(MemPos.yp2, MemPos.ypage);
                            Mem.SetByte(MemPos.yp, 0);
                            break;
                        case "IY":
                        case "IYP":
                            Mem.SetByte(MemPos.iyp2, MemPos.ypage);
                            Mem.SetByte(MemPos.iyp, 0);
                            break;
                        case "W":
                        case "WP":
                            Mem.SetByte(MemPos.wp2, MemPos.wpage);
                            Mem.SetByte(MemPos.wp, 0);
                            break;
                        case "IW":
                        case "IWP":
                            Mem.SetByte(MemPos.iwp2, MemPos.wpage);
                            Mem.SetByte(MemPos.iwp, 0);
                            break;
                        case "S":
                        case "SP":
                            Mem.SetByte(MemPos.sp2, MemPos.spage);
                            Mem.SetByte(MemPos.sp, 0);
                            break;
                        case "IS":
                        case "ISP":
                            Mem.SetByte(MemPos.isp2, MemPos.spage);
                            Mem.SetByte(MemPos.isp, 0);
                            break;
                        case "T":
                        case "TP":
                            Mem.SetByte(MemPos.tp2, MemPos.tpage);
                            Mem.SetByte(MemPos.tp, 0);
                            break;
                        case "IT":
                        case "ITP":
                            Mem.SetByte(MemPos.itp2, MemPos.tpage);
                            Mem.SetByte(MemPos.itp, 0);
                            break;
                        case "U":
                        case "UP":
                            Mem.SetByte(MemPos.up2, MemPos.upage);
                            Mem.SetByte(MemPos.up, 0);
                            break;
                        case "IU":
                        case "IUP":
                            Mem.SetByte(MemPos.iup2, MemPos.upage);
                            Mem.SetByte(MemPos.iup, 0);
                            break;
                        case "V":
                        case "VP":
                            Mem.SetByte(MemPos.vp2, MemPos.vpage);
                            Mem.SetByte(MemPos.vp, 0);
                            break;
                        case "IV":
                        case "IVP":
                            Mem.SetByte(MemPos.ivp2, MemPos.vpage);
                            Mem.SetByte(MemPos.ivp, 0);
                            break;
                    }
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
