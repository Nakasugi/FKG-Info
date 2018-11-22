namespace FKG_Info.UserInterface
{
    partial class AboutForm
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
            this.BtOk = new System.Windows.Forms.Button();
            this._lbThanks = new System.Windows.Forms.Label();
            this._lbLostLogia4 = new System.Windows.Forms.LinkLabel();
            this._lbHydroKirby = new System.Windows.Forms.LinkLabel();
            this._lbNakasugi = new System.Windows.Forms.Label();
            this.@__lbAppName = new System.Windows.Forms.Label();
            this._lbVersion = new System.Windows.Forms.Label();
            this.PicBoxNerine = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxNerine)).BeginInit();
            this.SuspendLayout();
            // 
            // BtOk
            // 
            this.BtOk.Location = new System.Drawing.Point(8, 200);
            this.BtOk.Name = "BtOk";
            this.BtOk.Size = new System.Drawing.Size(176, 32);
            this.BtOk.TabIndex = 0;
            this.BtOk.Text = "Ok";
            this.BtOk.UseVisualStyleBackColor = true;
            this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // _lbThanks
            // 
            this._lbThanks.AutoSize = true;
            this._lbThanks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lbThanks.Location = new System.Drawing.Point(196, 108);
            this._lbThanks.Name = "_lbThanks";
            this._lbThanks.Size = new System.Drawing.Size(54, 16);
            this._lbThanks.TabIndex = 1;
            this._lbThanks.Text = "Thanks:";
            // 
            // _lbLostLogia4
            // 
            this._lbLostLogia4.AutoSize = true;
            this._lbLostLogia4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lbLostLogia4.Location = new System.Drawing.Point(200, 128);
            this._lbLostLogia4.Name = "_lbLostLogia4";
            this._lbLostLogia4.Size = new System.Drawing.Size(71, 16);
            this._lbLostLogia4.TabIndex = 2;
            this._lbLostLogia4.TabStop = true;
            this._lbLostLogia4.Text = "LostLogia4";
            this._lbLostLogia4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // _lbHydroKirby
            // 
            this._lbHydroKirby.AutoSize = true;
            this._lbHydroKirby.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lbHydroKirby.Location = new System.Drawing.Point(200, 148);
            this._lbHydroKirby.Name = "_lbHydroKirby";
            this._lbHydroKirby.Size = new System.Drawing.Size(72, 16);
            this._lbHydroKirby.TabIndex = 3;
            this._lbHydroKirby.TabStop = true;
            this._lbHydroKirby.Text = "HydroKirby";
            this._lbHydroKirby.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // _lbNakasugi
            // 
            this._lbNakasugi.AutoSize = true;
            this._lbNakasugi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lbNakasugi.Location = new System.Drawing.Point(196, 68);
            this._lbNakasugi.Name = "_lbNakasugi";
            this._lbNakasugi.Size = new System.Drawing.Size(76, 16);
            this._lbNakasugi.TabIndex = 4;
            this._lbNakasugi.Text = "© Nakasugi";
            // 
            // __lbAppName
            // 
            this.@__lbAppName.AutoSize = true;
            this.@__lbAppName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.@__lbAppName.Location = new System.Drawing.Point(196, 12);
            this.@__lbAppName.Name = "__lbAppName";
            this.@__lbAppName.Size = new System.Drawing.Size(59, 16);
            this.@__lbAppName.TabIndex = 5;
            this.@__lbAppName.Text = "FKG-Info";
            // 
            // _lbVersion
            // 
            this._lbVersion.AutoSize = true;
            this._lbVersion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lbVersion.Location = new System.Drawing.Point(196, 30);
            this._lbVersion.Name = "_lbVersion";
            this._lbVersion.Size = new System.Drawing.Size(51, 16);
            this._lbVersion.TabIndex = 6;
            this._lbVersion.Text = "Version";
            // 
            // PicBoxNerine
            // 
            this.PicBoxNerine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PicBoxNerine.InitialImage = null;
            this.PicBoxNerine.Location = new System.Drawing.Point(8, 8);
            this.PicBoxNerine.Name = "PicBoxNerine";
            this.PicBoxNerine.Size = new System.Drawing.Size(176, 184);
            this.PicBoxNerine.TabIndex = 7;
            this.PicBoxNerine.TabStop = false;
            this.PicBoxNerine.Click += new System.EventHandler(this.PicBoxNerine_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 242);
            this.ControlBox = false;
            this.Controls.Add(this.PicBoxNerine);
            this.Controls.Add(this._lbVersion);
            this.Controls.Add(this.@__lbAppName);
            this.Controls.Add(this._lbNakasugi);
            this.Controls.Add(this._lbHydroKirby);
            this.Controls.Add(this._lbLostLogia4);
            this.Controls.Add(this._lbThanks);
            this.Controls.Add(this.BtOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxNerine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtOk;
        private System.Windows.Forms.Label _lbThanks;
        private System.Windows.Forms.LinkLabel _lbLostLogia4;
        private System.Windows.Forms.LinkLabel _lbHydroKirby;
        private System.Windows.Forms.Label _lbNakasugi;
        private System.Windows.Forms.Label __lbAppName;
        private System.Windows.Forms.Label _lbVersion;
        private System.Windows.Forms.PictureBox PicBoxNerine;
    }
}