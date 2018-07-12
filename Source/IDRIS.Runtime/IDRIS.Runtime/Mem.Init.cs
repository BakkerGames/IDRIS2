// Mem.Init.cs - 07/12/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class Mem
    {
        static Mem()
        {
            _mem[MemPos.machtype] = 21; // IDRIS2
            _mem[MemPos.sysrel] = 5; // release 5
            _mem[MemPos.sysrev] = 2; // version 2

            _mem[MemPos.rp] = 0;
            _mem[MemPos.rp2] = MemPos.rpage;
            _mem[MemPos.irp] = 0;
            _mem[MemPos.irp2] = MemPos.rpage;

            _mem[MemPos.zp] = 0;
            _mem[MemPos.zp2] = MemPos.zpage;
            _mem[MemPos.izp] = 0;
            _mem[MemPos.izp2] = MemPos.zpage;

            _mem[MemPos.xp] = 0;
            _mem[MemPos.xp2] = MemPos.xpage;
            _mem[MemPos.ixp] = 0;
            _mem[MemPos.ixp2] = MemPos.xpage;

            _mem[MemPos.yp] = 0;
            _mem[MemPos.yp2] = MemPos.ypage;
            _mem[MemPos.iyp] = 0;
            _mem[MemPos.iyp2] = MemPos.ypage;

            _mem[MemPos.wp] = 0;
            _mem[MemPos.wp2] = MemPos.wpage;
            _mem[MemPos.iwp] = 0;
            _mem[MemPos.iwp2] = MemPos.wpage;

            _mem[MemPos.sp] = 0;
            _mem[MemPos.sp2] = MemPos.spage;
            _mem[MemPos.isp] = 0;
            _mem[MemPos.isp2] = MemPos.spage;

            _mem[MemPos.tp] = 0;
            _mem[MemPos.tp2] = MemPos.tpage;
            _mem[MemPos.itp] = 0;
            _mem[MemPos.itp2] = MemPos.tpage;

            _mem[MemPos.up] = 0;
            _mem[MemPos.up2] = MemPos.upage;
            _mem[MemPos.iup] = 0;
            _mem[MemPos.iup2] = MemPos.upage;

            _mem[MemPos.vp] = 0;
            _mem[MemPos.vp2] = MemPos.vpage;
            _mem[MemPos.ivp] = 0;
            _mem[MemPos.ivp2] = MemPos.vpage;

            SetNum(MemPos.g, 2, 255); // ready for change
            SetByte(MemPos.printdev, 255); // no printer assigned

            SetAlpha(MemPos.date, DateTime.Today.ToString("MM/dd/yyyy"));
            SetNum(MemPos.date + 10, 4, int.Parse(DateTime.Today.ToString("yyyyMMdd")));
        }
    }
}
