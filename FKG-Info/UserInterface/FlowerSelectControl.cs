using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FKG_Info.FKG_GameData;

namespace FKG_Info.UserInterface
{
    public partial class FlowerSelectControl : UserControl
    {
        private FlowersList Flowers;
        private List<FastIcon> Icons;

        private FlowerInfo SelectedFlower;

        private bool Reloading;
        private bool Selecting;
        private bool AllTypesSelected;

        private ToolTip TTip;

        private const string TEXT_SEARCH = "Search...";

        private Timer SearchTimer;



        struct SortBy
        {
            public const string Default = "Sort by Default";
            public const string Category = "By Category";
            public const string TotalMaxStats = "By Overall Force";
            public const string Attack = "By Attack";
            public const string Defense = "By Defense";
            public const string HitPoints = "By Hit Points";
            public const string Speed = "By Speed";
        }



        /// <summary>
        /// Just Constructor
        /// </summary>
        /// <param name="main"></param>
        public FlowerSelectControl(MainForm main)
        {
            components = new System.ComponentModel.Container();

            SelectedFlower = null;

            Visible = false;
            main.LoadingControlsMessage(true);
            
            InitializeComponent();

            SuspendLayout();

            Parent = main;
            Reloading = true;
            Location = new Point(0, 26);

            foreach (string nt in FlowerInfo.Nations) CmBoxNation.Items.Add(nt);
            CmBoxNation.Items[0] = "All Nations";
            CmBoxNation.SelectedIndex = 0;

            string[] atags = Program.DB.GetAbilitiesTags();
            CmBoxAbility01.Items.Add("All Abilities");
            CmBoxAbility01.Items.AddRange(atags);
            CmBoxAbility01.SelectedIndex = 0;
            CmBoxAbility02.Items.Add("All Abilities");
            CmBoxAbility02.Items.AddRange(atags);
            CmBoxAbility02.SelectedIndex = 0;
            atags = null;

            ChBoxAcc1Has.Text = Program.DB.Account1Name;
            ChBoxAcc2Has.Text = Program.DB.Account2Name;

            foreach (FlowerInfo.SpecFilter spec in Enum.GetValues(typeof(FlowerInfo.SpecFilter)))
                CmBoxSpecFilter.Items.Add(spec.ToString().Replace("_", " "));
            CmBoxSpecFilter.SelectedIndex = 0;

            foreach (System.Reflection.FieldInfo fi in typeof(SortBy).GetFields())
                CmBoxSort.Items.Add(fi.GetValue(null).ToString());
            CmBoxSort.SelectedIndex = 0;

            foreach (string vr in FlowerInfo.VariationNames)
                CmBoxVariations.Items.Add(vr);
            CmBoxVariations.SelectedIndex = 0;

            SearchTimer = new Timer();
            SearchTimer.Interval = 1000;
            SearchTimer.Tick += (s, e) => OnSearchTimer();

            //TxBoxSearch.Text = "🔍 Search...";
            TxBoxSearch.Text = TEXT_SEARCH;
            TxBoxSearch.ForeColor = SystemColors.GrayText;

            TTip = new ToolTip();

            Flowers = Program.DB.Flowers;
            Icons = new List<FastIcon>();

            FastIcon icon;

            Animator ani = new Animator();
            ani.ImageType = Animator.Type.IconLarge;
            
            foreach (FlowerInfo flower in Flowers)
            {
                ani.Flower = flower;
                icon = new FastIcon((MainForm)Parent, flower);
                icon.Name = flower.ID.ToString();
                icon.Visible = false;
                Icons.Add(icon);

                components.Add(icon); // for auto disposing

                string ttip = flower.Name.Kanji + "\r\n" + flower.Name.Romaji;
                if (flower.Name.EngDMM != null) ttip += "\r\nDMM: " + flower.Name.EngDMM;
                if (flower.Name.EngNutaku != null) ttip += "\r\nNutaku: " + flower.Name.EngNutaku;
                TTip.SetToolTip(icon, ttip);

                PanelFlowers.Controls.Add(icon);
            }
            
            Reloading = false;
            ReloadList();
            ResumeLayout();
            main.LoadingControlsMessage(false);
            Selecting = false;
            AllTypesSelected = true;
            Visible = true;

            Flowers.ClearHistory();
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

                if (searching)
                {
                    if (flower.CheckNames(tofind)) flower.Filter = true;
                    continue;
                }

                if ((spec != FlowerInfo.SpecFilter.Can_Grow) && (spec != FlowerInfo.SpecFilter.Has_Exclusive_Skin))
                {
                    if (flower.CheckVariation(CmBoxVariations.SelectedIndex, true)) continue;
                }

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
                    case FlowerInfo.SpecFilter.All_Knights: if (flower.IsKnight) break; continue;
                    //case FlowerInfo.SpecFilter.Has_Bloom_Form: if (flower.CheckBloomForm()) break; continue;
                    case FlowerInfo.SpecFilter.No_Bloom_Form: if (flower.CheckBloomForm(false, true)) break; continue;
                    case FlowerInfo.SpecFilter.Has_Exclusive_Skin: if (flower.HasAdditionalSkin()) break; continue;
                    case FlowerInfo.SpecFilter.Can_Grow: if (flower.CanGrow) break; continue;
                    default: if (flower.CheckCategory(spec)) break; continue;
                }


