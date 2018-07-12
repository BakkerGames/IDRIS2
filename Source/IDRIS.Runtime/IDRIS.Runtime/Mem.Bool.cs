// Mem.Bool.cs - 07/12/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class Mem
    {
        public static bool GetBool(long pos)
        {
            if (pos < 0 || pos >= MemPos.totalmemsize)
            {
                throw new SystemException($"getbool({pos}) - out of bounds");
            }
            if (_mem[pos] == 0) // falseval
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void SetBool(long pos, bool value)
        {
            if (pos < 0 || pos >= MemPos.totalmemsize)
            {
                throw new SystemException($"getbool({pos}) - out of bounds");
            }
            if (value)
            {
                _mem[pos] = 1; // trueval
            }
            else
            {
                _mem[pos] = 0; // falseval
            }
        }
    }
}
