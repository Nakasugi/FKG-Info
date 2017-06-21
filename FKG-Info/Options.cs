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
            TxBoxImgFolder.Text = Program.DataBase.ImagesFolder;
            TxBoxDataFolder.Text = Program.DataBase.DataFolder;
            TxBoxURLDMM.Text = Program.DataBase.DMMURL;
            TxBoxURLNutaku.Text = Program.DataBase.NutakuURL;
            ChBoxSaveDw.Checked = Program.DataBase.StoreDownloaded;

            switch (Program.DataBase.ImageSource)
            {
                case FlowerDB.ImageSources.Nutaku: RdBtNutaku.Checked = true; break;
                case FlowerDB.ImageSources.NutakuDMM: RdBtNutakuDMM.Checked = true; break;
                case FlowerDB.ImageSources.DMM: RdBtDMM.Checked = true; break;
                case FlowerDB.ImageSources.DMMNutaku: RdBtDMMNutaku.Checked = true; break;

                default: RdBtLocal.Checked = true; break;
            }


            TxBoxGame01Name.Text = Program.DataBase.Game01Name;
            TxBoxGame02Name.Text = Program.DataBase.Game02Name;
            TxBoxGame03Name.Text = Program.DataBase.Game03Name;
        }

        private void BtOk_Click(object sender, EventArgs ev)
        {
            Program.DataBase.ImagesFolder = TxBoxImgFolder.Text;
            Program.DataBase.DataFolder = TxBoxDataFolder.Text;
            Program.DataBase.DMMURL = TxBoxURLDMM.Text;
            Program.DataBase.NutakuURL = TxBoxURLNutaku.Text;
            Program.DataBase.StoreDownloaded = ChBoxSaveDw.Checked;

            if (!System.IO.Directory.Exists(Program.DataBase.ImagesFolder))
            {
                try { System.IO.Directory.CreateDirectory(Program.DataBase.ImagesFolder); } catch { }
            }

            if (!System.IO.Directory.Exists(Program.DataBase.DataFolder))
            {
                try { System.IO.Directory.CreateDirectory(Program.DataBase.DataFolder); } catch { }
            }


            if (RdBtLocal.Checked) Program.DataBase.ImageSource = FlowerDB.ImageSources.Local;
            if (RdBtNutaku.Checked) Program.DataBase.ImageSource = FlowerDB.ImageSources.Nutaku;
            if (RdBtNutakuDMM.Checked) Program.DataBase.ImageSource = FlowerDB.ImageSources.NutakuDMM;
            if (RdBtDMM.Checked) Program.DataBase.ImageSource = FlowerDB.ImageSources.DMM;
            if (RdBtDMMNutaku.Checked) Program.DataBase.ImageSource = FlowerDB.ImageSources.DMMNutaku;

            Program.DataBase.Game01Name = TxBoxGame01Name.Text;
            Program.DataBase.Game02Name = TxBoxGame02Name.Text;
            Program.DataBase.Game03Name = TxBoxGame03Name.Text;

            Program.DataBase.Save();

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
