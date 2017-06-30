using System;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class MasterSummary : Form
    {
        public MasterSummary()
        {
            InitializeComponent();
        }

        private void MasterFilesInfo_Load(object sender, EventArgs e)
        {
            string info = "";
            info += "masterCharaLines = " + Program.DB._masterCharaLines + "\r\n";
            info += "masterCharaFields = " + Program.DB._masterCharaFields + "\r\n";
            info += "masterSkillLines = " + Program.DB._masterSkillLines + "\r\n";
            info += "masterSkillFields = " + Program.DB._masterSkillFields + "\r\n";
            info += "masterAbilityLines = " + Program.DB._masterAbilityLines + "\r\n";
            info += "masterAbililyFields = " + Program.DB._masterAbililyFields + "\r\n";
            TxBoxInfo.Text = info;
            TxBoxInfo.Select(TxBoxInfo.Text.Length - 1, TxBoxInfo.Text.Length - 1);
        }
    }
}
