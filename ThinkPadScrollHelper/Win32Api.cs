using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPadScrollHelper
{
    // WIN32API
    public static class Win32Api
    {
        // process
        [DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        // find window
        public delegate bool WNDENUMPROC(IntPtr hWnd, IntPtr lparam);
        [DllImport("user32.dll")] public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, IntPtr lparam);
        [DllImport("user32.dll")] public static extern bool EnumChildWindows(IntPtr hwnd, WNDENUMPROC func, IntPtr lParam);
        [DllImport("user32.dll")] public static extern IntPtr WindowFromPoint(Point p);

        // get window info
        [DllImport("user32.dll")] public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")] public static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll")] public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll")] public static extern bool IsWindowEnabled(IntPtr hWnd);

        // contorl window
        [DllImport("user32.dll")] public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")] public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")] public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        public const uint BM_SETCHECK = 0x00f1;
        public const uint BM_CLICK = 0x00F5;
        public const uint BM_GETCHECK = 0x00F0;

        public const uint BST_UNCHECKED = 0x0000;
        public const uint BST_CHECKED = 0x0001;

        public const uint TCM_FIRST = 0x1300;
        public const uint TCM_SETCURSEL = TCM_FIRST + 12;
        public const uint TCM_SETCURFOCUS = TCM_FIRST + 48;


    }
}
