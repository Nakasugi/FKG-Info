using System;
using System.Drawing;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class MainForm : Form
    {
        FlowerInfo.ImageTypes SelectedImageType;

        int SelectedEvolution;

        ContextMenu CMenu;


        public MainForm()
        {
            InitializeComponent();

            GridInfo.DefaultCellStyle.SelectionBackColor = Color.White;
            GridInfo.DefaultCellStyle.SelectionForeColor = Color.Purple;

            SelectedImageType = FlowerInfo.ImageTypes.Stand;
            SelectedEvolution = FlowerInfo.Evolution.Base;

            ToolStripMenuItem mi;
            CMenu = new ContextMenu();

            foreach (FlowerInfo.ImageTypes itp in Enum.GetValues(typeof(FlowerInfo.ImageTypes)))
            {
                if (itp == FlowerInfo.ImageTypes.IconSmall) continue;
                if (itp == FlowerInfo.ImageTypes.IconMedium) continue;
                if (itp == FlowerInfo.ImageTypes.IconLarge) continue;

                CMenu.MenuItems.Add(itp.ToString(), (sender, e) => CtMenuClick(itp.ToString()));
                mi = new ToolStripMenuItem(itp.ToString(), null, (sender, e) => CtMenuClick(itp.ToString()));
                mi.BackColor = SystemColors.Menu;
                MMItemImageType.DropDownItems.Add(mi);
            }

            CtMenuClick(FlowerInfo.ImageTypes.Stand.ToString());
            PicBoxBig.ContextMenu = CMenu;

            if (Program.DB.Master.Ok)
            {
                string[] exportNames = Program.DB.Master.GetNames();

                foreach (string name in exportNames)
                {
                    mi = new ToolStripMenuItem(name, null, (sender, e) => MMItemFileExportMaster_Click(name));
                    mi.BackColor = SystemColors.Menu;
                    MMItemFileExportMaster.DropDownItems.Add(mi);
                }
            }
            //ChBoxGame01.Text = Program.DB.Game01Name;
            //ChBoxGame02.Text = Program.DB.Game02Name;
            //ChBoxGame03.Text = Program.DB.Game03Name;
        }



        private void CtMenuClick(string itype)
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

            SetBigImage(Program.DB.GetSelected());
        }



        private void ReloadFlower(FlowerInfo flower)
        {
            SelectedEvolution = FlowerInfo.Evolution.Base;

            SetBigImage(flower);
            SetIconBase(flower);
            SetIconAwak(flower);
            SetIconBloom(flower);

            UpdateFlowerInfo();

            //ChBoxGame01.Checked = flower.Game01;
            //ChBoxGame02.Checked = flower.Game02;
            //ChBoxGame03.Checked = flower.Game03;
        }



        private void BtSelect_Click(object sender, EventArgs ev)
        {
            FlowerSelect selector = new FlowerSelect();
            selector.ShowDialog(this);

            if (Program.DB.IsSelected())
            {
                ReloadFlower(Program.DB.GetSelected());
                //ChBoxGame01.Enabled = true;
                //ChBoxGame02.Enabled = true;
                //ChBoxGame03.Enabled = true;
            }
            else
            {
                //ChBoxGame01.Enabled = false;
                //ChBoxGame02.Enabled = false;
                //ChBoxGame03.Enabled = false;
            }
        }



        private void PicBoxIconBase_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;
            SelectedEvolution = Program.DB.GetSelected().SelectEvolution(FlowerInfo.Evolution.Base);
            SetBigImage(Program.DB.GetSelected());
            UpdateFlowerInfo();
        }

        private void PicBoxIconAwak_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;
            SelectedEvolution = Program.DB.GetSelected().SelectEvolution(FlowerInfo.Evolution.Awakened);
            SetBigImage(Program.DB.GetSelected());
            UpdateFlowerInfo();
        }

        private void PicBoxIconBloom_Click(object sender, EventArgs ev)
        {
            if (!Program.DB.IsSelected()) return;
            SelectedEvolution = Program.DB.GetSelected().SelectEvolution(FlowerInfo.Evolution.Bloomed);
            SetBigImage(Program.DB.GetSelected());
            UpdateFlowerInfo();
        }



        private void SetBigImage(FlowerInfo flower)
        {
            Program.ImageLoader.GetImage(flower.GetImageName(SelectedEvolution, SelectedImageType), SetBigImageCallback);
        }
        private void SetIconBase(FlowerInfo flower)
        {
            Program.ImageLoader.GetImage(flower.GetImageName(FlowerInfo.Evolution.Base, FlowerInfo.ImageTypes.IconLarge), SetIconBaseCallback);
        }
        private void SetIconAwak(FlowerInfo flower)
        {
            Program.ImageLoader.GetImage(flower.GetImageName(FlowerInfo.Evolution.Awakened, FlowerInfo.ImageTypes.IconLarge), SetIconAwakCallback);
        }
        private void SetIconBloom(FlowerInfo flower)
        {
            Program.ImageLoader.GetImage(flower.GetImageName(FlowerInfo.Evolution.Bloomed, FlowerInfo.ImageTypes.IconLarge), SetIconBloomCallback);
        }

        private void SetBigImageCallback(Image img) { PicBoxBig.Image = img; }
        private void SetIconBaseCallback(Image img) { PicBoxIconBase.Image = img; }
        private void SetIconAwakCallback(Image img) { PicBoxIconAwak.Image = img; }
        private void SetIconBloomCallback(Image img) { PicBoxIconBloom.Image = img; }




        private void ChBoxAbilityTranslation_CheckedChanged(object sender, EventArgs ev)
        {
            UpdateFlowerInfo();
        }



        private void UpdateFlowerInfo()
        {
            if (!Program.DB.IsSelected()) return;

            FlowerInfo flower = Program.DB.GetSelected();

            flower.FillGrid(GridInfo, SelectedEvolution, ChBoxTranslation.Checked);
        }




        private void MMItemOptions_Click(object sender, EventArgs ev)
        {
            Options opt = new Options();
            opt.ShowDialog(this);
        }

        private void MMItemViewMasterSummary_Click(object sender, EventArgs ev)
        {
            MasterSummary mi = new MasterSummary();
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

        private void MMItemFileExportMaster_Click(string name)
        {
            SaveFileDialog fileSave = new SaveFileDialog();

            fileSave.InitialDirectory = Program.DB.DataFolder;
            fileSave.Filter = "Text file|*.txt";
            fileSave.FilterIndex = 0;
            fileSave.FileName = name;

            if (fileSave.ShowDialog() != DialogResult.OK) return;

            Program.DB.Master.Export(name, fileSave.FileName);
        }
    }
}
