namespace FKG_Info
{
    partial class OptionsForm
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
            this.TxBoxImagesFolder = new System.Windows.Forms.TextBox();
            this.TxBoxURLDMM = new System.Windows.Forms.TextBox();
            this.ChBoxSaveDw = new System.Windows.Forms.CheckBox();
            this.BtOk = new System.Windows.Forms.Button();
            this.TxBoxDataFolder = new System.Windows.Forms.TextBox();
            this.TxBoxSoundsFolder = new System.Windows.Forms.TextBox();
            this._Label01 = new System.Windows.Forms.Label();
            this._Label02 = new System.Windows.Forms.Label();
            this._Label03 = new System.Windows.Forms.Label();
            this._Label04 = new System.Windows.Forms.Label();
            this._Label06 = new System.Windows.Forms.Label();
            this._Label07 = new System.Windows.Forms.Label();
            this._Label08 = new System.Windows.Forms.Label();
            this.TxBoxAcc1Name = new System.Windows.Forms.TextBox();
            this.TxBoxAcc2Name = new System.Windows.Forms.TextBox();
            this.ChBoxAllowDw = new System.Windows.Forms.CheckBox();
            this.TrackVolume = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.TrackVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // TxBoxImagesFolder
            // 
            this.TxBoxImagesFolder.Location = new System.Drawing.Point(12, 70);
            this.TxBoxImagesFolder.Name = "TxBoxImagesFolder";
            this.TxBoxImagesFolder.Size = new System.Drawing.Size(472, 20);
            this.TxBoxImagesFolder.TabIndex = 0;
            // 
            // TxBoxURLDMM
            // 
            this.TxBoxURLDMM.Location = new System.Drawing.Point(12, 154);
            this.TxBoxURLDMM.Name = "TxBoxURLDMM";
            this.TxBoxURLDMM.Size = new System.Drawing.Size(472, 20);
            this.TxBoxURLDMM.TabIndex = 1;
            // 
            // ChBoxSaveDw
            // 
            this.ChBoxSaveDw.AutoSize = true;
            this.ChBoxSaveDw.Location = new System.Drawing.Point(15, 333);
            this.ChBoxSaveDw.Name = "ChBoxSaveDw";
            this.ChBoxSaveDw.Size = new System.Drawing.Size(211, 17);
            this.ChBoxSaveDw.TabIndex = 9;
            this.ChBoxSaveDw.Text = "Save downloaded images to local store";
            this.ChBoxSaveDw.UseVisualStyleBackColor = true;
            // 
            // BtOk
            // 
            this.BtOk.Location = new System.Drawing.Point(384, 318);
            this.BtOk.Name = "BtOk";
            this.BtOk.Size = new System.Drawing.Size(100, 32);
            this.BtOk.TabIndex = 13;
            this.BtOk.Text = "OK";
            this.BtOk.UseVisualStyleBackColor = true;
            this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // TxBoxDataFolder
            // 
            this.TxBoxDataFolder.Location = new System.Drawing.Point(12, 28);
            this.TxBoxDataFolder.Name = "TxBoxDataFolder";
            this.TxBoxDataFolder.Size = new System.Drawing.Size(472, 20);
            this.TxBoxDataFolder.TabIndex = 101;
            // 
            // TxBoxSoundsFolder
            // 
            this.TxBoxSoundsFolder.Location = new System.Drawing.Point(12, 112);
            this.TxBoxSoundsFolder.Name = "TxBoxSoundsFolder";
            this.TxBoxSoundsFolder.Size = new System.Drawing.Size(472, 20);
            this.TxBoxSoundsFolder.TabIndex = 103;
            // 
            // _Label01
            // 
            this._Label01.AutoSize = true;
            this._Label01.Location = new System.Drawing.Point(12, 12);
            this._Label01.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._Label01.Name = "_Label01";
            this._Label01.Size = new System.Drawing.Size(106, 13);
            this._Label01.TabIndex = 105;
            this._Label01.Text = "Data base files folder";
            // 
            // _Label02
            // 
            this._Label02.AutoSize = true;
            this._Label02.Location = new System.Drawing.Point(12, 54);
            this._Label02.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._Label02.Name = "_Label02";
            this._Label02.Size = new System.Drawing.Size(90, 13);
            this._Label02.TabIndex = 106;
            this._Label02.Text = "Local image store";
            // 
            // _Label03
            // 
            this._Label03.AutoSize = true;
            this._Label03.Location = new System.Drawing.Point(12, 96);
            this._Label03.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._Label03.Name = "_Label03";
            this._Label03.Size = new System.Drawing.Size(91, 13);
            this._Label03.TabIndex = 107;
            this._Label03.Text = "Local sound store";
            // 
            // _Label04
            // 
            this._Label04.AutoSize = true;
            this._Label04.Location = new System.Drawing.Point(12, 138);
            this._Label04.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._Label04.Name = "_Label04";
            this._Label04.Size = new System.Drawing.Size(56, 13);
            this._Label04.TabIndex = 109;
            this._Label04.Text = "DMM Link";
            // 
            // _Label06
            // 
            this._Label06.AutoSize = true;
            this._Label06.Location = new System.Drawing.Point(12, 190);
            this._Label06.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._Label06.Name = "_Label06";
            this._Label06.Size = new System.Drawing.Size(76, 13);
            this._Label06.TabIndex = 111;
            this._Label06.Text = "Sound Volume";
            // 
            // _Label07
            // 
            this._Label07.AutoSize = true;
            this._Label07.Location = new System.Drawing.Point(256, 209);
            this._Label07.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._Label07.Name = "_Label07";
            this._Label07.Size = new System.Drawing.Size(90, 13);
            this._Label07.TabIndex = 112;
            this._Label07.Text = "Account 1 Name:";
            // 
            // _Label08
            // 
            this._Label08.AutoSize = true;
            this._Label08.Location = new System.Drawing.Point(256, 235);
            this._Label08.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._Label08.Name = "_Label08";
            this._Label08.Size = new System.Drawing.Size(90, 13);
            this._Label08.TabIndex = 113;
            this._Label08.Text = "Account 2 Name:";
            // 
            // TxBoxAcc1Name
            // 
            this.TxBoxAcc1Name.Location = new System.Drawing.Point(349, 206);
            this.TxBoxAcc1Name.Name = "TxBoxAcc1Name";
            this.TxBoxAcc1Name.Size = new System.Drawing.Size(135, 20);
            this.TxBoxAcc1Name.TabIndex = 114;
            // 
            // TxBoxAcc2Name
            // 
            this.TxBoxAcc2Name.Location = new System.Drawing.Point(349, 232);
            this.TxBoxAcc2Name.Name = "TxBoxAcc2Name";
            this.TxBoxAcc2Name.Size = new System.Drawing.Size(135, 20);
            this.TxBoxAcc2Name.TabIndex = 115;
            // 
            // chBoxAllowDw
            // 
            this.ChBoxAllowDw.AutoSize = true;
            this.ChBoxAllowDw.Location = new System.Drawing.Point(15, 310);
            this.ChBoxAllowDw.Name = "chBoxAllowDw";
            this.ChBoxAllowDw.Size = new System.Drawing.Size(117, 17);
            this.ChBoxAllowDw.TabIndex = 116;
            this.ChBoxAllowDw.Text = "Enable downloader";
            this.ChBoxAllowDw.UseVisualStyleBackColor = true;
            // 
            // TrackVolume
            // 
            this.TrackVolume.LargeChange = 10;
            this.TrackVolume.Location = new System.Drawing.Point(4, 209);
            this.TrackVolume.Maximum = 100;
            this.TrackVolume.Name = "TrackVolume";
            this.TrackVolume.Size = new System.Drawing.Size(180, 45);
            this.TrackVolume.TabIndex = 117;
            this.TrackVolume.TickFrequency = 5;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 362);
            this.ControlBox = false;
            this.Controls.Add(this.TrackVolume);
            this.Controls.Add(this.ChBoxAllowDw);
            this.Controls.Add(this.TxBoxAcc2Name);
            this.Controls.Add(this.TxBoxAcc1Name);
            this.Controls.Add(this._Label08);
            this.Controls.Add(this._Label07);
            this.Controls.Add(this._Label06);
            this.Controls.Add(this._Label04);
            this.Controls.Add(this._Label03);
            this.Controls.Add(this._Label02);
            this.Controls.Add(this._Label01);
            this.Controls.Add(this.TxBoxSoundsFolder);
            this.Controls.Add(this.TxBoxDataFolder);
            this.Controls.Add(this.BtOk);
            this.Controls.Add(this.ChBoxSaveDw);
            this.Controls.Add(this.TxBoxURLDMM);
            this.Controls.Add(this.TxBoxImagesFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TrackVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TxBoxImagesFolder;
        private System.Windows.Forms.TextBox TxBoxURLDMM;
        private System.Windows.Forms.CheckBox ChBoxSaveDw;
        private System.Windows.Forms.Button BtOk;
        private System.Windows.Forms.TextBox TxBoxDataFolder;
        private System.Windows.Forms.TextBox TxBoxSoundsFolder;
        private System.Windows.Forms.Label _Label01;
        private System.Windows.Forms.Label _Label02;
        private System.Windows.Forms.Label _Label03;
        private System.Windows.Forms.Label _Label04;
        private System.Windows.Forms.Label _Label06;
        private System.Windows.Forms.Label _Label07;
        private System.Windows.Forms.Label _Label08;
        private System.Windows.Forms.TextBox TxBoxAcc1Name;
        private System.Windows.Forms.TextBox TxBoxAcc2Name;
        private System.Windows.Forms.CheckBox ChBoxAllowDw;
        private System.Windows.Forms.TrackBar TrackVolume;
    }
}