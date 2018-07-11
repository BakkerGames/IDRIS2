// ilcode.cs - 07/11/2019

using System;
using System.IO;

namespace IDRIS.Runtime
{
    public static class ILCode
    {
        public static string BasePath = "$DRIVE$\\IDRIS\\$ENV$\\PROGRAMS\\DEVICE";

        private static string[][] _ilcode;

        public static void OpenLib(long device, string volume, string library)
        {
            try
            {
                string libPath = $"{BasePath}{device.ToString("00")}\\{volume}\\{library}";
                if (!Directory.Exists(libPath))
                {
                    throw new SystemException($"openlib({device},{volume},{library}) - path not found");
                }
                _ilcode = new string[256][]; // clear library
                foreach (string filepath in Directory.GetFiles(libPath, "*.cvp", SearchOption.TopDirectoryOnly))
                {
                    string filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);
                    int prognum = int.Parse(filename.Substring(0, 3));
                    _ilcode[prognum] = File.ReadAllLines(filepath);
                }
            }
            catch (Exception ex)
            {
                throw new SystemException($"openlib({device},{volume},{library})\r\n{ex.Message}");
            }
        }

        public static string GetLine(int prognum, int linenum)
        {
            if (prognum < 0 || prognum >= 256)
            {
                throw new SystemException($"getline({prognum},{linenum}) - invalid prognum");
            }
            if (linenum < 0)
            {
                throw new SystemException($"getline({prognum},{linenum}) - invalid linenum");
            }
            if (_ilcode[prognum] == null)
            {
                throw new SystemException($"getline({prognum},{linenum}) - program not found");
            }
            if (linenum > _ilcode[prognum].GetUpperBound(0))
            {
                throw new SystemException($"getline({prognum},{linenum}) - line not found");
            }
            return _ilcode[prognum][linenum];
        }
    }
}
