// Keyboard.cs - 09/28/2018

using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace IDRIS.Runtime
{
    public static partial class Keyboard
    {
        private static Queue _kbd = new Queue();
        private static bool _klock = false;

        public static void KeyLock(bool value)
        {
            _klock = value;
        }

        public static void AddChar(char c)
        {
            _kbd.Enqueue(c);
        }

        public static char GetChar()
        {
            while (_kbd.Count == 0)
            {
                Application.DoEvents();
            }
            return (char)_kbd.Dequeue();
        }

        public static string GetEnteredString(long maxLen)
        {
            StringBuilder result = new StringBuilder();
            char c;
            do
            {
                c = GetChar();
                switch ((int)c)
                {
                    case 13: // enter
                        break;
                    case 8: // backspace
                        if (result.Length > 0)
                        {
                            result.Length--;
                            Screen.BackSpace();
                        }
                        break;
                    default:
                        if (result.Length < maxLen)
                        {
                            result.Append(c);
                            Screen.Display(c.ToString());
                        }
                        break;
                }
            } while (c != 13);
            return result.ToString();
        }
    }
}
