using System;
using System.Drawing;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class MainForm : Form
    {
        FlowerInfo.ImageTypes SelectedImageType;

        private int SelectedEvolution;

        private ContextMenu CMenu;

        private FlowerSelectControl FSC;
        private EquipmentSelectControl ESC;


        private Timer UpdateStatus;


        private enum Mode { Characters, Equipments, Furnitures, Enemies }
        private Mode CurrentMode;



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

            SelectedImageType = FlowerInfo.ImageTypes.Stand;
            SelectedEvolution = FlowerInfo.Evolution.Base;

            ToolStripMenuItem mi;
            CMenu = new ContextMenu();

            // Create image type selection menu
            foreach (FlowerInfo.ImageTypes itp in Enum.GetValues(typeof(FlowerInfo.ImageTypes)))
            {
                if (itp == FlowerInfo.ImageTypes.IconSmall) continue;
                if (itp == FlowerInfo.ImageTypes.IconMedium) continue;
                if (itp == FlowerInfo.ImageTypes.IconLarge) continue;

                CMenu.MenuItems.Add(itp.ToString(), (s, e) => MenuClick_ImageType(itp.ToString()));
                mi = new ToolStripMenuItem(itp.ToString(), null, (s, e) => MenuClick_ImageType(itp.ToString()));
                mi.BackColor = SystemColors.Menu;
                MMItemImageType.DropDownItems.Add(mi);
            }

            MenuClick_ImageType(FlowerInfo.ImageTypes.Stand.ToString());
            PicBoxBig.ContextMenu = CMenu;

            foreach (ToolStripMenuItem item in MMItemMode.DropDownItems)
                item.Click += (s, e) => MenuClick_Mode(item.Name);

            MainMenu.Renderer= new ToolStripProfessionalRenderer(new MenuColorTable());

            UpdateStatus = new Timer();
            UpdateStatus.Interval = 333;
            UpdateStatus.Tick += (s, e) => OnStatusUpdate();
            UpdateStatus.Start();
            
            FSC = null;

            CurrentMode = Mode.Characters;
            FlowerSelect_Click(this, null);
        }

        

        /// <summary>
        /// Select image type
        /// </summary>
        /// <param name="itype"></param>
        private void MenuClick_ImageType(string itype)
        {
            SelectedImageType = (FlowerInfo.ImageTypes)Enum.Parse(typeof(FlowerInfo.ImageTypes), itype);

            foreach(MenuItem mi in CMenu.MenuItems)
            {
                mi.Checked = false;
                if (mi.Text == itype) mi.Checked = true;
            }

            foreach (ToolStripMenuItem mi in MMItemImageType.DropDownItems)
            {
                mi.Checked = false;
                if (mi.Text == itype) mi.Checked = true;
            }

            if (!Program.DB.IsSelected()) return;

            FlowerInfo flower = Program.DB.GetSelected();
            flower.SelectImageType(SelectedEvolution, SelectedImageType);
            PicBoxBig.AsyncLoadChImage(flower);
        }



        /// <summary>
        /// Select current mode
        /// </summary>
        /// <param name="mode"></param>
        private void MenuClick_Mode(string mode)
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

            CloseChSelector();
            CloseEqSelector();

            PicBoxIconBase.Clear();
            PicBoxIconAwak.Clear();
            PicBoxIconBloom.Clear();

            Program.DB.Unselect();
            GridInfo.Rows.Clear();

            if (mode == MMItemModeChara.Name)
            {
                CurrentMode = Mode.Characters;
                PicBoxBig.Visible = false;
                FSC = new FlowerSelectControl(this);

                BtSelect.Enabled = true;
                MMItemSelect.Enabled = true;
            }

            if (mode == MMItemModeEquip.Name)
            {
                CurrentMode = Mode.Equipments;
                PicBoxBig.Visible = false;
                ESC = new EquipmentSelectControl(this);

                BtSelect.Enabled = false;
                MMItemSelect.Enabled = false;
            }

            if (mode == MMItemModeFurniture.Name)
            {
                CurrentMode = Mode.Furnitures;
                PicBoxBig.Visible = false;

                BtSelect.Enabled = false;
                MMItemSelect.Enabled = false;
            }

            if (mode == MMItemModeEnemy.Name)
            {
                CurrentMode = Mode.Enemies;
                PicBoxBig.Visible = false;

                BtSelect.Enabled = false;
                MMItemSelect.Enabled = false;
            }
        }



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
                FSC.Visible = false;
                PicBoxBig.Visible = true;
            }

            ReloadFlower();
        }



        public void SelectFromSelector(EquipmentInfo equip)
        {
            PicBoxIconBase.AsyncLoadEqImage(equip);
            equip.FillGrid(GridInfo, ChBoxTranslation.Checked);
        }



        private void ReloadFlower()
        {
            if (!Program.DB.IsSelected()) return;
            FlowerInfo flower = Program.DB.GetSelected();

            flower.SelectImageType(flower.CheckEvolutionValue(SelectedEvolution), SelectedImageType);
            PicBoxBig.AsyncLoadChImage(flower);

            flower.SelectImageType(FlowerInfo.Evolution.Base, FlowerInfo.ImageTypes.IconLarge);
            PicBoxIconBase.AsyncLoadChImage(flower);

            flower.SelectImageType(FlowerInfo.Evolution.Awakened, FlowerInfo.ImageTypes.IconLarge);
            PicBoxIconAwak.AsyncLoadChImage(flower);

            flower.SelectImageType(FlowerInfo.Evolution.Bloomed, FlowerInfo.ImageTypes.IconLarge);
            PicBoxIconBloom.AsyncLoadChImage(flower);

            UpdateFlowerInfo();
        }



        private void UpdateFlowerInfo()
        {
            if (!Program.DB.IsSelected()) return;

            FlowerInfo flower = Program.DB.GetSelected();

            flower.FillGrid(GridInfo, SelectedEvolution, ChBoxTranslation.Checked);
        }



        private void FlowerSelect_Click(object sender, EventArgs ev)
        {
            if (CurrentMode != Mode.Characters) return;

            if (FSC != null)
            {
                FSC.Visible ^= true;
                PicBoxBig.Visible ^= true;
                return;
            }

            FSC = new FlowerSelectControl(this);
        }



        private void CloseChSelector()
        {
            if (FSC == null) return;

            FSC.Dispose();
            PicBoxBig.Visible = true;
            FSC = null;
        }

        private void CloseEqSelector()
        {
            if (ESC == null) return;

            ESC.Dispose();
            PicBoxBig.Visible = true;
            ESC = null;
        }



        private void PicBoxIcon_DoubleClick(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;

            FSC.Visible = false;
            PicBoxBig.Visible = true;
        }

        private void PicBoxIconBase_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;
            FlowerInfo flower = Program.DB.GetSelected();
            SelectedEvolution = FlowerInfo.Evolution.Base;
            flower.SelectImageType(SelectedEvolution, SelectedImageType);
            PicBoxBig.AsyncLoadChImage(flower);
            UpdateFlowerInfo();
        }

        private void PicBoxIconAwak_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;
            FlowerInfo flower = Program.DB.GetSelected();
            SelectedEvolution = flower.CheckEvolutionValue(FlowerInfo.Evolution.Awakened);
            flower.SelectImageType(SelectedEvolution, SelectedImageType);
            PicBoxBig.AsyncLoadChImage(flower);
            UpdateFlowerInfo();
        }

        private void PicBoxIconBloom_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;
            FlowerInfo flower = Program.DB.GetSelected();
            SelectedEvolution = flower.CheckEvolutionValue(FlowerInfo.Evolution.Bloomed);
            flower.SelectImageType(SelectedEvolution, SelectedImageType);
            PicBoxBig.AsyncLoadChImage(flower);
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

        private void MMItemViewMasterSummary_Click(object sender, EventArgs ev)
        {
            MasterSummaryForm mi = new MasterSummaryForm();
            mi.ShowDialog(this);
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

        /*
        private void MMItemFileExportMaster_Click1(string name)
        {
            SaveFileDialog fileSave = new SaveFileDialog();

            fileSave.InitialDirectory = Program.DB.DataFolder;
            fileSave.Filter = "Text file|*.txt";
            fileSave.FilterIndex = 0;
            fileSave.FileName = name;

            if (fileSave.ShowDialog() != DialogResult.OK) return;

            Program.DB.Master.Export(name, fileSave.FileName);
        }
        */

        private void MMItemAbout_Click(object sender, EventArgs ev)
        {
            new AboutForm().ShowDialog(this);
        }



        private void MainForm_Closing(object sender, FormClosingEventArgs ev)
        {
            Program.DB.Running = false;
        }



        public void LoadingControlsMessage(bool visible) { _lbWait.Visible = visible; _lbWait.Refresh(); }
    }
}
