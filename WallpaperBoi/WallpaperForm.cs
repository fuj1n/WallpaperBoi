using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WallpaperBoi.natives;

namespace WallpaperBoi
{
    /**
     * The form that is attached to Progman
     */
    public partial class WallpaperForm : Form
    {
        // The search path
        public static readonly string PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        // The file name
        public const string FILENAME = "wallpaperoverride";

        // The extensions the file can be in
        public static readonly string[] SUPPORTED_EXTENSIONS = { "png", "jpg", "bmp", "gif", "tif", "tiff" };

        private Shell32.ShellClass shell;

        // File watcher
        private FileSystemWatcher watcher;

        public WallpaperForm()
        {
            InitializeComponent();

            // Create shell access
            shell = new Shell32.ShellClass();

            // Resize to screen size
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;

            LoadDesktop();
            LoadImage();

            // Create a file system watcher to watch for changes to the pictures
            watcher = new FileSystemWatcher(PATH);
            watcher.Changed += FileChanged;
            watcher.Created += FileChanged;
            watcher.Deleted += FileChanged;
            watcher.Renamed += FileChanged;
            watcher.EnableRaisingEvents = true;
        }

        public void FileChanged(object sender, FileSystemEventArgs args)
        {
            // If we're on a different thread, invoke FileChanged_Local on main thread, otherwise simply call FileChanged_Local
            if (InvokeRequired)
            {
                MethodInvoker method = new MethodInvoker(FileChanged_Local);
                Invoke(method);
            }
            else
            {
                FileChanged_Local();
            }
        }

        public void FileChanged_Local()
        {
            LoadImage();
        }

        // Iterates through the extensions and tries to find the file, returns the first found file
        public string FindFile()
        {
            foreach (string ex in SUPPORTED_EXTENSIONS)
            {
                string file = Path.Combine(PATH, FILENAME + "." + ex);
                if (File.Exists(file))
                    return file;
            }

            return "MISSINGNO;/";
        }

        // Loads the override image or displays the broken text
        public void LoadImage()
        {
            broken.Visible = false;

            if (wallpaper.Image != null)
            {
                wallpaper.Image.Dispose();
                wallpaper.Image = null;
            }

            string file = FindFile();

            if (file.Equals("MISSINGNO;/"))
            {
                broken.Visible = true;
            }
            else
            {
                // Reads the image without locking the file
                using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    wallpaper.Image = Image.FromStream(stream);
                }
            }
        }

        // Takes a screenshot of the desktop and makes that our background
        private void LoadDesktop()
        {
            ToggleDesktop();
            ToggleIcons();
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
            ToggleIcons();
            ToggleDesktop();
        }

        // Acts like the show desktop button
        private void ToggleDesktop()
        {
            ((Shell32.IShellDispatch4)shell).ToggleDesktop();
        }

        // Toggles desktop icons
        private void ToggleIcons()
        {
            IntPtr hWnd = User32.GetWindow(User32.FindWindow("Progman", "Program Manager"), User32.GetWindowType.GW_CHILD);
            User32.SendMessage(hWnd, User32.WM_COMMAND, new IntPtr(User32.TOGGLE_DESKTOP), IntPtr.Zero);
        }

        // Attaches the form to Program Manager
        private void AttachToProgman()
        {
            IntPtr progman = User32.FindWindow("Progman", null);
            IntPtr result = IntPtr.Zero;
            User32.SendMessageTimeout(progman, 0x052C, new IntPtr(0), IntPtr.Zero, User32.SendMessageTimeoutFlags.SMTO_NORMAL, 1000, out result);
            IntPtr workerW = IntPtr.Zero;
            User32.EnumWindows(new User32.EnumWindowsProc((tophandle, topparamhandle) =>
            {
                IntPtr p = User32.FindWindowEx(tophandle, IntPtr.Zero, "SHELLDLL_DefView", null);
                if (p != IntPtr.Zero)
                {
                    workerW = User32.FindWindowEx(IntPtr.Zero, tophandle, "WorkerW", null);
                }

                return true;
            }
            ), IntPtr.Zero);

            User32.ShowWindow(workerW, User32.ShowWindowCommands.Hide);
            User32.SetParent(Handle, progman);
        }

        private void WallpaperForm_Load(object sender, EventArgs e)
        {
            AttachToProgman();
        }
    }
}
