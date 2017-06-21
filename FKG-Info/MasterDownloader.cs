using System;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class MasterDownloader : Form
    {
        public MasterDownloader()
        {
            InitializeComponent();
        }



        private void BtDownload_Click(object sender, EventArgs e)
        {
            //http://web.flower-knight-girls.co.jp/api/v1/exchange/getExchangeList
            //new MiniServer();

            //System.Net.WebClient wc = new System.Net.WebClient();
            //byte[] buffer = wc.DownloadData("http://web.flower-knight-girls.co.jp/api/v1/exchange/getExchangeList");
            //System.IO.FileStream fs = new System.IO.FileStream("F:\\ExchangeList.bin", System.IO.FileMode.Create);
            //fs.Write(buffer, 0, buffer.Length);
            //fs.Close();
        }



        private void WriteInfo(string info)
        {
            if (TxBoxInfo.InvokeRequired)
            {
                TxBoxInfo.Invoke(new Action<string>(WriteInfo), new object[] { info });
                return;
            }

            TxBoxInfo.Text += info + Environment.NewLine;
            TxBoxInfo.Text += "    " + Environment.NewLine;
            TxBoxInfo.Text += "    " + Environment.NewLine;
        }

        private void BtClear_Click(object sender, EventArgs e)
        {
            TxBoxInfo.Text = "";
        }
    }
}
