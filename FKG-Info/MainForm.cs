using System;
using System.Drawing;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class MainForm : Form
    {
        FlowerInfo.ImageTypes SelectedImageType;
        FlowerInfo.Evolution SelectedEvolution;

        ContextMenu CMenu;


        public MainForm()
        {
            InitializeComponent();
            MainFormInitCustomControls();


            SelectedImageType = FlowerInfo.ImageTypes.Stand;
            SelectedEvolution = FlowerInfo.Evolution.Base;

            
            CMenu = new ContextMenu();

            foreach (FlowerInfo.ImageTypes itp in Enum.GetValues(typeof(FlowerInfo.ImageTypes)))
            {
                if (itp == FlowerInfo.ImageTypes.IconSmall) continue;
                if (itp == FlowerInfo.ImageTypes.IconMedium) continue;
                if (itp == FlowerInfo.ImageTypes.IconLarge) continue;

                CMenu.MenuItems.Add(itp.ToString(), (sender, e) => CtMenuClick(itp.ToString()));
            }

            CtMenuClick(FlowerInfo.ImageTypes.Stand.ToString());
            PicBoxBig.ContextMenu = CMenu;
        }



        void MainFormInitCustomControls()
        {
            DataGridViewColumn clmn;

                
            clmn = new DataGridViewColumn();

            clmn.HeaderText = "Type";
            clmn.Width = 80;
            clmn.ReadOnly = true;
            clmn.Name = "type";
            clmn.Frozen = true;
            clmn.Resizable = DataGridViewTriState.False;
            clmn.CellTemplate = new DataGridViewTextBoxCell();

            GridInfo.Columns.Add(clmn);


            clmn = new DataGridViewColumn();

            clmn.HeaderText = "Info";
            clmn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            clmn.Resizable = DataGridViewTriState.False;
            clmn.ReadOnly = true;
            clmn.Name = "info";
            clmn.CellTemplate = new DataGridViewTextBoxCell();

            GridInfo.Columns.Add(clmn);


            GridInfo.AllowUserToAddRows = false;
            GridInfo.AllowUserToDeleteRows = false;
            GridInfo.AllowUserToResizeRows = false;
            GridInfo.ReadOnly = true;
            GridInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GridInfo.DefaultCellStyle.SelectionBackColor = Color.White;
            GridInfo.DefaultCellStyle.SelectionForeColor = Color.Black;
            //GridInfo.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;


            ChBoxGame01.Text = Program.DB.Game01Name;
            ChBoxGame02.Text = Program.DB.Game02Name;
            ChBoxGame03.Text = Program.DB.Game03Name;
        }



        private void CtMenuClick(string itype)
        {
            SelectedImageType = (FlowerInfo.ImageTypes)Enum.Parse(typeof(FlowerInfo.ImageTypes), itype);

            foreach(MenuItem mi in CMenu.MenuItems)
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

            flower.FillGrid(GridInfo);

            UpdateBoxInfo();

            ChBoxGame01.Checked = flower.Game01;
            ChBoxGame02.Checked = flower.Game02;
            ChBoxGame03.Checked = flower.Game03;
        }



        private void BtSelect_Click(object sender, EventArgs ev)
        {
            FlowerSelect selector = new FlowerSelect();
            selector.ShowDialog(this);

            if (Program.DB.IsSelected())
            {
                ReloadFlower(Program.DB.GetSelected());
                ChBoxGame01.Enabled = true;
                ChBoxGame02.Enabled = true;
                ChBoxGame03.Enabled = true;
            }
            else
            {
                ChBoxGame01.Enabled = false;
                ChBoxGame02.Enabled = false;
                ChBoxGame03.Enabled = false;
            }
        }



        private void PicBoxIconBase_Click(object sender, EventArgs ev)
        {
            SelectedEvolution = FlowerInfo.Evolution.Base;
            if (!Program.DB.IsSelected()) return;
            SetBigImage(Program.DB.GetSelected());
        }

        private void PicBoxIconAwak_Click(object sender, EventArgs ev)
        {
            SelectedEvolution = FlowerInfo.Evolution.Awakened;
            if (!Program.DB.IsSelected()) return;
            SetBigImage(Program.DB.GetSelected());
        }

        private void PicBoxIconBloom_Click(object sender, EventArgs ev)
        {
            SelectedEvolution = FlowerInfo.Evolution.Bloomed;
            if (!Program.DB.IsSelected()) return;
            SetBigImage(Program.DB.GetSelected());
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



        private void BtOptions_Click(object sender, EventArgs ev)
        {
            Options opt = new Options();
            opt.ShowDialog(this);
        }



        private void ChBoxGame01_CheckedChanged(object sender, EventArgs ev)
        {
            if (Program.DB.IsSelected()) Program.DB.GetSelected().Game01 = ChBoxGame01.Checked;
        }

        private void ChBoxGame02_CheckedChanged(object sender, EventArgs ev)
        {
            if (Program.DB.IsSelected()) Program.DB.GetSelected().Game02 = ChBoxGame02.Checked;
        }

        private void ChBoxGame03_CheckedChanged(object sender, EventArgs ev)
        {
            if (Program.DB.IsSelected()) Program.DB.GetSelected().Game03 = ChBoxGame03.Checked;
        }



        private void ChBoxBaseAbilities_CheckedChanged(object sender, EventArgs ev)
        {
            UpdateBoxInfo();
        }

        private void ChBoxAbilityTranslation_CheckedChanged(object sender, EventArgs ev)
        {
            UpdateBoxInfo();
        }

        private void UpdateBoxInfo()
        {
            if (!Program.DB.IsSelected()) return;

            TxBoxAbilityInfo.Text = Program.DB.GetSelected().GetAbilitiesInfo(ChBoxTranslation.Checked, ChBoxBaseAbilities.Checked);
            TxBoxSkillInfo.Text = Program.DB.GetSelected().GetSkillInfo(ChBoxTranslation.Checked);
        }



        private void BtMastrerInfo_Click(object sender, EventArgs e)
        {
            MasterFilesInfo mi = new MasterFilesInfo();

            mi.ShowDialog(this);
        }
    }
}
