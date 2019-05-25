using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Dawnx.Win32.NativeMethods;
using static Dawnx.Win32.NativeConstants;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Dawnx.Win32.PInvoke;

namespace Dawnx.Win32App
{
    class Program
    {
        public static IntPtr WindowHwnd;
        public static IntPtr ProcessHwnd;

        static void Main(string[] args)
        {
            EnumWindows(EnumWindowsProc, IntPtr.Zero);


            var r = Marshal.SizeOf(new int());

            //dynamic a;
            //var b = a[a.F(a.I4(0x1) + 0x1)];

        }

        protected static int EnumWindowsProc(IntPtr hwnd, IntPtr lParam)
        {
            var pids = Process.GetProcesses()
                .Where(x => x.ProcessName == "Tutorial-i386.exe")
                .Select(x => x.Id).ToArray();

            using var pid = new AutoIntPtr<int>();
            using var windowText = new AutoCharPtr(255);

            GetWindowTextW(hwnd, windowText, windowText.Length);
            GetWindowThreadProcessId(hwnd, pid);

            var ret = pids.Contains(pid.Value)
                && windowText.Value == "Step 2";

            if (ret)
            {
                WindowHwnd = hwnd;
                ProcessHwnd = pid;
                return 0;
            }
            else return 1;
        }

    }
}