                switch (ChBoxEventKnights.CheckState)
                {
                    case CheckState.Checked: if (flower.CheckIsEvent(false)) break; continue;
                    case CheckState.Unchecked: if (flower.CheckIsEvent(true)) break; continue;
                    case CheckState.Indeterminate:
                    default: break;
                }

                switch (ChBoxBloomCG.CheckState)
                {
                    case CheckState.Checked: if (flower.CheckBloomForm(true)) break; continue;
                    case CheckState.Unchecked: if (flower.CheckBloomForm(true, true)) break; continue;
                    case CheckState.Indeterminate:
                    default: break;
                }


                switch (ChBoxAcc1Filter.CheckState)
                {
                    case CheckState.Checked: if (Flowers.CheckAccStatus(flower.RefID, 1)) break; continue;
                    case CheckState.Unchecked: if (!Flowers.CheckAccStatus(flower.RefID, 1)) break; continue;
                    case CheckState.Indeterminate:
                    default: break;
                }

                switch (ChBoxAcc2Filter.CheckState)
                {
                    case CheckState.Checked: if (Flowers.CheckAccStatus(flower.RefID, 2)) break; continue;
                    case CheckState.Unchecked: if (!Flowers.CheckAccStatus(flower.RefID, 2)) break; continue;
                    case CheckState.Indeterminate:
                    default: break;
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


                flower.Filter = true;
            }


            BaseInfo.SortBy sortType = BaseInfo.SortBy.Default;
            switch (CmBoxSort.Text)
            {
                case SortBy.Category: sortType = BaseInfo.SortBy.Category; break;
                case SortBy.TotalMaxStats: sortType = BaseInfo.SortBy.OverallForce; break;
                case SortBy.Attack: sortType = BaseInfo.SortBy.Attack; break;
                case SortBy.Defense: sortType = BaseInfo.SortBy.Defense; break;
                case SortBy.HitPoints: sortType = BaseInfo.SortBy.HitPoints; break;
                case SortBy.Speed: sortType = BaseInfo.SortBy.Speed; break;
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

            foreach (FastIcon pic in Icons)
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
            if (Reloading || Selecting) return;

            Selecting = true;

            int tcnt = 0;

            if (ChBoxSlash.Checked) tcnt++;
            if (ChBoxBlunt.Checked) tcnt++;
            if (ChBoxPierce.Checked) tcnt++;
            if (ChBoxMagic.Checked) tcnt++;

            if ((tcnt == 3) && (AllTypesSelected))
            {
                AllTypesSelected = false;
                ReversTypeSelection();
            }

            if (tcnt == 4) AllTypesSelected = true;

            ClearSearch();
            ReloadList();
            Selecting = false;
        }



        private void BtInv_Click(object sender, EventArgs ev)
        {
            Selecting = true;
            ReversTypeSelection();
            Selecting = false;
            ChBox_CheckedChanged(sender, ev);
        }



        private void ReversTypeSelection()
        {
            ChBoxSlash.Checked = !ChBoxSlash.Checked;
            ChBoxBlunt.Checked = !ChBoxBlunt.Checked;
            ChBoxPierce.Checked = !ChBoxPierce.Checked;
            ChBoxMagic.Checked = !ChBoxMagic.Checked;
        }



