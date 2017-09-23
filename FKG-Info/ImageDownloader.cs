using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
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

            public object Locker;

            public DownloadedFile() { Locker = new object(); }
        }



        private List<DownloadedFile> ChFiles;
        private List<DownloadedFile> EqFiles;

        private object Locker = new object();

        public int Count { get; private set; }
        public int Queue { get; private set; }


        public delegate void DwCompletedCallback(DownloadedFile ifile);


        private enum IconElement { Background, Frame, Type, Evolution }



        public ImageDownloader()
        {
            ChFiles = new List<DownloadedFile>();
            EqFiles = new List<DownloadedFile>();

            Count = 0;
            Queue = 0;
        }



        public void GetImage(Animator ani, DwCompletedCallback cbDelegate)
        {
            Animator aniLock = new Animator(ani);
            string fname = aniLock.GetImageName();

            if(fname == null)
            {
                cbDelegate?.Invoke(null);
                return;
            }

            DownloadedFile df;
            lock (Locker) df = ChFiles.Find(f => f.Name == fname);

            if (df != null) { cbDelegate?.Invoke(df); return; }

            Thread th = new Thread(() => Download(aniLock, cbDelegate));
            th.Name = "FKG Chara Downloder";
            th.Start();
        }



        public void GetImage(EquipmentInfo equip, DwCompletedCallback cbDelegate)
        {
            string fname = equip.GetImageName();

            if (fname == null) { cbDelegate?.Invoke(null); return; }

            DownloadedFile df;
            lock (Locker) df = EqFiles.Find(f => f.Name == fname);

            if (df != null) { cbDelegate?.Invoke(df); return; }

            Thread th = new Thread(() => Download(equip, cbDelegate));
            th.Name = "FKG Equip Downloder";
            th.Start();
        }



        /// <summary>
        /// Downloading character images and icons
        /// </summary>
        /// <param name="flower"></param>
        /// <param name="cbDelegate"></param>
        void Download(Animator ani, DwCompletedCallback cbDelegate)
        {
            Image dwImage = null;
            DownloadedFile df = new DownloadedFile();

            df.Name = ani.GetImageName();
            lock (Locker) ChFiles.Add(df);

            string path, relurl;

            if (ani.IsIcon())
            {
                path = Program.DB.IconsFolder;
                relurl = "i/";
            }
            else
            {
                path = Program.DB.ImagesFolder;
                relurl = "s/";
            }
            path += "\\" + df.Name + ".png";

            try { dwImage = Image.FromFile(path); } catch { dwImage = null; }


            if ((dwImage == null) && (Program.DB.ImageSource != FlowerDataBase.ImageSources.Local))
            {
                lock (Locker) Queue++;
                while (Count >= MAX_LOADERS_CH)
                {
                    if (!Program.DB.Running) return;

                    Thread.Sleep(200 + 10 * Queue);
                }
                lock (Locker) Count++;

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
                    stream = Helper.DecompressStream(stream);
                }
                catch
                {
                    try
                    {
                        buffer = wc.DownloadData(url2);
                        stream = new MemoryStream(buffer);
                        stream = Helper.DecompressStream(stream);
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

                lock (Locker) { Count--; Queue--; }
            }


            if (dwImage != null)
            {
                SaveFile(dwImage, path);
                if (ani.ImageType == Animator.Type.IconLarge) PlaceIconElements(ani, ref dwImage);
                if (ani.ImageType == Animator.Type.Home) DrawBaseHomeImage(ani, ref dwImage);
            }
            else
            {
                dwImage = Properties.Resources.NoImage;
            }

            lock (df.Locker) df.Image = dwImage;
            cbDelegate?.Invoke(df);
        }



        /// <summary>
        /// Downloading quipment images
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="cbDelegate"></param>
        void Download(EquipmentInfo equip, DwCompletedCallback cbDelegate)
        {
            Image dwImage = null; ;
            DownloadedFile df = new DownloadedFile();

            df.Name = equip.GetImageName();
            lock (Locker) EqFiles.Add(df);

            string path = Program.DB.EquipFolder + "\\" + df.Name + ".png";

            try { dwImage = Image.FromFile(path); } catch { dwImage = null; }


            if ((dwImage == null) && (Program.DB.ImageSource != FlowerDataBase.ImageSources.Local))
            {
                lock (Locker) Queue++;
                while (Count >= MAX_LOADERS_EQ)
                {
                    if (!Program.DB.Running) return;

                    Thread.Sleep(200 + 10 * Queue);
                }
                lock (Locker) Count++;

                WebClient wc = new WebClient();

                string tname = "item/100x100/" + df.Name + ".png";
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

                lock (Locker) { Count--; Queue--; }
            }


            if (dwImage != null)
            {
                SaveFile(dwImage, path);
            }
            else
            {
                dwImage = Properties.Resources.NoImage;
            }

            lock (df.Locker) df.Image = dwImage;
            cbDelegate?.Invoke(df);
        }



        /// <summary>
        /// Saving image if needed
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path"></param>
        private void SaveFile(Image image, string path)
        {
            if (Program.DB.StoreDownloaded)
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
            
            //if (bitmapSource.Format != System.Windows.Media.PixelFormats.Bgra32) bitmapSource = new SWMI.FormatConvertedBitmap(bitmapSource, System.Windows.Media.PixelFormats.Bgra32, null, 0);

            var encoder = new SWMI.BmpBitmapEncoder();
            encoder.Frames.Add(SWMI.BitmapFrame.Create(bitmapSource));
            encoder.Save(dstStream);

            srcStream.Close();

            return dstStream;
        }



        public static void PlaceIconElements(Animator ani, ref Image icon)
        {
            Bitmap outImage = new Bitmap(100, 100);

            Graphics gr = Graphics.FromImage(outImage);
            Rectangle rc = new Rectangle(0, 0, 100, 100);

            gr.Clear(Color.FromArgb(0, 0, 0, 0));
            gr.DrawImage(GetIconElement(IconElement.Background, ani.Flower.Rarity), 0, 0);
            gr.DrawImage(icon, rc, rc, GraphicsUnit.Pixel);
            gr.DrawImage(GetIconElement(IconElement.Frame, ani.Flower.Rarity), 0, 0);
            if (!ani.Flower.NoKnight) gr.DrawImage(GetIconElement(IconElement.Type, ani.Flower.AttackType), 0, 0);
            
            gr.Dispose();

            icon.Dispose();
            icon = outImage;
        }



        private static Image GetIconElement(IconElement el, int num)
        {
            switch (el)
            {
                case IconElement.Background:
                    switch (num)
                    {
                        case 1: return Properties.Resources.icon_bg1;
                        case 2: return Properties.Resources.icon_bg2;
                        case 3: return Properties.Resources.icon_bg3;
                        case 4: return Properties.Resources.icon_bg4;
                        case 5: return Properties.Resources.icon_bg5;
                        case 6: return Properties.Resources.icon_bg6;
                        default: break;
                    }
                    break;
                case IconElement.Frame:
                    switch (num)
                    {
                        case 1: return Properties.Resources.icon_frame1;
                        case 2: return Properties.Resources.icon_frame2;
                        case 3: return Properties.Resources.icon_frame3;
                        case 4: return Properties.Resources.icon_frame4;
                        case 5: return Properties.Resources.icon_frame5;
                        case 6: return Properties.Resources.icon_frame6;
                        default: break;
                    }
                    break;
                case IconElement.Type:
                    switch (num)
                    {
                        case 1: return Properties.Resources.icon_type1;
                        case 2: return Properties.Resources.icon_type2;
                        case 3: return Properties.Resources.icon_type3;
                        case 4: return Properties.Resources.icon_type4;
                        default: break;
                    }
                    break;
                case IconElement.Evolution:
                    switch (num)
                    {
                        case 1: return Properties.Resources.icon_evol1;
                        case 2: return Properties.Resources.icon_evol2;
                        case 3: return Properties.Resources.icon_evol3;
                        default: break;
                    }
                    break;
                default: break;
            }

            return Properties.Resources.icon_default;
        }



        public void DrawBaseHomeImage(Animator ani, ref Image hEmo)
        {
            if (ani.Emotion == Animator.EmoType.Normal) return;

            DownloadedFile df = null;
            Animator home = new Animator(ani);
            home.Emotion = Animator.EmoType.Normal;
            string fname = home.GetImageName();

            int tries = 0;
            bool dw = true;

            while (true)
            {
                if (df == null) lock (Locker) df = ChFiles.Find(f => f.Name == fname);
                if (df != null) lock (df.Locker) if (df.Image != null) break;

                tries++;
                if (tries > 33) break;

                if (dw) { GetImage(home, FakeDelegate); dw = false; }
                Thread.Sleep(333);
            }
            if (df == null) return;
            lock (df.Locker) if (df.Image == null) return;

            Bitmap outImage = new Bitmap(803, 640);
            Graphics gr = Graphics.FromImage(outImage);

            gr.Clear(Color.FromArgb(0, 0, 0, 0));

            // Lock, Lock, Looooooooooooooock!!!
            lock (df.Locker) gr.DrawImage(df.Image, 0, 0);
            gr.DrawImage(hEmo, 0, 0);

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
                fl = ChFiles.Find(cf => cf.Name.Contains(pt));
                if (fl == null) break;
                fl.Image.Dispose();
                ChFiles.Remove(fl);
            }

            pt = "*" + pt + "*";

            string[] files;

            files = Directory.GetFiles(Program.DB.ImagesFolder, pt);
            foreach (string file in files) { try { File.Delete(file); } catch { } }

            files = Directory.GetFiles(Program.DB.IconsFolder, pt);
            foreach (string file in files) { try { File.Delete(file); } catch { } }
        }
    }
}
