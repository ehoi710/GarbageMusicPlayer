using GarbageMusicPlayerClassLibrary;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GarbageMusicPlayer
{
    public class Win32
    {
        /// <summary>
        /// The SetForegroundWindow function puts the thread that created the specified window
        /// into the foreground and activates the window. Keyboard input is directed to the window,
        /// and various visual cues are changed for the user. The system assigns a slightly higher
        /// priority to the thread that created the foreground window than it does to other threads.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The ShowWindowAsync function sets the show state of a window created by a different thread.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern int GetClassName(int hwnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// The IsIconic function determines whether the specified window is minimized (iconic).
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activates and displays the window. If the window is minimized or maximized, the system
        // restores it to its original size and position. An application should specify this flag
        // when restoring a minimized window.
        public static int SW_RESTORE = 9;

        /// <summary>
        /// The SendMessage API
        /// </summary>
        /// <param name="hWnd">handle to the required window</param>
        /// <param name="Msg">the system/Custom message to send</param>
        /// <param name="wParam">first message parameter</param>
        /// <param name="lParam">second message parameter</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref CopyDataStruct lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalAlloc(int flag, int size);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr p);

        public const int WM_COPYDATA = 0x004A;

        public struct CopyDataStruct : IDisposable
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;

            public void Dispose()
            {
                if (this.lpData != IntPtr.Zero)
                {
                    LocalFree(this.lpData);
                    this.lpData = IntPtr.Zero;
                }
            }
        }
    }

    static class Program
    {
        public static MusicPlayer musicPlayer = null;
        public static MusicList playList = null;
        public static string rootPath = @"T:\ELx'sM\SOIM\";
        public static string parameter = "";
        public static bool isParam = false;

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                isParam = true;
                parameter = args[0];
            }
            IntPtr hWndOfPrevInstance = Win32.FindWindow(null, "GMP");

            if(hWndOfPrevInstance != IntPtr.Zero)
            {
                if (Win32.IsIconic(hWndOfPrevInstance))
                    Win32.ShowWindowAsync(hWndOfPrevInstance, Win32.SW_RESTORE);
                Win32.SetForegroundWindow(hWndOfPrevInstance);

                SendParams(hWndOfPrevInstance, ref parameter);

                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void SendParams(IntPtr hWndTarget, ref string param)
        {
            Win32.CopyDataStruct cds = new Win32.CopyDataStruct();
            try
            {
                cds.cbData = (param.Length + 1) * 2;
                cds.lpData = Win32.LocalAlloc(0x40, cds.cbData);
                Marshal.Copy(param.ToCharArray(), 0, cds.lpData, param.Length);
                cds.dwData = (IntPtr)1;
                Win32.SendMessage(hWndTarget, Win32.WM_COPYDATA, IntPtr.Zero, ref cds);
            }
            finally
            {
                cds.Dispose();
            }
        }
    }
}
