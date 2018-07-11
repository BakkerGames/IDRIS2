// mem.numeric.cs - 07/11/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class mem
    {
        public static long getbyte(long pos)
        {
            if (pos < 0 || pos >= mempos.totalmemsize)
            {
                throw new SystemException($"getbyte({pos}) - out of bounds");
            }
            long result = _mem[pos];
            if (result < 0)
            {
                result += 256;
            }
            return result;
        }

        public static long getnum(long pos, long size)
        {
            if (pos < 0 || pos + size - 1 >= mempos.totalmemsize || size < 1 || size > mempos.numslotsize)
            {
                throw new SystemException($"getnum({pos},{size}) - out of bounds");
            }
            long result = _mem[pos];
            for (long i = 1; i < size; i++)
            {
                result = (result * 256) + _mem[pos + i];
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
                throw new SystemException($"setbyte({pos}) - out of bounds");
            }
            if (value < -256 || value > 256)
            {
                if ((_mem[mempos.privg] & 2) == 0) // not set
                {
                    throw new SystemException($"setbyte({pos},{value}) - numeric overflow");
                }
            }
            if (value < 0 || value >= 256)
            {
                _mem[pos] = (byte)(value % 256);
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
                throw new SystemException($"setnum({pos},{size}) - out of bounds");
            }
            if (value < -halfsizemax(size) || value >= halfsizemax(size))
            {
                if ((_mem[mempos.privg] & 2) == 0) // not set
                {
                    throw new SystemException($"setnum({pos},{size},{value}) - numeric overflow");
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
    }
}
