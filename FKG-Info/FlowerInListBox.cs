using System.Drawing;

namespace FKG_Info
{
    public class FlowerInListBox
    {
        public FlowerInfo Flower;

        Color ATypeColor;

        static Image[] ImgStars = null;



        public FlowerInListBox(FlowerInfo flower)
        {
            string fname;

            Flower = flower;
            
            switch (Flower.AttackType)
            {
                case 1: ATypeColor = Color.FromArgb(0xBF, 0x00, 0x00); break;
                case 2: ATypeColor = Color.FromArgb(0x00, 0x00, 0xEF); break;
                case 3: ATypeColor = Color.FromArgb(0xA0, 0x80, 0x00); break;
                case 4: ATypeColor = Color.FromArgb(0xA0, 0x00, 0xC0); break;
                default: ATypeColor = Color.FromArgb(0x00, 0x00, 0x00); break;
            }

            if (ImgStars == null)
            {
                ImgStars = new Image[8];
                for (int i = 0; i < 8; i++)
                {
                    ImgStars[i] = null;
                    fname = Program.WorkFolder + "Stars-" + i + ".png";
                    if (System.IO.File.Exists(fname))
                    {
                        ImgStars[i] = Image.FromFile(fname);
                    }
                    else ImgStars[i] = global::FKG_Info.Properties.Resources.Stars_0;
                }
            }
        }



        public void DrawItem(System.Windows.Forms.DrawItemEventArgs ev, Font fnt)
        {
            Rectangle starsRect = new Rectangle(ev.Bounds.X, ev.Bounds.Y + 1, 26, 13);
            Rectangle nameRect = ev.Bounds;
            Brush br = new SolidBrush(ATypeColor);
            nameRect.X += 26; nameRect.Width -= 26;

            //Image Img = Image.FromFile(Program.WorkFolder+"Stars-5.png");

            ev.DrawBackground();
            ev.Graphics.DrawImage(ImgStars[Flower.Rarity], starsRect);
            ev.Graphics.DrawString(Flower.Name.Romaji, fnt, br, nameRect);
            nameRect.X += 140; nameRect.Width -= 140;
            ev.Graphics.DrawString(Flower.Name.Kanji, fnt, br, nameRect);
        }
    }
}