        private void TxBoxSearch_FocusEnter(object sender, EventArgs ev)
        {
            if ((TxBoxSearch.Text == TEXT_SEARCH) || (TxBoxSearch.Text.Length < 2)) TxBoxSearch.Text = "";
            TxBoxSearch.ForeColor = SystemColors.WindowText;
        }

        private void TxBoxSearch_FocusLeave(object sender, EventArgs ev)
        {
            if (TxBoxSearch.Text.Length < 2) ClearSearch();
        }

        public void SetSearchText(string text)
        {
            TxBoxSearch.Text = text;
            if ((text.Length > 0) || (text != TEXT_SEARCH)) TxBoxSearch.ForeColor = SystemColors.WindowText;
        }

        public void ClearSearch()
        {
            TxBoxSearch.Text = TEXT_SEARCH;
            TxBoxSearch.ForeColor = SystemColors.GrayText;
        }

        private void OnSearchTimer()
        {
            SearchTimer.Stop();
            ReloadList();
        }

        private void TxBoxSearch_TextChanged(object sender, EventArgs ev)
        {
            if (SearchTimer == null) return;

            SearchTimer.Stop();
            SearchTimer.Start();
        }

        private void BtClrSearch_Click(object sender, EventArgs ev) { ClearSearch(); }



        public void SelectFlower(FlowerInfo flower)
        {
            bool selecting = Selecting;

            Selecting = true;

            SelectedFlower = flower;

            if (SelectedFlower != null)
            {
                ChBoxAcc1Has.Checked = Flowers.CheckAccStatus(SelectedFlower.RefID, 1);
                ChBoxAcc2Has.Checked = Flowers.CheckAccStatus(SelectedFlower.RefID, 2);
            }

            Selecting = selecting;
        }



        private void ChBoxAccHas_Changed(object sender, EventArgs ev)
        {
            if (Reloading || Selecting) return;

            if (SelectedFlower != null)
            {
                if (ChBoxAcc1Has.Checked) Flowers.AddToAccount(SelectedFlower.RefID, 1); else Flowers.RemoveFromAccount(SelectedFlower.RefID, 1);
                if (ChBoxAcc2Has.Checked) Flowers.AddToAccount(SelectedFlower.RefID, 2); else Flowers.RemoveFromAccount(SelectedFlower.RefID, 2);

                Program.DB.OptionsChanged();
            }
        }



        private void CmBoxVariations_CheckedChanged(object sender, EventArgs ev)
        {
            if (Selecting) return;

            FlowerInfo.SpecFilter spec = (FlowerInfo.SpecFilter)Enum.Parse(typeof(FlowerInfo.SpecFilter), CmBoxSpecFilter.Text.Replace(" ", "_"));

            Selecting = true;
            if
            (
                (
                    (CmBoxVariations.SelectedIndex != FlowerInfo.Variation.Evolved) &&
                    (spec == FlowerInfo.SpecFilter.No_Bloom_Form)
                )
                ||
                (spec == FlowerInfo.SpecFilter.Has_Exclusive_Skin)
                ||
                (spec == FlowerInfo.SpecFilter.Can_Grow)
            )
                CmBoxSpecFilter.SelectedIndex = 0;

            ClearSearch();
            ReloadList();
            Selecting = false;
        }



        private void CmBoxSpecFilter_CheckedChanged(object sender, EventArgs ev)
        {
            if (Selecting) return;

            FlowerInfo.SpecFilter spec = (FlowerInfo.SpecFilter)Enum.Parse(typeof(FlowerInfo.SpecFilter), CmBoxSpecFilter.Text.Replace(" ", "_"));

            Selecting = true;
            //if (spec == FlowerInfo.SpecFilter.Has_Exclusive_Skin) CmBoxVariations.SelectedIndex = FlowerInfo.Variation.Base;
            if (spec == FlowerInfo.SpecFilter.No_Bloom_Form) CmBoxVariations.SelectedIndex = FlowerInfo.Variation.Evolved;

            ClearSearch();
            ReloadList();
            Selecting = false;
        }
    }
}
