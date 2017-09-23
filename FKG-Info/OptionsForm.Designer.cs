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
            this.TxBoxURLNutaku = new System.Windows.Forms.TextBox();
            this.RdBtLocal = new System.Windows.Forms.RadioButton();
            this.RdBtNutakuDMM = new System.Windows.Forms.RadioButton();
            this.RdBtDMM = new System.Windows.Forms.RadioButton();
            this.RdBtNutaku = new System.Windows.Forms.RadioButton();
            this.RdBtDMMNutaku = new System.Windows.Forms.RadioButton();
            this.ChBoxSaveDw = new System.Windows.Forms.CheckBox();
            this.BtOk = new System.Windows.Forms.Button();
            this.TxBoxDataFolder = new System.Windows.Forms.TextBox();
            this.TxBoxSoundsFolder = new System.Windows.Forms.TextBox();
            this._Label01 = new System.Windows.Forms.Label();
            this._Label02 = new System.Windows.Forms.Label();
            this._Label03 = new System.Windows.Forms.Label();
            this._Label05 = new System.Windows.Forms.Label();
            this._Label04 = new System.Windows.Forms.Label();
            this.NumSoundVolume = new System.Windows.Forms.NumericUpDown();
            this._Label06 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumSoundVolume)).BeginInit();
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
            // TxBoxURLNutaku
            // 
            this.TxBoxURLNutaku.Location = new System.Drawing.Point(12, 196);
            this.TxBoxURLNutaku.Name = "TxBoxURLNutaku";
            this.TxBoxURLNutaku.Size = new System.Drawing.Size(472, 20);
            this.TxBoxURLNutaku.TabIndex = 3;
            // 
            // RdBtLocal
            // 
            this.RdBtLocal.AutoSize = true;
            this.RdBtLocal.Location = new System.Drawing.Point(12, 222);
            this.RdBtLocal.Name = "RdBtLocal";
            this.RdBtLocal.Size = new System.Drawing.Size(148, 17);
            this.RdBtLocal.TabIndex = 4;
            this.RdBtLocal.Text = "Use only local image store";
            this.RdBtLocal.UseVisualStyleBackColor = true;
            // 
            // RdBtNutakuDMM
            // 
            this.RdBtNutakuDMM.AutoSize = true;
            this.RdBtNutakuDMM.Location = new System.Drawing.Point(12, 268);
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
            this.RdBtDMM.Location = new System.Drawing.Point(12, 291);
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
            this.RdBtNutaku.Location = new System.Drawing.Point(12, 245);
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
            this.RdBtDMMNutaku.Location = new System.Drawing.Point(12, 314);
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
            this.ChBoxSaveDw.Location = new System.Drawing.Point(12, 337);
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
            // _Label05
            // 
            this._Label05.AutoSize = true;
            this._Label05.Location = new System.Drawing.Point(12, 180);
            this._Label05.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._Label05.Name = "_Label05";
            this._Label05.Size = new System.Drawing.Size(65, 13);
            this._Label05.TabIndex = 108;
            this._Label05.Text = "Nutaku Link";
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
            // NumSoundVolume
            // 
            this.NumSoundVolume.Location = new System.Drawing.Point(428, 222);
            this.NumSoundVolume.Name = "NumSoundVolume";
            this.NumSoundVolume.Size = new System.Drawing.Size(56, 20);
            this.NumSoundVolume.TabIndex = 110;
            // 
            // _Label06
            // 
            this._Label06.AutoSize = true;
            this._Label06.Location = new System.Drawing.Point(346, 224);
            this._Label06.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._Label06.Name = "_Label06";
            this._Label06.Size = new System.Drawing.Size(76, 13);
            this._Label06.TabIndex = 111;
            this._Label06.Text = "Sound Volume";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 362);
            this.ControlBox = false;
            this.Controls.Add(this._Label06);
            this.Controls.Add(this.NumSoundVolume);
            this.Controls.Add(this._Label04);
            this.Controls.Add(this._Label05);
            this.Controls.Add(this._Label03);
            this.Controls.Add(this._Label02);
            this.Controls.Add(this._Label01);
            this.Controls.Add(this.TxBoxSoundsFolder);
            this.Controls.Add(this.TxBoxDataFolder);
            this.Controls.Add(this.BtOk);
            this.Controls.Add(this.ChBoxSaveDw);
            this.Controls.Add(this.RdBtDMMNutaku);
            this.Controls.Add(this.RdBtNutaku);
            this.Controls.Add(this.RdBtDMM);
            this.Controls.Add(this.RdBtNutakuDMM);
            this.Controls.Add(this.RdBtLocal);
            this.Controls.Add(this.TxBoxURLNutaku);
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
            ((System.ComponentModel.ISupportInitialize)(this.NumSoundVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TxBoxImagesFolder;
        private System.Windows.Forms.TextBox TxBoxURLDMM;
        private System.Windows.Forms.TextBox TxBoxURLNutaku;
        private System.Windows.Forms.RadioButton RdBtLocal;
        private System.Windows.Forms.RadioButton RdBtNutakuDMM;
        private System.Windows.Forms.RadioButton RdBtDMM;
        private System.Windows.Forms.RadioButton RdBtNutaku;
        private System.Windows.Forms.RadioButton RdBtDMMNutaku;
        private System.Windows.Forms.CheckBox ChBoxSaveDw;
        private System.Windows.Forms.Button BtOk;
        private System.Windows.Forms.TextBox TxBoxDataFolder;
        private System.Windows.Forms.TextBox TxBoxSoundsFolder;
        private System.Windows.Forms.Label _Label01;
        private System.Windows.Forms.Label _Label02;
        private System.Windows.Forms.Label _Label03;
        private System.Windows.Forms.Label _Label05;
        private System.Windows.Forms.Label _Label04;
        private System.Windows.Forms.NumericUpDown NumSoundVolume;
        private System.Windows.Forms.Label _Label06;
    }
}