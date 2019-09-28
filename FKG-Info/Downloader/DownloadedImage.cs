using System.Drawing;
using FKG_Info.FKG_GameData;

namespace FKG_Info.Downloader
{
    public class DownloadedImage : System.IDisposable
    {
        private Image ImageContainer;


        public string Name { get; private set; }
        public Image Image
        {
            get
            {
                LastTime = GetTimeFromStart();
                return ImageContainer;
            }
        }

        public int ImageID { get; private set; }
        public bool Mobile { get; private set; }

        public readonly object Locker = new object();


        private static readonly long StartTime = System.DateTime.Now.Ticks;

        public uint LastTime { get; private set; }



        public DownloadedImage()
        {
            LastTime = GetTimeFromStart();
        }



        public DownloadedImage(Animator ani, Image image) : this()
        {
            Name = ani.GetImageName();
            ImageID = ani.Flower.ID;
            Mobile = ani.Mobile;

            ImageContainer = image;
        }



        public DownloadedImage(EquipmentInfo equip, Image image) : this()
        {
            Name = equip.GetImageName();
            ImageID = equip.ImageID;

            ImageContainer = image;
        }



        private static uint GetTimeFromStart() { return (uint)((System.DateTime.Now.Ticks - StartTime) >> 10); }



        public void SetImage(Image image) { lock (Locker) ImageContainer = image; }



        public void Dispose()
        {
            lock (Locker)
            {
                ImageContainer?.Dispose();
                ImageContainer = null;
            }
        }



        public void SaveTemp(bool overwrite = false)
        {
            if (ImageContainer == null) return;

            string tmpdir = "F:\\Temp\\FKG-Temp";
            string tmpname = tmpdir + "\\" + Name;

            if ((!overwrite) && System.IO.File.Exists(tmpname)) return;

            if (!System.IO.Directory.Exists(tmpdir))
            {
                try { System.IO.Directory.CreateDirectory(tmpdir); } catch { return; }
            }

            ImageContainer.Save(tmpname + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
