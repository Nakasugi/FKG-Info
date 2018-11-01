using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using SWMI = System.Windows.Media.Imaging;



namespace FKG_Info
{
    public class ImageDownloader
    {
        private const int MAX_LOADERS_CH = 16;
        private const int MAX_LOADERS_EQ = 32;



        public class DownloadedFile : IDisposable
        {
            public string Name { get; private set; }
            public Image Image { get; private set; }

            public int ImageID { get; private set; }
            public bool Mobile { get; private set; }

            public object Locker;

            public DownloadedFile() { Locker = new object(); }

            public DownloadedFile(Animator ani, Image image) : this()
            {
                Name = ani.GetImageName();
                ImageID = ani.Flower.ID;
                Mobile = ani.Mobile;

                Image = image;
            }

            public DownloadedFile(EquipmentInfo equip, Image image)
            {
                Name = equip.GetImageName();
                ImageID = equip.ImageID;

                Image = image;
            }

            public void SetImage(Image image) { lock (Locker) Image = image; }

            public void Dispose()
            {
                lock (Locker)
                {
                    Image.Dispose();
                    Image = null;
                }
            }
        }


        
        private ActionQueueLauncher FlowerImageDwQueue;
        private ActionQueueLauncher EquipmentImageDwQueue;



        private List<DownloadedFile> CharaFiles;
        //private List<DownloadedFile> EquipFiles;

        private object Locker = new object();

        public int DwCount { get; private set; }
        public int InQueue { get { return FlowerImageDwQueue.InQueue + EquipmentImageDwQueue.InQueue; } }


        public delegate void DwCompletedCallback(DownloadedFile ifile);


        
        public ImageDownloader()
        {
            CharaFiles = new List<DownloadedFile>();
            //EquipFiles = new List<DownloadedFile>();

            FlowerImageDwQueue = new ActionQueueLauncher(Program.Life, MAX_LOADERS_CH);
            EquipmentImageDwQueue = new ActionQueueLauncher(Program.Life, MAX_LOADERS_EQ);

            DwCount = 0;
        }


        public bool IsStopped() { return FlowerImageDwQueue.IsStopped() & EquipmentImageDwQueue.IsStopped(); }


        public void GetImage(Animator ani, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh = null)
        {
            if (GetLoaded(ani, cbDelegate, toRefresh)) return;
            if (Load(ani, cbDelegate, toRefresh)) return;

            if (Program.DB.EnableDownloader)
                FlowerImageDwQueue.Add(() => Download(ani, cbDelegate, toRefresh));
        }



        public void GetImage(EquipmentInfo equip, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh = null)
        {
            string fname = equip.GetImageName();

            if (fname == null) { RunCallbacks(null, cbDelegate, toRefresh); return; }

            if (Program.DB.EnableDownloader)
                EquipmentImageDwQueue.Add(() => Download(equip, cbDelegate, toRefresh));
        }



        private bool GetLoaded(Animator ani, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh)
        {
            if (ani.RawImage == true) return false;

            string fname = ani.GetImageName();

            if (fname == null) { RunCallbacks(null, cbDelegate, toRefresh); return true; }

            DownloadedFile df = null;
            lock (Locker)
            {
                df = CharaFiles.Find(f => (f.Name == fname) && (f.Mobile == ani.Mobile));
            }

            if (df == null) return false;

            RunCallbacks(df, cbDelegate, toRefresh);
            return true;
        }



        /// <summary>
        /// Try load from HDD
        /// </summary>
        /// <param name="ani"></param>
        /// <param name="cbDelegate"></param>
        /// <param name="toRefresh"></param>
        /// <returns></returns>
        private bool Load(Animator ani, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh)
        {
            string path = ani.GetPath();

            if (!File.Exists(path)) return false;

            Image image = null;
            try { image = Image.FromFile(path); } catch { image = null; }

            if (image == null) return false;

            DownloadedFile df = new DownloadedFile(ani, image);

            if (ani.RawImage != true) lock (Locker) CharaFiles.Add(df);

            RunCallbacks(df, cbDelegate, toRefresh);
            return true;
        }



