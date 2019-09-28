using System;
using System.Drawing;
using System.Windows.Forms;
using FKG_Info.FKG_GameData;

namespace FKG_Info.UserInterface
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
            GridInfo.DefaultCellStyle.SelectionForeColor = Color.Blue;

            Animation = new Animator();
            CMenu = new ContextMenu();

            Animation.InitializeAnimation(PicBoxBig);

            MMItemImageType.DropDownItems.Add("Mobile", null, (s, e) => ImagePlatformChange());
            MMItemImageType.DropDownItems.Add("-");
            CMenu.MenuItems.Add("Mobile", (s, e) => ImagePlatformChange());
            CMenu.MenuItems.Add("-");

            Animator.FillMenu(CMenu.MenuItems, MenuImageType_Click);
            Animator.FillMenu(MMItemImageType.DropDownItems, MenuImageType_Click);

            CMenu.MenuItems.Add("-");
            CMenu.MenuItems.Add("Download Images", (s, e) => CMenuDownloadAll_Click());
            CMenu.MenuItems.Add("Delete Images", (s, e) => CMenuDelBloom_Click());
            

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



        private void ImagePlatformChange()
        {
            Animation.SetMobile(!Animation.Mobile);
            PicBoxBig.AsyncLoadImage(Animation);
            MenuPlatformChange(Animation.Mobile);
        }



        private void MenuPlatformChange(bool mobile)
        {
            MenuItem cmi = CMenu.MenuItems[0];
            ToolStripMenuItem tmi = MMItemImageType.DropDownItems[0] as ToolStripMenuItem;

            cmi.Checked = mobile;
            tmi.Checked = mobile;
        }



        private void MenuPlatformEnabled(bool enable)
        {
            MenuItem cmi = CMenu.MenuItems[0];
            ToolStripMenuItem tmi = MMItemImageType.DropDownItems[0] as ToolStripMenuItem;

            cmi.Enabled = enable;
            tmi.Enabled = enable;
        }



        /// <summary>
        /// Select image type
        /// </summary>
        /// <param name="type"></param>
        private void MenuImageType_Click(Animator.Type type, Animator.EmoType emo = Animator.EmoType.Normal)
        {
            Animation.SetImageParams(type, emo);

            MenuItem ctmi = null;
            ToolStripMenuItem tsmi = null;

            string stype = Animator.GetMenuName(type);
            string semo = Animator.GetMenuName(emo);

            string sbup = Animator.GetMenuName(Animator.Type.Bustup);


            for (int i = 2; i < CMenu.MenuItems.Count; i++)
            {
                MenuItem mi = CMenu.MenuItems[i];

                if (mi.Text == sbup) ctmi = mi;
                if (mi.Text == stype)
                {
                    if (mi.Text != sbup) mi.Checked = true;
                }
                else mi.Checked = false;
            }

            for (int i = 2; i < MMItemImageType.DropDownItems.Count; i++)
            {
                if (!(MMItemImageType.DropDownItems[i] is ToolStripMenuItem mi)) continue;

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


            // Diseble/enable mobile for home image
            if (Animation.ImageType == Animator.Type.Home)
            {
                Animation.SetMobile(false);
                MenuPlatformChange(false);
                MenuPlatformEnabled(false);
            }
            else
            {
                MenuPlatformEnabled(true);
            }

            if (!Program.DB.Flowers.IsSelected()) return;

            Animation.SetFlower(Program.DB.Flowers.GetSelected());
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

            IconMain.Clear();
            Icon2nd.Clear();
            Icon3rd.Clear();

            Program.DB.Flowers.Unselect();
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
                BtEvolutions.Visible = true;
                BtVariations.Visible = true;
                BtPrevEvolution.Visible = true;
                Icon2nd.Visible = true;
                Icon3rd.Visible = true;
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
                BtEvolutions.Visible = false;
                BtVariations.Visible = false;
                BtPrevEvolution.Visible = false;
                Icon2nd.Visible = false;
                Icon3rd.Visible = false;
            }

            if (mode == MMItemModeFurniture.Name)
            {
                CurrentMode = Mode.Furnitures;
                PicBoxBig.Visible = false;

                BtSelect.Enabled = false;
                BtEquip.Enabled = false;
                BtVoices.Enabled = false;
                MMItemSelect.Enabled = false;
                BtEvolutions.Visible = false;
                BtVariations.Visible = false;
                BtPrevEvolution.Visible = false;
                Icon2nd.Visible = false;
                Icon3rd.Visible = false;
            }

            if (mode == MMItemModeEnemy.Name)
            {
                CurrentMode = Mode.Enemies;
                PicBoxBig.Visible = false;

                BtSelect.Enabled = false;
                BtEquip.Enabled = false;
                BtVoices.Enabled = false;
                MMItemSelect.Enabled = false;
                BtEvolutions.Visible = false;
                BtVariations.Visible = false;
                BtPrevEvolution.Visible = false;
                Icon2nd.Visible = false;
                Icon3rd.Visible = false;
            }
        }



        private void CMenuDelBloom_Click()
        {
            if (!Program.DB.Flowers.IsSelected()) return;

            FlowerInfo flower = Program.DB.Flowers.GetSelected();

            Program.ImageLoader.DeleteImages(flower.ID);
            Program.FlowerIcons.UpdateIconImage(flower.ID);
            Program.DB.Flowers.Unselect();
            ReloadFlower();

            CloseFlowerSelector();
            PicBoxBig.Visible = false;
            FSCtrl = new FlowerSelectControl(this);
        }



        private void CMenuDownloadAll_Click()
        {
            if (!Program.DB.Flowers.IsSelected()) return;
            FlowerInfo flower = Program.DB.Flowers.GetSelected();
            if (flower == null) return;

            var frames = Animator.GetAllFrames(flower);

            foreach (var frame in frames) Program.ImageLoader.GetImage(frame, null);
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

            var imdw = Program.ImageLoader;

            if (imdw.DwCount + imdw.InQueue == 0)
            {
                if (_lbLoading.Visible) _lbLoading.Visible = false;
            }
            else
            {
                if (!_lbLoading.Visible) _lbLoading.Visible = true;

                string ldst = string.Format("{0} loading, {1} in queue ...", imdw.DwCount, imdw.InQueue);
                _lbLoading.Text = ldst;
            }
        }



        public void SelectFromSelector(FlowerInfo flower, bool selectorHide)
        {
            Program.DB.Flowers.Select(flower.ID);

            FSCtrl.SelectFlower(flower);

            if (selectorHide)
            {
                FSCtrl.Visible = false;
                PicBoxBig.Visible = true;
            }

            ReloadFlower();
        }



        public void SelectFromSelector(EquipmentInfo equip)
        {
            IconMain.SetIcon(equip);
            equip.FillGrid(GridInfo);
        }



        /// <summary>
        /// Update all information and images for selected flower
        /// </summary>
        private void ReloadFlower()
        {
            if (!Program.DB.Flowers.IsSelected())
            {
                PicBoxBig.SetImage(null);
                IconMain.Clear();
                Icon2nd.Clear();
                Icon3rd.Clear();
                GridInfo.Rows.Clear();
                return;
            }

            FlowerInfo flower = Program.DB.Flowers.GetSelected();
            Animation.SetFlower(flower);
            Animation.SetSkinIndex(CmBoxSkins.SelectedIndex);
            PicBoxBig.AsyncLoadImage(Animation);
            CmBoxSkins.Items.Clear();
            CmBoxSkins.Items.AddRange(flower.GetSkinNames());
            CmBoxSkins.SelectedIndex = 0;

            IconMain.SetIcon(flower);
            Icon2nd.SetIcon(Program.DB.Flowers.GetNextEvolution(flower));

            UpdateFlowerInfo();
        }



        /// <summary>
        /// Just fill DataGridView if flower selected
        /// </summary>
        private void UpdateFlowerInfo()
        {
            if (!Program.DB.Flowers.IsSelected()) return;

            FlowerInfo flower = Program.DB.Flowers.GetSelected();

            flower.FillGrid(GridInfo);
            CmBoxSkins.Enabled = flower.HasAdditionalSkin();
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



        private void PicBoxIconBase_DoubleClick(object sender, EventArgs ev)
        {
            if (!Program.DB.Flowers.IsSelected()) return;

            FSCtrl.Visible = false;
            PicBoxBig.Visible = true;
        }

        private void PicBoxIconBase_Click(object sender, EventArgs ev)
        {

        }

        private void PicBoxIconV1_Click(object sender, EventArgs ev)
        {
            if ((CurrentMode == Mode.Characters) && (Icon2nd.Flower != null))
            {
                Program.DB.Flowers.Select(Icon2nd.Flower.ID);
                ReloadFlower();
            }

        }

        private void PicBoxIconV2_Click(object sender, EventArgs ev)
        {

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


        private void MMItemFileExportNames_Click(object sender, EventArgs ev)
        {
            Program.DB.ExportNames();
            MessageBox.Show("Done");
        }

        private void MMItemFileExportCurrent_Click(object sender, EventArgs e)
        {
            FlowerInfo flower = Program.DB.Flowers.GetSelected();
            if (flower == null) return;
            Program.DB.Master.ExportFlowerData(flower.RefID);
        }



        private void MMItemPrev_Click(object sender, EventArgs ev)
        {
            if (CurrentMode == Mode.Characters)
            {
                FlowerInfo flower = Program.DB.Flowers.GetPrev();
                if (flower != null) ReloadFlower();
            }
        }


        private void MMItemNext_Click(object sender, EventArgs ev)
        {
            if (CurrentMode == Mode.Characters)
            {
                FlowerInfo flower = Program.DB.Flowers.GetNext();
                if (flower != null) ReloadFlower();
            }
        }



        public void LoadingControlsMessage(bool visible) { _lbWait.Visible = visible; _lbWait.Refresh(); }

        private void MainForm_Closing(object sender, FormClosingEventArgs ev) { Program.Life.Kill(); }

        private void MMItemAbout_Click(object sender, EventArgs ev) { new AboutForm().ShowDialog(this); }

        private void PicBoxBig_Click(object sender, EventArgs ev) { Animation.AnimationClick(); }

        private void CmBoxSkins_SelectedIndexChanged(object sender, EventArgs e)
        {
            Animation.SetSkinIndex(CmBoxSkins.SelectedIndex);
            PicBoxBig.AsyncLoadImage(Animation);
        }

        private void GridInfo_MouseMove(object sender, MouseEventArgs ev) { GridInfo.Focus(); }



        private void MainForm_Shown(object sender, EventArgs ev)
        {
            Activate();
            SplashWindow.Stop();
        }



        private void BtEquip_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.Flowers.IsSelected()) return;

            var eqs = Program.DB.GetFlowerEquipment(Program.DB.Flowers.GetSelected().RefID);

            new EquipFastViewForm(eqs).ShowDialog(this);
        }



        private void BtVoices_Click(object sender, EventArgs ev)
        {
            if (CurrentMode != Mode.Characters) return;
            if (!Program.DB.Flowers.IsSelected()) return;

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




        private void BtEvolutions_Click(object sender, EventArgs ev)
        {
            if ((CurrentMode == Mode.Characters) && (Program.DB.Flowers.IsSelected()))
            {
                FlowerInfo flower = Program.DB.Flowers.GetSelected();
                if (flower == null) return;
                FSCtrl.SetSearchText("id=" + flower.RefID);
                FSCtrl.Show();
                VCCtrl.Hide();
                PicBoxBig.Hide();
            }
        }

        private void BtVariations_Click(object sender, EventArgs ev)
        {
            if ((CurrentMode == Mode.Characters) && (Program.DB.Flowers.IsSelected()))
            {
                FlowerInfo flower = Program.DB.Flowers.GetSelected();
                if (flower == null) return;
                FSCtrl.SetSearchText("name=" + flower.Name.Romaji);
                FSCtrl.Show();
                VCCtrl.Hide();
                PicBoxBig.Hide();
            }
        }

        private void BtPrevEvolution_Click(object sender, EventArgs ev)
        {
            if ((CurrentMode == Mode.Characters) && (Program.DB.Flowers.IsSelected()))
            {
                FlowerInfo flower = Program.DB.Flowers.GetSelected();
                if (flower == null) return;
                flower = Program.DB.Flowers.GetPrevEvolution(flower);
                if (flower == null) return;
                Program.DB.Flowers.Select(flower.ID);
                ReloadFlower();
            }
        }

        private void MMItemFileExportIcons_Click(object sender, EventArgs e)
        {
            bool res = false;
            res |= Program.FlowerIcons.Export();
            res |= Program.EquipmentIcons.Export();

            if (res)
                MessageBox.Show(this, "Icons export successfully completed.", "Export");
            else
                MessageBox.Show(this, "Some error occured.", "Export");
        }

        private void MMItemFileDownloadImages_Click(object sender, EventArgs ev)
        {
            new MassDownloaderForm().ShowDialog(this);
        }

        private void MMItemFileDownloadByID_Click(object sender, EventArgs ev)
        {
            new InputDownloadID().ShowDialog(this);
        }
    }
}
