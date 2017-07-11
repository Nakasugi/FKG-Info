using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

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
        }



        List<DownloadedFile> ChFiles;
        List<DownloadedFile> EqFiles;

        private object Locker = new object();

        public int Count { get; private set; }
        public int Queue { get; private set; }


        public delegate void DownloadCompletedCallback(DownloadedFile ifile);



        public ImageDownloader()
        {
            ChFiles = new List<DownloadedFile>();
            EqFiles = new List<DownloadedFile>();

            Count = 0;
        }



        public void GetChImage(string fname, DownloadCompletedCallback cbDelegate)
        {
            if (GetImage(fname, ChFiles, cbDelegate)) return;

            Thread th = new Thread(() => ChDownload(fname, cbDelegate));
            th.Name = "FKG Chara Downloder";
            th.Start();
        }



        public void GetEqImage(string fname, DownloadCompletedCallback cbDelegate)
        {
            if (GetImage(fname, EqFiles, cbDelegate)) return;

            Thread th = new Thread(() => EqDownload(fname, cbDelegate));
            th.Name = "FKG Equip Downloder";
            th.Start();
        }



        private bool GetImage(string fname, List<DownloadedFile> files, DownloadCompletedCallback cbDelegate)
        {
            if (fname == null) { cbDelegate?.Invoke(null); return true; }

            DownloadedFile file;

            lock (Locker) file = files.Find(f => f.Name == fname);

            if (file != null)
            {
                cbDelegate?.Invoke(file);
                return true;
            }

            return false;
        }




        /// <summary>
        /// Downloading character images and icons
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="cbDelegate"></param>
        void ChDownload(string fname, DownloadCompletedCallback cbDelegate)
        {
            DownloadedFile file = new DownloadedFile();

            file.Name = fname;

            string path = Program.DB.ImagesFolder, suffix = "s/";

            string checkIcon = fname.Substring(0, 4);
            if (checkIcon == "icon")
            {
                suffix = "i/";
                path = Program.DB.IconsFolder;
            }
            path += "\\" + fname + ".png";

            try
            {
                file.Image = Image.FromFile(path);
            }
            catch
            {
                file.Image = null;
            }


            if (file.Image == null)
            {
                lock (Locker) Queue++;
                while (Count >= MAX_LOADERS_CH)
                {
                    if (!Program.DB.Running) return;

                    Thread.Sleep(200 + 10 * Queue);
                }
                lock (Locker) Count++;

                WebClient wc = new WebClient();

                string hashname = suffix + StringHelper.GetMD5Hash(fname) + ".bin";

                string url1 = null, url2 = null;

                switch (Program.DB.ImageSource)
                {
                    case FlowerDataBase.ImageSources.Nutaku: url1 = Program.DB.NutakuURL + hashname; break;
                    case FlowerDataBase.ImageSources.NutakuDMM: url2 = Program.DB.DMMURL + hashname; goto case FlowerDataBase.ImageSources.Nutaku;

                    case FlowerDataBase.ImageSources.DMM: url1 = Program.DB.DMMURL + hashname; break;
                    case FlowerDataBase.ImageSources.DMMNutaku: url2 = Program.DB.NutakuURL + hashname; goto case FlowerDataBase.ImageSources.DMM;

                    default:
                        lock (Locker) { Count--; Queue--; }
                        cbDelegate?.Invoke(null);
                        return;
                }


                MemoryStream stream;
                byte[] buffer;

                try
                {
                    buffer = wc.DownloadData(url1);
                    stream = new MemoryStream(buffer);
                    stream = DeflateSream(stream);
                }
                catch
                {
                    try
                    {
                        buffer = wc.DownloadData(url2);
                        stream = new MemoryStream(buffer);
                        stream = DeflateSream(stream);
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
                lock (Locker) ChFiles.Add(file);
                SaveFile(file.Image, path);
            }

            cbDelegate?.Invoke(file);
        }



        /// <summary>
        /// Downloading quipment images
        /// </summary>
        /// <param name="eq"></param>
        /// <param name="cbDelegate"></param>
        void EqDownload(string fname, DownloadCompletedCallback cbDelegate)
        {
            DownloadedFile file = new DownloadedFile();

            file.Name = fname;
            string path = Program.DB.EquipFolder + "\\" + fname + ".png";

            try
            {
                file.Image = Image.FromFile(path);
            }
            catch
            {
                file.Image = null;
            }


            if (file.Image == null)
            {

                lock (Locker) Queue++;
                while (Count >= MAX_LOADERS_EQ)
                {
                    if (!Program.DB.Running) return;

                    Thread.Sleep(200 + 10 * Queue);
                }
                lock (Locker) Count++;

                WebClient wc = new WebClient();


                string url1 = null, url2 = null;

                switch (Program.DB.ImageSource)
                {
                    case FlowerDataBase.ImageSources.Nutaku: url1 = Program.DB.NutakuURL; break;
                    case FlowerDataBase.ImageSources.NutakuDMM: url2 = Program.DB.DMMURL; goto case FlowerDataBase.ImageSources.Nutaku;

                    case FlowerDataBase.ImageSources.DMM: url1 = Program.DB.DMMURL; break;
                    case FlowerDataBase.ImageSources.DMMNutaku: url2 = Program.DB.NutakuURL; goto case FlowerDataBase.ImageSources.DMM;

                    default:
                        lock (Locker) { Count--; Queue--; }
                        cbDelegate?.Invoke(null);
                        return;
                }

                url1 = url1.Replace("character/", "") + "item/100x100/" + fname + ".png";
                url2 = url2.Replace("character/", "") + "item/100x100/" + fname + ".png";


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
                lock (Locker) EqFiles.Add(file);
                SaveFile(file.Image, path);
            }

            cbDelegate?.Invoke(file);
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
        static MemoryStream DeflateSream(MemoryStream srcStream)
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
    }
}
