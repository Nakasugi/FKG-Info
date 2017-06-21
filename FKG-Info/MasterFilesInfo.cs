using System;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class MasterFilesInfo : Form
    {
        public MasterFilesInfo()
        {
            InitializeComponent();
        }

        private void MasterFilesInfo_Load(object sender, EventArgs e)
        {
            string info = "";
            info += "masterCharaLines = " + Program.DB.masterCharaLines + "\r\n";
            info += "masterCharaFields = " + Program.DB.masterCharaFields + "\r\n";
            info += "masterSkillLines = " + Program.DB.masterSkillLines + "\r\n";
            info += "masterSkillFields = " + Program.DB.masterSkillFields + "\r\n";
            info += "masterAbilityLines = " + Program.DB.masterAbilityLines + "\r\n";
            info += "masterAbililyFields = " + Program.DB.masterAbililyFields + "\r\n";
            TxBoxInfo.Text = info;
        }
    }
}
