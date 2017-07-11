using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class FlowerSelectControl : UserControl
    {
        private List<FlowerInfo> Flowers;
        private List<AdvPictureBox> Icons;

        private bool Reloading;

        private ToolTip TTip;



        struct SortType
        {
            public const string Default = "Default";
        }



        public FlowerSelectControl(MainForm main)
        {
            InitializeComponent();

            Parent = main;

            Reloading = true;

            ChBoxS2.Checked = Program.DB.SelectorR2;
            ChBoxS3.Checked = Program.DB.SelectorR3;
            ChBoxS4.Checked = Program.DB.SelectorR4;
            ChBoxS5.Checked = Program.DB.SelectorR5;
            ChBoxS6.Checked = Program.DB.SelectorR6;

            foreach (string nt in FlowerInfo.Nations) CmBoxNation.Items.Add(nt);
            CmBoxNation.Items[0] = "All";
            CmBoxNation.SelectedIndex = 0;

            CmBoxSort.Items.Add(SortType.Default);
            CmBoxSort.SelectedIndex = 0;

            CmBoxAbility.Items.Add("All");
            CmBoxAbility.Items.AddRange(Program.DB.GetAbilitiesShortNames());
            CmBoxAbility.SelectedIndex = 0;

            TTip = new ToolTip();

            Flowers = Program.DB.Flowers;
            Icons = new List<AdvPictureBox>();

            AdvPictureBox picBox;

            foreach (FlowerInfo flower in Flowers)
            {
                picBox = new AdvPictureBox((MainForm)Parent, flower);
                picBox.Name = flower.ID.ToString();
                picBox.Width = 100;
                picBox.Height = 100;
                picBox.Image = Properties.Resources.icon_l_default;
                picBox.AsyncLoadChImage(flower, 0, FlowerInfo.ImageTypes.IconLarge);
                Icons.Add(picBox);

                string ttip = flower.Name.Kanji + "\r\n" + flower.Name.Romaji;
                if (flower.Name.EngDMM != null) ttip += "\r\nDMM: " + flower.Name.EngDMM;
                if (flower.Name.EngNutaku != null) ttip += "\r\nNutaku: " + flower.Name.EngNutaku;
                TTip.SetToolTip(picBox, ttip);
            }

            Reloading = false;
            ReloadList();
        }



        private void ReloadList()
        {
            if (Reloading) return;
            Reloading = true;

            foreach(FlowerInfo flower in Flowers)
            {
                flower.Filter = false;

                switch (flower.Rarity)
                {
                    case 0x02: if (!ChBoxS2.Checked) continue; break;
                    case 0x03: if (!ChBoxS3.Checked) continue; break;
                    case 0x04: if (!ChBoxS4.Checked) continue; break;
                    case 0x05: if (!ChBoxS5.Checked) continue; break;
                    case 0x06: if (!ChBoxS6.Checked) continue; break;
                    default: break;
                }

                switch (flower.AttackType)
                {
                    case 1: if (!ChBoxSlash.Checked) continue; break;
                    case 2: if (!ChBoxBlunt.Checked) continue; break;
                    case 3: if (!ChBoxPierce.Checked) continue; break;
                    case 4: if (!ChBoxMagic.Checked) continue; break;
                    default: break;
                }

                if (CmBoxNation.Text != "All")
                {
                    if (flower.GetNation() != CmBoxNation.Text) continue;
                }

                if (CmBoxAbility.Text != "All")
                {
                    if (!flower.CheckAbilityShortName(CmBoxAbility.Text)) continue;
                }

                flower.Filter = true;
            }
            
            Icons.Sort();
            RedrawPanel();
            Reloading = false;
        }



        private void RedrawPanel()
        {
            int x = 2, y = 2;

            PanelFlowers.SuspendLayout();
            PanelFlowers.Controls.Clear();
            PanelFlowers.VerticalScroll.Value = 0;

            foreach (AdvPictureBox pic in Icons)
            {
                if (!pic.Flower.Filter) continue;

                pic.Location = new Point(x, y);
                PanelFlowers.Controls.Add(pic);

                x += 104;
                if (x >= 728) { x = 2; y += 104; }
            }

            PanelFlowers.ResumeLayout();
        }



        private void ChBox_CheckedChanged(object sender, EventArgs ev)
        {
            if (Reloading) return;

            Program.DB.SelectorR2 = ChBoxS2.Checked;
            Program.DB.SelectorR3 = ChBoxS3.Checked;
            Program.DB.SelectorR4 = ChBoxS4.Checked;
            Program.DB.SelectorR5 = ChBoxS5.Checked;
            Program.DB.SelectorR6 = ChBoxS6.Checked;

            ReloadList();
        }



        private void BtInv_Click(object sender, EventArgs ev)
        {
            ChBoxSlash.Checked = !ChBoxSlash.Checked;
            ChBoxBlunt.Checked = !ChBoxBlunt.Checked;
            ChBoxPierce.Checked = !ChBoxPierce.Checked;
            ChBoxMagic.Checked = !ChBoxMagic.Checked;
            ReloadList();
        }
    }
}
