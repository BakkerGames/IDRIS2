// Keyboard.cs - 07/13/2018

using System;
using System.Collections;

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
            if (_kbd.Count <=0)
            {
                throw new SystemException("keyboard buffer underflow");
            }
            return (char)_kbd.Dequeue();
        }
    }
}
