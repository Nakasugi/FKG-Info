using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FKG_Info
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        public static SplashWindow WPFWindow = null;

        private static Stopwatch SW = new Stopwatch();



        private SplashWindow()
        {
            InitializeComponent();

            StartingImage.Source = ConvertBitmapToPngSource(Properties.Resources.splash_img_0);
        }


        
        public static void Start()
        {
            if (WPFWindow != null) return;

            Thread th = new Thread(new ThreadStart(ActivateForm));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

            SW.Start();
        }



        public static void Stop()
        {
            if (WPFWindow == null) return;
            WPFWindow.Dispatcher.Invoke(new Action(() => { WPFWindow.Close(); WPFWindow = null; }));
            SW.Stop();
        }



        private static void ActivateForm()
        {
            WPFWindow = new SplashWindow();
            WPFWindow.Show();

            DoubleAnimation da = new DoubleAnimation();
            da.From = 0.1;
            da.To = 1.0;
            da.Duration = new Duration(TimeSpan.FromSeconds(2));
            da.AutoReverse = false;
            WPFWindow.StartingImage.BeginAnimation(OpacityProperty, da);

            System.Windows.Threading.Dispatcher.Run();
        }



        private BitmapImage ConvertBitmapToPngSource(System.Drawing.Bitmap src)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            src.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }



        public static void WaitForShow()
        {
            if (SW.ElapsedMilliseconds < 2000) Thread.Sleep(2000 - (int)SW.ElapsedMilliseconds);
        }
    }
}
