using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThinkPadScrollHelper
{
    public static class RichScrollDialog
    {
        static IntPtr _hwndPropertyDialog;
        static IntPtr _hwndCheck;
        static IntPtr _hwndApplyButton;

        public static void Init()
        {
            // マウスプロパティを開く
            string mousePropertyPath = Environment.ExpandEnvironmentVariables(@"%windir%\system32\control.exe");
            // Console.WriteLine(mousePropertyPath, mousePropertyPath + " mouse");
            Process mousePropertyProcess = Process.Start(mousePropertyPath, "mouse");
            // Console.WriteLine("mousePropertyProcess = " + mousePropertyProcess.Id);
            // mousePropertyProcess.WaitForInputIdle(1000);
            // mousePropertyProcess.Refresh();
            mousePropertyProcess.WaitForExit();
            try
            {
                Thread.Sleep(500);
            }
            catch (Exception)
            {
            }

            // マウスプロパティのウィンドウを取得
            _hwndPropertyDialog = Util.FindMousePropertiesWindow();
            if (_hwndPropertyDialog == IntPtr.Zero) throw new Exception("Mouse Properties Dialog not found");
            Console.WriteLine("hwndProperty = " + _hwndPropertyDialog);

            // External Keyboard タブを選択
            IntPtr hwndTab = Util.FindChildWindowByClassName(_hwndPropertyDialog, "SysTabControl32");
            if (hwndTab == IntPtr.Zero) throw new Exception("Mouse properties TabControl not found");
            Console.WriteLine("hwndTab = " + hwndTab);
            Win32Api.PostMessage(hwndTab, Win32Api.TCM_SETCURFOCUS, new IntPtr(5), IntPtr.Zero);
            try
            {
                Thread.Sleep(500);
            }
            catch(Exception ex)
            {

            }

            // チェックボックスを取得
            // _hwndCheck = Util.FindChildWindowByCaption(_hwndPropertyDialog, "Enable &TouchPad");
            _hwndCheck = Util.FindChildWindowByCaption(_hwndPropertyDialog, "ThinkPad Preferred Scrolling");
            if (_hwndCheck == IntPtr.Zero) throw new Exception("Mouse Properties Checkbox not found");
            Console.WriteLine("hwndCheck = " + _hwndCheck);

            // ボタンを取得
            // _hwndApplyButton = Util.FindChildWindowByCaption(_hwndPropertyDialog, "&Apply");
            _hwndApplyButton = Util.FindChildWindowByCaption(_hwndPropertyDialog, "&Apply");
            if (_hwndApplyButton == IntPtr.Zero) throw new Exception("Mouse Properties ApplyButton not found");
            Console.WriteLine("hwndApplyButton = " + _hwndApplyButton);
        }

        public static void RestartIfClosed()
        {
            if (!Win32Api.IsWindowEnabled(_hwndPropertyDialog))
            {
                Console.WriteLine($"---- Restart Properties dialog ----");
                Init();
            }
        }

        public static void SetEnabled(bool rich)
        {
            RestartIfClosed();

            // チェック
            int currentState = Win32Api.SendMessage(_hwndCheck, Win32Api.BM_GETCHECK, IntPtr.Zero, IntPtr.Zero);
            bool currentChecked = (currentState & Win32Api.BST_CHECKED) != 0;

            // 変更必要無し
            if (currentChecked == rich) return;

            // 変更摘要
            if (rich)
            {
                // Win32Api.PostMessage(_hwndCheck, Win32Api.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                Win32Api.PostMessage(_hwndCheck, Win32Api.BM_SETCHECK, new IntPtr(Win32Api.BST_CHECKED), IntPtr.Zero);
                Win32Api.PostMessage(_hwndApplyButton, Win32Api.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                // Win32Api.PostMessage(_hwndCheck, Win32Api.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                Win32Api.PostMessage(_hwndCheck, Win32Api.BM_SETCHECK, new IntPtr(Win32Api.BST_UNCHECKED), IntPtr.Zero);
                Win32Api.PostMessage(_hwndApplyButton, Win32Api.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
