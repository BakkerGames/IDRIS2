// Functions.cs - 07/11/2018

namespace IDRIS.Runtime
{
    public static class Functions
    {
        public static int CharToAscii(int c)
        {
            c = AccentedToAscii(c);
            if (c < 32 || c > 126)
            {
                return '?';
            }
            return c;
        }

        public static int AccentedToAscii(int c)
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
