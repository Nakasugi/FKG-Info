using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class EquipmentSelectControl : UserControl
    {
        private bool Reloading;

        private List<EquipmentInfo> Equipments;
        private List<FastIcon> Icons;

        private ToolTip TTip;



        private struct SortBy
        {
            public const string Default = "Default";
            public const string Name = "By Name";
            public const string Attack = "By Attack";
            public const string Defense = "By Defense";
            public const string Total = "By Total";
        }



        /// <summary>
        /// Just constructor
        /// </summary>
        /// <param name="main"></param>
        public EquipmentSelectControl(MainForm main)
        {
            components = new System.ComponentModel.Container();

            Visible = false;
            main.LoadingControlsMessage(true);
            InitializeComponent();
            SuspendLayout();

            Parent = main;
            Reloading = true;
            Location = new Point(0, 26);
            TTip = new ToolTip();
            Equipments = Program.DB.Equipments;
            Icons = new List<FastIcon>();

            FastIcon icon;

            foreach (EquipmentInfo equip in Equipments)
            {
                icon = new FastIcon((MainForm)Parent, equip);
                icon.Name = equip.ID.ToString();
                /*
                icon.Width = 100;
                icon.Height = 100;
                icon.SetImage(Properties.Resources.equip_default, false);
                icon.AsyncLoadImage(equip);
                */
                icon.Visible = false;
                Icons.Add(icon);

                components.Add(icon); // for auto disposing

                string ttip = equip.KName;
                TTip.SetToolTip(icon, ttip);

                PanelEquipments.Controls.Add(icon);
            }


            foreach (System.Reflection.FieldInfo fi in typeof(SortBy).GetFields())
            {
                CmBoxSort.Items.Add(fi.GetValue(null).ToString());
            }

            if (CmBoxSort.Items.Count > 0) CmBoxSort.SelectedIndex = 0;


            Reloading = false;
            ApplyFilter();
            ResumeLayout();
            main.LoadingControlsMessage(false);
            Visible = true;
        }



        private void ApplyFilter()
        {
            if (Reloading) return;
            Reloading = true;

            foreach (EquipmentInfo equip in Equipments)
            {
                equip.Filter = false;

                if (ChBoxSetMode.Checked)
                {
                    if (equip.ESetID != 0) equip.Filter = true;
                    continue;
                }

                switch (equip.Rarity)
                {
                    case 0x01: if (!ChBoxS2.Checked) continue; break;
                    case 0x03: if (!ChBoxS3.Checked) continue; break;
                    case 0x05: if (!ChBoxS4.Checked) continue; break;
                    case 0x07: if (!ChBoxS5.Checked) continue; break;
                    default: continue;
                }

                switch (equip.Type)
                {
                    case 1: if (!ChBoxRing.Checked) continue; break;
                    case 2: if (!ChBoxBracelet.Checked) continue; break;
                    case 3: if (!ChBoxNecklace.Checked) continue; break;
                    case 4: if (!ChBoxEarrings.Checked) continue; break;
                    case 8: if (!ChBoxPersonal.Checked) continue; break;
                    case 12: if (!ChBoxWeapon.Checked) continue; break;
                    default: break;
                }

                equip.Filter = true;
            }


            BaseInfo.SortBy sortType = BaseInfo.SortBy.Default;
            if (ChBoxSetMode.Checked)
            {
                switch (CmBoxSort.Text)
                {
                    case SortBy.Attack: sortType = BaseInfo.SortBy.SetAttack; break;
                    case SortBy.Defense: sortType = BaseInfo.SortBy.SetDefense; break;
                    case SortBy.Total: sortType = BaseInfo.SortBy.SetTotalStats; break;
                    default: break;
                }
            }
            else
            {
                switch (CmBoxSort.Text)
                {
                    case SortBy.Name: sortType = BaseInfo.SortBy.Name; break;
                    case SortBy.Attack: sortType = BaseInfo.SortBy.Attack; break;
                    case SortBy.Defense: sortType = BaseInfo.SortBy.Defense; break;
                    case SortBy.Total: sortType = BaseInfo.SortBy.OverallForce; break;
                    default: break;
                }
            }

            Icons.Sort((pb1, pb2) => pb1.Equipment.CompareTo(pb2.Equipment, sortType));

            RedrawPanel();
            Reloading = false;
        }



        private void RedrawPanel()
        {
            int x = 2, y = 2;

            PanelEquipments.SuspendLayout();
            PanelEquipments.VerticalScroll.Value = 0;

            foreach (FastIcon pic in Icons)
            {
                if (!pic.Equipment.Filter)
                {
                    pic.Visible = false;
                    continue;
                }

                pic.Location = new Point(x, y);
                pic.Visible = true;
                
                x += 104;
                if (x >= 728) { x = 2; y += 104; }
            }

            PanelEquipments.ResumeLayout();
        }



        private void ChBox_CheckedChanged(object sender, EventArgs ev)
        {
            if (Reloading) return;

            ApplyFilter();
        }



        private void BtInv_Click(object sender, EventArgs ev)
        {
            ChBoxRing.Checked = !ChBoxRing.Checked;
            ChBoxBracelet.Checked = !ChBoxBracelet.Checked;
            ChBoxNecklace.Checked = !ChBoxNecklace.Checked;
            ChBoxEarrings.Checked = !ChBoxEarrings.Checked;
            ChBoxPersonal.Checked = !ChBoxPersonal.Checked;
            ChBoxWeapon.Checked = !ChBoxWeapon.Checked;
            ApplyFilter();
        }
    }
}
