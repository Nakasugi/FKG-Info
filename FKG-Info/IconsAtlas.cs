using System.Collections.Generic;
using System.Drawing;



namespace FKG_Info
{
    public class IconsAtlas : System.IDisposable
    {
        const int Columns = 64;
        const int PixelSizeX = 100 * Columns;

        int Rows;
        int PixelSizeY;

        Bitmap Atlas;
        Graphics GR;

        private object DrawLocker;
        private object QueueLocker;


        List<int> LoadedIDs;
        List<AdvPictureBox> PicQueue;




        public IconsAtlas(FlowersList flowers)
        {
            DrawLocker = new object();
            QueueLocker = new object();

            LoadedIDs = new List<int>();
            PicQueue = new List<AdvPictureBox>();


            Rows = flowers.Count / 64;
            Rows++;
            PixelSizeY = Rows * 100;

            Atlas = new Bitmap(PixelSizeX, PixelSizeY, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GR = Graphics.FromImage(Atlas);
            GR.Clear(Color.FromArgb(128, 128, 128));

            Rectangle srcRc = new Rectangle(0, 0, 100, 100);
            Rectangle dstRc = new Rectangle(0, 0, 100, 100);

            for (int x = 0; x < Columns; x++)
            {
                dstRc.X = x * 100;
                for (int y = 0; y < Rows; y++)
                {
                    dstRc.Y = y * 100;
                    GR.DrawImage(Properties.Resources.icon_l_default, dstRc, srcRc, GraphicsUnit.Pixel);
                }
            }
            


            int atlasIndex = 0;

            foreach(FlowerInfo flower in flowers)
            {
                flower.AtlasIconID = atlasIndex;
                Animator ani = new Animator();
                ani.Flower = flower;
                ani.ImageType = Animator.Type.IconLarge;
                ani.RawImage = true;
                ani.ExID = flower.ID;

                Program.ImageLoader.GetImage(ani, DrawToAtlas);

                atlasIndex++;
            }
        }



        public void Dispose()
        {
            if (GR != null) GR.Dispose();
            if (Atlas != null) Atlas.Dispose();

            GR = null;
            Atlas = null;
        }

        ~IconsAtlas() { Dispose(); }



        private void DrawToAtlas(ImageDownloader.DownloadedFile ifile)
        {
            FlowerInfo flower = Program.DB.Flowers.Find(fw => fw.ID == ifile.ExID);

            if (flower == null)
            {
                if (ifile.Image != null) ifile.Image.Dispose();
                return;
            }

            if (ifile.Image == null) return;


            int IconId = flower.AtlasIconID;
            int posY = IconId / 64;
            int posX = IconId - posY * 64;

            posX *= 100; posY *= 100;

            Rectangle srcRc = new Rectangle(0, 0, 100, 100);
            Rectangle dstRc = new Rectangle(posX, posY, 100, 100);


            lock (DrawLocker)
            {
                GR.DrawImage(ResHelper.GetIconElement(ResHelper.IconElement.Background, flower.Rarity), dstRc, srcRc, GraphicsUnit.Pixel);
                GR.DrawImage(ifile.Image, dstRc, srcRc, GraphicsUnit.Pixel);
                GR.DrawImage(ResHelper.GetIconElement(ResHelper.IconElement.Frame, flower.Rarity), dstRc, srcRc, GraphicsUnit.Pixel);
                if (flower.IsKnight) GR.DrawImage(ResHelper.GetIconElement(ResHelper.IconElement.Type, flower.AttackType), dstRc, srcRc, GraphicsUnit.Pixel);
            }

            lock (QueueLocker)
            {
                LoadedIDs.Add(IconId);
                AdvPictureBox picBox = PicQueue.Find(pb => pb.Flower.AtlasIconID == IconId);
                if (picBox != null)
                {
                    picBox.SetImage(GetImage(IconId));
                    PicQueue.Remove(picBox);
                }
            }

            ifile.Image.Dispose();
            ifile.Image = null;
        }



        public Bitmap GetImage(int atlasid)
        {
            int posY = atlasid / 64;
            int posX = atlasid - posY * 64;

            posX *= 100; posY *= 100;

            lock (DrawLocker)
            {
                return Atlas.Clone(new Rectangle(posX, posY, 100, 100), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            }
        }



        public void GetImage(AdvPictureBox picBox)
        {
            if (picBox.Flower == null)
            {
                picBox.SetImage(null);
                return;
            }

            int atlasId = picBox.Flower.AtlasIconID;
            lock (QueueLocker)
            {
                if (LoadedIDs.Contains(atlasId))
                {
                    picBox.SetImage(GetImage(atlasId));
                    return;
                }

                picBox.SetImage(Properties.Resources.icon_l_default, false);
                PicQueue.Add(picBox);
            }
        }



        public void Save()
        {
            Atlas.Save("F:\\atlas.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
