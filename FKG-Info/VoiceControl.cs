using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;



namespace FKG_Info
{
    public partial class VoiceControl : UserControl
    {
        public VoiceControl(MainForm main)
        {
            InitializeComponent();

            Parent = main;
            Location = new Point(0, 26);
            Visible = false;

            LoadVoiceTable();
        }



        private void LoadVoiceTable()
        {
            string path = Program.DB.DataFolder + "\\en_voicetable.txt";

            FileStream fs;

            try { fs = new FileStream(path, FileMode.Open); } catch { return; }


            StreamReader rd = new StreamReader(fs);

            while (!rd.EndOfStream)
            {
                List<string> fields = new List<string>();
                fields.Add("Play");
                string[] scells = rd.ReadLine().Split(';');
                fields.AddRange(scells);
                VoicesDGV.Rows.Add(fields.ToArray());
            }


            fs.Close();
        }



        private void VoicesDGV_MouseMove(object sender, MouseEventArgs ev)
        {
            VoicesDGV.Focus();
        }



        private void VoicesDGV_CellContentClick(object sender, DataGridViewCellEventArgs ev)
        {
            if (ev.ColumnIndex != 0) return;
            FlowerInfo flower = Program.DB.Flowers.GetSelected();
            if (flower == null) return;

            string name = VoicesDGV.Rows[ev.RowIndex].Cells[3].Value.ToString();

            SoundPlayer.Play(flower.GetBaseID().ToString(), name);
        }
    }
}
