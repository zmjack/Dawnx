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
        private static int[] pids;
        static void Main(string[] args)
        {
            pids = Process.GetProcesses()
                .Where(x => x.ProcessName.StartsWith("notepad"))
                .Select(x => x.Id).ToArray();

            EnumWindows(EnumWindowsProc, IntPtr.Zero);

        }

        protected static int EnumWindowsProc(IntPtr hwnd, IntPtr lParam)
        {
            using var pid = new AutoIntPtr<int>();

            StringBuilder window_text_ptr = new StringBuilder(255);
            string window_text;

            GetWindowTextW(hwnd, window_text_ptr, 255);
            window_text = window_text_ptr.ToString();

            GetWindowThreadProcessId(hwnd, pid);

            if (pids.Any(x => x == pid.Value))
            {
                if (window_text.StartsWith("无标题"))
                {
                    return 0;
                }
                else return 1;
            }
            else return 1;
        }

    }
}
