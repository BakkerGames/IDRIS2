// Screen.Debug.cs - 07/11/2018

using System.Text;

namespace IDRIS.Runtime
{
    public partial class Screen
    {
        public static new string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    result.Append((char)_screen[y*_height + x]);
                }
                if (y < _height - 1)
                {
                    result.AppendLine();
                }
            }
            return result.ToString();
        }
    }
}
