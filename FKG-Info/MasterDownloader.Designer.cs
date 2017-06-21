namespace FKG_Info
{
    partial class MasterDownloader
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
            this.TxBoxInfo = new System.Windows.Forms.TextBox();
            this.BtDownload = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.BtClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxBoxInfo
            // 
            this.TxBoxInfo.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxBoxInfo.Location = new System.Drawing.Point(12, 12);
            this.TxBoxInfo.Multiline = true;
            this.TxBoxInfo.Name = "TxBoxInfo";
            this.TxBoxInfo.ReadOnly = true;
            this.TxBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxBoxInfo.Size = new System.Drawing.Size(876, 589);
            this.TxBoxInfo.TabIndex = 0;
            // 
            // BtDownload
            // 
            this.BtDownload.Location = new System.Drawing.Point(12, 607);
            this.BtDownload.Name = "BtDownload";
            this.BtDownload.Size = new System.Drawing.Size(100, 32);
            this.BtDownload.TabIndex = 1;
            this.BtDownload.Text = "Download";
            this.BtDownload.UseVisualStyleBackColor = true;
            this.BtDownload.Click += new System.EventHandler(this.BtDownload_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(788, 607);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "Download DMM";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // BtClear
            // 
            this.BtClear.Location = new System.Drawing.Point(118, 607);
            this.BtClear.Name = "BtClear";
            this.BtClear.Size = new System.Drawing.Size(100, 32);
            this.BtClear.TabIndex = 3;
            this.BtClear.Text = "Clear";
            this.BtClear.UseVisualStyleBackColor = true;
            this.BtClear.Click += new System.EventHandler(this.BtClear_Click);
            // 
            // MasterDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 651);
            this.Controls.Add(this.BtClear);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BtDownload);
            this.Controls.Add(this.TxBoxInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MasterDownloader";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MasterDownloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxBoxInfo;
        private System.Windows.Forms.Button BtDownload;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button BtClear;
    }
}