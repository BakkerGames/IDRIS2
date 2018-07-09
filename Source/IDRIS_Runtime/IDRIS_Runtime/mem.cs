// mem.cs - 07/09/2018

using System;

namespace IDRIS_Runtime
{
    public static partial class mem
    {
        // --- Memory block ---

        private static byte[] _mem = new byte[mempos.totalmemsize];

        public static long sizemax(long size)
        {
            switch (size)
            {
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
                case 1: return 128L;
                case 2: return 32768L;
                case 3: return 8388608L;
                case 4: return 2147483648L;
                case 5: return 549755813888L;
                case 6: return 140737488355328L;
            }
            throw new ArgumentOutOfRangeException($"halfsizemax({size})");
        }

        public static long getnum(long pos, long size)
        {
            if (pos < 0 || pos + size >= mempos.totalmemsize || size < 1 || size > mempos.numslotsize)
            {
                throw new ArgumentOutOfRangeException($"getnum({pos},{size})");
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

        public static void setnum(long pos, long size, long value)
        {
            if (pos < 0 || pos + size >= mempos.totalmemsize || size < 1 || size > mempos.numslotsize)
            {
                throw new ArgumentOutOfRangeException($"setnum({pos},{size})");
            }
            long tempValue = value;
            if (size == 1)
            {
                if (tempValue < -sizemax(1) || tempValue > sizemax(1))
                {
                    if ((_mem[mempos.privg] & 2) == 0) // not set
                    {
                        throw new ArgumentOutOfRangeException($"setnum({pos},{size},{tempValue})");
                    }
                }
                if (tempValue < 0 || tempValue >= sizemax(1))
                {
                    tempValue = tempValue % sizemax(1);
                }
                _mem[pos] = (byte)tempValue;
            }
            else
            {
                if (tempValue < -halfsizemax(size) || tempValue >= halfsizemax(size))
                {
                    if ((_mem[mempos.privg] & 2) == 0) // not set
                    {
                        throw new ArgumentOutOfRangeException($"setnum({pos},{size},{tempValue})");
                    }
                    tempValue -= (tempValue / sizemax(size)) * sizemax(size);
                }
                else if (tempValue < 0)
                {
                    tempValue += sizemax(size);
                }
                for (int i = 0; i < size; i++)
                {
                    byte tempByte = (byte)(tempValue / sizemax(size - i - 1));
                    _mem[pos + i] = tempByte;
                    tempValue -= (tempByte * sizemax(size - i - 1));
                }
            }
        }
    }
}
