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
            public FlowerInfo.ImageTypes Type;
            public string Name;
            public Image Image;
        }



        List<DownloadedFile> ChFiles;
        List<DownloadedFile> EqFiles;

        private object Locker = new object();

        public int Count { get; private set; }
        public int Queue { get; private set; }


        public delegate void DwCompletedCallback(DownloadedFile ifile);



        //Image[] IconBG, IconRarity, IconType, IconEvol;
        private enum IconElement { Background, Frame, Type, Evolution }



        public ImageDownloader()
        {
            ChFiles = new List<DownloadedFile>();
            EqFiles = new List<DownloadedFile>();

            Count = 0;
        }



        public void GetImage(FlowerInfo flower, DwCompletedCallback cbDelegate)
        {
            string fname = flower.GetImageName();

            if(fname == null)
            {
                flower.UnlockSelection();
                cbDelegate?.Invoke(null);
                return;
            }

            DownloadedFile file;
            lock (Locker) file = ChFiles.Find(f => f.Name == fname);

            if (file != null)
            {
                flower.UnlockSelection();
                cbDelegate?.Invoke(file);
                return;
            }

            Thread th = new Thread(() => Download(flower, cbDelegate));
            th.Name = "FKG Chara Downloder";
            th.Start();
        }



        public void GetImage(EquipmentInfo equip, DwCompletedCallback cbDelegate)
        {
            string fname = equip.GetImageName();

            if (fname == null) { cbDelegate?.Invoke(null); return; }

            DownloadedFile file;
            lock (Locker) file = EqFiles.Find(f => f.Name == fname);

            if (file != null) { cbDelegate?.Invoke(file); return; }

            Thread th = new Thread(() => Download(equip, cbDelegate));
            th.Name = "FKG Equip Downloder";
            th.Start();
        }



        /// <summary>
        /// Downloading character images and icons
        /// </summary>
        /// <param name="flower"></param>
        /// <param name="cbDelegate"></param>
        void Download(FlowerInfo flower, DwCompletedCallback cbDelegate)
        {
            DownloadedFile file = new DownloadedFile();

            file.Name = flower.GetImageName();
            file.Type = flower.SelectedImage;
            file.Image = null;

            lock (Locker) ChFiles.Add(file);

            flower.UnlockSelection();

            string path = Program.DB.ImagesFolder, tname = "s/";
            string checkIcon = file.Name.Substring(0, 4);
            if (checkIcon == "icon")
            {
                tname = "i/";
                path = Program.DB.IconsFolder;
            }
            path += "\\" + file.Name + ".png";

            try
            {
                file.Image = Image.FromFile(path);
            }
            catch
            {
                file.Image = null;
            }


            if ((file.Image == null) && (Program.DB.ImageSource != FlowerDataBase.ImageSources.Local))
            {
                lock (Locker) Queue++;
                while (Count >= MAX_LOADERS_CH)
                {
                    if (!Program.DB.Running) return;

                    Thread.Sleep(200 + 10 * Queue);
                }
                lock (Locker) Count++;

                WebClient wc = new WebClient();


                tname = "character/" + tname + StringHelper.GetMD5Hash(file.Name) + ".bin";
                string url1 = Program.DB.GetUrl(1) + tname;
                string url2 = Program.DB.GetUrl(2) + tname;

                MemoryStream stream;
                byte[] buffer;

                try
                {
                    buffer = wc.DownloadData(url1);
                    stream = new MemoryStream(buffer);
                    stream = DecompressStream(stream);
                }
                catch
                {
                    try
                    {
                        buffer = wc.DownloadData(url2);
                        stream = new MemoryStream(buffer);
                        stream = DecompressStream(stream);
                    }
                    catch { stream = null; }
                }


                try
                {
                    file.Image = Image.FromStream(stream);
                }
                catch
                {
                    try
                    {
                        file.Image = Image.FromStream(ReadJpegXR(stream));
                    }
                    catch
                    {
                        file.Image = null;
                    }
                }

                lock (Locker) { Count--; Queue--; }
            }


            if (file.Image != null)
            {
                SaveFile(file.Image, path);
                if (file.Type == FlowerInfo.ImageTypes.IconLarge) PlaceIconElements(flower, ref file.Image);
                cbDelegate?.Invoke(file);
            }
            else
            {
                cbDelegate?.Invoke(null);
            }
        }



        /// <summary>
        /// Downloading quipment images
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="cbDelegate"></param>
        void Download(EquipmentInfo equip, DwCompletedCallback cbDelegate)
        {
            DownloadedFile file = new DownloadedFile();

            file.Name = equip.GetImageName();
            lock (Locker) EqFiles.Add(file);

            string path = Program.DB.EquipFolder + "\\" + file.Name + ".png";

            try
            {
                file.Image = Image.FromFile(path);
            }
            catch
            {
                file.Image = null;
            }


            if ((file.Image == null) && (Program.DB.ImageSource != FlowerDataBase.ImageSources.Local))
            {
                lock (Locker) Queue++;
                while (Count >= MAX_LOADERS_EQ)
                {
                    if (!Program.DB.Running) return;

                    Thread.Sleep(200 + 10 * Queue);
                }
                lock (Locker) Count++;

                WebClient wc = new WebClient();

                string tname = "item/100x100/" + file.Name + ".png";
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


                try
                {
                    file.Image = Image.FromStream(stream);
                }
                catch
                {
                    file.Image = null;
                }

                lock (Locker) { Count--; Queue--; }
            }


            if (file.Image != null)
            {
                SaveFile(file.Image, path);
                cbDelegate?.Invoke(file);
            }
            else
            {
                cbDelegate?.Invoke(null);
            }
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



        /// <summary>
        /// Extract Zip
        /// </summary>
        /// <param name="srcStream"></param>
        /// <returns></returns>
        static MemoryStream DecompressStream(MemoryStream srcStream)
        {
            MemoryStream dstStream = new MemoryStream();

            srcStream.Position = 2;

            using (DeflateStream outStream = new DeflateStream(srcStream, CompressionMode.Decompress))
            {
                outStream.CopyTo(dstStream);
            }

            srcStream.Close();

            return dstStream;
        }



        static MemoryStream ReadJpegXR(MemoryStream srcStream)
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



        public static void PlaceIconElements(FlowerInfo flower, ref Image icon)
        {
            Bitmap outImage = new Bitmap(100, 100);

            Graphics gr = Graphics.FromImage(outImage);
            Rectangle rc = new Rectangle(0, 0, 100, 100);

            gr.Clear(Color.FromArgb(0, 0, 0, 0));
            gr.DrawImage(GetIconElement(IconElement.Background, flower.Rarity), 0, 0);
            gr.DrawImage(icon, rc, rc, GraphicsUnit.Pixel);
            gr.DrawImage(GetIconElement(IconElement.Frame, flower.Rarity), 0, 0);
            if (!flower.NoKnight) gr.DrawImage(GetIconElement(IconElement.Type, flower.AttackType), 0, 0);
            
            //if (evol != 0) gr.DrawImage(GetIconElement(IconElement.Evolution, evol), 0, 0);

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
    }
}