        private void RunCallbacks(DownloadedFile df, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh)
        {
            cbDelegate?.Invoke(df);

            if (toRefresh != null)
            {
                try
                {
                    toRefresh?.Invoke(new Action(() => { toRefresh.Refresh(); }));
                }
                catch { }
            }
        }



        /// <summary>
        /// Downloading character images and icons
        /// </summary>
        /// <param name="flower"></param>
        /// <param name="cbDelegate"></param>
        private void Download(Animator ani, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh)
        {
            lock (Locker) DwCount++;

            Image image = null;

            string relurl = ani.GetUrlSubFolder() + Helper.GetMD5Hash(ani.GetImageName()) + ".bin";
            string url = Program.DB.GetUrl(relurl, ani.Mobile);

            MemoryStream stream;
            byte[] buffer;

            // Download
            try
            {
                var client = new WebClient();

                buffer = client.DownloadData(url);
                stream = new MemoryStream(buffer);
                if (!ani.Mobile) stream = Helper.DecompressStream(stream, 2);
            }
            catch { stream = null; }

            // Load
            if (stream != null)
            {
                try
                {
                    image = Image.FromStream(stream);
                }
                catch
                {
                    try
                    {
                        image = Image.FromStream(ReadJpegXR(stream));
                    }
                    catch
                    {
                        image = null;
                    }
                }
            }

            // Save if ok
            if (image != null)
            {
                if (!ani.IsIcon()) SaveFile(image, ani.GetPath());
                if (ani.ImageType == Animator.Type.IconLarge) PlaceIconElements(ani, ref image);
                if (ani.ImageType == Animator.Type.Home) DrawBaseHomeImage(ani, ref image);
            }
            else
            {
                image = Properties.Resources.no_image;
            }


            DownloadedFile df = new DownloadedFile(ani, image);
            if (ani.RawImage != true) lock (Locker) CharaFiles.Add(df);

            RunCallbacks(df, cbDelegate, toRefresh);

            lock (Locker) DwCount--;
        }



        /// <summary>
        /// Downloading quipment images
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="cbDelegate"></param>
        private void Download(EquipmentInfo equip, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh)
        {
            lock (Locker) DwCount++;

            Image image = null;

            string relurl = "images/item/100x100/" + equip.GetImageName() + ".png";
            string url = Program.DB.GetUrl(relurl);

            MemoryStream stream;
            byte[] buffer;

            // Download
            try
            {
                var client = new WebClient();
                buffer = client.DownloadData(url);
                stream = new MemoryStream(buffer);
            }
            catch { stream = null; }
            
            // Load
            try { image = Image.FromStream(stream); } catch { image = null; }


            if (image == null) image = Properties.Resources.no_image;

            DownloadedFile df = new DownloadedFile(equip,image);

            RunCallbacks(df, cbDelegate, toRefresh);

            lock (Locker) DwCount--;
        }



