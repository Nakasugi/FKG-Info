using System.Drawing;
using System.Windows.Forms;

namespace FKG_Info.UserInterface
{
    public partial class EquipFastViewForm : Form
    {
        public EquipFastViewForm(FKG_GameData.EquipmentInfo[] equips)
        {
            InitializeComponent();

            ClientSize = new Size(320, 100 * equips.Length);


            for (int i = 0; i < equips.Length; i++)
            {
                FastIcon icon = new FastIcon();
                icon.Location = new Point(0, 100 * i);
                icon.SetIcon(equips[i]);
                Controls.Add(icon);

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
