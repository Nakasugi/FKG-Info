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

                ImgStars[0] = Properties.Resources.Stars_0;
                ImgStars[1] = Properties.Resources.Stars_1;
                ImgStars[2] = Properties.Resources.Stars_2;
                ImgStars[3] = Properties.Resources.Stars_3;
                ImgStars[4] = Properties.Resources.Stars_4;
                ImgStars[5] = Properties.Resources.Stars_5;
                ImgStars[6] = Properties.Resources.Stars_6;
            }
        }



        public void DrawItem(System.Windows.Forms.DrawItemEventArgs ev, Font fnt)
        {
            Rectangle starsRect = new Rectangle(ev.Bounds.X, ev.Bounds.Y + 1, 26, 13);
            Rectangle nameRect = ev.Bounds;
            Brush br = new SolidBrush(ATypeColor);
            nameRect.X += 26; nameRect.Width -= 26;

            ev.DrawBackground();
            ev.Graphics.DrawImage(ImgStars[Flower.Rarity], starsRect);
            ev.Graphics.DrawString(Flower.Name.Romaji, fnt, br, nameRect);
            nameRect.X += 136; nameRect.Width -= 136;
            ev.Graphics.DrawString(Flower.Name.Kanji, fnt, br, nameRect);
        }
    }
}
