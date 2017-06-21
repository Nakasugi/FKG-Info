using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

namespace FKG_Info
{
    class ImageDownloader
    {
        class DownloadedFile
        {
            public string Name;
            public Image Img;
        }



        List<DownloadedFile> Files;

        private object Locker = new object();



        public delegate void DownloadCompletedCallback(Image img);



        public ImageDownloader()
        {
            Files = new List<DownloadedFile>();
        }



        public void GetImage(string fname, DownloadCompletedCallback cbDelegate)
        {
            if (fname == null) { cbDelegate?.Invoke(null); return; }

            DownloadedFile file = Files.Find(f => f.Name == fname);

            if (file != null)
            {
                cbDelegate?.Invoke(file.Img);
                return;
            }

            Thread th = new Thread(() => Download(fname, cbDelegate));
            th.Name = "FKG Downloder";
            th.Start();
        }



        void Download(string fname, DownloadCompletedCallback cbDelegate)
        {
            DownloadedFile file = new DownloadedFile();
            file.Name = fname;

            string path = Program.DataBase.ImagesFolder + "\\" + fname + ".png";

            try
            {
                file.Img = Image.FromFile(path);
            }
            catch
            {
                file.Img = null;
            }


            if (file.Img == null)
            {
                WebClient wc = new WebClient();

                string hashname = StringHelper.GetHash(fname) + ".bin";

                string checkIcon = fname.Substring(0, 4);
                if (checkIcon == "icon")
                {
                    hashname = "i/" + hashname;
                }
                else
                {
                    hashname = "s/" + hashname;
                }

                string url1 = null, url2 = null;

                switch (Program.DataBase.ImageSource)
                {
                    case FlowerDB.ImageSources.Nutaku: url1 = Program.DataBase.NutakuURL + hashname; break;
                    case FlowerDB.ImageSources.NutakuDMM: url2 = Program.DataBase.DMMURL + hashname; goto case FlowerDB.ImageSources.Nutaku;

                    case FlowerDB.ImageSources.DMM: url1 = Program.DataBase.DMMURL + hashname; break;
                    case FlowerDB.ImageSources.DMMNutaku: url2 = Program.DataBase.NutakuURL + hashname; goto case FlowerDB.ImageSources.DMM;

                    default: return;
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
                    file.Img = Image.FromStream(stream);
                }
                catch
                {
                    file.Img = null;
                }
            }


            if (file.Img != null)
            {
                lock (Locker)
                {
                    Files.Add(file);
                }

                if (Program.DataBase.StoreDownloaded) Save(file);
            }


            cbDelegate?.Invoke(file.Img);
        }



        private void Save(DownloadedFile file)
        {
            string path = Program.DataBase.ImagesFolder + "\\" + file.Name + ".png";

            if (File.Exists(path)) return;

            try
            {
                file.Img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch { }
        }



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
