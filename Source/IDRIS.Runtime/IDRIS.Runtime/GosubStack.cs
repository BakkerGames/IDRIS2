// GosubStack.cs - 07/13/2018

using System;
using System.Collections;

namespace IDRIS.Runtime
{
    public static partial class GosubStack
    {
        private static Stack _gosubstack = new Stack();

        public static void Push()
        {
            StackItem item = new StackItem();
            item.prog = Mem.GetByte(MemPos.prog);
            item.progline = Mem.GetNum(MemPos.progline, 2);
            _gosubstack.Push(item);
        }

        public static void Pop()
        {
            if (_gosubstack.Count < 1)
            {
                throw new SystemException($"gosub stack underflow");
            }
            StackItem item = (StackItem)_gosubstack.Pop();
            Mem.SetByte(MemPos.prog, item.prog);
            Mem.SetNum(MemPos.progline, 2, item.progline);
            Mem.SetByte(MemPos.progtoken, 0);
        }
    }

    internal class StackItem
    {
        public long prog;
        public long progline;
    }

}
