// screen.cs - 07/11/2019

using System;

namespace IDRIS.Runtime
{
    public static partial class Screen
    {
        private const int _height = 24; // doesn't include status bar
        private const int _width = 80;
        private static int[] _screen = new int[_height * _width];
        private static int[] _attrib = new int[_height * _width];
        private static int[] _statusbar = new int[_width];
        private static int _cursorx = 0;
        private static int _cursory = 0;
        private static bool _stay = false;

        public static void Reset()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _screen[y * _width + x] = 32;
                    _attrib[y * _width + x] = -1;
                }
            }
            CursorAt(0, 0);
        }

        public static void Clear()
        {
            int lastAtt = 0;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_attrib[y * _width + x] >= 0)
                    {
                        lastAtt = _attrib[y * _width + x];
                    }
                    if (y >= _cursory || (y == _cursory && x >= _cursorx))
                    {
                        if ((lastAtt / 2) % 2 == 0)
                        {
                            _screen[y * _width + x] = 32;
                        }
                    }
                }
            }
        }

        public static void CursorAt(int y, int x)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                throw new SystemException($"setcursor({y},{x}) - invalid position");
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
                _stay = false;
                return;
            }
            // todo tab to next unprotected spot
            bool foundSpot = false;
            for (int y = _cursory; y < _height; y++)
            {
                if (foundSpot)
                {
                    break;
                }
                for (int x = 0; x < _width; x++)
                {
                    if (y == _cursory && x <= _cursorx)
                    {
                        continue;
                    }
                    if (_attrib[y * _width + x] >= 0)
                    {
                        if ((_attrib[y * _width + x] / 2) % 2 == 0)
                        {
                            _cursory = y;
                            _cursorx = x;
                            IncrementCursor();
                            foundSpot = true;
                            break;
                        }
                    }
                }
            }
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
