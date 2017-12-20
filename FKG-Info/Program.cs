using System;
using System.Windows.Forms;

//using WpfHelper;


namespace FKG_Info
{
    static class Program
    {
        public static FlowerDataBase DB;
        public static ImageDownloader ImageLoader;

        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashWindow.Start();

            DB = FlowerDataBase.Load();
            ImageLoader = new ImageDownloader();

            MainForm mf = new MainForm();
            SplashWindow.Stop();
            Application.Run(mf);
        }
    }
}
