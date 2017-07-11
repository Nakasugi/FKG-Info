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

            // fill export menu
            /*
            if (Program.DB.Master.Ok)
            {
                string[] exportNames = Program.DB.Master.GetNames();

                foreach (string name in exportNames)
                {
                    mi = new ToolStripMenuItem(name, null, (s, e) => MMItemFileExportMaster_Click(name));
                    mi.BackColor = SystemColors.Menu;
                    MMItemFileExportMaster.DropDownItems.Add(mi);
                }
            }
            */
            MainMenu.Renderer= new ToolStripProfessionalRenderer(new MenuColorTable());

            UpdateStatus = new Timer();
            UpdateStatus.Interval = 333;
            UpdateStatus.Tick += (s, e) => OnStatusUpdate();
            UpdateStatus.Start();
            
            FSC = null;
        }

        

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
            PicBoxBig.AsyncLoadChImage(Program.DB.GetSelected(), SelectedEvolution, SelectedImageType);
        }



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

            BtSelect.Enabled = true;
            MMItemSelect.Enabled = true;

            CloseChSelector();
            CloseEqSelector();

            PicBoxIconBase.Clear();
            PicBoxIconAwak.Clear();
            PicBoxIconBloom.Clear();

            if (mode == MMItemModeEquip.Name)
            {
                PicBoxBig.Visible = false;
                ESC = new EquipmentSelectControl(this);

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



        public void SelectFromSelector(FlowerInfo flower, bool selectorClose)
        {
            Program.DB.Select(flower);

            if (selectorClose) CloseChSelector();

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

            SelectedEvolution = FlowerInfo.Evolution.Base;

            PicBoxBig.AsyncLoadChImage(flower, SelectedEvolution, SelectedImageType);
            PicBoxIconBase.AsyncLoadChImage(flower, FlowerInfo.Evolution.Base, FlowerInfo.ImageTypes.IconLarge);
            PicBoxIconAwak.AsyncLoadChImage(flower, FlowerInfo.Evolution.Awakened, FlowerInfo.ImageTypes.IconLarge);
            PicBoxIconBloom.AsyncLoadChImage(flower, FlowerInfo.Evolution.Bloomed, FlowerInfo.ImageTypes.IconLarge);

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
            if (FSC != null) { CloseChSelector(); return; }

            PicBoxBig.Visible = false;
            FSC = new FlowerSelectControl(this);
            FSC.Location = new Point(0, 26);
            Controls.Add(FSC);
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

            if (FSC != null) CloseChSelector();
        }

        private void PicBoxIconBase_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;
            SelectedEvolution = Program.DB.GetSelected().SelectEvolution(FlowerInfo.Evolution.Base);
            PicBoxBig.AsyncLoadChImage(Program.DB.GetSelected(), SelectedEvolution, SelectedImageType);
            UpdateFlowerInfo();
        }

        private void PicBoxIconAwak_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;
            SelectedEvolution = Program.DB.GetSelected().SelectEvolution(FlowerInfo.Evolution.Awakened);
            PicBoxBig.AsyncLoadChImage(Program.DB.GetSelected(), SelectedEvolution, SelectedImageType);
            UpdateFlowerInfo();
        }

        private void PicBoxIconBloom_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;
            SelectedEvolution = Program.DB.GetSelected().SelectEvolution(FlowerInfo.Evolution.Bloomed);
            PicBoxBig.AsyncLoadChImage(Program.DB.GetSelected(), SelectedEvolution, SelectedImageType);
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
    }
}
