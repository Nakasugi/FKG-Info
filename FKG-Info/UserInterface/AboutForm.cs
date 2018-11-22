using System;
using System.Windows.Forms;

namespace FKG_Info.UserInterface
{
    public partial class AboutForm : Form
    {
        Random RNG;
        Timer TMR;

        int NerineType, NerineExpr;

        bool BlockExpr;

        public AboutForm()
        {
            InitializeComponent();

            LinkLabel.Link link;

            link = new LinkLabel.Link();
            link.LinkData = "http://himeuta.org/member.php?7554-LostLogia4";
            _lbLostLogia4.Links.Add(link);

            link = new LinkLabel.Link();
            link.LinkData = "http://himeuta.org/member.php?20069-HydroKirby";
            _lbHydroKirby.Links.Add(link);

            RNG = new Random();

            NerineType = RNG.Next(0, 3);
            //NerineExpr = RNG.Next(0, 4);

            PicBoxNerine.Image = GetImage(NerineType, 0);

            TMR = new Timer();
            TMR.Interval = 1500;
            TMR.Tick += (s, e) => TimerTick();

            BlockExpr = false;

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            _lbVersion.Text = "v." + fvi.FileVersion;
        }



        private void BtOk_Click(object sender, EventArgs ev)
        {
            Close();
        }



        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs ev)
        {
            System.Diagnostics.Process.Start(ev.Link.LinkData as string);
        }



        private void PicBoxNerine_Click(object sender, EventArgs ev)
        {
            if (BlockExpr) return;

            NerineExpr = RNG.Next(1, 4);
            PicBoxNerine.Image = GetImage(NerineType, NerineExpr);
            TMR.Start();
            BlockExpr = true;
        }


        private void TimerTick()
        {
            TMR.Stop();
            PicBoxNerine.Image = GetImage(NerineType, 0);
            BlockExpr = false;
        }


        private System.Drawing.Image GetImage(int type, int expr)
        {
            int selection = type * 10 + expr;

            switch (selection)
            {
                case 00: return Properties.Resources.nerine_ab_00_00;
                case 01: return Properties.Resources.nerine_ab_00_01;
                case 02: return Properties.Resources.nerine_ab_00_02;
                case 03: return Properties.Resources.nerine_ab_00_03;
                case 10: return Properties.Resources.nerine_ab_01_00;
                case 11: return Properties.Resources.nerine_ab_01_01;
                case 12: return Properties.Resources.nerine_ab_01_02;
                case 13: return Properties.Resources.nerine_ab_01_03;
                case 20: return Properties.Resources.nerine_ab_02_00;
                case 21: return Properties.Resources.nerine_ab_02_01;
                case 22: return Properties.Resources.nerine_ab_02_02;
                case 23: return Properties.Resources.nerine_ab_02_03;
                default: goto case 00;
            }
        }
    }
}
