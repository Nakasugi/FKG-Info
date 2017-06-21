using System;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs ev)
        {
            TxBoxImgFolder.Text = Program.DB.ImagesFolder;
            TxBoxDataFolder.Text = Program.DB.DataFolder;
            TxBoxURLDMM.Text = Program.DB.DMMURL;
            TxBoxURLNutaku.Text = Program.DB.NutakuURL;
            ChBoxSaveDw.Checked = Program.DB.StoreDownloaded;

            switch (Program.DB.ImageSource)
            {
                case FlowerDataBase.ImageSources.Nutaku: RdBtNutaku.Checked = true; break;
                case FlowerDataBase.ImageSources.NutakuDMM: RdBtNutakuDMM.Checked = true; break;
                case FlowerDataBase.ImageSources.DMM: RdBtDMM.Checked = true; break;
                case FlowerDataBase.ImageSources.DMMNutaku: RdBtDMMNutaku.Checked = true; break;

                default: RdBtLocal.Checked = true; break;
            }


            TxBoxGame01Name.Text = Program.DB.Game01Name;
            TxBoxGame02Name.Text = Program.DB.Game02Name;
            TxBoxGame03Name.Text = Program.DB.Game03Name;
        }

        private void BtOk_Click(object sender, EventArgs ev)
        {
            Program.DB.ImagesFolder = TxBoxImgFolder.Text;
            Program.DB.DataFolder = TxBoxDataFolder.Text;
            Program.DB.DMMURL = TxBoxURLDMM.Text;
            Program.DB.NutakuURL = TxBoxURLNutaku.Text;
            Program.DB.StoreDownloaded = ChBoxSaveDw.Checked;

            if (!System.IO.Directory.Exists(Program.DB.ImagesFolder))
            {
                try { System.IO.Directory.CreateDirectory(Program.DB.ImagesFolder); } catch { }
            }

            if (!System.IO.Directory.Exists(Program.DB.DataFolder))
            {
                try { System.IO.Directory.CreateDirectory(Program.DB.DataFolder); } catch { }
            }


            if (RdBtLocal.Checked) Program.DB.ImageSource = FlowerDataBase.ImageSources.Local;
            if (RdBtNutaku.Checked) Program.DB.ImageSource = FlowerDataBase.ImageSources.Nutaku;
            if (RdBtNutakuDMM.Checked) Program.DB.ImageSource = FlowerDataBase.ImageSources.NutakuDMM;
            if (RdBtDMM.Checked) Program.DB.ImageSource = FlowerDataBase.ImageSources.DMM;
            if (RdBtDMMNutaku.Checked) Program.DB.ImageSource = FlowerDataBase.ImageSources.DMMNutaku;

            Program.DB.Game01Name = TxBoxGame01Name.Text;
            Program.DB.Game02Name = TxBoxGame02Name.Text;
            Program.DB.Game03Name = TxBoxGame03Name.Text;

            Program.DB.Save();

            Close();
        }



        /*
        private void BtImportDMM_Click(object sender, EventArgs ev)
        {
            OpenFileDialog of = new OpenFileDialog();


            of.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            of.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            of.FilterIndex = 0;
            of.Multiselect = false;
            of.RestoreDirectory = true;

            if (of.ShowDialog() != DialogResult.OK) return;

            System.IO.FileStream fs;
            System.IO.StreamReader rd;

            try
            {
                fs = new System.IO.FileStream(of.FileName, System.IO.FileMode.Open);
                rd = new System.IO.StreamReader(fs);
            }
            catch { return; }

            string st;

            while (!rd.EndOfStream)
            {
                st = rd.ReadLine();
                Program.DataBase.Add(new FlowerInfo(st));
            }

            rd.Close();
        }
        */
    }
}
