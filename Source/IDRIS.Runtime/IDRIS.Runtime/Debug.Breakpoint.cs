// Debug.Breakpoint.cs - 07/31/2018

using System.Collections.Generic;
using System.Linq;

namespace IDRIS.Runtime
{
    public static partial class Debug
    {
        private static List<ILBreakpoint> _breakpoints = new List<ILBreakpoint>();
        private static bool _nextLineBreakpoint = false;

        public static void SetBreakpoint(long prognum, long linenum)
        {
            SetBreakpoint(prognum, linenum, false);
        }

        public static void SetBreakpoint(long prognum, long linenum, bool autoclear)
        {
            ILBreakpoint bpoint;
            for (int i = 0; i < _breakpoints.Count(); i++)
            {
                bpoint = _breakpoints[i];
                if (bpoint.prognum == prognum 
                    && bpoint.linenum == linenum 
                    && bpoint.autoclear == autoclear)
                {
                    return; // already exists
                }
            }
            bpoint = new ILBreakpoint();
            bpoint.prognum = prognum;
            bpoint.linenum = linenum;
            bpoint.autoclear = autoclear;
            _breakpoints.Add(bpoint);
        }

        public static void ClearBreakpoint(long prognum, long linenum)
        {
            ILBreakpoint bpoint;
            for (int i = 0; i < _breakpoints.Count(); i++)
            {
                bpoint = _breakpoints[i];
                if (bpoint.prognum == prognum && bpoint.linenum == linenum)
                {
                    _breakpoints.RemoveAt(i);
                    return;
                }
            }
        }

        public static void ClearAllBreakpoints()
        {
            _breakpoints.Clear();
        }

        public static bool HasBreakpoint(long prognum, long linenum)
        {
            bool result = false;
            ILBreakpoint bpoint;
            for (int i = 0; i < _breakpoints.Count(); i++)
            {
                bpoint = _breakpoints[i];
                if (bpoint.prognum == prognum && bpoint.linenum == linenum)
                {
                    if (bpoint.autoclear)
                    {
                        _breakpoints.RemoveAt(i);
                    }
                    result = true;
                    break;
                }
            }
            // could and/or have next line breakpoint
            if (_nextLineBreakpoint)
            {
                _nextLineBreakpoint = false;
                result = true;
            }
            return result;
        }
    }

    public class ILBreakpoint
    {
        public long prognum;
        public long linenum;
        public bool autoclear = false;
    }
}
