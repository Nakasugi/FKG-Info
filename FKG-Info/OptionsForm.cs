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
            ChBoxAllowDw.Checked = Program.DB.EnableDownloader;
            ChBoxSaveDw.Checked = Program.DB.SaveDownloaded;
            TrackVolume.Value = Program.DB.SoundVolume;
            TxBoxAcc1Name.Text = Program.DB.Account1Name;
            TxBoxAcc2Name.Text = Program.DB.Account2Name;
        }



        private void BtOk_Click(object sender, EventArgs ev)
        {
            Program.DB.DataFolder = TxBoxDataFolder.Text;
            Program.DB.ImagesFolder = TxBoxImagesFolder.Text;
            Program.DB.SoundFolder = TxBoxSoundsFolder.Text;
            Program.DB.DMMURL = TxBoxURLDMM.Text;
            Program.DB.EnableDownloader = ChBoxAllowDw.Checked;
            Program.DB.SaveDownloaded = ChBoxSaveDw.Checked;
            Program.DB.SoundVolume = TrackVolume.Value;
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



            Program.DB.SaveOptions();

            Close();
        }
    }
}
