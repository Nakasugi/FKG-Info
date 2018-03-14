using System;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }



        private void Options_Load(object sender, EventArgs ev)
        {
            TxBoxDataFolder.Text = Program.DB.DataFolder;
            TxBoxImagesFolder.Text = Program.DB.ImagesFolder;
            TxBoxSoundsFolder.Text = Program.DB.SoundFolder;
            TxBoxURLDMM.Text = Program.DB.DMMURL;
            TxBoxURLNutaku.Text = Program.DB.NutakuURL;
            ChBoxSaveDw.Checked = Program.DB.StoreDownloaded;
            NumSoundVolume.Value = Program.DB.SoundVolume;
            TxBoxAcc1Name.Text = Program.DB.Account1Name;
            TxBoxAcc2Name.Text = Program.DB.Account2Name;

            switch (Program.DB.ImageSource)
            {
                case FlowerDataBase.ImageSources.Nutaku: RdBtNutaku.Checked = true; break;
                case FlowerDataBase.ImageSources.NutakuDMM: RdBtNutakuDMM.Checked = true; break;
                case FlowerDataBase.ImageSources.DMM: RdBtDMM.Checked = true; break;
                case FlowerDataBase.ImageSources.DMMNutaku: RdBtDMMNutaku.Checked = true; break;

                default: RdBtLocal.Checked = true; break;
            }
        }



        private void BtOk_Click(object sender, EventArgs ev)
        {
            Program.DB.DataFolder = TxBoxDataFolder.Text;
            Program.DB.ImagesFolder = TxBoxImagesFolder.Text;
            Program.DB.SoundFolder = TxBoxSoundsFolder.Text;
            Program.DB.DMMURL = TxBoxURLDMM.Text;
            Program.DB.NutakuURL = TxBoxURLNutaku.Text;
            Program.DB.StoreDownloaded = ChBoxSaveDw.Checked;
            Program.DB.SoundVolume = (int)NumSoundVolume.Value;
            Program.DB.Account1Name = TxBoxAcc1Name.Text;
            Program.DB.Account2Name = TxBoxAcc2Name.Text;

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

            Program.DB.SaveOptions();

            Close();
        }
    }
}
