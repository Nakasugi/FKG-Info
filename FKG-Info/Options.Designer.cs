namespace FKG_Info
{
    partial class Options
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
            this._TXBoxInfo01 = new System.Windows.Forms.TextBox();
            this.TxBoxImgFolder = new System.Windows.Forms.TextBox();
            this._TXBoxInfo03 = new System.Windows.Forms.TextBox();
            this.TxBoxURLDMM = new System.Windows.Forms.TextBox();
            this.TxBoxURLNutaku = new System.Windows.Forms.TextBox();
            this._TXBoxInfo04 = new System.Windows.Forms.TextBox();
            this.RdBtLocal = new System.Windows.Forms.RadioButton();
            this.RdBtNutakuDMM = new System.Windows.Forms.RadioButton();
            this.RdBtDMM = new System.Windows.Forms.RadioButton();
            this.RdBtNutaku = new System.Windows.Forms.RadioButton();
            this.RdBtDMMNutaku = new System.Windows.Forms.RadioButton();
            this.ChBoxSaveDw = new System.Windows.Forms.CheckBox();
            this.BtOk = new System.Windows.Forms.Button();
            this.TxBoxGame01Name = new System.Windows.Forms.TextBox();
            this.TxBoxGame02Name = new System.Windows.Forms.TextBox();
            this.TxBoxGame03Name = new System.Windows.Forms.TextBox();
            this._TXBoxInfo05 = new System.Windows.Forms.TextBox();
            this._TXBoxInfo06 = new System.Windows.Forms.TextBox();
            this._TXBoxInfo07 = new System.Windows.Forms.TextBox();
            this.TxBoxDataFolder = new System.Windows.Forms.TextBox();
            this._TXBoxInfo02 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _TXBoxInfo01
            // 
            this._TXBoxInfo01.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._TXBoxInfo01.Location = new System.Drawing.Point(12, 12);
            this._TXBoxInfo01.Name = "_TXBoxInfo01";
            this._TXBoxInfo01.ReadOnly = true;
            this._TXBoxInfo01.Size = new System.Drawing.Size(472, 20);
            this._TXBoxInfo01.TabIndex = 99;
            this._TXBoxInfo01.TabStop = false;
            this._TXBoxInfo01.Text = "Local image store";
            // 
            // TxBoxImgFolder
            // 
            this.TxBoxImgFolder.Location = new System.Drawing.Point(12, 38);
            this.TxBoxImgFolder.Name = "TxBoxImgFolder";
            this.TxBoxImgFolder.Size = new System.Drawing.Size(472, 20);
            this.TxBoxImgFolder.TabIndex = 0;
            // 
            // _TXBoxInfo03
            // 
            this._TXBoxInfo03.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._TXBoxInfo03.Location = new System.Drawing.Point(12, 116);
            this._TXBoxInfo03.Name = "_TXBoxInfo03";
            this._TXBoxInfo03.ReadOnly = true;
            this._TXBoxInfo03.Size = new System.Drawing.Size(472, 20);
            this._TXBoxInfo03.TabIndex = 99;
            this._TXBoxInfo03.TabStop = false;
            this._TXBoxInfo03.Text = "DMM Link";
            // 
            // TxBoxURLDMM
            // 
            this.TxBoxURLDMM.Location = new System.Drawing.Point(12, 142);
            this.TxBoxURLDMM.Name = "TxBoxURLDMM";
            this.TxBoxURLDMM.Size = new System.Drawing.Size(472, 20);
            this.TxBoxURLDMM.TabIndex = 1;
            // 
            // TxBoxURLNutaku
            // 
            this.TxBoxURLNutaku.Location = new System.Drawing.Point(12, 194);
            this.TxBoxURLNutaku.Name = "TxBoxURLNutaku";
            this.TxBoxURLNutaku.Size = new System.Drawing.Size(472, 20);
            this.TxBoxURLNutaku.TabIndex = 3;
            // 
            // _TXBoxInfo04
            // 
            this._TXBoxInfo04.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._TXBoxInfo04.Location = new System.Drawing.Point(12, 168);
            this._TXBoxInfo04.Name = "_TXBoxInfo04";
            this._TXBoxInfo04.ReadOnly = true;
            this._TXBoxInfo04.Size = new System.Drawing.Size(472, 20);
            this._TXBoxInfo04.TabIndex = 99;
            this._TXBoxInfo04.TabStop = false;
            this._TXBoxInfo04.Text = "Nutaku Link";
            // 
            // RdBtLocal
            // 
            this.RdBtLocal.AutoSize = true;
            this.RdBtLocal.Location = new System.Drawing.Point(12, 220);
            this.RdBtLocal.Name = "RdBtLocal";
            this.RdBtLocal.Size = new System.Drawing.Size(148, 17);
            this.RdBtLocal.TabIndex = 4;
            this.RdBtLocal.Text = "Use only local image store";
            this.RdBtLocal.UseVisualStyleBackColor = true;
            // 
            // RdBtNutakuDMM
            // 
            this.RdBtNutakuDMM.AutoSize = true;
            this.RdBtNutakuDMM.Location = new System.Drawing.Point(12, 266);
            this.RdBtNutakuDMM.Name = "RdBtNutakuDMM";
            this.RdBtNutakuDMM.Size = new System.Drawing.Size(130, 17);
            this.RdBtNutakuDMM.TabIndex = 6;
            this.RdBtNutakuDMM.TabStop = true;
            this.RdBtNutakuDMM.Text = "Download Nutaku first";
            this.RdBtNutakuDMM.UseVisualStyleBackColor = true;
            // 
            // RdBtDMM
            // 
            this.RdBtDMM.AutoSize = true;
            this.RdBtDMM.Location = new System.Drawing.Point(12, 289);
            this.RdBtDMM.Name = "RdBtDMM";
            this.RdBtDMM.Size = new System.Drawing.Size(124, 17);
            this.RdBtDMM.TabIndex = 7;
            this.RdBtDMM.TabStop = true;
            this.RdBtDMM.Text = "Download DMM only";
            this.RdBtDMM.UseVisualStyleBackColor = true;
            // 
            // RdBtNutaku
            // 
            this.RdBtNutaku.AutoSize = true;
            this.RdBtNutaku.Location = new System.Drawing.Point(12, 243);
            this.RdBtNutaku.Name = "RdBtNutaku";
            this.RdBtNutaku.Size = new System.Drawing.Size(133, 17);
            this.RdBtNutaku.TabIndex = 5;
            this.RdBtNutaku.TabStop = true;
            this.RdBtNutaku.Text = "Download Nutaku only";
            this.RdBtNutaku.UseVisualStyleBackColor = true;
            // 
            // RdBtDMMNutaku
            // 
            this.RdBtDMMNutaku.AutoSize = true;
            this.RdBtDMMNutaku.Location = new System.Drawing.Point(12, 312);
            this.RdBtDMMNutaku.Name = "RdBtDMMNutaku";
            this.RdBtDMMNutaku.Size = new System.Drawing.Size(121, 17);
            this.RdBtDMMNutaku.TabIndex = 8;
            this.RdBtDMMNutaku.TabStop = true;
            this.RdBtDMMNutaku.Text = "Download DMM first";
            this.RdBtDMMNutaku.UseVisualStyleBackColor = true;
            // 
            // ChBoxSaveDw
            // 
            this.ChBoxSaveDw.AutoSize = true;
            this.ChBoxSaveDw.Location = new System.Drawing.Point(12, 335);
            this.ChBoxSaveDw.Name = "ChBoxSaveDw";
            this.ChBoxSaveDw.Size = new System.Drawing.Size(211, 17);
            this.ChBoxSaveDw.TabIndex = 9;
            this.ChBoxSaveDw.Text = "Save downloaded images to local store";
            this.ChBoxSaveDw.UseVisualStyleBackColor = true;
            // 
            // BtOk
            // 
            this.BtOk.Location = new System.Drawing.Point(384, 316);
            this.BtOk.Name = "BtOk";
            this.BtOk.Size = new System.Drawing.Size(100, 32);
            this.BtOk.TabIndex = 13;
            this.BtOk.Text = "OK";
            this.BtOk.UseVisualStyleBackColor = true;
            this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // TxBoxGame01Name
            // 
            this.TxBoxGame01Name.Location = new System.Drawing.Point(356, 220);
            this.TxBoxGame01Name.Name = "TxBoxGame01Name";
            this.TxBoxGame01Name.Size = new System.Drawing.Size(128, 20);
            this.TxBoxGame01Name.TabIndex = 10;
            // 
            // TxBoxGame02Name
            // 
            this.TxBoxGame02Name.Location = new System.Drawing.Point(356, 246);
            this.TxBoxGame02Name.Name = "TxBoxGame02Name";
            this.TxBoxGame02Name.Size = new System.Drawing.Size(128, 20);
            this.TxBoxGame02Name.TabIndex = 11;
            // 
            // TxBoxGame03Name
            // 
            this.TxBoxGame03Name.Location = new System.Drawing.Point(356, 272);
            this.TxBoxGame03Name.Name = "TxBoxGame03Name";
            this.TxBoxGame03Name.Size = new System.Drawing.Size(128, 20);
            this.TxBoxGame03Name.TabIndex = 12;
            // 
            // _TXBoxInfo05
            // 
            this._TXBoxInfo05.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._TXBoxInfo05.Location = new System.Drawing.Point(302, 220);
            this._TXBoxInfo05.Name = "_TXBoxInfo05";
            this._TXBoxInfo05.ReadOnly = true;
            this._TXBoxInfo05.Size = new System.Drawing.Size(48, 20);
            this._TXBoxInfo05.TabIndex = 99;
            this._TXBoxInfo05.TabStop = false;
            this._TXBoxInfo05.Text = "Game01";
            this._TXBoxInfo05.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _TXBoxInfo06
            // 
            this._TXBoxInfo06.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._TXBoxInfo06.Location = new System.Drawing.Point(302, 246);
            this._TXBoxInfo06.Name = "_TXBoxInfo06";
            this._TXBoxInfo06.ReadOnly = true;
            this._TXBoxInfo06.Size = new System.Drawing.Size(48, 20);
            this._TXBoxInfo06.TabIndex = 99;
            this._TXBoxInfo06.TabStop = false;
            this._TXBoxInfo06.Text = "Game02";
            this._TXBoxInfo06.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _TXBoxInfo07
            // 
            this._TXBoxInfo07.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._TXBoxInfo07.Location = new System.Drawing.Point(302, 272);
            this._TXBoxInfo07.Name = "_TXBoxInfo07";
            this._TXBoxInfo07.ReadOnly = true;
            this._TXBoxInfo07.Size = new System.Drawing.Size(48, 20);
            this._TXBoxInfo07.TabIndex = 99;
            this._TXBoxInfo07.TabStop = false;
            this._TXBoxInfo07.Text = "Game03";
            this._TXBoxInfo07.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxBoxDataFolder
            // 
            this.TxBoxDataFolder.Location = new System.Drawing.Point(12, 90);
            this.TxBoxDataFolder.Name = "TxBoxDataFolder";
            this.TxBoxDataFolder.Size = new System.Drawing.Size(472, 20);
            this.TxBoxDataFolder.TabIndex = 101;
            // 
            // _TXBoxInfo02
            // 
            this._TXBoxInfo02.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._TXBoxInfo02.Location = new System.Drawing.Point(12, 64);
            this._TXBoxInfo02.Name = "_TXBoxInfo02";
            this._TXBoxInfo02.ReadOnly = true;
            this._TXBoxInfo02.Size = new System.Drawing.Size(472, 20);
            this._TXBoxInfo02.TabIndex = 102;
            this._TXBoxInfo02.TabStop = false;
            this._TXBoxInfo02.Text = "Data base files folder";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 360);
            this.ControlBox = false;
            this.Controls.Add(this.TxBoxDataFolder);
            this.Controls.Add(this._TXBoxInfo02);
            this.Controls.Add(this._TXBoxInfo07);
            this.Controls.Add(this._TXBoxInfo06);
            this.Controls.Add(this._TXBoxInfo05);
            this.Controls.Add(this.TxBoxGame03Name);
            this.Controls.Add(this.TxBoxGame02Name);
            this.Controls.Add(this.TxBoxGame01Name);
            this.Controls.Add(this.BtOk);
            this.Controls.Add(this.ChBoxSaveDw);
            this.Controls.Add(this.RdBtDMMNutaku);
            this.Controls.Add(this.RdBtNutaku);
            this.Controls.Add(this.RdBtDMM);
            this.Controls.Add(this.RdBtNutakuDMM);
            this.Controls.Add(this.RdBtLocal);
            this.Controls.Add(this.TxBoxURLNutaku);
            this.Controls.Add(this._TXBoxInfo04);
            this.Controls.Add(this.TxBoxURLDMM);
            this.Controls.Add(this._TXBoxInfo03);
            this.Controls.Add(this.TxBoxImgFolder);
            this.Controls.Add(this._TXBoxInfo01);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _TXBoxInfo01;
        private System.Windows.Forms.TextBox TxBoxImgFolder;
        private System.Windows.Forms.TextBox _TXBoxInfo03;
        private System.Windows.Forms.TextBox TxBoxURLDMM;
        private System.Windows.Forms.TextBox TxBoxURLNutaku;
        private System.Windows.Forms.TextBox _TXBoxInfo04;
        private System.Windows.Forms.RadioButton RdBtLocal;
        private System.Windows.Forms.RadioButton RdBtNutakuDMM;
        private System.Windows.Forms.RadioButton RdBtDMM;
        private System.Windows.Forms.RadioButton RdBtNutaku;
        private System.Windows.Forms.RadioButton RdBtDMMNutaku;
        private System.Windows.Forms.CheckBox ChBoxSaveDw;
        private System.Windows.Forms.Button BtOk;
        private System.Windows.Forms.TextBox TxBoxGame01Name;
        private System.Windows.Forms.TextBox TxBoxGame02Name;
        private System.Windows.Forms.TextBox TxBoxGame03Name;
        private System.Windows.Forms.TextBox _TXBoxInfo05;
        private System.Windows.Forms.TextBox _TXBoxInfo06;
        private System.Windows.Forms.TextBox _TXBoxInfo07;
        private System.Windows.Forms.TextBox TxBoxDataFolder;
        private System.Windows.Forms.TextBox _TXBoxInfo02;
    }
}