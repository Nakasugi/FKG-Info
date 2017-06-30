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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PicBoxBig = new System.Windows.Forms.PictureBox();
            this.PicBoxIconBase = new System.Windows.Forms.PictureBox();
            this.PicBoxIconAwak = new System.Windows.Forms.PictureBox();
            this.PicBoxIconBloom = new System.Windows.Forms.PictureBox();
            this.GridInfo = new System.Windows.Forms.DataGridView();
            this.GIC1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GIC2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtSelect = new System.Windows.Forms.Button();
            this.ChBoxTranslation = new System.Windows.Forms.CheckBox();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MMItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemFileImportMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemFileExperimental = new System.Windows.Forms.ToolStripMenuItem();
            this.getMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemViewMasterSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemImageType = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemMode = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemModeChara = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemModeEquip = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemModeFurniture = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemModeEnemy = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemFileExportMaster = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxBig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconAwak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconBloom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicBoxBig
            // 
            this.PicBoxBig.Location = new System.Drawing.Point(0, 26);
            this.PicBoxBig.Name = "PicBoxBig";
            this.PicBoxBig.Size = new System.Drawing.Size(803, 640);
            this.PicBoxBig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBoxBig.TabIndex = 0;
            this.PicBoxBig.TabStop = false;
            // 
            // PicBoxIconBase
            // 
            this.PicBoxIconBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBoxIconBase.Location = new System.Drawing.Point(809, 12);
            this.PicBoxIconBase.Name = "PicBoxIconBase";
            this.PicBoxIconBase.Size = new System.Drawing.Size(100, 100);
            this.PicBoxIconBase.TabIndex = 1;
            this.PicBoxIconBase.TabStop = false;
            this.PicBoxIconBase.Click += new System.EventHandler(this.PicBoxIconBase_Click);
            // 
            // PicBoxIconAwak
            // 
            this.PicBoxIconAwak.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBoxIconAwak.Location = new System.Drawing.Point(915, 12);
            this.PicBoxIconAwak.Name = "PicBoxIconAwak";
            this.PicBoxIconAwak.Size = new System.Drawing.Size(100, 100);
            this.PicBoxIconAwak.TabIndex = 2;
            this.PicBoxIconAwak.TabStop = false;
            this.PicBoxIconAwak.Click += new System.EventHandler(this.PicBoxIconAwak_Click);
            // 
            // PicBoxIconBloom
            // 
            this.PicBoxIconBloom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicBoxIconBloom.Location = new System.Drawing.Point(1021, 12);
            this.PicBoxIconBloom.Name = "PicBoxIconBloom";
            this.PicBoxIconBloom.Size = new System.Drawing.Size(100, 100);
            this.PicBoxIconBloom.TabIndex = 3;
            this.PicBoxIconBloom.TabStop = false;
            this.PicBoxIconBloom.Click += new System.EventHandler(this.PicBoxIconBloom_Click);
            // 
            // GridInfo
            // 
            this.GridInfo.AllowUserToAddRows = false;
            this.GridInfo.AllowUserToDeleteRows = false;
            this.GridInfo.AllowUserToResizeColumns = false;
            this.GridInfo.AllowUserToResizeRows = false;
            this.GridInfo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridInfo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GIC1,
            this.GIC2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GridInfo.GridColor = System.Drawing.SystemColors.Control;
            this.GridInfo.Location = new System.Drawing.Point(809, 118);
            this.GridInfo.MultiSelect = false;
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridInfo.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.GridInfo.RowTemplate.Height = 18;
            this.GridInfo.RowTemplate.ReadOnly = true;
            this.GridInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridInfo.Size = new System.Drawing.Size(312, 502);
            this.GridInfo.TabIndex = 5;
            // 
            // GIC1
            // 
            this.GIC1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GIC1.HeaderText = "Type";
            this.GIC1.Name = "GIC1";
            this.GIC1.ReadOnly = true;
            this.GIC1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GIC1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GIC1.Width = 84;
            // 
            // GIC2
            // 
            this.GIC2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GIC2.HeaderText = "Info";
            this.GIC2.Name = "GIC2";
            this.GIC2.ReadOnly = true;
            this.GIC2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GIC2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BtSelect
            // 
            this.BtSelect.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtSelect.ForeColor = System.Drawing.Color.Blue;
            this.BtSelect.Location = new System.Drawing.Point(809, 627);
            this.BtSelect.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.BtSelect.Name = "BtSelect";
            this.BtSelect.Size = new System.Drawing.Size(92, 32);
            this.BtSelect.TabIndex = 6;
            this.BtSelect.Text = "<<  SELECT";
            this.BtSelect.UseVisualStyleBackColor = true;
            this.BtSelect.Click += new System.EventHandler(this.BtSelect_Click);
            // 
            // ChBoxTranslation
            // 
            this.ChBoxTranslation.AutoSize = true;
            this.ChBoxTranslation.Checked = true;
            this.ChBoxTranslation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxTranslation.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ChBoxTranslation.Location = new System.Drawing.Point(1043, 636);
            this.ChBoxTranslation.Name = "ChBoxTranslation";
            this.ChBoxTranslation.Size = new System.Drawing.Size(78, 17);
            this.ChBoxTranslation.TabIndex = 19;
            this.ChBoxTranslation.Text = "Translation";
            this.ChBoxTranslation.UseVisualStyleBackColor = true;
            this.ChBoxTranslation.CheckedChanged += new System.EventHandler(this.ChBoxAbilityTranslation_CheckedChanged);
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.SystemColors.Menu;
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItemFile,
            this.MMItemView,
            this.MMItemImageType,
            this.MMItemMode,
            this.MMItemOptions,
            this.selectToolStripMenuItem,
            this.MMItemAbout});
            this.MainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(475, 24);
            this.MainMenu.TabIndex = 23;
            this.MainMenu.Text = "MainMenu";
            // 
            // MMItemFile
            // 
            this.MMItemFile.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItemFileImportMaster,
            this.MMItemFileExportMaster,
            this.MMItemFileExperimental});
            this.MMItemFile.Name = "MMItemFile";
            this.MMItemFile.Size = new System.Drawing.Size(37, 20);
            this.MMItemFile.Text = "&File";
            // 
            // MMItemFileImportMaster
            // 
            this.MMItemFileImportMaster.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemFileImportMaster.Name = "MMItemFileImportMaster";
            this.MMItemFileImportMaster.Size = new System.Drawing.Size(152, 22);
            this.MMItemFileImportMaster.Text = "Import Master";
            this.MMItemFileImportMaster.Click += new System.EventHandler(this.MMItemFileImportMaster_Click);
            // 
            // MMItemFileExperimental
            // 
            this.MMItemFileExperimental.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemFileExperimental.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getMasterToolStripMenuItem});
            this.MMItemFileExperimental.Name = "MMItemFileExperimental";
            this.MMItemFileExperimental.Size = new System.Drawing.Size(152, 22);
            this.MMItemFileExperimental.Text = "Experimental";
            // 
            // getMasterToolStripMenuItem
            // 
            this.getMasterToolStripMenuItem.BackColor = System.Drawing.SystemColors.Menu;
            this.getMasterToolStripMenuItem.Name = "getMasterToolStripMenuItem";
            this.getMasterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.getMasterToolStripMenuItem.Text = "getMaster";
            this.getMasterToolStripMenuItem.Click += new System.EventHandler(this.MMItemFileGetMaster_Click);
            // 
            // MMItemView
            // 
            this.MMItemView.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItemViewMasterSummary});
            this.MMItemView.ImageTransparentColor = System.Drawing.SystemColors.Menu;
            this.MMItemView.Name = "MMItemView";
            this.MMItemView.Size = new System.Drawing.Size(44, 20);
            this.MMItemView.Text = "&View";
            // 
            // MMItemViewMasterSummary
            // 
            this.MMItemViewMasterSummary.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemViewMasterSummary.Name = "MMItemViewMasterSummary";
            this.MMItemViewMasterSummary.Size = new System.Drawing.Size(164, 22);
            this.MMItemViewMasterSummary.Text = "Master Summary";
            this.MMItemViewMasterSummary.Click += new System.EventHandler(this.MMItemViewMasterSummary_Click);
            // 
            // MMItemImageType
            // 
            this.MMItemImageType.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemImageType.Name = "MMItemImageType";
            this.MMItemImageType.Size = new System.Drawing.Size(81, 20);
            this.MMItemImageType.Text = "Image &Type";
            // 
            // MMItemMode
            // 
            this.MMItemMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItemModeChara,
            this.MMItemModeEquip,
            this.MMItemModeFurniture,
            this.MMItemModeEnemy});
            this.MMItemMode.Name = "MMItemMode";
            this.MMItemMode.Size = new System.Drawing.Size(50, 20);
            this.MMItemMode.Text = "&Mode";
            // 
            // MMItemModeChara
            // 
            this.MMItemModeChara.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemModeChara.Checked = true;
            this.MMItemModeChara.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MMItemModeChara.Name = "MMItemModeChara";
            this.MMItemModeChara.Size = new System.Drawing.Size(132, 22);
            this.MMItemModeChara.Text = "Characters";
            // 
            // MMItemModeEquip
            // 
            this.MMItemModeEquip.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemModeEquip.Name = "MMItemModeEquip";
            this.MMItemModeEquip.Size = new System.Drawing.Size(132, 22);
            this.MMItemModeEquip.Text = "Equipment";
            // 
            // MMItemModeFurniture
            // 
            this.MMItemModeFurniture.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemModeFurniture.Name = "MMItemModeFurniture";
            this.MMItemModeFurniture.Size = new System.Drawing.Size(132, 22);
            this.MMItemModeFurniture.Text = "Furnitures";
            // 
            // MMItemModeEnemy
            // 
            this.MMItemModeEnemy.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemModeEnemy.Name = "MMItemModeEnemy";
            this.MMItemModeEnemy.Size = new System.Drawing.Size(132, 22);
            this.MMItemModeEnemy.Text = "Enemies";
            // 
            // MMItemOptions
            // 
            this.MMItemOptions.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemOptions.Name = "MMItemOptions";
            this.MMItemOptions.Size = new System.Drawing.Size(61, 20);
            this.MMItemOptions.Text = "&Options";
            this.MMItemOptions.Click += new System.EventHandler(this.MMItemOptions_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.selectToolStripMenuItem.Text = "&Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.BtSelect_Click);
            // 
            // MMItemAbout
            // 
            this.MMItemAbout.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemAbout.Name = "MMItemAbout";
            this.MMItemAbout.Size = new System.Drawing.Size(52, 20);
            this.MMItemAbout.Text = "&About";
            // 
            // MMItemFileExportMaster
            // 
            this.MMItemFileExportMaster.BackColor = System.Drawing.SystemColors.Menu;
            this.MMItemFileExportMaster.Name = "MMItemFileExportMaster";
            this.MMItemFileExportMaster.Size = new System.Drawing.Size(152, 22);
            this.MMItemFileExportMaster.Text = "Export Master";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 666);
            this.Controls.Add(this.ChBoxTranslation);
            this.Controls.Add(this.BtSelect);
            this.Controls.Add(this.GridInfo);
            this.Controls.Add(this.PicBoxIconBloom);
            this.Controls.Add(this.PicBoxIconAwak);
            this.Controls.Add(this.PicBoxIconBase);
            this.Controls.Add(this.PicBoxBig);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1148, 704);
            this.MinimumSize = new System.Drawing.Size(1148, 704);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FKG-Info";
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxBig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconAwak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxIconBloom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
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
        private System.Windows.Forms.CheckBox ChBoxTranslation;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MMItemOptions;
        private System.Windows.Forms.ToolStripMenuItem MMItemAbout;
        private System.Windows.Forms.ToolStripMenuItem MMItemFile;
        private System.Windows.Forms.ToolStripMenuItem MMItemView;
        private System.Windows.Forms.ToolStripMenuItem MMItemImageType;
        private System.Windows.Forms.ToolStripMenuItem MMItemViewMasterSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn GIC1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GIC2;
        private System.Windows.Forms.ToolStripMenuItem MMItemMode;
        private System.Windows.Forms.ToolStripMenuItem MMItemModeChara;
        private System.Windows.Forms.ToolStripMenuItem MMItemModeEquip;
        private System.Windows.Forms.ToolStripMenuItem MMItemModeFurniture;
        private System.Windows.Forms.ToolStripMenuItem MMItemModeEnemy;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileImportMaster;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileExperimental;
        private System.Windows.Forms.ToolStripMenuItem getMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileExportMaster;
    }
}

