using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.Win32.PInvoke
{
    public partial class MemoryAccessor
    {
        public int TargetPointerLength { get; private set; }
        public IntPtr ProcessHwnd { get; private set; }

        public MemoryAccessor(IntPtr hProcess, int targetPointerLength = 4)
        {
            if (new[] { 4, 8 }.Contains(targetPointerLength))
            {
                ProcessHwnd = hProcess;
                TargetPointerLength = targetPointerLength;
            }
            else throw new ArgumentException("The `TargetPointerLength` must be 4(x86) ro 8(x64).");
        }


        public IntPtr Ptr(IntPtr lpBaseAddress)
        {
            using (var buffer = new AutoIntPtr(TargetPointerLength))
            {
                NativeMethods.ReadProcessMemory(ProcessHwnd, lpBaseAddress, buffer, TargetPointerLength, _);
                }
        }

        //    public IntPtr this[IntPtr lpBaseAddress]
        //    {

        //    }

        //    public static IntPtr ReadProcessMemory_(IntPtr hProcess, IntPtr lpBaseAddress, out int lpBuffer)
        //    {
        //        using (var ret = new AutoIntPtr<int>())
        //        {

        //        }

        //        var nSize = (uint)sizeof(int);
        //        var bytes = new byte[nSize];
        //        uint lpNumberOfBytesRead;

        //        bool ret = NativeMethods.ReadProcessMemory(hProcess, lpBaseAddress, bytes, nSize, out lpNumberOfBytesRead);
        //        lpBuffer = BitConverter.ToInt32(bytes, 0);

        //        return ret;
        //    }
        //    public static bool ReadProcessMemory_(IntPtr hProcess, IntPtr lpBaseAddress, out short lpBuffer)
        //    {
        //        var nSize = (uint)sizeof(short);
        //        var bytes = new byte[nSize];
        //        uint lpNumberOfBytesRead;

        //        bool ret
        //             = ReadProcessMemory(hProcess, lpBaseAddress, bytes, nSize, out lpNumberOfBytesRead);
        //        lpBuffer = BitConverter.ToInt16(bytes, 0);

        //        return ret;
        //    }
        //    public static bool ReadProcessMemory_(IntPtr hProcess, IntPtr lpBaseAddress, out float lpBuffer)
        //    {
        //        var nSize = (uint)sizeof(float);
        //        var bytes = new byte[nSize];
        //        uint lpNumberOfBytesRead;

        //        bool ret
        //             = ReadProcessMemory(hProcess, lpBaseAddress, bytes, nSize, out lpNumberOfBytesRead);
        //        lpBuffer = BitConverter.ToSingle(bytes, 0);

        //        return ret;
        //    }
        //    public static bool ReadProcessMemory_(IntPtr hProcess, IntPtr lpBaseAddress, out double lpBuffer)
        //    {
        //        var nSize = (uint)sizeof(double);
        //        var bytes = new byte[nSize];
        //        uint lpNumberOfBytesRead;

        //        bool ret
        //             = ReadProcessMemory(hProcess, lpBaseAddress, bytes, nSize, out lpNumberOfBytesRead);
        //        lpBuffer = BitConverter.ToDouble(bytes, 0);

        //        return ret;
        //    }
        //    public static bool ReadProcessMemory_(IntPtr hProcess, IntPtr lpBaseAddress, out byte lpBuffer)
        //    {
        //        var nSize = (uint)sizeof(char);
        //        var bytes = new byte[nSize];
        //        uint lpNumberOfBytesRead;

        //        bool ret
        //             = ReadProcessMemory(hProcess, lpBaseAddress, bytes, nSize, out lpNumberOfBytesRead);
        //        lpBuffer = bytes[0];

        //        return ret;
        //    }
        //    public static bool ReadProcessMemory_(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer)
        //    {
        //        var nSize = (uint)lpBuffer.Count();
        //        var bytes = new byte[nSize];
        //        uint lpNumberOfBytesRead;

        //        bool ret
        //             = ReadProcessMemory(hProcess, lpBaseAddress, bytes, nSize, out lpNumberOfBytesRead);
        //        Array.Copy(bytes, lpBuffer, nSize);

        //        return ret;
        //    }
    }
}
