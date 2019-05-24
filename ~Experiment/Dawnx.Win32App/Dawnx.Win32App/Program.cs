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
using Dawnx;

namespace Dawnx.Win32App
{
    class Program
    {
        static void Main(string[] args)
        {
            var prefix = "notepad";

            EnumWindows((IntPtr hwnd, IntPtr lParam) =>
            {
                var pids = Process.GetProcesses()
                    .Where(x => x.ProcessName.StartsWith(prefix))
                    .Select(x => x.Id).ToArray();

                using var pid = new AutoIntPtr<int>();
                var windowText = new StringBuilder(255);

                GetWindowTextW(hwnd, windowText, windowText.Capacity);
                GetWindowThreadProcessId(hwnd, pid);

                if (pids.Contains(pid.Value))
                {
                    if (windowText.ToString().StartsWith("无标题"))
                        return 0;
                    else return 1;
                }
                else return 1;
            }, IntPtr.Zero);

        }

    }
}
