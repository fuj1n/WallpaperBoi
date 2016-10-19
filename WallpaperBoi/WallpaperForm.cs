using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperBoi
{
    public partial class WallpaperForm : Form
    {
        public static readonly string PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "wallpaperoverride.jpg");
        public static readonly string PATH2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "wallpaperoverride.png");

        public WallpaperForm()
        {
            InitializeComponent();

            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;

            if (!File.Exists(PATH) && !File.Exists(PATH2))
            {
                broken.Visible = true;
                BackColor = Color.White;
            }
            else {
                IntPtr lHwnd = Program.FindWindow("Shell_TrayWnd", null);
                Program.SendMessage(lHwnd, Program.WM_COMMAND, (IntPtr)Program.MIN_ALL, IntPtr.Zero);
                System.Threading.Thread.Sleep(1000);
                wallpaper.BackgroundImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                using (Graphics g = Graphics.FromImage(wallpaper.BackgroundImage))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0, 0,
                                     wallpaper.BackgroundImage.Size,
                                     CopyPixelOperation.SourceCopy);
                }
                wallpaper.Image = new Bitmap(File.Exists(PATH2) ? PATH2 : PATH);
                Program.SendMessage(lHwnd, Program.WM_COMMAND, (IntPtr)Program.MIN_ALL_UNDO, IntPtr.Zero);
            }
        }

        private void WallpaperForm_Load(object sender, EventArgs e)
        {
            IntPtr progman = Program.FindWindow("Progman", null);
            IntPtr result = IntPtr.Zero;
            Program.SendMessageTimeout(progman, 0x052C, new IntPtr(0), IntPtr.Zero, Program.SendMessageTimeoutFlags.SMTO_NORMAL, 1000, out result);
            IntPtr workerW = IntPtr.Zero;
            Program.EnumWindows(new Program.EnumWindowsProc((tophandle, topparamhandle) =>
            {
                IntPtr p = Program.FindWindowEx(tophandle, IntPtr.Zero, "SHELLDLL_DefView", null);
                if(p != IntPtr.Zero)
                {
                    workerW = Program.FindWindowEx(IntPtr.Zero, tophandle, "WorkerW", null);
                }

                return true;
            }
            ), IntPtr.Zero);

            Program.ShowWindow(workerW, Program.ShowWindowCommands.Hide);
            Program.SetParent(Handle, progman);
        }
    }
}
