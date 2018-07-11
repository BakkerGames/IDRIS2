// mem.numeric.cs - 07/11/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class Mem
    {
        public static long GetByte(long pos)
        {
            if (pos < 0 || pos >= MemPos.totalmemsize)
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

        public static long GetNum(long pos, long size)
        {
            if (pos < 0 || pos + size - 1 >= MemPos.totalmemsize || size < 1 || size > MemPos.numslotsize)
            {
                throw new SystemException($"getnum({pos},{size}) - out of bounds");
            }
            long result = _mem[pos];
            for (long i = 1; i < size; i++)
            {
                result = (result * 256) + _mem[pos + i];
            }
            if (result >= HalfSizeMax(size))
            {
                result -= SizeMax(size);
            }
            return result;
        }

        public static void SetByte(long pos, long value)
        {
            if (pos < 0 || pos >= MemPos.totalmemsize)
            {
                throw new SystemException($"setbyte({pos}) - out of bounds");
            }
            if (value < -256 || value > 256)
            {
                if ((_mem[MemPos.privg] & 2) == 0) // not set
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

        public static void SetNum(long pos, long size, long value)
        {
            if (pos < 0 || pos + size - 1 >= MemPos.totalmemsize || size < 1 || size > MemPos.numslotsize)
            {
                throw new SystemException($"setnum({pos},{size}) - out of bounds");
            }
            if (value < -HalfSizeMax(size) || value >= HalfSizeMax(size))
            {
                if ((_mem[MemPos.privg] & 2) == 0) // not set
                {
                    throw new SystemException($"setnum({pos},{size},{value}) - numeric overflow");
                }
                value -= (value / SizeMax(size)) * SizeMax(size);
            }
            else if (value < 0)
            {
                value += SizeMax(size);
            }
            for (int i = 0; i < size; i++)
            {
                byte tempByte = (byte)(value / SizeMax(size - i - 1));
                _mem[pos + i] = tempByte;
                value -= (tempByte * SizeMax(size - i - 1));
            }
        }
    }
}
