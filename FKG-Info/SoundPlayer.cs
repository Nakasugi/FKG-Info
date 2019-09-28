using System.IO;
using System.Net;
using System.Threading;
using WMPLib;
using System.Runtime.InteropServices;
using System;
using System.Text;

//[DllImport("winmm.dll")]


namespace FKG_Info
{
    class SoundPlayer
    {
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);


        private static int PlaysCounter = 0;
        private static readonly object Locker = new object();


        public static void Play(string id, string hashname)
        {
            Thread th = new Thread(() => LoadAndPlay(id, hashname));
            th.Name = "Sound player " + id;
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }



        private static void LoadAndPlay(string id, string name)
        {
            if ((id == null) || (name == null)) return;

            string hashname;
            string path = Program.DB.SoundFolder + "\\" + id + "\\" + name + ".mp3";

            if ((name.IndexOfAny("_ghijklmpopqrstuvwxyz".ToCharArray()) == -1) && (name.Length == 32))
            {
                hashname = name; // Hash on input
            }
            else
            {
                // Original name on input
                hashname = Helper.GetMD5Hash(name);

                string hashpath = Program.DB.SoundFolder + "\\" + id + "\\" + hashname + ".mp3";
                try { if (File.Exists(hashpath)) File.Move(hashpath, path); } catch { }
            }


            string reluri = "voice/c/" + id + "/" + hashname + ".bin";

            if (!File.Exists(path)) Download(reluri, path);

            Play(path);
        }



        private static bool Download(string reluri, string path)
        {
            string dir = Path.GetDirectoryName(path);
            if (!Helper.CheckFolder(dir)) return false;

            string url = Program.DB.GetUrl(reluri, FlowerDataBase.UrlType.Sound);

            WebClient wc = new WebClient();
            byte[] buffer;

            try
            {
                buffer = wc.DownloadData(url);
            }
            catch { return false; }
            finally { wc.Dispose(); }
            

            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(buffer);
                ms = Helper.DecompressStream(ms, 2);
            }
            catch { ms = null; }


            if (ms == null) return false;


            bool noError = true;

            FileStream fs = null;

            try
            {
                fs = new FileStream(path, FileMode.Create);

                ms.Position = 0;
                ms.CopyTo(fs);
            }
            catch { noError = false; }
            finally { if (fs != null) fs.Close(); }

            ms.Close();

            return noError;
        }



        private static void Play(string path)
        {
            string cmd;
            string media = '"' + path + '"';
            int res = 0;

            WindowsMediaPlayer player = new WindowsMediaPlayer();
            player.settings.volume = Program.DB.SoundVolume;

            while (true)
            {
                string alias;

                lock (Locker)
                {
                    PlaysCounter++;
                    alias = "FKGsnd" + PlaysCounter.ToString("D4");
                }

                cmd = "open " + media + " alias " + alias;
                res = mciSendString(cmd, null, 0, IntPtr.Zero);
                if (res != 0) break;

                //cmd = "setaudio " + alias + " volume to " + Program.DB.SoundVolume * 10;
                //mciSendString(cmd, null, 0, IntPtr.Zero);
                //if (res != 0) break;

                cmd = "play " + alias;
                mciSendString(cmd, null, 0, IntPtr.Zero);
                if (res != 0) break;

                return;
            }

            
            try
            {
                player.URL = path;
                player.controls.play();
            }
            catch
            {
                //System.Windows.Forms.MessageBox.Show("Not supported format, or DirectX or Windows Media Player not installed.", "Sound Error");
            }
        }
    }
}
