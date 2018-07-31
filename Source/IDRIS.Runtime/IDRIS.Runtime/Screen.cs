// screen.cs - 07/31/2019

using System;

namespace IDRIS.Runtime
{
    public static partial class Screen
    {
        private const long _height = 24; // doesn't include status bar
        private const long _width = 80;
        private static int[] _screen = new int[_height * _width];
        private static int[] _attrib = new int[_height * _width];
        private static int[] _statusbar = new int[_width];
        private static long _cursorx = 0;
        private static long _cursory = 0;
        private static bool _graphics = false;

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
            _graphics = false;
            SetStay(false);
            CursorAt(0, 0);
        }

        internal static void Back()
        {
            throw new NotImplementedException();
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

        public static void CursorAt(long y, long x)
        {
            if (x == -1)
            {
                x = _cursorx;
            }
            if (y == -1)
            {
                y = _cursory;
            }
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                throw new SystemException($"setcursor({y},{x}) - invalid position");
            }
            _cursorx = x;
            _cursory = y;
            SetStay(true);
        }

        public static void Display(string value)
        {
            int c;
            for (int i = 0; i < value.Length; i++)
            {
                c = value[i];
                c = Functions.CharToAscii(c);
                if (_graphics)
                {
                    _screen[_cursory * _width + _cursorx] = '+'; // todo real graphics char
                }
                else
                {
                    _screen[_cursory * _width + _cursorx] = (byte)c;
                }
                _attrib[_cursory * _width + _cursorx] = -1;
                IncrementCursor();
            }
        }

        public static void Tab(long value)
        {
            for (long l = 0; l < value; l++)
            {
                Tab();
            }
        }

        public static void Tab()
        {
            if (IsStay())
            {
                SetStay(false);
                return;
            }
            // todo tab to next unprotected spot
            bool foundSpot = false;
            for (long y = _cursory; y < _height; y++)
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
            if (!foundSpot) // move to end of screen
            {
                CursorAt(_height, _width);
            }
        }

        internal static void Reject()
        {
            if (Mem.GetBool(MemPos.background) || Mem.GetBool(MemPos.printon))
            {
                return;
            }
            throw new NotImplementedException();
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

        public static bool IsStay()
        {
            return (Mem.GetByte(MemPos.charval) == 255);
        }

        public static void SetStay(bool value)
        {
            if (value)
            {
                Mem.SetByte(MemPos.charval, 255);
            }
            else
            {
                Mem.SetByte(MemPos.charval, 0);
            }
        }

        public static void SetGraphics(bool value)
        {
            _graphics = value;
        }

        public static void NL(long value)
        {
            for (long l = 0; l < value; l++)
            {
                NL();
            }
        }

        public static void NL()
        {
            CursorAt(-1, _width - 1);
            IncrementCursor();
        }
    }
}
