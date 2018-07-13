// data.cs - 07/13/2018

namespace IDRIS.Runtime
{
    public static partial class Data
    {
        private static bool _lockflag = false;

        public static void LockFlag(bool value)
        {
            _lockflag = value;
        }

        public static bool Read()
        {
            // todo
            return false;
        }

        public static bool ReadKey()
        {
            // todo
            return false;
        }

        public static bool ReadRec()
        {
            // todo
            return false;
        }

        public static bool Write()
        {
            // todo
            return false;
        }

        public static bool WriteRec()
        {
            // todo
            return false;
        }

        public static void WriteBack()
        {
            // todo
        }
    }
}
