using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WpfSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            new ResidentTest();
        }
    }

    class ResidentTest
    {
        NotifyIcon _icon;

        public ResidentTest()
        {
            // this.ShowInTaskbar = false;
            // this.Visible = false;
            this.setComponents();
        }
        ~ResidentTest()
        {
        }


        private void Close_Click(object sender, EventArgs e)
        {
            if (_icon != null)
            {
                _icon.Visible = false;
                _icon.Dispose();
                _icon = null;
            }

            Environment.Exit(0);
            // Application.Exit();

            // Application.Exit();
        }

        private Icon LoadMainIcon()
        {
            Assembly a = Assembly.GetExecutingAssembly();
            return new Icon(a.GetManifestResourceStream("WpfSample.main.ico"));
        }

        private void setComponents()
        {
            _icon = new NotifyIcon();
            _icon.Icon = LoadMainIcon();
            _icon.Visible = true;
            _icon.Text = "常駐アプリテスト";
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = "&終了";
            menuItem.Click += new EventHandler(Close_Click);
            menu.Items.Add(menuItem);
            _icon.ContextMenuStrip = menu;
        }
    }
}
