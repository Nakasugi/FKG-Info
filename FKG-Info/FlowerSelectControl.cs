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

        private const string TEXT_SEARCH = "Search...";


        private Timer SearchTimer;


        struct SortBy
        {
            public const string Default = "Default";
            public const string Category = "By Category";
            public const string TotalMaxStats = "By Total Maxed Stats";
        }



        /// <summary>
        /// Just Constructor
        /// </summary>
        /// <param name="main"></param>
        public FlowerSelectControl(MainForm main)
        {
            components = new System.ComponentModel.Container();

            Visible = false;
            main.LoadingControlsMessage(true);
            SuspendLayout();
            InitializeComponent();

            Parent = main;
            Reloading = true;
            Location = new Point(0, 26);

            foreach (string nt in FlowerInfo.Nations) CmBoxNation.Items.Add(nt);
            CmBoxNation.Items[0] = "All Nations";
            CmBoxNation.SelectedIndex = 0;

            CmBoxAbility01.Items.Add("All Abilities");
            CmBoxAbility01.Items.AddRange(Program.DB.GetAbilitiesShortNames());
            CmBoxAbility01.SelectedIndex = 0;
            CmBoxAbility02.Items.Add("All Abilities");
            CmBoxAbility02.Items.AddRange(Program.DB.GetAbilitiesShortNames());
            CmBoxAbility02.SelectedIndex = 0;

            foreach (FlowerInfo.SpecFilter spec in Enum.GetValues(typeof(FlowerInfo.SpecFilter)))
                CmBoxSpecFilter.Items.Add(spec.ToString().Replace("_", " "));
            CmBoxSpecFilter.SelectedIndex = 0;

            foreach (System.Reflection.FieldInfo fi in typeof(SortBy).GetFields())
                CmBoxSort.Items.Add(fi.GetValue(null).ToString());
            CmBoxSort.SelectedIndex = 0;

            SearchTimer = new Timer();
            SearchTimer.Interval = 1000;
            SearchTimer.Tick += (s, e) => OnSearchTimer();

            //TxBoxSearch.Text = "🔍 Search...";
            TxBoxSearch.Text = TEXT_SEARCH;
            TxBoxSearch.ForeColor = SystemColors.GrayText;

            TTip = new ToolTip();

            Flowers = Program.DB.Flowers;
            Icons = new List<AdvPictureBox>();

            AdvPictureBox picBox;

            Animator icon = new Animator();
            icon.ImageType = Animator.Type.IconLarge;

            foreach (FlowerInfo flower in Flowers)
            {
                icon.Flower = flower;
                picBox = new AdvPictureBox((MainForm)Parent, flower);
                picBox.Name = flower.ID.ToString();
                picBox.Width = 100;
                picBox.Height = 100;
                picBox.Image = Properties.Resources.icon_l_default;
                picBox.AsyncLoadImage(icon);
                picBox.Visible = false;
                Icons.Add(picBox);

                components.Add(picBox); // for auto disposing

                string ttip = flower.Name.Kanji + "\r\n" + flower.Name.Romaji;
                if (flower.Name.EngDMM != null) ttip += "\r\nDMM: " + flower.Name.EngDMM;
                if (flower.Name.EngNutaku != null) ttip += "\r\nNutaku: " + flower.Name.EngNutaku;
                TTip.SetToolTip(picBox, ttip);

                PanelFlowers.Controls.Add(picBox);
            }

            Reloading = false;
            ReloadList();
            ResumeLayout();
            main.LoadingControlsMessage(false);
            Visible = true;
        }



        private void ReloadList()
        {
            if (Reloading) return;
            Reloading = true;

            string tofind = TxBoxSearch.Text;
            bool searching = (tofind.Length > 1) && (tofind != TEXT_SEARCH);

            FlowerInfo.SpecFilter spec = (FlowerInfo.SpecFilter)Enum.Parse(typeof(FlowerInfo.SpecFilter), CmBoxSpecFilter.Text.Replace(" ", "_"));

            foreach (FlowerInfo flower in Flowers)
            {
                flower.Filter = false;

                switch (flower.Rarity)
                {
                    case 0x02: if (ChBoxS2.Checked) break; continue;
                    case 0x03: if (ChBoxS3.Checked) break; continue;
                    case 0x04: if (ChBoxS4.Checked) break; continue;
                    case 0x05: if (ChBoxS5.Checked) break; continue;
                    case 0x06: if (ChBoxS6.Checked) break; continue;
                    default: break;
                }

                switch (flower.AttackType)
                {
                    case 1: if (ChBoxSlash.Checked) break; continue;
                    case 2: if (ChBoxBlunt.Checked) break; continue;
                    case 3: if (ChBoxPierce.Checked) break; continue;
                    case 4: if (ChBoxMagic.Checked) break; continue;
                    default: break;
                }

                switch (spec)
                {
                    case FlowerInfo.SpecFilter.All_Knights: if (flower.NoKnight) continue; break;
                    case FlowerInfo.SpecFilter.Has_Bloom_Form: if (flower.HasBloomForm()) break; continue;
                    case FlowerInfo.SpecFilter.Has_Bloom_CG: if(flower.HasBloomForm(1)) break; continue;
                    case FlowerInfo.SpecFilter.No_Bloom_CG: if (flower.HasBloomForm(2)) break; continue;
                    case FlowerInfo.SpecFilter.Has_Exclusive_Skin: if (flower.HasExclusiveSkin()) break; continue;
                    default: if(flower.CheckCategory(spec)) break; continue;
                }
                

                if (CmBoxNation.Text != "All Nations")
                {
                    if (flower.GetNation() != CmBoxNation.Text) continue;
                }

                if (CmBoxAbility01.Text != "All Abilities")
                {
                    if (!flower.CheckAbilityShortName(CmBoxAbility01.Text)) continue;
                }

                if (CmBoxAbility02.Text != "All Abilities")
                {
                    if (!flower.CheckAbilityShortName(CmBoxAbility02.Text)) continue;
                }


                if (searching) if (!flower.CheckNames(tofind)) continue;

                flower.Filter = true;
            }


            BaseInfo.SortBy sortType = BaseInfo.SortBy.Default;
            switch (CmBoxSort.Text)
            {
                case SortBy.Category: sortType = BaseInfo.SortBy.Category; break;
                case SortBy.TotalMaxStats: sortType = BaseInfo.SortBy.TotalStats; break;
                default: break;
            }

            Icons.Sort((pb1, pb2) => pb1.Flower.CompareTo(pb2.Flower, sortType));

            RedrawPanel();
            Reloading = false;
        }



        private void RedrawPanel()
        {
            int x = 2, y = 2;

            PanelFlowers.SuspendLayout();
            PanelFlowers.VerticalScroll.Value = 0;

            foreach (AdvPictureBox pic in Icons)
            {
                if (!pic.Flower.Filter)
                {
                    pic.Visible = false;
                    continue;
                }

                pic.Location = new Point(x, y);
                pic.Visible = true;

                x += 104;
                if (x >= 728) { x = 2; y += 104; }
            }

            PanelFlowers.ResumeLayout();
        }



        private void ChBox_CheckedChanged(object sender, EventArgs ev)
        {
            if (Reloading) return;

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



        private void TxBoxSearch_FocusEnter(object sender, EventArgs ev)
        {
            if ((TxBoxSearch.Text == TEXT_SEARCH) || (TxBoxSearch.Text.Length < 2)) TxBoxSearch.Text = "";
            TxBoxSearch.ForeColor = SystemColors.WindowText;
        }

        private void TxBoxSearch_FocusLeave(object sender, EventArgs ev)
        {
            if (TxBoxSearch.Text.Length < 2)
            {
                TxBoxSearch.Text = TEXT_SEARCH;
                TxBoxSearch.ForeColor = SystemColors.GrayText;
            }
        }



        private void OnSearchTimer()
        {
            SearchTimer.Stop();
            ReloadList();
        }

        private void TxBoxSearch_TextChanged(object sender, EventArgs ev)
        {
            SearchTimer.Stop();
            SearchTimer.Start();
        }
    }
}
