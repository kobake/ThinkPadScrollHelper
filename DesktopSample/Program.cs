using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DesktopSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new ResidentTest();
            // Application.Run();

            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            Application.Run();
        }
    }

    class ResidentTest
    {
        NotifyIcon _icon;
        ContextMenuStrip _menu;

        public ResidentTest()
        {
            // this.ShowInTaskbar = false;
            // this.Visible = false;
            this.setComponents();
        }
        ~ResidentTest()
        {
        }

        ContextMenuStrip GetMenu()
        {
            if (_menu == null)
            {
                _menu = new ContextMenuStrip();
                {
                    ToolStripMenuItem menuItem = new ToolStripMenuItem();
                    menuItem.Text = "&Setting and log";
                    menuItem.Click += new EventHandler(Setting_Click);
                    _menu.Items.Add(menuItem);
                }
                {
                    ToolStripMenuItem menuItem = new ToolStripMenuItem();
                    menuItem.Text = "E&xit";
                    menuItem.Click += new EventHandler(Close_Click);
                    _menu.Items.Add(menuItem);
                }
            }
            return _menu;
        }


        private void Icon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                GetMenu().Hide();
                new Form1().Show();
            }
            else if (e.Button == MouseButtons.Right)
            {
                // GetMenu().Show(Cursor.Position);
            }

        }

        private void Setting_Click(object sender, EventArgs e)
        {
            new Form1().Show();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            if (_icon != null)
            {
                _icon.Visible = false;
                _icon.Dispose();
                _icon = null;
            }

            Application.Exit();
        }

        private Icon LoadMainIcon()
        {
            Assembly a = Assembly.GetExecutingAssembly();
            return new Icon(a.GetManifestResourceStream("DesktopSample.main.ico"));
        }

        private void setComponents()
        {
            _icon = new NotifyIcon();
            _icon.Icon = LoadMainIcon();
            _icon.Visible = true;
            _icon.Text = "ThinkPadScrollHelper";
            _icon.MouseClick += new MouseEventHandler(Icon_Click);
            // _icon.ContextMenuStrip
            _icon.ContextMenuStrip = GetMenu();
        }
    }
}
