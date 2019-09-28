using System;
using System.Windows.Forms;
using FKG_Info.FKG_GameData;

namespace FKG_Info.UserInterface
{
    public partial class InputDownloadID : Form
    {
        public InputDownloadID()
        {
            InitializeComponent();
        }



        private void BtCancel_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void BtOk_Click(object sender, EventArgs e)
        {
            int id = 0;

            try
            {
                id = int.Parse(TxBoxID.Text);
            }
            catch
            {
                Close();
            }


            FlowerInfo flower = new FlowerInfo(id);

            var frames = Animator.GetAllFrames(flower);

            foreach (var frame in frames) Program.ImageLoader.GetImage(frame, null);

            Close();
        }
    }
}
