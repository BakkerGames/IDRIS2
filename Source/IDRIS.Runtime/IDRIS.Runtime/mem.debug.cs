// mem.debug.cs - 07/11/2018

using System;
using System.Text;

namespace IDRIS.Runtime
{
    public static partial class Mem
    {
        public static string GetPage(long pagenum)
        {
            if (pagenum < 0 || pagenum >= MemPos.totalpagecount)
            {
                throw new SystemException($"getpage({pagenum}) - out of bounds");
            }
            StringBuilder result = new StringBuilder();
            byte c;
            for (int y = 0; y < 16; y++)
            {
                result.Append(pagenum.ToString("x2"));
                result.Append((y * 16).ToString("x2"));
                result.Append("  ");
                for (int x = 0; x < 16; x++)
                {
                    result.Append(_mem[(pagenum * 256) + (y * 16) + x].ToString("x2"));
                    result.Append(" ");
                }
                result.Append(" ");
                for (int x = 0; x < 16; x++)
                {
                    c = _mem[(pagenum * 256) + (y * 16) + x];
                    if (c >= 128)
                    {
                        c -= 128;
                    }
                    if (c < 32 || c > 126)
                    {
                        c = (byte)'.';
                    }
                    result.Append((char)c);
                }
                if (y < 15)
                {
                    result.AppendLine();
                }
            }
            return result.ToString();
        }
    }
}
