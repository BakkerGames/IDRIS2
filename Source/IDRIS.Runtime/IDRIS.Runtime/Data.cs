// data.cs - 09/28/2018

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
            // todo Read()
            return false;
        }

        public static bool ReadKey()
        {
            // todo ReadKey()
            return false;
        }

        public static bool ReadRec()
        {
            // todo ReadRec()
            return false;
        }

        public static bool Write()
        {
            // todo Write()
            return false;
        }

        public static bool WriteRec()
        {
            // todo WriteRec()
            return false;
        }

        public static void WriteBack()
        {
            // todo WriteBack()
        }
    }
}
