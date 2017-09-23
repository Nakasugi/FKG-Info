namespace FKG_Info
{
    partial class VoiceControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.VoicesDGV = new System.Windows.Forms.DataGridView();
            this.ColButtons = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColKanji = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEnglish = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.VoicesDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // VoicesDGV
            // 
            this.VoicesDGV.AllowUserToAddRows = false;
            this.VoicesDGV.AllowUserToDeleteRows = false;
            this.VoicesDGV.AllowUserToResizeColumns = false;
            this.VoicesDGV.AllowUserToResizeRows = false;
            this.VoicesDGV.BackgroundColor = System.Drawing.SystemColors.Control;
            this.VoicesDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.VoicesDGV.ColumnHeadersVisible = false;
            this.VoicesDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColButtons,
            this.ColKanji,
            this.ColEnglish,
            this.Hash});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.VoicesDGV.DefaultCellStyle = dataGridViewCellStyle1;
            this.VoicesDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.VoicesDGV.Location = new System.Drawing.Point(26, 22);
            this.VoicesDGV.MultiSelect = false;
            this.VoicesDGV.Name = "VoicesDGV";
            this.VoicesDGV.ReadOnly = true;
            this.VoicesDGV.RowHeadersVisible = false;
            this.VoicesDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.VoicesDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.VoicesDGV.Size = new System.Drawing.Size(750, 596);
            this.VoicesDGV.TabIndex = 0;
            this.VoicesDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VoicesDGV_CellContentClick);
            this.VoicesDGV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VoicesDGV_MouseMove);
            // 
            // ColButtons
            // 
            this.ColButtons.HeaderText = "Play";
            this.ColButtons.Name = "ColButtons";
            this.ColButtons.ReadOnly = true;
            this.ColButtons.Width = 64;
            // 
            // ColKanji
            // 
            this.ColKanji.HeaderText = "Kanji";
            this.ColKanji.Name = "ColKanji";
            this.ColKanji.ReadOnly = true;
            this.ColKanji.Width = 240;
            // 
            // ColEnglish
            // 
            this.ColEnglish.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColEnglish.HeaderText = "English";
            this.ColEnglish.Name = "ColEnglish";
            this.ColEnglish.ReadOnly = true;
            // 
            // Hash
            // 
            this.Hash.HeaderText = "Hash";
            this.Hash.Name = "Hash";
            this.Hash.ReadOnly = true;
            this.Hash.Visible = false;
            // 
            // VoiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VoicesDGV);
            this.Name = "VoiceControl";
            this.Size = new System.Drawing.Size(802, 640);
            ((System.ComponentModel.ISupportInitialize)(this.VoicesDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView VoicesDGV;
        private System.Windows.Forms.DataGridViewButtonColumn ColButtons;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColKanji;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEnglish;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hash;
    }
}
