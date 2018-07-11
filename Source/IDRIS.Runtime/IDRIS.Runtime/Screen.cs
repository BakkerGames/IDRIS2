// screen.cs - 07/11/2019

using System;

namespace IDRIS.Runtime
{
    public partial class Screen
    {
        private const int _height = 24;
        private const int _width = 80;
        private static int[] _screen = new int[_height * _width];
        private static int[] _attrib = new int[_height * _width];
        private static int _cursorx = 0;
        private static int _cursory = 0;
        private static bool _stay = false;

        public static void Reset()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _screen[(y * _width) + x] = 32;
                    _attrib[(y * _width) + x] = -1;
                }
            }
            SetCursor(0, 0);
        }

        public static void Clear()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (y >= _cursory || (y == _cursory && x >= _cursorx))
                    {
                        if (((_attrib[y * _width + x] / 2) & 2) == 0)
                        {
                            _screen[y * _width + x] = 32;
                        }
                    }
                }
            }
        }

        public static void SetCursor(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                throw new SystemException($"setcursor({x},{y}) - invalid position");
            }
            _cursorx = x;
            _cursory = y;
        }

        public static void Display(string value)
        {
            int c;
            for (int i = 0; i < value.Length; i++)
            {
                c = value[i];
                c = Functions.CharToAscii(c);
                _screen[_cursory * _width + _cursorx] = (byte)c;
                _attrib[_cursory * _width + _cursorx] = -1;
                IncrementCursor();
            }
        }

        public static void Tab()
        {
            if (_stay)
            {
                return;
            }
            // todo tab to next unprotected spot
        }

        public static void SetAttrib(int value)
        {
            if (value < 0 || value > 16 || value % 2 != 0)
            {
                throw new SystemException($"setattrib({value}) - invalid attribute");
            }
            _attrib[_cursory * _width + _cursorx] = value;
            IncrementCursor();
        }

        public static void Stay()
        {
            _stay = true;
        }
    }
}
