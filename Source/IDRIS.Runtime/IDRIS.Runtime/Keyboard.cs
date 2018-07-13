// Keyboard.cs - 07/13/2018

namespace IDRIS.Runtime
{
    public static partial class Keyboard
    {
        private static bool _klock = false;

        public static void KeyLock(bool value)
        {
            _klock = value;
        }
    }
}
