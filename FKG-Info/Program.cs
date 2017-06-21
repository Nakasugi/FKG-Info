using System;
using System.Windows.Forms;

namespace FKG_Info
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 


        public static FlowerDataBase DB;// = new FlowerDB();
        public static ImageDownloader ImageLoader;

        public static string WorkFolder = "F:\\Pictures\\Anime\\FKG\\Img\\";

        [STAThread]
        static void Main()
        {
            DB = FlowerDataBase.Load();

            ImageLoader = new ImageDownloader();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