        /// <summary>
        /// Saving image if needed
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path"></param>
        private void SaveFile(Image image, string path)
        {
            if (Program.DB.SaveDownloaded)
            {
                if (!File.Exists(path))
                {
                    string dir = Path.GetDirectoryName(path);
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                    try
                    {
                        image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    catch { }
                }
            }
        }



        public static MemoryStream ReadJpegXR(MemoryStream srcStream)
        {
            MemoryStream dstStream = new MemoryStream();

            SWMI.WmpBitmapDecoder decoder =
                new SWMI.WmpBitmapDecoder(srcStream, SWMI.BitmapCreateOptions.PreservePixelFormat, SWMI.BitmapCacheOption.Default);
            SWMI.BitmapSource bitmapSource = decoder.Frames[0];
            
            var encoder = new SWMI.BmpBitmapEncoder();
            encoder.Frames.Add(SWMI.BitmapFrame.Create(bitmapSource));
            encoder.Save(dstStream);

            srcStream.Close();

            return dstStream;
        }



        public static void PlaceIconElements(Animator ani, ref Image icon)
        {
            if (ani.RawImage == true) return;

            FlowerInfo flower = ani.Flower;

            if (flower == null) return;

            Bitmap outImage = new Bitmap(100, 100);

            Graphics gr = Graphics.FromImage(outImage);
            Rectangle rc = new Rectangle(0, 0, 100, 100);

            

            gr.Clear(Color.FromArgb(0, 0, 0, 0));
            gr.DrawImage(ResHelper.GetIconElement(ResHelper.IconElement.Background, flower.Rarity), 0, 0);
            gr.DrawImage(icon, rc, rc, GraphicsUnit.Pixel);
            gr.DrawImage(ResHelper.GetIconElement(ResHelper.IconElement.Frame, flower.Rarity), 0, 0);
            if (flower.IsKnight) gr.DrawImage(ResHelper.GetIconElement(ResHelper.IconElement.Type, flower.AttackType), 0, 0);
            
            gr.Dispose();

            icon.Dispose();
            icon = outImage;
        }



        public void DrawBaseHomeImage(Animator ani, ref Image hEmo)
        {
            const int HOME_WIDTH = 803;
            const int HOME_HEIGHT = 640;


            Bitmap outImage;
            Graphics gr;

            Rectangle srcRect = new Rectangle(hEmo.Width - HOME_WIDTH, 0, HOME_WIDTH, HOME_HEIGHT);
            Rectangle dstRect = new Rectangle(0, 0, HOME_WIDTH, HOME_HEIGHT);
            if (srcRect.X < 0) srcRect.X = 0;


            if (ani.Emotion == Animator.EmoType.Normal)
            {
                if (hEmo.Width > HOME_WIDTH)
                {
                    outImage = new Bitmap(HOME_WIDTH, HOME_HEIGHT);
                    gr = Graphics.FromImage(outImage);
                    gr.Clear(Color.FromArgb(0, 0, 0, 0));
                    gr.DrawImage(hEmo, dstRect, srcRect, GraphicsUnit.Pixel);
                    gr.Dispose();
                    hEmo.Dispose();
                    hEmo = outImage;
                }

                return;
            }


            DownloadedFile df = null;
            Animator home = new Animator(ani);
            home.Emotion = Animator.EmoType.Normal;
            string fname = home.GetImageName();

            int tries = 0;
            bool dw = true;

            while (true)
            {
                if (df == null) lock (Locker) df = CharaFiles.Find(f => f.Name == fname);
                if (df != null) lock (df.Locker) if (df.Image != null) break;

                tries++;
                if (tries > 33) break;

                if (dw) { GetImage(home, FakeDelegate); dw = false; }
                Thread.Sleep(333);
            }
            if (df == null) return;
            lock (df.Locker) if (df.Image == null) return;


            outImage = new Bitmap(HOME_WIDTH, HOME_HEIGHT);
            gr = Graphics.FromImage(outImage);
            gr.Clear(Color.FromArgb(0, 0, 0, 0));
            // Lock, Lock, Looooooooooooooock!!!
            lock (df.Locker) gr.DrawImage(df.Image, 0, 0);
            gr.DrawImage(hEmo, dstRect, srcRect, GraphicsUnit.Pixel);
            gr.Dispose();
            hEmo.Dispose();
            hEmo = outImage;
        }

        void FakeDelegate(DownloadedFile noUsed) { }



        public void DeleteImages(int flowerImageID)
        {
            string pt = flowerImageID.ToString();

            DownloadedFile fl;

            while (true)
            {
                fl = CharaFiles.Find(cf => cf.Name.Contains(pt));
                if (fl == null) break;
                fl.Image.Dispose();
                CharaFiles.Remove(fl);
            }

            pt = "*" + pt + "*";

            string[] files;

            files = Directory.GetFiles(Program.DB.ImagesFolder, pt, SearchOption.AllDirectories);
            foreach (string file in files) { try { File.Delete(file); } catch { } }
        }
    }
}
