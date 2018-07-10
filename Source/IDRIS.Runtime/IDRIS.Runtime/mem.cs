// mem.cs - 07/10/2018

using System;
using System.Text;

namespace IDRIS.Runtime
{
    public static partial class mem
    {
        private static byte[] _mem = new byte[mempos.totalmemsize];

        public static long sizemax(long size)
        {
            switch (size)
            {
                case 0: return 1L;
                case 1: return 256L;
                case 2: return 65536L;
                case 3: return 16777216L;
                case 4: return 4294967296L;
                case 5: return 1099511627776L;
                case 6: return 281474976710656L;
            }
            throw new ArgumentOutOfRangeException($"sizemax({size})");
        }

        public static long halfsizemax(long size)
        {
            switch (size)
            {
                case 0: return 1L;
                case 1: return 128L;
                case 2: return 32768L;
                case 3: return 8388608L;
                case 4: return 2147483648L;
                case 5: return 549755813888L;
                case 6: return 140737488355328L;
            }
            throw new ArgumentOutOfRangeException($"halfsizemax({size})");
        }

        public static long getbyte(long pos)
        {
            if (pos < 0 || pos >= mempos.totalmemsize)
            {
                throw new ArgumentOutOfRangeException($"getbyte({pos})");
            }
            long result = _mem[pos];
            if (result < 0)
            {
                result += sizemax(1);
            }
            return result;
        }

        public static long getnum(long pos, long size)
        {
            if (pos < 0 || pos + size - 1 >= mempos.totalmemsize || size < 1 || size > mempos.numslotsize)
            {
                throw new ArgumentOutOfRangeException($"getnum({pos},{size})");
            }
            long result = _mem[pos];
            for (long i = 1; i < size; i++)
            {
                result = (result * sizemax(1)) + _mem[pos + i];
            }
            if (result >= halfsizemax(size))
            {
                result -= sizemax(size);
            }
            return result;
        }

        public static void setbyte(long pos, long value)
        {
            if (pos < 0 || pos >= mempos.totalmemsize)
            {
                throw new ArgumentOutOfRangeException($"setbyte({pos})");
            }
            if (value < -sizemax(1) || value > sizemax(1))
            {
                if ((_mem[mempos.privg] & 2) == 0) // not set
                {
                    throw new ArgumentOutOfRangeException($"setbyte({pos},{value})");
                }
            }
            if (value < 0 || value >= sizemax(1))
            {
                _mem[pos] = (byte)(value % sizemax(1));
            }
            else
            {
                _mem[pos] = (byte)value;
            }
        }

        public static void setnum(long pos, long size, long value)
        {
            if (pos < 0 || pos + size - 1 >= mempos.totalmemsize || size < 1 || size > mempos.numslotsize)
            {
                throw new ArgumentOutOfRangeException($"setnum({pos},{size})");
            }
            if (value < -halfsizemax(size) || value >= halfsizemax(size))
            {
                if ((_mem[mempos.privg] & 2) == 0) // not set
                {
                    throw new ArgumentOutOfRangeException($"setnum({pos},{size},{value})");
                }
                value -= (value / sizemax(size)) * sizemax(size);
            }
            else if (value < 0)
            {
                value += sizemax(size);
            }
            for (int i = 0; i < size; i++)
            {
                byte tempByte = (byte)(value / sizemax(size - i - 1));
                _mem[pos + i] = tempByte;
                value -= (tempByte * sizemax(size - i - 1));
            }
        }

        public static string getpage(long pagenum)
        {
            if (pagenum < 0 || pagenum >= mempos.totalpagecount)
            {
                throw new ArgumentOutOfRangeException($"getpage({pagenum})");
            }
            StringBuilder result = new StringBuilder();
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    result.Append(_mem[(pagenum * 256) + (y * 16) + x].ToString("x2"));
                    if (x < 15)
                    {
                        result.Append(" ");
                    }
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
