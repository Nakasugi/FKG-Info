namespace FKG_Info.UserInterface
{
    partial class InputDownloadID
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
            this._lb01 = new System.Windows.Forms.Label();
            this.BtOk = new System.Windows.Forms.Button();
            this.BtCancel = new System.Windows.Forms.Button();
            this.TxBoxID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _lb01
            // 
            this._lb01.AutoSize = true;
            this._lb01.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lb01.Location = new System.Drawing.Point(16, 16);
            this._lb01.Name = "_lb01";
            this._lb01.Size = new System.Drawing.Size(19, 15);
            this._lb01.TabIndex = 0;
            this._lb01.Text = "ID";
            // 
            // BtOk
            // 
            this.BtOk.Location = new System.Drawing.Point(12, 68);
            this.BtOk.Name = "BtOk";
            this.BtOk.Size = new System.Drawing.Size(64, 32);
            this.BtOk.TabIndex = 1;
            this.BtOk.Text = "Ok";
            this.BtOk.UseVisualStyleBackColor = true;
            this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // BtCancel
            // 
            this.BtCancel.Location = new System.Drawing.Point(82, 68);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(64, 32);
            this.BtCancel.TabIndex = 2;
            this.BtCancel.Text = "Cancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // TxBoxID
            // 
            this.TxBoxID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxBoxID.Location = new System.Drawing.Point(40, 14);
            this.TxBoxID.Name = "TxBoxID";
            this.TxBoxID.Size = new System.Drawing.Size(95, 21);
            this.TxBoxID.TabIndex = 3;
            // 
            // InputDownloadID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(158, 112);
            this.Controls.Add(this.TxBoxID);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOk);
            this.Controls.Add(this._lb01);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InputDownloadID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Input Download ID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lb01;
        private System.Windows.Forms.Button BtOk;
        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.TextBox TxBoxID;
    }
}