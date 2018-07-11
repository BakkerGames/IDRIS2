// Screen.Internal.cs - 07/11/2018

namespace IDRIS.Runtime
{
    public partial class Screen
    {
        private static void IncrementCursor()
        {
            _cursorx++;
            if (_cursorx >= _width)
            {
                _cursorx = 0;
                _cursory++;
                if (_cursory >= _height)
                {
                    ScrollScreen();
                }
            }
        }

        private static void ScrollScreen()
        {
            for (int y = 1; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _screen[(y - 1) * _width + x] = _screen[y * _width + x];
                    _attrib[(y - 1) * _width + x] = _attrib[y * _width + x];
                }
            }
            for (int x = 0; x < _width; x++)
            {
                _screen[(_height - 1) * _width + x] = 32;
                _attrib[(_height - 1) * _width + x] = -1;
            }
            _cursory--;
        }
    }
}
