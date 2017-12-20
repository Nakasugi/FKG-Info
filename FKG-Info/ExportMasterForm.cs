using System;
using System.IO;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class ExportMasterForm : Form
    {
        public ExportMasterForm()
        {
            if (!Program.DB.Master.Ok) Close();

            InitializeComponent();


            LsBoxMasterFields.Items.AddRange(Program.DB.Master.GetNames());
            if (LsBoxMasterFields.Items.Count > 0) LsBoxMasterFields.SelectedIndex = 0;
        }



        private void BtExport_Click(object sender, EventArgs ev)
        {
            if (LsBoxMasterFields.SelectedItems.Count == 0) return;

            string name, folder, path;

            folder = Program.DB.DataFolder + "\\Export";


            if(!Directory.Exists(folder))
            {
                try
                {
                    Directory.CreateDirectory(folder);
                }
                catch
                {
                    Close();
                    return;
                }
            }


            foreach(var item in LsBoxMasterFields.SelectedItems)
            {
                name = item.ToString();
                path = folder + "\\" + name + ".txt";

                Program.DB.Master.Export(name, path);
            }


            MessageBox.Show(this, "    Done!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Close();
        }



        private void BtCancel_Click(object sender, EventArgs ev)
        {
            Close();
        }
    }
}
