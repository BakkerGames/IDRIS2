// Mem.Buffer.cs - 07/11/2018

using System;

namespace IDRIS.Runtime
{
    public static partial class Mem
    {
        // ptr parameter equals rp, zp, etc.
        // ptr+1 = rp2, zp2, etc.

        public static long GetNumBuffer(long ptr, long size)
        {
            CheckBufferPtr(ptr);
            long pos = GetByte(ptr + 1) * 256 + GetByte(ptr);
            long result = GetNum(pos, size);
            pos += size;
            SetByte(ptr + 1, pos / 256);
            SetByte(ptr, pos % 256);
            return result;
        }

        public static void SetNumBuffer(long ptr, long size, long value)
        {
            CheckBufferPtr(ptr);
            long pos = GetByte(ptr + 1) * 256 + GetByte(ptr);
            SetNum(pos, size, value);
            pos += size;
            SetByte(ptr + 1, pos / 256);
            SetByte(ptr, pos % 256);
        }

        public static string GetAlphaBuffer(long ptr)
        {
            CheckBufferPtr(ptr);
            long pos = GetByte(ptr + 1) * 256 + GetByte(ptr);
            string result = GetAlpha(pos);
            if (result == "")
            {
                pos += 1;
            }
            else
            {
                pos += result.Length;
            }
            SetByte(ptr + 1, pos / 256);
            SetByte(ptr, pos % 256);
            return result;
        }

        public static void SetAlphaBuffer(long ptr, string value)
        {
            CheckBufferPtr(ptr);
            long pos = GetByte(ptr + 1) * 256 + GetByte(ptr);
            SetAlpha(pos, value);
            if (value == "")
            {
                pos += 1;
            }
            else
            {
                pos += value.Length;
            }
            SetByte(ptr + 1, pos / 256);
            SetByte(ptr, pos % 256);
        }

        private static void CheckBufferPtr(long ptr)
        {
            switch (ptr)
            {
                case MemPos.rp:
                case MemPos.zp:
                case MemPos.xp:
                case MemPos.yp:
                case MemPos.wp:
                case MemPos.sp:
                case MemPos.tp:
                case MemPos.up:
                case MemPos.vp:
                    break;
                default:
                    throw new SystemException($"checkbufferptr({ptr}) - unknown buffer ptr");
            }
        }
    }
}
