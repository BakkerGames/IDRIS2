// mem.cs - 07/11/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class Mem
    {
        private static byte[] _mem = new byte[MemPos.totalmemsize];

        public static long SizeMax(long size)
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
            throw new SystemException($"sizemax({size}) - invalid size");
        }

        public static long HalfSizeMax(long size)
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
            throw new SystemException($"halfsizemax({size}) - invalid size");
        }

        public static void Move(long frompos, long topos, long len)
        {
            if (len == 0)
            {
                len = 256; // todo make sure this is valid logic
            }
            if (len < 0 || len > 256)
            {
                throw new SystemException($"move({frompos},{topos},{len}) - invalid len");
            }
            if (frompos < 0 || frompos + len - 1 >= MemPos.totalmemsize
                || topos < 0 || topos + len - 1 >= MemPos.totalmemsize)
            {
                throw new SystemException($"move({frompos},{topos},{len}) - out of bounds");
            }
            for (int i = 0; i < len; i++)
            {
                _mem[topos + i] = _mem[frompos + 1];
            }
        }
    }
}
