using System;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class MasterSummaryForm : Form
    {
        public MasterSummaryForm()
        {
            InitializeComponent();
        }

        private void MasterFilesInfo_Load(object sender, EventArgs e)
        {
            string info = "";
            info += "masterCharaLines = " + Program.DB.Summary.MrCharaLines + "\r\n";
            info += "masterCharaFields = " + Program.DB.Summary.MrCharaFields + "\r\n";
            info += "masterSkillLines = " + Program.DB.Summary.MrSkillLines + "\r\n";
            info += "masterSkillFields = " + Program.DB.Summary.MrSkillFields + "\r\n";
            info += "masterAbilityLines = " + Program.DB.Summary.MrAbilityLines + "\r\n";
            info += "masterAbililyFields = " + Program.DB.Summary.MrAbililyFields + "\r\n";
            info += "Total characters = " + Program.DB.Summary.TotalCharacters + "\r\n";
            info += "Total materials = " + Program.DB.Summary.TotalMaterials + "\r\n";
            info += "Total equipments = " + Program.DB.Summary.TotalEquipments + "\r\n";
            TxBoxInfo.Text = info;
            TxBoxInfo.Select(TxBoxInfo.Text.Length - 1, TxBoxInfo.Text.Length - 1);
        }
    }
}
