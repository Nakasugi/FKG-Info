﻿namespace FKG_Info.UserInterface
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GridInfo = new System.Windows.Forms.DataGridView();
            this.GIC1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GIC2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtSelect = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MMItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemFileExportMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemFileExportIDs = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemFileExportNames = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemFileExportCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemFileExportIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemFileDownloadImages = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemFileDownloadByID = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MMItemFileExperimental = new System.Windows.Forms.ToolStripMenuItem();
            this.getMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemImageType = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemMode = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemModeChara = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemModeEquip = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemModeFurniture = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemModeEnemy = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.MMItemNext = new System.Windows.Forms.ToolStripMenuItem();
            this._lbLoading = new System.Windows.Forms.Label();
            this._lbWait = new System.Windows.Forms.Label();
            this.BtEquip = new System.Windows.Forms.Button();
            this.BtVoices = new System.Windows.Forms.Button();
            this.BtEvolutions = new System.Windows.Forms.Button();
            this.BtVariations = new System.Windows.Forms.Button();
            this.BtPrevEvolution = new System.Windows.Forms.Button();
            this.CmBoxSkins = new System.Windows.Forms.ComboBox();
            this.Icon3rd = new FKG_Info.UserInterface.FastIcon(this.components);
            this.Icon2nd = new FKG_Info.UserInterface.FastIcon(this.components);
            this.IconMain = new FKG_Info.UserInterface.FastIcon(this.components);
            this.PicBoxBig = new FKG_Info.UserInterface.AdvPictureBox(this.components);
            this.PgBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).BeginInit();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxBig)).BeginInit();
            this.SuspendLayout();
            // 
            // GridInfo
            // 
            this.GridInfo.AllowUserToAddRows = false;
            this.GridInfo.AllowUserToDeleteRows = false;
            this.GridInfo.AllowUserToResizeColumns = false;
            this.GridInfo.AllowUserToResizeRows = false;
            this.GridInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridInfo.DefaultCellStyle = dataGridViewCellStyle3;
            this.GridInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GridInfo.GridColor = System.Drawing.SystemColors.Control;
            this.GridInfo.Location = new System.Drawing.Point(809, 118);
            this.GridInfo.MultiSelect = false;
            this.GridInfo.Name = "GridInfo";
            this.GridInfo.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.GridInfo.RowHeadersVisible = false;
            this.GridInfo.RowTemplate.Height = 18;
            this.GridInfo.RowTemplate.ReadOnly = true;
            this.GridInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridInfo.Size = new System.Drawing.Size(364, 522);
            this.GridInfo.TabIndex = 5;
            this.GridInfo.TabStop = false;
            this.GridInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GridInfo_MouseMove);
            // 
            // GIC1
            // 
            this.GIC1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GIC1.DefaultCellStyle = dataGridViewCellStyle2;
            this.GIC1.HeaderText = "Type";
            this.GIC1.Name = "GIC1";
            this.GIC1.ReadOnly = true;
            this.GIC1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GIC1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GIC1.Width = 92;
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
            this.BtSelect.Location = new System.Drawing.Point(809, 647);
            this.BtSelect.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.BtSelect.Name = "BtSelect";
            this.BtSelect.Size = new System.Drawing.Size(92, 32);
            this.BtSelect.TabIndex = 6;
            this.BtSelect.Text = "<<  SELECT";
            this.BtSelect.UseVisualStyleBackColor = true;
            this.BtSelect.Click += new System.EventHandler(this.FlowerSelect_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItemFile,
            this.MMItemImageType,
            this.MMItemMode,
            this.MMItemOptions,
            this.MMItemSelect,
            this.MMItemAbout,
            this.MMItemPrev,
            this.MMItemNext});
            this.MainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MainMenu.Size = new System.Drawing.Size(529, 24);
            this.MainMenu.TabIndex = 23;
            this.MainMenu.Text = "MainMenu";
            // 
            // MMItemFile
            // 
            this.MMItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MMItemFileExportMaster,
            this.MMItemFileExportIDs,
            this.MMItemFileExportNames,
            this.MMItemFileExportCurrent,
            this.MMItemFileExportIcons,
            this.MMItemFileDownloadImages,
            this.MMItemFileDownloadByID,
            this.toolStripSeparator1,
            this.MMItemFileExperimental});
            this.MMItemFile.Name = "MMItemFile";
            this.MMItemFile.Size = new System.Drawing.Size(37, 20);
            this.MMItemFile.Text = "&File";
            // 
            // MMItemFileExportMaster
            // 
            this.MMItemFileExportMaster.Name = "MMItemFileExportMaster";
            this.MMItemFileExportMaster.Size = new System.Drawing.Size(215, 22);
            this.MMItemFileExportMaster.Text = "Export Master";
            this.MMItemFileExportMaster.Click += new System.EventHandler(this.MMItemFileExportMaster_Click);
            // 
            // MMItemFileExportIDs
            // 
            this.MMItemFileExportIDs.Name = "MMItemFileExportIDs";
            this.MMItemFileExportIDs.Size = new System.Drawing.Size(215, 22);
            this.MMItemFileExportIDs.Text = "Export IDs";
            this.MMItemFileExportIDs.Click += new System.EventHandler(this.MMItemFileExportIDs_Click);
            // 
            // MMItemFileExportNames
            // 
            this.MMItemFileExportNames.Name = "MMItemFileExportNames";
            this.MMItemFileExportNames.Size = new System.Drawing.Size(215, 22);
            this.MMItemFileExportNames.Text = "Export Names";
            this.MMItemFileExportNames.Click += new System.EventHandler(this.MMItemFileExportNames_Click);
            // 
            // MMItemFileExportCurrent
            // 
            this.MMItemFileExportCurrent.Name = "MMItemFileExportCurrent";
            this.MMItemFileExportCurrent.Size = new System.Drawing.Size(215, 22);
            this.MMItemFileExportCurrent.Text = "Export Current Knight Data";
            this.MMItemFileExportCurrent.Click += new System.EventHandler(this.MMItemFileExportCurrent_Click);
            // 
            // MMItemFileExportIcons
            // 
            this.MMItemFileExportIcons.Name = "MMItemFileExportIcons";
            this.MMItemFileExportIcons.Size = new System.Drawing.Size(215, 22);
            this.MMItemFileExportIcons.Text = "Export Icons";
            this.MMItemFileExportIcons.Click += new System.EventHandler(this.MMItemFileExportIcons_Click);
            // 
            // MMItemFileDownloadImages
            // 
            this.MMItemFileDownloadImages.Name = "MMItemFileDownloadImages";
            this.MMItemFileDownloadImages.Size = new System.Drawing.Size(215, 22);
            this.MMItemFileDownloadImages.Text = "Download Images";
            this.MMItemFileDownloadImages.Click += new System.EventHandler(this.MMItemFileDownloadImages_Click);
            // 
            // MMItemFileDownloadByID
            // 
            this.MMItemFileDownloadByID.Name = "MMItemFileDownloadByID";
            this.MMItemFileDownloadByID.Size = new System.Drawing.Size(215, 22);
            this.MMItemFileDownloadByID.Text = "Download by ID";
            this.MMItemFileDownloadByID.Click += new System.EventHandler(this.MMItemFileDownloadByID_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // MMItemFileExperimental
            // 
            this.MMItemFileExperimental.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getMasterToolStripMenuItem});
            this.MMItemFileExperimental.Name = "MMItemFileExperimental";
            this.MMItemFileExperimental.Size = new System.Drawing.Size(215, 22);
            this.MMItemFileExperimental.Text = "Experimental";
            // 
            // getMasterToolStripMenuItem
            // 
            this.getMasterToolStripMenuItem.Name = "getMasterToolStripMenuItem";
            this.getMasterToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.getMasterToolStripMenuItem.Text = "getMaster";
            this.getMasterToolStripMenuItem.Click += new System.EventHandler(this.MMItemFileGetMaster_Click);
            // 
            // MMItemImageType
            // 
            this.MMItemImageType.ImageTransparentColor = System.Drawing.SystemColors.Menu;
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
            this.MMItemModeChara.Checked = true;
            this.MMItemModeChara.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MMItemModeChara.Name = "MMItemModeChara";
            this.MMItemModeChara.Size = new System.Drawing.Size(137, 22);
            this.MMItemModeChara.Text = "Characters";
            // 
            // MMItemModeEquip
            // 
            this.MMItemModeEquip.Name = "MMItemModeEquip";
            this.MMItemModeEquip.Size = new System.Drawing.Size(137, 22);
            this.MMItemModeEquip.Text = "Equipments";
            // 
            // MMItemModeFurniture
            // 
            this.MMItemModeFurniture.Name = "MMItemModeFurniture";
            this.MMItemModeFurniture.Size = new System.Drawing.Size(137, 22);
            this.MMItemModeFurniture.Text = "Furnitures";
            // 
            // MMItemModeEnemy
            // 
            this.MMItemModeEnemy.Name = "MMItemModeEnemy";
            this.MMItemModeEnemy.Size = new System.Drawing.Size(137, 22);
            this.MMItemModeEnemy.Text = "Enemies";
            // 
            // MMItemOptions
            // 
            this.MMItemOptions.Name = "MMItemOptions";
            this.MMItemOptions.Size = new System.Drawing.Size(61, 20);
            this.MMItemOptions.Text = "&Options";
            this.MMItemOptions.Click += new System.EventHandler(this.MMItemOptions_Click);
            // 
            // MMItemSelect
            // 
            this.MMItemSelect.Name = "MMItemSelect";
            this.MMItemSelect.Size = new System.Drawing.Size(50, 20);
            this.MMItemSelect.Text = "&Select";
            this.MMItemSelect.Click += new System.EventHandler(this.FlowerSelect_Click);
            // 
            // MMItemAbout
            // 
            this.MMItemAbout.Name = "MMItemAbout";
            this.MMItemAbout.Size = new System.Drawing.Size(52, 20);
            this.MMItemAbout.Text = "&About";
            this.MMItemAbout.Click += new System.EventHandler(this.MMItemAbout_Click);
            // 
            // MMItemPrev
            // 
            this.MMItemPrev.Name = "MMItemPrev";
            this.MMItemPrev.Size = new System.Drawing.Size(35, 20);
            this.MMItemPrev.Text = "<<";
            this.MMItemPrev.Click += new System.EventHandler(this.MMItemPrev_Click);
            // 
            // MMItemNext
            // 
            this.MMItemNext.Name = "MMItemNext";
            this.MMItemNext.Size = new System.Drawing.Size(35, 20);
            this.MMItemNext.Text = ">>";
            this.MMItemNext.Click += new System.EventHandler(this.MMItemNext_Click);
            // 
            // _lbLoading
            // 
            this._lbLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lbLoading.Location = new System.Drawing.Point(603, 9);
            this._lbLoading.Name = "_lbLoading";
            this._lbLoading.Size = new System.Drawing.Size(200, 13);
            this._lbLoading.TabIndex = 24;
            this._lbLoading.Text = "Loading ...";
            this._lbLoading.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this._lbLoading.Visible = false;
            // 
            // _lbWait
            // 
            this._lbWait.AutoSize = true;
            this._lbWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._lbWait.Location = new System.Drawing.Point(340, 300);
            this._lbWait.Name = "_lbWait";
            this._lbWait.Size = new System.Drawing.Size(125, 25);
            this._lbWait.TabIndex = 25;
            this._lbWait.Text = "Loading . . .";
            this._lbWait.Visible = false;
            // 
            // BtEquip
            // 
            this.BtEquip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtEquip.ForeColor = System.Drawing.Color.Maroon;
            this.BtEquip.Location = new System.Drawing.Point(907, 647);
            this.BtEquip.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.BtEquip.Name = "BtEquip";
            this.BtEquip.Size = new System.Drawing.Size(92, 32);
            this.BtEquip.TabIndex = 27;
            this.BtEquip.Text = "Equipment";
            this.BtEquip.UseVisualStyleBackColor = true;
            this.BtEquip.Click += new System.EventHandler(this.BtEquip_Click);
            // 
            // BtVoices
            // 
            this.BtVoices.BackgroundImage = global::FKG_Info.Properties.Resources.chbox_ico_sound;
            this.BtVoices.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtVoices.Location = new System.Drawing.Point(1005, 647);
            this.BtVoices.Name = "BtVoices";
            this.BtVoices.Size = new System.Drawing.Size(32, 32);
            this.BtVoices.TabIndex = 29;
            this.BtVoices.UseVisualStyleBackColor = true;
            this.BtVoices.Click += new System.EventHandler(this.BtVoices_Click);
            // 
            // BtEvolutions
            // 
            this.BtEvolutions.Font = new System.Drawing.Font("Calibri", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtEvolutions.Location = new System.Drawing.Point(914, 12);
            this.BtEvolutions.Name = "BtEvolutions";
            this.BtEvolutions.Size = new System.Drawing.Size(48, 28);
            this.BtEvolutions.TabIndex = 30;
            this.BtEvolutions.Text = "All Evolutions";
            this.BtEvolutions.UseVisualStyleBackColor = true;
            this.BtEvolutions.Click += new System.EventHandler(this.BtEvolutions_Click);
            // 
            // BtVariations
            // 
            this.BtVariations.Font = new System.Drawing.Font("Calibri", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtVariations.Location = new System.Drawing.Point(914, 42);
            this.BtVariations.Name = "BtVariations";
            this.BtVariations.Size = new System.Drawing.Size(48, 28);
            this.BtVariations.TabIndex = 33;
            this.BtVariations.Text = "All Variations";
            this.BtVariations.UseVisualStyleBackColor = true;
            this.BtVariations.Click += new System.EventHandler(this.BtVariations_Click);
            // 
            // BtPrevEvolution
            // 
            this.BtPrevEvolution.Font = new System.Drawing.Font("Calibri", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtPrevEvolution.Location = new System.Drawing.Point(914, 84);
            this.BtPrevEvolution.Name = "BtPrevEvolution";
            this.BtPrevEvolution.Size = new System.Drawing.Size(48, 28);
            this.BtPrevEvolution.TabIndex = 34;
            this.BtPrevEvolution.Text = "Prev Evolution";
            this.BtPrevEvolution.UseVisualStyleBackColor = true;
            this.BtPrevEvolution.Click += new System.EventHandler(this.BtPrevEvolution_Click);
            // 
            // CmBoxSkins
            // 
            this.CmBoxSkins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmBoxSkins.Enabled = false;
            this.CmBoxSkins.FormattingEnabled = true;
            this.CmBoxSkins.Location = new System.Drawing.Point(1043, 648);
            this.CmBoxSkins.Name = "CmBoxSkins";
            this.CmBoxSkins.Size = new System.Drawing.Size(130, 21);
            this.CmBoxSkins.TabIndex = 39;
            this.CmBoxSkins.SelectedIndexChanged += new System.EventHandler(this.CmBoxSkins_SelectedIndexChanged);
            // 
            // Icon3rd
            // 
            this.Icon3rd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Icon3rd.Location = new System.Drawing.Point(1072, 12);
            this.Icon3rd.Name = "Icon3rd";
            this.Icon3rd.Size = new System.Drawing.Size(100, 100);
            this.Icon3rd.TabIndex = 37;
            this.Icon3rd.Click += new System.EventHandler(this.PicBoxIconV2_Click);
            // 
            // Icon2nd
            // 
            this.Icon2nd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Icon2nd.Location = new System.Drawing.Point(967, 12);
            this.Icon2nd.Name = "Icon2nd";
            this.Icon2nd.Size = new System.Drawing.Size(100, 100);
            this.Icon2nd.TabIndex = 36;
            this.Icon2nd.Click += new System.EventHandler(this.PicBoxIconV1_Click);
            // 
            // IconMain
            // 
            this.IconMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IconMain.Location = new System.Drawing.Point(809, 12);
            this.IconMain.Name = "IconMain";
            this.IconMain.Size = new System.Drawing.Size(100, 100);
            this.IconMain.TabIndex = 35;
            this.IconMain.Click += new System.EventHandler(this.PicBoxIconBase_Click);
            this.IconMain.DoubleClick += new System.EventHandler(this.PicBoxIconBase_DoubleClick);
            // 
            // PicBoxBig
            // 
            this.PicBoxBig.Image = null;
            this.PicBoxBig.Location = new System.Drawing.Point(0, 46);
            this.PicBoxBig.Name = "PicBoxBig";
            this.PicBoxBig.Size = new System.Drawing.Size(803, 640);
            this.PicBoxBig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBoxBig.TabIndex = 0;
            this.PicBoxBig.TabStop = false;
            this.PicBoxBig.Visible = false;
            this.PicBoxBig.Click += new System.EventHandler(this.PicBoxBig_Click);
            // 
            // PgBar
            // 
            this.PgBar.Location = new System.Drawing.Point(412, 4);
            this.PgBar.Name = "PgBar";
            this.PgBar.Size = new System.Drawing.Size(185, 16);
            this.PgBar.TabIndex = 40;
            this.PgBar.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1184, 686);
            this.Controls.Add(this.PgBar);
            this.Controls.Add(this.CmBoxSkins);
            this.Controls.Add(this.Icon3rd);
            this.Controls.Add(this.Icon2nd);
            this.Controls.Add(this.IconMain);
            this.Controls.Add(this.BtPrevEvolution);
            this.Controls.Add(this.BtVariations);
            this.Controls.Add(this.BtEvolutions);
            this.Controls.Add(this.BtVoices);
            this.Controls.Add(this.BtEquip);
            this.Controls.Add(this._lbLoading);
            this.Controls.Add(this.BtSelect);
            this.Controls.Add(this.GridInfo);
            this.Controls.Add(this.PicBoxBig);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this._lbWait);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FKG-Info";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxBig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AdvPictureBox PicBoxBig;
        private System.Windows.Forms.DataGridView GridInfo;
        private System.Windows.Forms.Button BtSelect;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MMItemOptions;
        private System.Windows.Forms.ToolStripMenuItem MMItemAbout;
        private System.Windows.Forms.ToolStripMenuItem MMItemFile;
        private System.Windows.Forms.ToolStripMenuItem MMItemImageType;
        private System.Windows.Forms.ToolStripMenuItem MMItemMode;
        private System.Windows.Forms.ToolStripMenuItem MMItemModeChara;
        private System.Windows.Forms.ToolStripMenuItem MMItemModeEquip;
        private System.Windows.Forms.ToolStripMenuItem MMItemModeFurniture;
        private System.Windows.Forms.ToolStripMenuItem MMItemModeEnemy;
        private System.Windows.Forms.ToolStripMenuItem MMItemSelect;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileExperimental;
        private System.Windows.Forms.ToolStripMenuItem getMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileExportMaster;
        private System.Windows.Forms.Label _lbLoading;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label _lbWait;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileExportIDs;
        private System.Windows.Forms.Button BtEquip;
        private System.Windows.Forms.Button BtVoices;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileExportNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn GIC1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GIC2;
        private System.Windows.Forms.Button BtEvolutions;
        private System.Windows.Forms.Button BtVariations;
        private System.Windows.Forms.Button BtPrevEvolution;
        private System.Windows.Forms.ToolStripMenuItem MMItemPrev;
        private System.Windows.Forms.ToolStripMenuItem MMItemNext;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileExportCurrent;
        private FastIcon IconMain;
        private FastIcon Icon2nd;
        private FastIcon Icon3rd;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileExportIcons;
        private System.Windows.Forms.ComboBox CmBoxSkins;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileDownloadImages;
        private System.Windows.Forms.ProgressBar PgBar;
        private System.Windows.Forms.ToolStripMenuItem MMItemFileDownloadByID;
    }
}