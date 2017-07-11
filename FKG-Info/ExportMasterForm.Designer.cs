namespace FKG_Info
{
    partial class ExportMasterForm
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
            this.BtExport = new System.Windows.Forms.Button();
            this.LsBoxMasterFields = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // BtCancel
            // 
            this.BtCancel.Location = new System.Drawing.Point(404, 434);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(80, 32);
            this.BtCancel.TabIndex = 0;
            this.BtCancel.Text = "Cancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // BtExport
            // 
            this.BtExport.Location = new System.Drawing.Point(404, 396);
            this.BtExport.Name = "BtExport";
            this.BtExport.Size = new System.Drawing.Size(80, 32);
            this.BtExport.TabIndex = 1;
            this.BtExport.Text = "Export";
            this.BtExport.UseVisualStyleBackColor = true;
            this.BtExport.Click += new System.EventHandler(this.BtExport_Click);
            // 
            // LsBoxMasterFields
            // 
            this.LsBoxMasterFields.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LsBoxMasterFields.FormattingEnabled = true;
            this.LsBoxMasterFields.ItemHeight = 15;
            this.LsBoxMasterFields.Location = new System.Drawing.Point(12, 12);
            this.LsBoxMasterFields.Name = "LsBoxMasterFields";
            this.LsBoxMasterFields.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LsBoxMasterFields.Size = new System.Drawing.Size(386, 454);
            this.LsBoxMasterFields.TabIndex = 2;
            // 
            // ExportMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 477);
            this.ControlBox = false;
            this.Controls.Add(this.LsBoxMasterFields);
            this.Controls.Add(this.BtExport);
            this.Controls.Add(this.BtCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExportMasterForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select fields to export:";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtCancel;
        private System.Windows.Forms.Button BtExport;
        private System.Windows.Forms.ListBox LsBoxMasterFields;
    }
}