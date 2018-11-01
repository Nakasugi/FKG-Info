namespace FKG_Info
{
    partial class MassDownloaderForm
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
            this.BtCancel = new System.Windows.Forms.Button();
            this.BtStart = new System.Windows.Forms.Button();
            this.BtSelectAll = new System.Windows.Forms.Button();
            this.BtDeselectAll = new System.Windows.Forms.Button();
            this._lb01 = new System.Windows.Forms.Label();
            this._lb02 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtCancel
            // 
            this.BtCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtCancel.Location = new System.Drawing.Point(270, 305);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 40);
            this.BtCancel.TabIndex = 0;
            this.BtCancel.Text = "Cancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // BtStart
            // 
            this.BtStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtStart.Location = new System.Drawing.Point(184, 305);
            this.BtStart.Name = "BtStart";
            this.BtStart.Size = new System.Drawing.Size(80, 40);
            this.BtStart.TabIndex = 1;
            this.BtStart.Text = "Start";
            this.BtStart.UseVisualStyleBackColor = true;
            this.BtStart.Click += new System.EventHandler(this.BtStart_Click);
            // 
            // BtSelectAll
            // 
            this.BtSelectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtSelectAll.Location = new System.Drawing.Point(12, 305);
            this.BtSelectAll.Name = "BtSelectAll";
            this.BtSelectAll.Size = new System.Drawing.Size(80, 40);
            this.BtSelectAll.TabIndex = 2;
            this.BtSelectAll.Text = "Select All";
            this.BtSelectAll.UseVisualStyleBackColor = true;
            this.BtSelectAll.Click += new System.EventHandler(this.BtSelectAll_Click);
            // 
            // BtDeselectAll
            // 
            this.BtDeselectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtDeselectAll.Location = new System.Drawing.Point(98, 305);
            this.BtDeselectAll.Name = "BtDeselectAll";
            this.BtDeselectAll.Size = new System.Drawing.Size(80, 40);
            this.BtDeselectAll.TabIndex = 3;
            this.BtDeselectAll.Text = "Deselect All";
            this.BtDeselectAll.UseVisualStyleBackColor = true;
            this.BtDeselectAll.Click += new System.EventHandler(this.BtDeselectAll_Click);
            // 
            // _lb01
            // 
            this._lb01.AutoSize = true;
            this._lb01.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lb01.Location = new System.Drawing.Point(12, 9);
            this._lb01.Name = "_lb01";
            this._lb01.Size = new System.Drawing.Size(116, 16);
            this._lb01.TabIndex = 4;
            this._lb01.Text = "Select image type";
            // 
            // _lb02
            // 
            this._lb02.AutoSize = true;
            this._lb02.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lb02.Location = new System.Drawing.Point(12, 264);
            this._lb02.Name = "_lb02";
            this._lb02.Size = new System.Drawing.Size(129, 16);
            this._lb02.TabIndex = 5;
            this._lb02.Text = "No images selected";
            // 
            // MassDownloaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 357);
            this.ControlBox = false;
            this.Controls.Add(this._lb02);
            this.Controls.Add(this._lb01);
            this.Controls.Add(this.BtDeselectAll);
            this.Controls.Add(this.BtSelectAll);
            this.Controls.Add(this.BtStart);
            this.Controls.Add(this.BtCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MassDownloaderForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.Button BtStart;
        private System.Windows.Forms.Button BtSelectAll;
        private System.Windows.Forms.Button BtDeselectAll;
        private System.Windows.Forms.Label _lb01;
        private System.Windows.Forms.Label _lb02;
    }
}