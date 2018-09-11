using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPadScrollHelper
{
    public static class RichScrollDaemon
    {
        public static void RestartIfCrashed()
        {
            string scrollBackgroundPath = @"C:\Program Files (x86)\Lenovo\ThinkPad Compact Keyboard with TrackPoint driver\HScrollFun.exe";
            string scrollBackgroundName = @"HScrollFun";
            // scrollBackgroundPath = @"C:\WINDOWS\system32\notepad.exe";
            // scrollBackgroundName = @"notepad";

            var processesScroll = Process.GetProcessesByName(scrollBackgroundName);
            // Console.WriteLine(processesScroll.Length);
            if (processesScroll.Length < 1)
            {
                Console.WriteLine($"---- Restart {scrollBackgroundName} ----");
                Process.Start(scrollBackgroundPath);
            }
        }
    }
}
