// mem.alpha.cs - 07/11/2018

using System;
using System.Text;

namespace IDRIS.Runtime
{
    public static partial class Mem
    {
        public static string GetAlpha(long pos)
        {
            if (pos < 0 || pos >= MemPos.totalmemsize)
            {
                throw new SystemException($"getalpha({pos}) - out of bounds");
            }
            if (_mem[pos] == 0) // very common!
            {
                return "";
            }
            StringBuilder result = new StringBuilder();
            long offset = 0;
            int origChar;
            int c;
            do
            {
                if (pos + offset >= MemPos.totalmemsize)
                {
                    throw new SystemException($"getalpha({pos}+{offset}) - out of bounds");
                }
                if (result.Length >= 256)
                {
                    throw new SystemException($"getalpha({pos}) - string too long");
                }
                origChar = _mem[pos + offset];
                c = origChar;
                if (c >= 128)
                {
                    c -= 128;
                }
                if (c < 32 || c > 126)
                {
                    c = '?';
                }
                result.Append((char)c);
                offset++;
            } while (origChar >= 128);
            return result.ToString();
        }

        public static void SetAlpha(long pos, string value)
        {
            if (pos < 0 || pos >= MemPos.totalmemsize)
            {
                throw new SystemException($"setalpha({pos}) - out of bounds");
            }
            if (string.IsNullOrEmpty(value)) // very common!
            {
                _mem[pos] = 0;
                return;
            }
            int c;
            int offset = 0;
            while (offset < value.Length - 1)
            {
                c = Functions.CharToAscii(value[offset]);
                _mem[pos + offset] = (byte)(c + 128);
                offset++;
            }
            c = Functions.CharToAscii(value[offset]);
            _mem[pos + offset] = (byte)c;
        }

        public static void SpoolAlpha(long pos, long len, string value)
        {
            if (len < 0 || len > 256)
            {
                throw new SystemException($"spoolalpha({pos},{len}) - invalid len");
            }
            if (pos < 0 || pos + len - 1 >= MemPos.totalmemsize)
            {
                throw new SystemException($"spoolalpha({pos},{len}) - out of bounds");
            }
            int c;
            int offset = 0;
            if (!string.IsNullOrEmpty(value))
            {
                while (offset < len && offset < value.Length)
                {
                    c = Functions.CharToAscii(value[offset]);
                    _mem[pos + offset] = (byte)(c + 128);
                    offset++;
                }
            }
            while (offset < len)
            {
                _mem[pos + offset] = (' ' + 128);
                offset++;
            }
        }

        public static string PackAlpha(long pos, long len)
        {
            if (len < 0 || len > 256)
            {
                throw new SystemException($"packalpha({pos},{len}) - invalid len");
            }
            if (pos < 0 || pos + len - 1 >= MemPos.totalmemsize)
            {
                throw new SystemException($"packalpha({pos},{len}) - out of bounds");
            }
            StringBuilder result = new StringBuilder();
            int offset = 0;
            int origChar = 128;
            int c;
            while (offset < len && origChar >= 128)
            {
                origChar = _mem[pos + offset];
                c = origChar;
                if (c == 0)
                {
                    c = ' ';
                }
                if (c >= 128)
                {
                    c -= 128;
                }
                if (c < 32 || c > 126)
                {
                    c = '?';
                }
                result.Append((char)c);
                offset++;
            }
            return result.ToString().TrimEnd();
        }
    }
}
