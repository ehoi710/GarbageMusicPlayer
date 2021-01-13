using GarbageMusicPlayerClassLibrary;
using System;
using System.Windows.Forms;

namespace GarbageMusicPlayer
{
    static class Program
    {
        public static MusicPlayer musicPlayer = null;
        public static MusicTree musicTree = null;
        public static MusicList playList = null;
        public static string rootPath = @"T:\ELx'sM\SOIM\";
        public static string parameter = null;

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                parameter = args[0];
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
