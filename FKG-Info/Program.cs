using System;
using System.Windows.Forms;

//using WpfHelper;


namespace FKG_Info
{
    static class Program
    {
        public static LifeContcol Life = new LifeContcol();
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

            // Show Intro
            SplashWindow.Start();

            DB = FlowerDataBase.Load();
            ImageLoader = new ImageDownloader();
            DB.DeleteOldBloomCGs();

            DB.FlowerIcons = IconsAtlas.Load(IconsAtlas.Type.FlowerIcons);
            if (DB.FlowerIcons == null) DB.FlowerIcons = new IconsAtlas(DB.Flowers);
            DB.EquipmentIcons = IconsAtlas.Load(IconsAtlas.Type.EquipmentIcons);
            if (DB.EquipmentIcons == null) DB.EquipmentIcons = new IconsAtlas(DB.Equipments);

            MainForm mf = new MainForm();
            
            // Intro off
            SplashWindow.WaitForShow();
            SplashWindow.Stop();

            // Rum MainForm
            Application.Run(mf);

            DB.SaveOptIfNeeded();
            DB.FlowerIcons.SaveIfNeeded();
            DB.EquipmentIcons.SaveIfNeeded();
        }
    }
}
