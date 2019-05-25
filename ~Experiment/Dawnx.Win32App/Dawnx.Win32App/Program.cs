using Dawnx.Win32.PInvoke;
using System;
using System.Diagnostics;
using System.Linq;
using static Dawnx.Win32.NativeMethods;

namespace Dawnx.Win32App
{
    class Program
    {
        public static IntPtr HWnd;
        public static uint ProcessId;

        static void Main(string[] args)
        {
            var a = EnumWindows(EnumWindowsProc, IntPtr.Zero);

            using (var m = new MemoryAccessor(ProcessId))
            {
                var v = m.F(0x016A86C4);

                m.Write(0x016A86C4, 5000f);
                Console.WriteLine(v);
            }

        }

        protected static int EnumWindowsProc(IntPtr hwnd, IntPtr lParam)
        {
            var pids = Process.GetProcesses()
                .Where(x => x.ProcessName == "Tutorial-i386")
                .Select(x => (uint)x.Id).ToArray();

            using var pid = new AutoIntPtr<uint>();
            using var windowText = new AutoCharPtr(255);

            GetWindowTextW(hwnd, windowText, windowText.Length);
            GetWindowThreadProcessId(hwnd, pid);

            var ret = pids.Contains(pid.Value)
                && windowText.Value == "Step 2";

            if (ret)
            {
                HWnd = hwnd;
                ProcessId = pid.Value;
                return 0;
            }
            else return 1;
        }

    }
}
