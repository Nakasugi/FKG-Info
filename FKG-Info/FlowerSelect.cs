using System;
using System.Drawing;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class FlowerSelect : Form
    {
        //FlowerDB DataBase;

        ToolTip TTip;
        int     TTipIndex;
        

        //List<ToolTip> Tips;

        
        
        struct SortType
        {
            public const string ByName = "Name";
        }



        public FlowerSelect()
        {
            InitializeComponent();

            //DataBase = DB;

            foreach (string nt in FlowerInfo.Nations) CmBoxNation.Items.Add(nt);
            CmBoxNation.Items[0] = "All";
            CmBoxNation.SelectedIndex = 0;

            CmBoxSort.Items.Add(SortType.ByName);
            CmBoxSort.SelectedIndex = 0;

            ChBoxGame01.Text = Program.DB.Game01Name;
            ChBoxGame02.Text = Program.DB.Game02Name;
            ChBoxGame03.Text = Program.DB.Game03Name;

            //Tips = new List<ToolTip>();
            TTipIndex = -1;
            TTip = new ToolTip();

            CmBoxAbility.Items.Add("All");
            CmBoxAbility.Items.AddRange(Program.DB.GetAbilitiesShortNames());
            CmBoxAbility.SelectedIndex = 0;

            ReloadList();
        }



        private void ReloadList()
        {
            switch (CmBoxSort.Text)
            {
                //case SortType.BySkillBaseRMS: Program.DataBase.Flowers.Sort(FlowerInfo.BySkillBaseRMS); break;
                //case SortType.BySkillRaidRMS: Program.DataBase.Flowers.Sort(FlowerInfo.BySkillRaidRMS); break;
                //case SortType.BySABaseRMS: Program.DataBase.Flowers.Sort(FlowerInfo.BySABaseRMS); break;
                //case SortType.BySARaidRMS: Program.DataBase.Flowers.Sort(FlowerInfo.BySARaidRMS); break;
                default: Program.DB.Flowers.Sort(); break;
            }

            LsBoxFlowers.Items.Clear();

            foreach (FlowerInfo Flower in Program.DB.Flowers)
            {

                if (ChBoxGame01.CheckState != CheckState.Indeterminate)
                {
                    if (ChBoxGame01.CheckState == CheckState.Checked) if (!Flower.Game01) continue;
                    if (ChBoxGame01.CheckState == CheckState.Unchecked) if (Flower.Game01) continue;
                }
                if (ChBoxGame02.CheckState != CheckState.Indeterminate)
                {
                    if (ChBoxGame02.CheckState == CheckState.Checked) if (!Flower.Game02) continue;
                    if (ChBoxGame02.CheckState == CheckState.Unchecked) if (Flower.Game02) continue;
                }
                if (ChBoxGame03.CheckState != CheckState.Indeterminate)
                {
                    if (ChBoxGame03.CheckState == CheckState.Checked) if (!Flower.Game03) continue;
                    if (ChBoxGame03.CheckState == CheckState.Unchecked) if (Flower.Game03) continue;
                }


                switch (Flower.Rarity)
                {
                    case 0x02: if (!ChBoxS2.Checked) continue; break;
                    case 0x03: if (!ChBoxS3.Checked) continue; break;
                    case 0x04: if (!ChBoxS4.Checked) continue; break;
                    case 0x05: if (!ChBoxS5.Checked) continue; break;
                    case 0x06: if (!ChBoxS6.Checked) continue; break;
                    default: break;
                }

                switch (Flower.AttackType)
                {
                    case 1: if (!ChBoxSlash.Checked) continue; break;
                    case 2: if (!ChBoxBlunt.Checked) continue; break;
                    case 3: if (!ChBoxPierce.Checked) continue; break;
                    case 4: if (!ChBoxMagic.Checked) continue; break;
                    default: break;
                }

                if (CmBoxNation.Text != "All")
                {
                    if (Flower.GetNation() != CmBoxNation.Text) continue;
                }

                if (CmBoxAbility.Text != "All")
                {
                    if (!Flower.CheckAbilityShortName(CmBoxAbility.Text)) continue;
                }

                /*
                switch(Flower.GetNation())
                {
                    case FlowerInfo.Nationality.BananaOcean: if (FSCmBoxNation.Text) continue; break;
                    default: break;
                }
                */


                LsBoxFlowers.Items.Add(new FlowerInListBox(Flower));
            }

            Program.DB.Unselect();
            PicBoxIcon.Image = null;
            BtOk.Enabled = false;
        }
        


        private void BtCancel_Click(object sender, EventArgs ev)
        {
            Program.DB.Unselect();
            this.Close();
        }



        private void LsBox_DrawItem(object sender, DrawItemEventArgs ev)
        {
            if (ev.Index < 0) return;
            if (ev.Index >= LsBoxFlowers.Items.Count) return;
            FlowerInListBox item = LsBoxFlowers.Items[ev.Index] as FlowerInListBox;
            item.DrawItem(ev, LsBoxFlowers.Font);
        }



        private void LsBox_SelectedIndexChanged(object sender, EventArgs ev)
        {
            SetIconImage(((FlowerInListBox)LsBoxFlowers.Items[LsBoxFlowers.SelectedIndex]).Flower);
            BtOk.Enabled = true;
        }



        private void BtOk_Click(object sender, EventArgs ev)
        {
            if (LsBoxFlowers.SelectedIndex != -1)
            {
                FlowerInfo Flower = ((FlowerInListBox)LsBoxFlowers.Items[LsBoxFlowers.SelectedIndex]).Flower;
                Program.DB.Select(Flower);
            }

            Close();
        }



        private void ChBox_CheckedChanged(object sender, EventArgs ev) { ReloadList(); }
        private void ChBox_CheckedStateChanged(object sender, EventArgs ev) { ReloadList(); }
        private void CmBox_SelectedIndexChanged(object sender, EventArgs ev) { ReloadList(); }
        private void CmBoxAbility_SelectedIndexChanged(object sender, EventArgs ev) { ReloadList(); }



        private void BtAll_Click(object sender, EventArgs ev)
        {
            if (ChBoxSlash.Checked) ChBoxSlash.Checked = false; else ChBoxSlash.Checked = true;
            if (ChBoxBlunt.Checked) ChBoxBlunt.Checked = false; else ChBoxBlunt.Checked = true;
            if (ChBoxPierce.Checked) ChBoxPierce.Checked = false; else ChBoxPierce.Checked = true;
            if (ChBoxMagic.Checked) ChBoxMagic.Checked = false; else ChBoxMagic.Checked = true;
            ReloadList();
        }



        private void LsBox_MouseMove(object sender, MouseEventArgs ev)
        {
            int NewIndex = LsBoxFlowers.IndexFromPoint(ev.Location);

            // If the row has changed since last moving the mouse:
            if (TTipIndex != NewIndex)
            {
                // Change the variable for the next timw we move the mouse:
                TTipIndex = NewIndex;

                // If over a row showing data (rather than blank space):
                if (TTipIndex > -1)
                {
                    //Set tooltip text for the row now under the mouse:
                    TTip.Active = false;
                    switch (CmBoxSort.Text)
                    {
                        //case SortType.BySkillBaseRMS: TTip.SetToolTip(LsBoxFlowers, "RMS=" + ((FlowerInListBox)LsBoxFlowers.Items[TTipIndex]).Flower.Skill.GetBaseRMS().ToString()); break;
                        //case SortType.BySkillRaidRMS: TTip.SetToolTip(LsBoxFlowers, "RMS=" + ((FlowerInListBox)LsBoxFlowers.Items[TTipIndex]).Flower.Skill.GetRaidRMS().ToString()); break;
                        //case SortType.BySABaseRMS: TTip.SetToolTip(LsBoxFlowers, "RMS=" + ((FlowerInListBox)LsBoxFlowers.Items[TTipIndex]).Flower.GetSABaseRMS().ToString()); break;
                        //case SortType.BySARaidRMS: TTip.SetToolTip(LsBoxFlowers, "RMS=" + ((FlowerInListBox)LsBoxFlowers.Items[TTipIndex]).Flower.GetSARaidRMS().ToString()); break;
                        default: TTip.RemoveAll(); break;
                    }

                    TTip.Active = true;
                }
            }
        }



        private void SetIconImage(FlowerInfo flower)
        {
            //if (PicBoxIcon.Image != null) PicBoxIcon.Image.Dispose();

            //PicBoxIcon.Image = flower.GetImage(FlowerInfo.Evolution.Base, FlowerInfo.ImageTypes.IconLarge);
            //if (PicBoxIcon.Image == null)
            Program.ImageLoader.GetImage(flower.GetImageName(FlowerInfo.Evolution.Base, FlowerInfo.ImageTypes.IconLarge), SetIconImageCallback);
        }



        private void SetIconImageCallback(Image Img) { PicBoxIcon.Image = Img; }
    }
}
