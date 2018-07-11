// Screen.Debug.cs - 07/11/2018

using System.Text;

namespace IDRIS.Runtime
{
    public partial class Screen
    {
        public static new string ToString()
        {
            return ToString(false);
        }

        public static string ToString(bool showAttrib)
        {
            StringBuilder result = new StringBuilder();
            for (int y = 0; y < _height; y++)
            {
                result.Append(y.ToString("00 "));
                for (int x = 0; x < _width; x++)
                {
                    if (_screen[y * _width + x] <= 32)
                    {
                        result.Append('.');
                    }
                    else
                    {
                        result.Append((char)_screen[y * _width + x]);
                    }
                }
                result.AppendLine();
            }
            result.AppendLine();
            if (showAttrib)
            {
                for (int y = 0; y < _height; y++)
                {
                    result.Append(y.ToString("00 "));
                    for (int x = 0; x < _width; x++)
                    {
                        if (_attrib[y * _width + x] < 0)
                        {
                            result.Append('.');
                        }
                        else
                        {
                            result.Append('@');
                        }
                    }
                    result.AppendLine();
                }
                result.AppendLine();
            }
            result.Append($"Cursor at {_cursorx},{_cursory}");
            return result.ToString();
        }
    }
}
