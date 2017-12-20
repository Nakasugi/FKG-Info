using System;
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
        public static SplashWindow WpfWindow = null;



        private SplashWindow()
        {
            InitializeComponent();


            StartingImage.Source = ConvertBitmapToPngSource(Properties.Resources.splash_img_0);
        }


        
        public static void Start()
        {
            if (WpfWindow != null) return;

            Thread th = new Thread(new ThreadStart(ActivateForm));
            th.IsBackground = true;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }



        public static void Stop()
        {
            if (WpfWindow == null) return;
            WpfWindow.Dispatcher.Invoke(new Action(() => { WpfWindow.Close(); WpfWindow = null; }));
        }



        private static void ActivateForm()
        {
            WpfWindow = new SplashWindow();
            WpfWindow.Show();

            DoubleAnimation da = new DoubleAnimation();
            da.From = 0.1;
            da.To = 1.0;
            da.Duration = new Duration(TimeSpan.FromSeconds(1.5));
            da.AutoReverse = false;
            WpfWindow.StartingImage.BeginAnimation(OpacityProperty, da);

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
    }
}
