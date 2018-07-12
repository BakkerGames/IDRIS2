// Screen.Debug.cs - 07/11/2018

using System.Text;

namespace IDRIS.Runtime
{
    public static partial class Screen
    {
        public static new string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int y = 0; y < _height; y++)
            {
                result.Append(y.ToString("00 "));
                for (int x = 0; x < _width; x++)
                {
                    if (y == _cursory && x == _cursorx)
                    {
                        result.Append("#");
                    }
                    else if (_attrib[y * _width + x] < 0)
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
                    else if ((_attrib[y * _width + x] / 2) % 2 == 0)
                    {
                        result.Append('U');
                    }
                    else
                    {
                        result.Append('P');
                    }
                }
                result.AppendLine();
            }
            result.Append("24 "); // status line
            for (int x = 0; x < _width; x++)
            {
                if (_statusbar[x] <= 32)
                {
                    result.Append('_');
                }
                else
                {
                    result.Append((char)_statusbar[x]);
                }
            }
            return result.ToString();
        }
    }
}
