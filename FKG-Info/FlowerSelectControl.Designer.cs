namespace FKG_Info
{
    partial class FlowerSelectControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PanelFlowers = new System.Windows.Forms.Panel();
            this.BtAll = new System.Windows.Forms.Button();
            this.ChBoxMagic = new System.Windows.Forms.CheckBox();
            this.ChBoxPierce = new System.Windows.Forms.CheckBox();
            this.ChBoxBlunt = new System.Windows.Forms.CheckBox();
            this.ChBoxSlash = new System.Windows.Forms.CheckBox();
            this.ChBoxS6 = new System.Windows.Forms.CheckBox();
            this.ChBoxS5 = new System.Windows.Forms.CheckBox();
            this.ChBoxS4 = new System.Windows.Forms.CheckBox();
            this.ChBoxS3 = new System.Windows.Forms.CheckBox();
            this.ChBoxS2 = new System.Windows.Forms.CheckBox();
            this.CmBoxAbility01 = new System.Windows.Forms.ComboBox();
            this.CmBoxSort = new System.Windows.Forms.ComboBox();
            this.CmBoxNation = new System.Windows.Forms.ComboBox();
            this.TxBoxSearch = new System.Windows.Forms.TextBox();
            this.CmBoxSpecFilter = new System.Windows.Forms.ComboBox();
            this.CmBoxAbility02 = new System.Windows.Forms.ComboBox();
            this.BtClrSearch = new System.Windows.Forms.Button();
            this.ChBoxEventKnights = new System.Windows.Forms.CheckBox();
            this.ChBoxBloomCG = new System.Windows.Forms.CheckBox();
            this.ChBoxAcc1Has = new System.Windows.Forms.CheckBox();
            this.ChBoxAcc2Has = new System.Windows.Forms.CheckBox();
            this.ChBoxAcc1Filter = new System.Windows.Forms.CheckBox();
            this.ChBoxAcc2Filter = new System.Windows.Forms.CheckBox();
            this.CmBoxVariations = new System.Windows.Forms.ComboBox();
            this.ChBoxLatestChara = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // PanelFlowers
            // 
            this.PanelFlowers.AutoScroll = true;
            this.PanelFlowers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelFlowers.Location = new System.Drawing.Point(26, 106);
            this.PanelFlowers.Name = "PanelFlowers";
            this.PanelFlowers.Size = new System.Drawing.Size(750, 524);
            this.PanelFlowers.TabIndex = 0;
            // 
            // BtAll
            // 
            this.BtAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtAll.Location = new System.Drawing.Point(202, 12);
            this.BtAll.Name = "BtAll";
            this.BtAll.Size = new System.Drawing.Size(38, 38);
            this.BtAll.TabIndex = 10;
            this.BtAll.Text = "Inv";
            this.BtAll.UseVisualStyleBackColor = true;
            this.BtAll.Click += new System.EventHandler(this.BtInv_Click);
            // 
            // ChBoxMagic
            // 
            this.ChBoxMagic.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChBoxMagic.AutoSize = true;
            this.ChBoxMagic.Checked = true;
            this.ChBoxMagic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxMagic.Image = global::FKG_Info.Properties.Resources.chbox_ico_magic;
            this.ChBoxMagic.Location = new System.Drawing.Point(158, 12);
            this.ChBoxMagic.Name = "ChBoxMagic";
            this.ChBoxMagic.Size = new System.Drawing.Size(38, 38);
            this.ChBoxMagic.TabIndex = 9;
            this.ChBoxMagic.UseVisualStyleBackColor = true;
            this.ChBoxMagic.CheckedChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxPierce
            // 
            this.ChBoxPierce.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChBoxPierce.AutoSize = true;
            this.ChBoxPierce.Checked = true;
            this.ChBoxPierce.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxPierce.Image = global::FKG_Info.Properties.Resources.chbox_ico_pierce;
            this.ChBoxPierce.Location = new System.Drawing.Point(114, 12);
            this.ChBoxPierce.Name = "ChBoxPierce";
            this.ChBoxPierce.Size = new System.Drawing.Size(38, 38);
            this.ChBoxPierce.TabIndex = 8;
            this.ChBoxPierce.UseVisualStyleBackColor = true;
            this.ChBoxPierce.CheckedChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxBlunt
            // 
            this.ChBoxBlunt.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChBoxBlunt.AutoSize = true;
            this.ChBoxBlunt.Checked = true;
            this.ChBoxBlunt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxBlunt.Image = global::FKG_Info.Properties.Resources.chbox_ico_blunt;
            this.ChBoxBlunt.Location = new System.Drawing.Point(70, 12);
            this.ChBoxBlunt.Name = "ChBoxBlunt";
            this.ChBoxBlunt.Size = new System.Drawing.Size(38, 38);
            this.ChBoxBlunt.TabIndex = 7;
            this.ChBoxBlunt.UseVisualStyleBackColor = true;
            this.ChBoxBlunt.CheckedChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxSlash
            // 
            this.ChBoxSlash.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChBoxSlash.AutoSize = true;
            this.ChBoxSlash.Checked = true;
            this.ChBoxSlash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxSlash.Image = global::FKG_Info.Properties.Resources.chbox_ico_slash;
            this.ChBoxSlash.Location = new System.Drawing.Point(28, 12);
            this.ChBoxSlash.Name = "ChBoxSlash";
            this.ChBoxSlash.Size = new System.Drawing.Size(38, 38);
            this.ChBoxSlash.TabIndex = 6;
            this.ChBoxSlash.UseVisualStyleBackColor = true;
            this.ChBoxSlash.CheckedChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxS6
            // 
            this.ChBoxS6.Checked = true;
            this.ChBoxS6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxS6.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChBoxS6.Location = new System.Drawing.Point(262, 11);
            this.ChBoxS6.Margin = new System.Windows.Forms.Padding(0);
            this.ChBoxS6.Name = "ChBoxS6";
            this.ChBoxS6.Size = new System.Drawing.Size(48, 20);
            this.ChBoxS6.TabIndex = 15;
            this.ChBoxS6.Text = "★ 6";
            this.ChBoxS6.UseVisualStyleBackColor = true;
            this.ChBoxS6.CheckedChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxS5
            // 
            this.ChBoxS5.Checked = true;
            this.ChBoxS5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxS5.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChBoxS5.Location = new System.Drawing.Point(262, 28);
            this.ChBoxS5.Margin = new System.Windows.Forms.Padding(0);
            this.ChBoxS5.Name = "ChBoxS5";
            this.ChBoxS5.Size = new System.Drawing.Size(48, 20);
            this.ChBoxS5.TabIndex = 14;
            this.ChBoxS5.Text = "★ 5";
            this.ChBoxS5.UseVisualStyleBackColor = true;
            this.ChBoxS5.CheckedChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxS4
            // 
            this.ChBoxS4.Checked = true;
            this.ChBoxS4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxS4.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChBoxS4.Location = new System.Drawing.Point(262, 45);
            this.ChBoxS4.Margin = new System.Windows.Forms.Padding(0);
            this.ChBoxS4.Name = "ChBoxS4";
            this.ChBoxS4.Size = new System.Drawing.Size(48, 20);
            this.ChBoxS4.TabIndex = 13;
            this.ChBoxS4.Text = "★ 4";
            this.ChBoxS4.UseVisualStyleBackColor = true;
            this.ChBoxS4.CheckedChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxS3
            // 
            this.ChBoxS3.Checked = true;
            this.ChBoxS3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxS3.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChBoxS3.Location = new System.Drawing.Point(262, 62);
            this.ChBoxS3.Margin = new System.Windows.Forms.Padding(0);
            this.ChBoxS3.Name = "ChBoxS3";
            this.ChBoxS3.Size = new System.Drawing.Size(48, 20);
            this.ChBoxS3.TabIndex = 12;
            this.ChBoxS3.Text = "★ 3";
            this.ChBoxS3.UseVisualStyleBackColor = true;
            this.ChBoxS3.CheckedChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxS2
            // 
            this.ChBoxS2.Checked = true;
            this.ChBoxS2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChBoxS2.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChBoxS2.Location = new System.Drawing.Point(262, 79);
            this.ChBoxS2.Margin = new System.Windows.Forms.Padding(0);
            this.ChBoxS2.Name = "ChBoxS2";
            this.ChBoxS2.Size = new System.Drawing.Size(48, 20);
            this.ChBoxS2.TabIndex = 11;
            this.ChBoxS2.Text = "★ 2";
            this.ChBoxS2.UseVisualStyleBackColor = true;
            this.ChBoxS2.CheckedChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // CmBoxAbility01
            // 
            this.CmBoxAbility01.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmBoxAbility01.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmBoxAbility01.FormattingEnabled = true;
            this.CmBoxAbility01.Location = new System.Drawing.Point(616, 39);
            this.CmBoxAbility01.Name = "CmBoxAbility01";
            this.CmBoxAbility01.Size = new System.Drawing.Size(160, 21);
            this.CmBoxAbility01.TabIndex = 21;
            this.CmBoxAbility01.SelectedIndexChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // CmBoxSort
            // 
            this.CmBoxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmBoxSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmBoxSort.FormattingEnabled = true;
            this.CmBoxSort.Location = new System.Drawing.Point(451, 12);
            this.CmBoxSort.Name = "CmBoxSort";
            this.CmBoxSort.Size = new System.Drawing.Size(159, 21);
            this.CmBoxSort.TabIndex = 20;
            this.CmBoxSort.SelectedIndexChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // CmBoxNation
            // 
            this.CmBoxNation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmBoxNation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmBoxNation.FormattingEnabled = true;
            this.CmBoxNation.Location = new System.Drawing.Point(616, 12);
            this.CmBoxNation.Name = "CmBoxNation";
            this.CmBoxNation.Size = new System.Drawing.Size(160, 21);
            this.CmBoxNation.TabIndex = 19;
            this.CmBoxNation.SelectedIndexChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // TxBoxSearch
            // 
            this.TxBoxSearch.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TxBoxSearch.Location = new System.Drawing.Point(451, 66);
            this.TxBoxSearch.Name = "TxBoxSearch";
            this.TxBoxSearch.Size = new System.Drawing.Size(136, 22);
            this.TxBoxSearch.TabIndex = 22;
            this.TxBoxSearch.TextChanged += new System.EventHandler(this.TxBoxSearch_TextChanged);
            this.TxBoxSearch.Enter += new System.EventHandler(this.TxBoxSearch_FocusEnter);
            this.TxBoxSearch.Leave += new System.EventHandler(this.TxBoxSearch_FocusLeave);
            // 
            // CmBoxSpecFilter
            // 
            this.CmBoxSpecFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmBoxSpecFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmBoxSpecFilter.FormattingEnabled = true;
            this.CmBoxSpecFilter.Location = new System.Drawing.Point(451, 39);
            this.CmBoxSpecFilter.Name = "CmBoxSpecFilter";
            this.CmBoxSpecFilter.Size = new System.Drawing.Size(160, 21);
            this.CmBoxSpecFilter.TabIndex = 23;
            this.CmBoxSpecFilter.SelectedIndexChanged += new System.EventHandler(this.CmBoxSpecFilter_CheckedChanged);
            // 
            // CmBoxAbility02
            // 
            this.CmBoxAbility02.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmBoxAbility02.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmBoxAbility02.FormattingEnabled = true;
            this.CmBoxAbility02.Location = new System.Drawing.Point(616, 66);
            this.CmBoxAbility02.Name = "CmBoxAbility02";
            this.CmBoxAbility02.Size = new System.Drawing.Size(160, 21);
            this.CmBoxAbility02.TabIndex = 24;
            this.CmBoxAbility02.SelectedIndexChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // BtClrSearch
            // 
            this.BtClrSearch.Location = new System.Drawing.Point(591, 65);
            this.BtClrSearch.Name = "BtClrSearch";
            this.BtClrSearch.Size = new System.Drawing.Size(21, 23);
            this.BtClrSearch.TabIndex = 25;
            this.BtClrSearch.Text = "C";
            this.BtClrSearch.UseVisualStyleBackColor = true;
            this.BtClrSearch.Click += new System.EventHandler(this.BtClrSearch_Click);
            // 
            // ChBoxEventKnights
            // 
            this.ChBoxEventKnights.AutoSize = true;
            this.ChBoxEventKnights.Checked = true;
            this.ChBoxEventKnights.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.ChBoxEventKnights.Location = new System.Drawing.Point(316, 64);
            this.ChBoxEventKnights.Name = "ChBoxEventKnights";
            this.ChBoxEventKnights.Size = new System.Drawing.Size(92, 17);
            this.ChBoxEventKnights.TabIndex = 26;
            this.ChBoxEventKnights.Text = "Event Knights";
            this.ChBoxEventKnights.ThreeState = true;
            this.ChBoxEventKnights.UseVisualStyleBackColor = true;
            this.ChBoxEventKnights.CheckStateChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxBloomCG
            // 
            this.ChBoxBloomCG.AutoSize = true;
            this.ChBoxBloomCG.Checked = true;
            this.ChBoxBloomCG.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.ChBoxBloomCG.Location = new System.Drawing.Point(316, 81);
            this.ChBoxBloomCG.Name = "ChBoxBloomCG";
            this.ChBoxBloomCG.Size = new System.Drawing.Size(73, 17);
            this.ChBoxBloomCG.TabIndex = 27;
            this.ChBoxBloomCG.Text = "Bloom CG";
            this.ChBoxBloomCG.ThreeState = true;
            this.ChBoxBloomCG.UseVisualStyleBackColor = true;
            this.ChBoxBloomCG.CheckStateChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxAcc1Has
            // 
            this.ChBoxAcc1Has.AutoSize = true;
            this.ChBoxAcc1Has.Location = new System.Drawing.Point(50, 64);
            this.ChBoxAcc1Has.Name = "ChBoxAcc1Has";
            this.ChBoxAcc1Has.Size = new System.Drawing.Size(75, 17);
            this.ChBoxAcc1Has.TabIndex = 28;
            this.ChBoxAcc1Has.Text = "Account 1";
            this.ChBoxAcc1Has.UseVisualStyleBackColor = true;
            this.ChBoxAcc1Has.CheckedChanged += new System.EventHandler(this.ChBoxAccHas_Changed);
            // 
            // ChBoxAcc2Has
            // 
            this.ChBoxAcc2Has.AutoSize = true;
            this.ChBoxAcc2Has.Location = new System.Drawing.Point(50, 81);
            this.ChBoxAcc2Has.Name = "ChBoxAcc2Has";
            this.ChBoxAcc2Has.Size = new System.Drawing.Size(75, 17);
            this.ChBoxAcc2Has.TabIndex = 29;
            this.ChBoxAcc2Has.Text = "Account 2";
            this.ChBoxAcc2Has.UseVisualStyleBackColor = true;
            this.ChBoxAcc2Has.CheckedChanged += new System.EventHandler(this.ChBoxAccHas_Changed);
            // 
            // ChBoxAcc1Filter
            // 
            this.ChBoxAcc1Filter.AutoSize = true;
            this.ChBoxAcc1Filter.Checked = true;
            this.ChBoxAcc1Filter.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.ChBoxAcc1Filter.Location = new System.Drawing.Point(29, 65);
            this.ChBoxAcc1Filter.Name = "ChBoxAcc1Filter";
            this.ChBoxAcc1Filter.Size = new System.Drawing.Size(15, 14);
            this.ChBoxAcc1Filter.TabIndex = 30;
            this.ChBoxAcc1Filter.ThreeState = true;
            this.ChBoxAcc1Filter.UseVisualStyleBackColor = true;
            this.ChBoxAcc1Filter.CheckStateChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // ChBoxAcc2Filter
            // 
            this.ChBoxAcc2Filter.AutoSize = true;
            this.ChBoxAcc2Filter.Checked = true;
            this.ChBoxAcc2Filter.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.ChBoxAcc2Filter.Location = new System.Drawing.Point(29, 82);
            this.ChBoxAcc2Filter.Name = "ChBoxAcc2Filter";
            this.ChBoxAcc2Filter.Size = new System.Drawing.Size(15, 14);
            this.ChBoxAcc2Filter.TabIndex = 31;
            this.ChBoxAcc2Filter.ThreeState = true;
            this.ChBoxAcc2Filter.UseVisualStyleBackColor = true;
            this.ChBoxAcc2Filter.CheckStateChanged += new System.EventHandler(this.ChBox_CheckedChanged);
            // 
            // CmBoxVariations
            // 
            this.CmBoxVariations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmBoxVariations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmBoxVariations.FormattingEnabled = true;
            this.CmBoxVariations.Location = new System.Drawing.Point(316, 12);
            this.CmBoxVariations.Name = "CmBoxVariations";
            this.CmBoxVariations.Size = new System.Drawing.Size(129, 21);
            this.CmBoxVariations.TabIndex = 32;
            this.CmBoxVariations.SelectedIndexChanged += new System.EventHandler(this.CmBoxVariations_CheckedChanged);
            // 
            // ChBoxLatestChara
            // 
            this.ChBoxLatestChara.AutoSize = true;
            this.ChBoxLatestChara.Location = new System.Drawing.Point(316, 41);
            this.ChBoxLatestChara.Name = "ChBoxLatestChara";
            this.ChBoxLatestChara.Size = new System.Drawing.Size(108, 17);
            this.ChBoxLatestChara.TabIndex = 33;
            this.ChBoxLatestChara.Text = "Latest characters";
            this.ChBoxLatestChara.UseVisualStyleBackColor = true;
            // 
            // FlowerSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChBoxLatestChara);
            this.Controls.Add(this.CmBoxVariations);
            this.Controls.Add(this.ChBoxAcc2Filter);
            this.Controls.Add(this.ChBoxAcc1Filter);
            this.Controls.Add(this.ChBoxAcc2Has);
            this.Controls.Add(this.ChBoxAcc1Has);
            this.Controls.Add(this.ChBoxBloomCG);
            this.Controls.Add(this.ChBoxEventKnights);
            this.Controls.Add(this.BtClrSearch);
            this.Controls.Add(this.CmBoxAbility02);
            this.Controls.Add(this.CmBoxSpecFilter);
            this.Controls.Add(this.TxBoxSearch);
            this.Controls.Add(this.CmBoxAbility01);
            this.Controls.Add(this.CmBoxSort);
            this.Controls.Add(this.CmBoxNation);
            this.Controls.Add(this.ChBoxS6);
            this.Controls.Add(this.ChBoxS5);
            this.Controls.Add(this.ChBoxS4);
            this.Controls.Add(this.ChBoxS3);
            this.Controls.Add(this.ChBoxS2);
            this.Controls.Add(this.BtAll);
            this.Controls.Add(this.PanelFlowers);
            this.Controls.Add(this.ChBoxMagic);
            this.Controls.Add(this.ChBoxSlash);
            this.Controls.Add(this.ChBoxPierce);
            this.Controls.Add(this.ChBoxBlunt);
            this.Name = "FlowerSelectControl";
            this.Size = new System.Drawing.Size(802, 640);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelFlowers;
        private System.Windows.Forms.Button BtAll;
        private System.Windows.Forms.CheckBox ChBoxMagic;
        private System.Windows.Forms.CheckBox ChBoxPierce;
        private System.Windows.Forms.CheckBox ChBoxBlunt;
        private System.Windows.Forms.CheckBox ChBoxSlash;
        private System.Windows.Forms.CheckBox ChBoxS6;
        private System.Windows.Forms.CheckBox ChBoxS5;
        private System.Windows.Forms.CheckBox ChBoxS4;
        private System.Windows.Forms.CheckBox ChBoxS3;
        private System.Windows.Forms.CheckBox ChBoxS2;
        private System.Windows.Forms.ComboBox CmBoxAbility01;
        private System.Windows.Forms.ComboBox CmBoxSort;
        private System.Windows.Forms.ComboBox CmBoxNation;
        private System.Windows.Forms.TextBox TxBoxSearch;
        private System.Windows.Forms.ComboBox CmBoxSpecFilter;
        private System.Windows.Forms.ComboBox CmBoxAbility02;
        private System.Windows.Forms.Button BtClrSearch;
        private System.Windows.Forms.CheckBox ChBoxEventKnights;
        private System.Windows.Forms.CheckBox ChBoxBloomCG;
        private System.Windows.Forms.CheckBox ChBoxAcc1Has;
        private System.Windows.Forms.CheckBox ChBoxAcc2Has;
        private System.Windows.Forms.CheckBox ChBoxAcc1Filter;
        private System.Windows.Forms.CheckBox ChBoxAcc2Filter;
        private System.Windows.Forms.ComboBox CmBoxVariations;
        private System.Windows.Forms.CheckBox ChBoxLatestChara;
    }
}
