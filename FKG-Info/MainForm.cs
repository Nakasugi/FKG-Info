    using System;
using System.Drawing;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class MainForm : Form
    {
        private Animator Animation;

        private ContextMenu CMenu;

        private FlowerSelectControl FSCtrl;
        private EquipmentSelectControl ESCtrl;
        private VoiceControl VCCtrl;


        private Timer UpdateStatus;


        private enum Mode { Characters, Equipments, Furnitures, Enemies }
        private Mode CurrentMode;

        private Graphics GR;
        private Brush BrPink;
        private Brush BrBase;



        public class MenuColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected { get { return Color.FromArgb(255, 180, 255); } }
            public override Color MenuBorder { get { return Color.MediumPurple; } }
            public override Color MenuItemBorder { get { return Color.MediumPurple; } }
            public override Color MenuStripGradientBegin { get { return SystemColors.Menu; } }
            public override Color MenuStripGradientEnd { get { return SystemColors.Menu; } }
            public override Color ImageMarginGradientBegin { get { return SystemColors.Menu; } }
            public override Color ImageMarginGradientMiddle { get { return SystemColors.Menu; } }
            public override Color ImageMarginGradientEnd { get { return SystemColors.Menu; } }
            public override Color ToolStripDropDownBackground { get { return SystemColors.Menu; } }
            public override Color CheckBackground { get { return Color.Cyan; } }
            public override Color CheckSelectedBackground { get { return Color.Magenta; } }
            public override Color SeparatorDark { get { return Color.Black; } }
        }



        public MainForm()
        {
            InitializeComponent();

            GridInfo.DefaultCellStyle.SelectionBackColor = Color.White;
            GridInfo.DefaultCellStyle.SelectionForeColor = Color.Purple;

            Animation = new Animator();
            CMenu = new ContextMenu();

            Animation.InitializeAnimation(PicBoxBig);

            Animator.FillMenu(CMenu.MenuItems, MenuImageType_Click);
            Animator.FillMenu(MMItemImageType.DropDownItems, MenuImageType_Click);

            CMenu.MenuItems.Add("-");
            MenuItem mi = CMenu.MenuItems.Add("Delete Images", (s, e) => CMenuDelBloom_Click());

            MenuImageType_Click(Animator.Type.Stand);
            PicBoxBig.ContextMenu = CMenu;

            foreach (ToolStripMenuItem item in MMItemMode.DropDownItems)
                item.Click += (s, e) => MenuMode_Click(item.Name);

            MainMenu.Renderer= new ToolStripProfessionalRenderer(new MenuColorTable());

            UpdateStatus = new Timer();
            UpdateStatus.Interval = 333;
            UpdateStatus.Tick += (s, e) => OnStatusUpdate();
            UpdateStatus.Start();

            GR = CreateGraphics();
            BrBase = new SolidBrush(SystemColors.Control);
            BrPink = new SolidBrush(Color.Pink);

            FSCtrl = null;
            ESCtrl = null;
            VCCtrl = new VoiceControl(this);

            CurrentMode = Mode.Characters;
            FlowerSelect_Click(this, null);
        }



        /// <summary>
        /// Select image type
        /// </summary>
        /// <param name="type"></param>
        private void MenuImageType_Click(Animator.Type type, Animator.EmoType emo = Animator.EmoType.Normal)
        {
            Animation.ImageType = type;
            Animation.Emotion = emo;

            MenuItem ctmi = null;
            ToolStripMenuItem tsmi = null;

            string stype = Animator.GetMenuName(type);
            string semo = Animator.GetMenuName(emo);

            string sbup = Animator.GetMenuName(Animator.Type.Bustup);


            foreach (MenuItem mi in CMenu.MenuItems)
            {
                if (mi.Text == sbup) ctmi = mi;
                if (mi.Text == stype)
                { 
                    if (mi.Text != sbup) mi.Checked = true;
                }
                else mi.Checked = false;
            }

            foreach (ToolStripMenuItem mi in MMItemImageType.DropDownItems)
            {
                if (mi.Text == sbup) tsmi = mi;
                if (mi.Text == stype)
                {
                    if (mi.Text != sbup) mi.Checked = true;
                }
                else mi.Checked = false;
            }


            if (ctmi != null)
                foreach (MenuItem mi in ctmi.MenuItems)
                {
                    mi.Checked = false;
                    if ((type == Animator.Type.Bustup) && (mi.Text == semo)) mi.Checked = true;
                }

            if (tsmi != null)
                foreach (ToolStripMenuItem mi in tsmi.DropDownItems)
                {
                    mi.Checked = false;
                    if ((type == Animator.Type.Bustup) && (mi.Text == semo)) mi.Checked = true;
                }



            if (!Program.DB.IsSelected()) return;

            Animation.Flower = Program.DB.GetSelected();
            PicBoxBig.AsyncLoadImage(Animation);
        }



        /// <summary>
        /// Select current mode
        /// </summary>
        /// <param name="mode"></param>
        private void MenuMode_Click(string mode)
        {
            bool changed = false;

            foreach (ToolStripMenuItem mi in MMItemMode.DropDownItems)
            {
                if (mi.Name == mode)
                {
                    if (!mi.Checked)
                    {
                        mi.Checked = true;
                        changed = true;
                    }
                }
                else
                {
                    mi.Checked = false;
                }
            }

            if (!changed) return;

            VCCtrl.Hide();

            CloseFlowerSelector();
            CloseEquipmentSelector();

            PicBoxIconBase.Clear();
            PicBoxIconAwak.Clear();
            PicBoxIconBloom.Clear();

            Program.DB.Unselect();
            GridInfo.Rows.Clear();

            if (mode == MMItemModeChara.Name)
            {
                CurrentMode = Mode.Characters;
                PicBoxBig.Visible = false;
                FSCtrl = new FlowerSelectControl(this);

                BtSelect.Enabled = true;
                BtEquip.Enabled = true;
                BtVoices.Enabled = true;
                MMItemSelect.Enabled = true;
            }

            if (mode == MMItemModeEquip.Name)
            {
                CurrentMode = Mode.Equipments;
                PicBoxBig.Visible = false;
                ESCtrl = new EquipmentSelectControl(this);

                BtSelect.Enabled = false;
                BtEquip.Enabled = false;
                BtVoices.Enabled = false;
                MMItemSelect.Enabled = false;
            }

            if (mode == MMItemModeFurniture.Name)
            {
                CurrentMode = Mode.Furnitures;
                PicBoxBig.Visible = false;

                BtSelect.Enabled = false;
                BtEquip.Enabled = false;
                BtVoices.Enabled = false;
                MMItemSelect.Enabled = false;
            }

            if (mode == MMItemModeEnemy.Name)
            {
                CurrentMode = Mode.Enemies;
                PicBoxBig.Visible = false;

                BtSelect.Enabled = false;
                BtEquip.Enabled = false;
                BtVoices.Enabled = false;
                MMItemSelect.Enabled = false;
            }
        }



        private void CMenuDelBloom_Click()
        {
            if (!Program.DB.IsSelected()) return;
            int id = Program.DB.GetSelected().GetImageEvolID(Animation.Evolution);
            if (id == 0) return;
            Program.ImageLoader.DeleteImages(id);
            Program.DB.Unselect();
            ReloadFlower();

            CloseFlowerSelector();
            PicBoxBig.Visible = false;
            FSCtrl = new FlowerSelectControl(this);
        }



        /// <summary>
        /// Information about downloading process
        /// </summary>
        private void OnStatusUpdate()
        {
            if (_lbLoading.InvokeRequired)
            {
                _lbLoading.Invoke(new Action(OnStatusUpdate));
                return;
            }

            if (Program.ImageLoader.Count == 0)
            {
                if (_lbLoading.Visible) _lbLoading.Visible = false;
            }
            else
            {
                if (!_lbLoading.Visible) _lbLoading.Visible = true;
                ImageDownloader imdw = Program.ImageLoader;
                string ldst = String.Format("{0} loading", imdw.Count);
                if (imdw.Queue - imdw.Count > 0) ldst += String.Format(", {0} in queue", imdw.Queue - imdw.Count);
                ldst += " ...";
                _lbLoading.Text = ldst;
            }
        }



        public void SelectFromSelector(FlowerInfo flower, bool selectorHide)
        {
            Program.DB.Select(flower);

            if (selectorHide)
            {
                FSCtrl.Visible = false;
                PicBoxBig.Visible = true;
            }

            ReloadFlower();
        }



        public void SelectFromSelector(EquipmentInfo equip)
        {
            PicBoxIconBase.AsyncLoadImage(equip);
            equip.FillGrid(GridInfo);
        }



        /// <summary>
        /// Update all information and images for selected flower
        /// </summary>
        private void ReloadFlower()
        {
            if (!Program.DB.IsSelected())
            {
                PicBoxBig.Image = null;
                PicBoxIconBase.Image = null;
                PicBoxIconAwak.Image = null;
                PicBoxIconBloom.Image = null;
                GridInfo.Rows.Clear();
                return;
            }

            Animation.Flower = Program.DB.GetSelected();
            Animation.Exclusive = ChBoxExSkin.Checked;
            PicBoxBig.AsyncLoadImage(Animation);

            Animator icon = new Animator(Animation);
            icon.ImageType = Animator.Type.IconLarge;
            icon.Exclusive = false;

            icon.Evolution = FlowerInfo.Evolution.Base;
            PicBoxIconBase.AsyncLoadImage(icon);
            icon.Evolution = FlowerInfo.Evolution.Awakened;
            PicBoxIconAwak.AsyncLoadImage(icon);
            icon.Evolution = FlowerInfo.Evolution.Bloomed;
            PicBoxIconBloom.AsyncLoadImage(icon);

            UpdateFlowerInfo();
        }



        /// <summary>
        /// Just fill DataGridView if flower selected
        /// </summary>
        private void UpdateFlowerInfo()
        {
            if (!Program.DB.IsSelected()) return;

            FlowerInfo flower = Program.DB.GetSelected();

            flower.FillGrid(GridInfo, Animation.Evolution);
            ChBoxExSkin.Enabled = flower.HasExclusiveSkin();
        }



        private void FlowerSelect_Click(object sender, EventArgs ev)
        {
            if (CurrentMode != Mode.Characters) return;

            if (FSCtrl != null)
            {
                if(FSCtrl.Visible)
                {
                    FSCtrl.Hide();
                    VCCtrl.Hide();
                    PicBoxBig.Show();
                }
                else if(VCCtrl.Visible)
                {
                    VCCtrl.Hide();
                    FSCtrl.Show();
                    PicBoxBig.Hide();
                }
                else
                {
                    FSCtrl.Show();
                    VCCtrl.Hide();
                    PicBoxBig.Hide();
                }
                return;
            }

            PicBoxBig.Hide();
            VCCtrl.Hide();
            FSCtrl = new FlowerSelectControl(this);
        }



        private void CloseFlowerSelector()
        {
            if (FSCtrl == null) return;

            FSCtrl.Dispose();
            PicBoxBig.Visible = true;
            FSCtrl = null;
        }

        private void CloseEquipmentSelector()
        {
            if (ESCtrl == null) return;

            ESCtrl.Dispose();
            PicBoxBig.Visible = true;
            ESCtrl = null;
        }



        private void PicBoxIcon_DoubleClick(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;

            FSCtrl.Visible = false;
            PicBoxBig.Visible = true;
        }

        private void PicBoxIconBase_Click(object sender, EventArgs ev)
        {
            DrawIconSelectionBorder(FlowerInfo.Evolution.Base);
            Animation.Evolution = FlowerInfo.Evolution.Base;

            if (!Program.DB.IsSelected()) return;
            Animation.Exclusive = ChBoxExSkin.Checked;
            PicBoxBig.AsyncLoadImage(Animation);
            UpdateFlowerInfo();
        }

        private void PicBoxIconAwak_Click(object sender, EventArgs ev)
        {
            DrawIconSelectionBorder(FlowerInfo.Evolution.Awakened);
            Animation.Evolution = FlowerInfo.Evolution.Awakened;

            if (!Program.DB.IsSelected()) return;
            Animation.Exclusive = ChBoxExSkin.Checked;
            PicBoxBig.AsyncLoadImage(Animation);
            UpdateFlowerInfo();
        }

        private void PicBoxIconBloom_Click(object sender, EventArgs ev)
        {
            DrawIconSelectionBorder(FlowerInfo.Evolution.Bloomed);
            Animation.Evolution = FlowerInfo.Evolution.Bloomed;

            if (!Program.DB.IsSelected()) return;
            Animation.Exclusive = ChBoxExSkin.Checked;
            PicBoxBig.AsyncLoadImage(Animation);
            UpdateFlowerInfo();
        }
        


        private void ChBoxAbilityTranslation_CheckedChanged(object sender, EventArgs ev)
        {
            UpdateFlowerInfo();
        }



        private void MMItemOptions_Click(object sender, EventArgs ev)
        {
            OptionsForm opt = new OptionsForm();
            opt.ShowDialog(this);
        }

        private void MMItemFileGetMaster_Click(object sender, EventArgs ev)
        {
            new getMaster();
        }

        private void MMItemFileImportMaster_Click(object sender, EventArgs ev)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();


            fileOpen.InitialDirectory = Program.DB.DataFolder;
            fileOpen.Filter = "Any file (*.*)|*.*";
            fileOpen.FilterIndex = 0;
            fileOpen.RestoreDirectory = true;

            if (fileOpen.ShowDialog() != DialogResult.OK) return;

            MasterData md = new MasterData(fileOpen.FileName);
            if (md.Ok) Program.DB.Master = md;
        }


        private void MMItemFileExportMaster_Click(object sender, EventArgs ev)
        {
            new ExportMasterForm().ShowDialog(this);
        }

        
        private void MMItemFileExportIDs_Click(object sender, EventArgs ev)
        {
            Program.DB.Master.ExportIDs();
            MessageBox.Show("Done");
        }




        public void LoadingControlsMessage(bool visible) { _lbWait.Visible = visible; _lbWait.Refresh(); }

        private void MainForm_Closing(object sender, FormClosingEventArgs ev) { Program.DB.Running = false; }

        private void MMItemAbout_Click(object sender, EventArgs ev) { new AboutForm().ShowDialog(this); }

        private void PicBoxBig_Click(object sender, EventArgs ev) { Animation.AnimationClick(); }

        private void ChBoxExSkin_CheckedChanged(object sender, EventArgs ev) { ReloadFlower(); }


        private void GridInfo_MouseMove(object sender, MouseEventArgs ev) { GridInfo.Focus(); }



        private void DrawIconSelectionBorder(int evol)
        {
            Brush[] brs = new Brush[3];

            for (int i = 0; i < 3; i++) if (i == evol) brs[i] = BrPink; else brs[i] = BrBase;

            GR.FillRectangle(brs[0], GetExtRectangle(PicBoxIconBase));
            GR.FillRectangle(brs[1], GetExtRectangle(PicBoxIconAwak));
            GR.FillRectangle(brs[2], GetExtRectangle(PicBoxIconBloom));
        }

        private Rectangle GetExtRectangle(PictureBox pic)
        {
            return new Rectangle(pic.Left - 2, pic.Top - 2, pic.Width + 4, pic.Height + 4);
        }



        private void MainForm_Shown(object sender, EventArgs ev)
        {
            //CurrentMode = Mode.Characters;
            //FlowerSelect_Click(this, null);

            DrawIconSelectionBorder(0);
            TopMost = true;
            BringToFront();
            TopMost = false;
        }



        private void BtEquip_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;

            var eqs = Program.DB.GetFlowerEquipment(Program.DB.GetSelected().ID);

            new EquipFastViewForm(eqs).ShowDialog(this);
        }



        private void BtVoices_Click(object sender, EventArgs e)
        {
            if (CurrentMode != Mode.Characters) return;
            if (!Program.DB.IsSelected()) return;

            if (!VCCtrl.Visible)
            {
                VCCtrl.Show();
                FSCtrl.Hide();
                PicBoxBig.Hide();
            }
            else
            {
                VCCtrl.Hide();
                FSCtrl.Hide();
                PicBoxBig.Show();
            }
        }
    }
}
