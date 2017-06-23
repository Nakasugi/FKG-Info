namespace FKG_Info
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PicBoxBig = new System.Windows.Forms.PictureBox();
            this.PicBoxIconBase = new System.Windows.Forms.PictureBox();
            this.PicBoxIconAwak = new System.Windows.Forms.PictureBox();
            this.PicBoxIconBloom = new System.Windows.Forms.PictureBox();
            this.GridInfo = new System.Windows.Forms.DataGridView();
            this.BtSelect = new System.Windows.Forms.Button();
            this.TxBoxAbilityInfo = new System.Windows.Forms.TextBox();
            this.ChBoxGame02 = new System.Windows.Forms.CheckBox();
            this.ChBoxGame01 = new System.Windows.Forms.CheckBox();
            this.BtOptions = new System.Windows.Forms.Button();
            this.ChBoxGame03 = new System.Windows.Forms.CheckBox();
            this.ChBoxBaseAbilities = new System.Windows.Forms.CheckBox();
            this.ChBoxTranslation = new System.Windows.Forms.CheckBox();
            this.TxBoxSkillInfo = new System.Windows.Forms.TextBox();
            this.BtMastrerInfo = new System.Windows.Forms.Button();
            this.BtGetMaster = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxBig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconAwak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconBloom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // PicBoxBig
            // 
            this.PicBoxBig.Location = new System.Drawing.Point(0, 0);
            this.PicBoxBig.Name = "PicBoxBig";
            this.PicBoxBig.Size = new System.Drawing.Size(852, 680);
            this.PicBoxBig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBoxBig.TabIndex = 0;
            this.PicBoxBig.TabStop = false;
            // 
            // PicBoxIconBase
            // 
            this.PicBoxIconBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBoxIconBase.Location = new System.Drawing.Point(860, 12);
            this.PicBoxIconBase.Name = "PicBoxIconBase";
            this.PicBoxIconBase.Size = new System.Drawing.Size(100, 100);
            this.PicBoxIconBase.TabIndex = 1;
            this.PicBoxIconBase.TabStop = false;
            this.PicBoxIconBase.Click += new System.EventHandler(this.PicBoxIconBase_Click);
            // 
            // PicBoxIconAwak
            // 
            this.PicBoxIconAwak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBoxIconAwak.Location = new System.Drawing.Point(966, 12);
            this.PicBoxIconAwak.Name = "PicBoxIconAwak";
            this.PicBoxIconAwak.Size = new System.Drawing.Size(100, 100);
            this.PicBoxIconAwak.TabIndex = 2;
            this.PicBoxIconAwak.TabStop = false;
            this.PicBoxIconAwak.Click += new System.EventHandler(this.PicBoxIconAwak_Click);
            // 
            // PicBoxIconBloom
            // 
            this.PicBoxIconBloom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBoxIconBloom.Location = new System.Drawing.Point(1072, 12);
            this.PicBoxIconBloom.Name = "PicBoxIconBloom";
            this.PicBoxIconBloom.Size = new System.Drawing.Size(100, 100);
            this.PicBoxIconBloom.TabIndex = 3;
            this.PicBoxIconBloom.TabStop = false;
            this.PicBoxIconBloom.Click += new System.EventHandler(this.PicBoxIconBloom_Click);
            // 
            // GridInfo
            // 
            this.GridInfo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridInfo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridInfo.GridColor = System.Drawing.SystemColors.Control;
            this.GridInfo.Location = new System.Drawing.Point(860, 118);
            this.GridInfo.Name = "GridInfo";
            this.GridInfo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridInfo.RowHeadersVisible = false;
            this.GridInfo.RowTemplate.Height = 18;
            this.GridInfo.RowTemplate.ReadOnly = true;
            this.GridInfo.Size = new System.Drawing.Size(312, 254);
            this.GridInfo.TabIndex = 5;
            // 
            // BtSelect
            // 
            this.BtSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtSelect.ForeColor = System.Drawing.Color.DarkBlue;
            this.BtSelect.Location = new System.Drawing.Point(243, 689);
            this.BtSelect.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.BtSelect.Name = "BtSelect";
            this.BtSelect.Size = new System.Drawing.Size(72, 32);
            this.BtSelect.TabIndex = 6;
            this.BtSelect.Text = "SELECT";
            this.BtSelect.UseVisualStyleBackColor = true;
            this.BtSelect.Click += new System.EventHandler(this.BtSelect_Click);
            // 
            // TxBoxAbilityInfo
            // 
            this.TxBoxAbilityInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TxBoxAbilityInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxBoxAbilityInfo.Location = new System.Drawing.Point(860, 470);
            this.TxBoxAbilityInfo.Multiline = true;
            this.TxBoxAbilityInfo.Name = "TxBoxAbilityInfo";
            this.TxBoxAbilityInfo.ReadOnly = true;
            this.TxBoxAbilityInfo.Size = new System.Drawing.Size(312, 167);
            this.TxBoxAbilityInfo.TabIndex = 10;
            // 
            // ChBoxGame02
            // 
            this.ChBoxGame02.AutoSize = true;
            this.ChBoxGame02.Enabled = false;
            this.ChBoxGame02.Location = new System.Drawing.Point(860, 678);
            this.ChBoxGame02.Name = "ChBoxGame02";
            this.ChBoxGame02.Size = new System.Drawing.Size(66, 17);
            this.ChBoxGame02.TabIndex = 11;
            this.ChBoxGame02.Text = "Game02";
            this.ChBoxGame02.UseVisualStyleBackColor = true;
            this.ChBoxGame02.CheckedChanged += new System.EventHandler(this.ChBoxGame02_CheckedChanged);
            // 
            // ChBoxGame01
            // 
            this.ChBoxGame01.AutoSize = true;
            this.ChBoxGame01.Enabled = false;
            this.ChBoxGame01.Location = new System.Drawing.Point(860, 655);
            this.ChBoxGame01.Name = "ChBoxGame01";
            this.ChBoxGame01.Size = new System.Drawing.Size(66, 17);
            this.ChBoxGame01.TabIndex = 12;
            this.ChBoxGame01.Text = "Game01";
            this.ChBoxGame01.UseVisualStyleBackColor = true;
            this.ChBoxGame01.CheckedChanged += new System.EventHandler(this.ChBoxGame01_CheckedChanged);
            // 
            // BtOptions
            // 
            this.BtOptions.ForeColor = System.Drawing.Color.DimGray;
            this.BtOptions.Location = new System.Drawing.Point(165, 689);
            this.BtOptions.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.BtOptions.Name = "BtOptions";
            this.BtOptions.Size = new System.Drawing.Size(72, 32);
            this.BtOptions.TabIndex = 14;
            this.BtOptions.Text = "OPTIONS";
            this.BtOptions.UseVisualStyleBackColor = true;
            this.BtOptions.Click += new System.EventHandler(this.BtOptions_Click);
            // 
            // ChBoxGame03
            // 
            this.ChBoxGame03.AutoSize = true;
            this.ChBoxGame03.Enabled = false;
            this.ChBoxGame03.Location = new System.Drawing.Point(860, 701);
            this.ChBoxGame03.Name = "ChBoxGame03";
            this.ChBoxGame03.Size = new System.Drawing.Size(66, 17);
            this.ChBoxGame03.TabIndex = 15;
            this.ChBoxGame03.Text = "Game03";
            this.ChBoxGame03.UseVisualStyleBackColor = true;
            this.ChBoxGame03.CheckedChanged += new System.EventHandler(this.ChBoxGame03_CheckedChanged);
            // 
            // ChBoxBaseAbilities
            // 
            this.ChBoxBaseAbilities.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChBoxBaseAbilities.AutoSize = true;
            this.ChBoxBaseAbilities.ForeColor = System.Drawing.Color.OrangeRed;
            this.ChBoxBaseAbilities.Location = new System.Drawing.Point(1093, 643);
            this.ChBoxBaseAbilities.Name = "ChBoxBaseAbilities";
            this.ChBoxBaseAbilities.Size = new System.Drawing.Size(79, 23);
            this.ChBoxBaseAbilities.TabIndex = 18;
            this.ChBoxBaseAbilities.Text = "Base Abilities";
            this.ChBoxBaseAbilities.UseVisualStyleBackColor = true;
            this.ChBoxBaseAbilities.CheckedChanged += new System.EventHandler(this.ChBoxBaseAbilities_CheckedChanged);
            // 
            // ChBoxTranslation
            // 
            this.ChBoxTranslation.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChBoxTranslation.AutoSize = true;
            this.ChBoxTranslation.Checked = true;
            this.ChBoxTranslation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxTranslation.ForeColor = System.Drawing.Color.DarkViolet;
            this.ChBoxTranslation.Location = new System.Drawing.Point(1018, 643);
            this.ChBoxTranslation.Name = "ChBoxTranslation";
            this.ChBoxTranslation.Size = new System.Drawing.Size(69, 23);
            this.ChBoxTranslation.TabIndex = 19;
            this.ChBoxTranslation.Text = "Translation";
            this.ChBoxTranslation.UseVisualStyleBackColor = true;
            this.ChBoxTranslation.CheckedChanged += new System.EventHandler(this.ChBoxAbilityTranslation_CheckedChanged);
            // 
            // TxBoxSkillInfo
            // 
            this.TxBoxSkillInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TxBoxSkillInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxBoxSkillInfo.Location = new System.Drawing.Point(860, 378);
            this.TxBoxSkillInfo.Multiline = true;
            this.TxBoxSkillInfo.Name = "TxBoxSkillInfo";
            this.TxBoxSkillInfo.ReadOnly = true;
            this.TxBoxSkillInfo.Size = new System.Drawing.Size(312, 86);
            this.TxBoxSkillInfo.TabIndex = 20;
            // 
            // BtMastrerInfo
            // 
            this.BtMastrerInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtMastrerInfo.ForeColor = System.Drawing.Color.DarkBlue;
            this.BtMastrerInfo.Location = new System.Drawing.Point(9, 689);
            this.BtMastrerInfo.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.BtMastrerInfo.Name = "BtMastrerInfo";
            this.BtMastrerInfo.Size = new System.Drawing.Size(72, 32);
            this.BtMastrerInfo.TabIndex = 21;
            this.BtMastrerInfo.Text = "i";
            this.BtMastrerInfo.UseVisualStyleBackColor = true;
            this.BtMastrerInfo.Click += new System.EventHandler(this.BtMastrerInfo_Click);
            // 
            // BtGetMaster
            // 
            this.BtGetMaster.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtGetMaster.ForeColor = System.Drawing.Color.Maroon;
            this.BtGetMaster.Location = new System.Drawing.Point(87, 689);
            this.BtGetMaster.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.BtGetMaster.Name = "BtGetMaster";
            this.BtGetMaster.Size = new System.Drawing.Size(72, 32);
            this.BtGetMaster.TabIndex = 22;
            this.BtGetMaster.Text = "getMaster";
            this.BtGetMaster.UseVisualStyleBackColor = true;
            this.BtGetMaster.Click += new System.EventHandler(this.BtGetMaster_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 730);
            this.Controls.Add(this.BtGetMaster);
            this.Controls.Add(this.BtMastrerInfo);
            this.Controls.Add(this.TxBoxSkillInfo);
            this.Controls.Add(this.ChBoxTranslation);
            this.Controls.Add(this.ChBoxBaseAbilities);
            this.Controls.Add(this.ChBoxGame03);
            this.Controls.Add(this.BtOptions);
            this.Controls.Add(this.ChBoxGame01);
            this.Controls.Add(this.ChBoxGame02);
            this.Controls.Add(this.TxBoxAbilityInfo);
            this.Controls.Add(this.BtSelect);
            this.Controls.Add(this.GridInfo);
            this.Controls.Add(this.PicBoxIconBloom);
            this.Controls.Add(this.PicBoxIconAwak);
            this.Controls.Add(this.PicBoxIconBase);
            this.Controls.Add(this.PicBoxBig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 768);
            this.MinimumSize = new System.Drawing.Size(1200, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FKG-Info";
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxBig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconAwak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconBloom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicBoxBig;
        private System.Windows.Forms.PictureBox PicBoxIconBase;
        private System.Windows.Forms.PictureBox PicBoxIconAwak;
        private System.Windows.Forms.PictureBox PicBoxIconBloom;
        private System.Windows.Forms.DataGridView GridInfo;
        private System.Windows.Forms.Button BtSelect;
        private System.Windows.Forms.TextBox TxBoxAbilityInfo;
        private System.Windows.Forms.CheckBox ChBoxGame02;
        private System.Windows.Forms.CheckBox ChBoxGame01;
        private System.Windows.Forms.Button BtOptions;
        private System.Windows.Forms.CheckBox ChBoxGame03;
        private System.Windows.Forms.CheckBox ChBoxBaseAbilities;
        private System.Windows.Forms.CheckBox ChBoxTranslation;
        private System.Windows.Forms.TextBox TxBoxSkillInfo;
        private System.Windows.Forms.Button BtMastrerInfo;
        private System.Windows.Forms.Button BtGetMaster;
    }
}

