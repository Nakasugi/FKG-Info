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



        public class DownloadedFile
        {
            public string Name;
            public Image Image;

            public int ImageID;

            public object Locker;

            public DownloadedFile() { Locker = new object(); }
        }


        
        private ActionQueueLauncher FlowerImageDwQueue;
        private ActionQueueLauncher EquipmentImageDwQueue;



        private List<DownloadedFile> CharaFiles;
        private List<DownloadedFile> EquipFiles;

        private object Locker = new object();

        public int DwCount { get; private set; }
        public int InQueue { get { return FlowerImageDwQueue.InQueue + EquipmentImageDwQueue.InQueue; } }


        public delegate void DwCompletedCallback(DownloadedFile ifile);


        
        public ImageDownloader()
        {
            CharaFiles = new List<DownloadedFile>();
            EquipFiles = new List<DownloadedFile>();

            FlowerImageDwQueue = new ActionQueueLauncher(Program.Life, MAX_LOADERS_CH);
            EquipmentImageDwQueue = new ActionQueueLauncher(Program.Life, MAX_LOADERS_EQ);

            DwCount = 0;
        }


        public bool IsStopped() { return FlowerImageDwQueue.IsStopped() & EquipmentImageDwQueue.IsStopped(); }


        public void GetImage(Animator ani, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh = null)
        {
            Animator aniLock = new Animator(ani);
            string fname = aniLock.GetImageName();

            if(fname == null)
            {
                cbDelegate?.Invoke(null);
                return;
            }

            DownloadedFile df = null;

            if (ani.RawImage != true) lock (Locker) df = CharaFiles.Find(f => f.Name == fname);

            if (df != null) { cbDelegate?.Invoke(df); return; }


            FlowerImageDwQueue.Add(() => Download(ani, cbDelegate, toRefresh));
        }



        public void GetImage(EquipmentInfo equip, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh = null)
        {
            string fname = equip.GetImageName();

            if (fname == null) { cbDelegate?.Invoke(null); return; }

            DownloadedFile df;
            lock (Locker) df = EquipFiles.Find(f => f.Name == fname);

            if (df != null) { cbDelegate?.Invoke(df); return; }


            EquipmentImageDwQueue.Add(() => Download(equip, cbDelegate, toRefresh));
        }




        /// <summary>
        /// Downloading character images and icons
        /// </summary>
        /// <param name="flower"></param>
        /// <param name="cbDelegate"></param>
        private void Download(Animator ani, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh)
        {
            Image dwImage = null;

            DownloadedFile df = new DownloadedFile();
            df.Name = ani.GetImageName();
            df.ImageID = ani.Flower.ID;

            if (ani.RawImage != true) lock (Locker) CharaFiles.Add(df);

            string path, relurl = ani.GetUrlSubFolder();


            path = Program.DB.ImagesFolder;

            path += "\\" + df.Name + ".png";

            try { dwImage = Image.FromFile(path); } catch { dwImage = null; }


            if ((dwImage == null) && (Program.DB.ImageSource != FlowerDataBase.ImageSources.Local))
            {
                lock (Locker) DwCount++;

                WebClient wc = new WebClient();


                relurl = "images/character/" + relurl + Helper.GetMD5Hash(df.Name) + ".bin";
                string url1 = Program.DB.GetUrl(1, relurl);
                string url2 = Program.DB.GetUrl(2, relurl);

                MemoryStream stream;
                byte[] buffer;

                try
                {
                    buffer = wc.DownloadData(url1);
                    stream = new MemoryStream(buffer);
                    stream = Helper.DecompressStream(stream, 2);
                }
                catch
                {
                    try
                    {
                        buffer = wc.DownloadData(url2);
                        stream = new MemoryStream(buffer);
                        stream = Helper.DecompressStream(stream, 2);
                    }
                    catch { stream = null; }
                }


                try
                {
                    dwImage = Image.FromStream(stream);
                }
                catch
                {
                    try
                    {
                        dwImage = Image.FromStream(ReadJpegXR(stream));
                    }
                    catch
                    {
                        dwImage = null;
                    }
                }

                lock (Locker) { DwCount--; }
            }


            if (dwImage != null)
            {
                if (!ani.IsIcon()) SaveFile(dwImage, path);
                if (ani.ImageType == Animator.Type.IconLarge) PlaceIconElements(ani, ref dwImage);
                if (ani.ImageType == Animator.Type.Home) DrawBaseHomeImage(ani, ref dwImage);
            }
            else
            {
                dwImage = Properties.Resources.no_image;
            }

            lock (df.Locker) df.Image = dwImage;
            cbDelegate?.Invoke(df);

            RefreshFormsControl(toRefresh);
        }



        /// <summary>
        /// Downloading quipment images
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="cbDelegate"></param>
        private void Download(EquipmentInfo equip, DwCompletedCallback cbDelegate, System.Windows.Forms.Control toRefresh)
        {
            Image dwImage = null; ;
            DownloadedFile df = new DownloadedFile();
            df.Name = equip.GetImageName();
            df.ImageID = equip.ImageID;

            lock (Locker) EquipFiles.Add(df);

            string path = Program.DB.EquipFolder + "\\" + df.Name + ".png";

            try { dwImage = Image.FromFile(path); } catch { dwImage = null; }


            if ((dwImage == null) && (Program.DB.ImageSource != FlowerDataBase.ImageSources.Local))
            {
                while (DwCount >= MAX_LOADERS_EQ)
                {
                    if (Program.Life.IsDead) return;

                    Thread.Sleep(200 + 10 * DwCount);
                }
                lock (Locker) DwCount++;

                WebClient wc = new WebClient();

                string tname = "images/item/100x100/" + df.Name + ".png";
                string url1 = Program.DB.GetUrl(1) + tname;
                string url2 = Program.DB.GetUrl(2) + tname;

                MemoryStream stream;
                byte[] buffer;

                try
                {
                    buffer = wc.DownloadData(url1);
                    stream = new MemoryStream(buffer);
                }
                catch
                {
                    try
                    {
                        {
                            buffer = wc.DownloadData(url2);
                            stream = new MemoryStream(buffer);
                        }
                    }
                    catch { stream = null; }
                }

                try { dwImage = Image.FromStream(stream); } catch { dwImage = null; }

                lock (Locker) { DwCount--; }
            }


            if (dwImage != null)
            {
                SaveFile(dwImage, path);
            }
            else
            {
                dwImage = Properties.Resources.no_image;
            }

            lock (df.Locker) df.Image = dwImage;
            cbDelegate?.Invoke(df);

            RefreshFormsControl(toRefresh);
        }



        /// <summary>
        /// Try refresh control
        /// </summary>
        /// <param name="toRefresh"></param>
        private void RefreshFormsControl(System.Windows.Forms.Control toRefresh)
        {
            if (toRefresh == null) return;

            try
            {
                toRefresh?.Invoke(new Action(() => { toRefresh.Refresh(); }));
            }
            catch { }
        }



        /// <summary>
        /// Saving image if needed
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path"></param>
        private void SaveFile(Image image, string path)
        {
            if (Program.DB.StoreDownloadedImages)
            {
                if (!File.Exists(path))
                {
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

            files = Directory.GetFiles(Program.DB.ImagesFolder, pt);
            foreach (string file in files) { try { File.Delete(file); } catch { } }
        }
    }
}
