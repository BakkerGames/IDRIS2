// mem.alpha.cs - 07/11/2018

using System;
using System.Text;

namespace IDRIS.Runtime
{
    public static partial class mem
    {
        public static string getalpha(long pos)
        {
            if (pos < 0 || pos >= mempos.totalmemsize)
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
                if (pos + offset >= mempos.totalmemsize)
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

        public static void setalpha(long pos, string value)
        {
            if (pos < 0 || pos >= mempos.totalmemsize)
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
                c = AccentedToAscii(value[offset]);
                if (c < 32 || c > 126)
                {
                    c = '?';
                }
                _mem[pos + offset] = (byte)(c + 128);
                offset++;
            }
            c = AccentedToAscii(value[offset]);
            if (c < 32 || c > 126)
            {
                c = '?';
            }
            _mem[pos + offset] = (byte)c;
        }

        public static void spoolalpha(long pos, long len, string value)
        {
            if (len < 0 || len > 256)
            {
                throw new SystemException($"spoolalpha({pos},{len}) - invalid len");
            }
            if (pos < 0 || pos + len - 1 >= mempos.totalmemsize)
            {
                throw new SystemException($"spoolalpha({pos},{len}) - out of bounds");
            }
            int c;
            int offset = 0;
            if (!string.IsNullOrEmpty(value))
            {
                while (offset < len && offset < value.Length)
                {
                    c = AccentedToAscii(value[offset]);
                    if (c < 32 || c > 126)
                    {
                        c = '?';
                    }
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

        public static string packalpha(long pos, long len)
        {
            if (len < 0 || len > 256)
            {
                throw new SystemException($"packalpha({pos},{len}) - invalid len");
            }
            if (pos < 0 || pos + len - 1 >= mempos.totalmemsize)
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

        private static int AccentedToAscii(int c)
        {
            switch (c)
            {
                case 'À': return 'A';
                case 'Á': return 'A';
                case 'Â': return 'A';
                case 'Ã': return 'A';
                case 'Ä': return 'A';
                case 'Å': return 'A';
                case 'à': return 'a';
                case 'á': return 'a';
                case 'â': return 'a';
                case 'ã': return 'a';
                case 'ä': return 'a';
                case 'å': return 'a';
                case 'Ç': return 'C';
                case 'ç': return 'c';
                case 'È': return 'E';
                case 'É': return 'E';
                case 'Ê': return 'E';
                case 'Ë': return 'E';
                case 'è': return 'e';
                case 'é': return 'e';
                case 'ê': return 'e';
                case 'ë': return 'e';
                case 'Ì': return 'I';
                case 'Í': return 'I';
                case 'Î': return 'I';
                case 'Ï': return 'I';
                case 'ì': return 'i';
                case 'í': return 'i';
                case 'î': return 'i';
                case 'ï': return 'i';
                case 'Ñ': return 'N';
                case 'ñ': return 'n';
                case 'Ò': return 'O';
                case 'Ó': return 'O';
                case 'Ô': return 'O';
                case 'Õ': return 'O';
                case 'Ö': return 'O';
                case 'Ø': return 'O';
                case 'ò': return 'o';
                case 'ó': return 'o';
                case 'ô': return 'o';
                case 'õ': return 'o';
                case 'ö': return 'o';
                case 'ø': return 'o';
                case 'Š': return 'S';
                case 'š': return 's';
                case 'Ù': return 'U';
                case 'Ú': return 'U';
                case 'Û': return 'U';
                case 'Ü': return 'U';
                case 'ù': return 'u';
                case 'ú': return 'u';
                case 'û': return 'u';
                case 'ü': return 'u';
                case 'Ÿ': return 'Y';
                case 'Ý': return 'Y';
                case 'ÿ': return 'y';
                case 'ý': return 'y';
                case 'Ž': return 'Z';
                case 'ž': return 'z';
            }
            return c;
        }
    }
}
