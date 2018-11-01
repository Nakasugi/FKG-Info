using System.Windows.Forms;



namespace FKG_Info
{
    public partial class MassDownloaderForm : Form
    {
        public MassDownloaderForm()
        {
            InitializeComponent();

            int x = 16, y = 40;
            
            foreach (Animator.Type type in System.Enum.GetValues(typeof(Animator.Type)))
            {
                if (Animator.IsIcon(type) || (type == Animator.Type.None)) continue;

                var ch = new CheckBox();

                ch.Name = "Ch" + type.ToString();
                ch.Text = Animator.GetMenuName(type);
                ch.Location = new System.Drawing.Point(x, y);
                ch.Font = _lb01.Font;
                ch.CheckedChanged += ChBoxesCheckedChanged;

                Controls.Add(ch);

                y += 28;
            }
        }



        private void BtCancel_Click(object sender, System.EventArgs ev)
        {
            Close();
        }

        private void BtSelectAll_Click(object sender, System.EventArgs ev)
        {
            foreach (var c in Controls) if (c is CheckBox) ((CheckBox)c).Checked = true;
        }

        private void BtDeselectAll_Click(object sender, System.EventArgs ev)
        {
            foreach (var c in Controls) if (c is CheckBox) ((CheckBox)c).Checked = false;
        }

        private void BtStart_Click(object sender, System.EventArgs ev)
        {
            Close();
        }



        private void ChBoxesCheckedChanged(object sender, System.EventArgs ev)
        {
            int n = 0;
            foreach (var c in Controls) if (c is CheckBox) if(((CheckBox)c).Checked) n++;

            if (n == 0) { _lb02.Text = "No images selected"; return; }

            n *= Program.DB.Flowers.Count;

            _lb02.Text = "Approximate number of images = " + n;
        }
    }
}
