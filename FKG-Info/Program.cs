using System;
using System.Windows.Forms;
using FKG_Info.UserInterface;

namespace FKG_Info
{
    static class Program
    {
        public static LifeContcol Life = new LifeContcol();
        public static FlowerDataBase DB;
        public static Downloader.ImageDownloader ImageLoader;
        public static IconsAtlas FlowerIcons;
        public static IconsAtlas EquipmentIcons;



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
            ImageLoader = new Downloader.ImageDownloader();

            FlowerIcons = IconsAtlas.Load(IconsAtlas.Type.FlowerIcons);
            if (FlowerIcons == null) FlowerIcons = new IconsAtlas(DB.Flowers);
            EquipmentIcons = IconsAtlas.Load(IconsAtlas.Type.EquipmentIcons);
            if (EquipmentIcons == null) EquipmentIcons = new IconsAtlas(DB.Equipments);

            DB.DeleteOldBloomCGs();

            MainForm mf = new MainForm();
            
            // Intro show 2 sec minimum
            SplashWindow.WaitForShow();

            // Run MainForm
            Application.Run(mf);

            DB.SaveOptIfNeeded();
            FlowerIcons.SaveIfNeeded();
            EquipmentIcons.SaveIfNeeded();
        }
    }
}
