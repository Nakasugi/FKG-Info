using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class EquipFastViewForm : Form
    {
        public EquipFastViewForm(EquipmentInfo[] equips)
        {
            InitializeComponent();

            ClientSize = new Size(320, 100 * equips.Length);


            for (int i = 0; i < equips.Length; i++)
            {
                PictureBox pic = new PictureBox();
                pic.Location = new Point(0, 100 * i);
                pic.Width = pic.Height = 100;
                Program.ImageLoader.GetImage(equips[i], (ImageDownloader.DownloadedFile f) => { pic.Image = f.Image; });
                Controls.Add(pic);

                TextBox txb = new TextBox();
                txb.Location = new Point(110, 100 * i + 4);
                txb.Multiline = true;
                txb.Name = "txB" + i;
                txb.ReadOnly = true;
                txb.Size = new Size(200, 92);
                txb.TabIndex = i;
                txb.Font = new Font("Consolas", 10);

                txb.Text = equips[i].KName + "\r\n";
                txb.Text += "ID: " + equips[i].ID + "\r\n\r\n";
                txb.Text += "Atk: " + equips[i].AttackMax + "\r\n";
                txb.Text += "Def: " + equips[i].DefenseMax;
                txb.Select(0, 0);
                Controls.Add(txb);
            }
        }
    }
}
