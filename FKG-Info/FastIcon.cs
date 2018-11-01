using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;




namespace FKG_Info
{
    public partial class FastIcon : UserControl
    {
        private MainForm Main;


        public FlowerInfo Flower { get; private set; }
        public EquipmentInfo Equipment { get; private set; }


        private enum Type { Base, FlowerIcon, EquipmentIcon }

        private Type PictureType;



        private Rectangle AtlasRect;

        private bool FirstDraw;
        private bool Cleared;



        public FastIcon()
        {
            InitializeComponent();

            FirstDraw = true;
            Cleared = false;
            AtlasRect = new Rectangle(0, 0, 100, 100);
        }



        public FastIcon(MainForm main, FlowerInfo flower) : this()
        {
            PictureType = Type.FlowerIcon;
            Main = main;
            Flower = flower;

            MouseHover += OnMouseHover;
            Click += OnClick;
            DoubleClick += OnDoubleClick;
        }


        public FastIcon(MainForm main, EquipmentInfo equip) : this()
        {
            PictureType = Type.EquipmentIcon;
            Main = main;
            Equipment = equip;

            MouseHover += OnMouseHover;
            Click += OnClick;
            DoubleClick += OnDoubleClick;
        }



        public FastIcon(IContainer components)
        {
            this.components = components;
        }



        public void Clear() { Cleared = true; Refresh(); }



        public void SetIcon(FlowerInfo flower)
        {
            if (flower == null)
            {
                Cleared = true;
                Refresh();
                return;
            }

            FirstDraw = true;
            PictureType = Type.FlowerIcon;
            Flower = flower;
            Cleared = false;

            Refresh();
        }



        public void SetIcon(EquipmentInfo equip)
        {
            if (equip == null)
            {
                Cleared = true;
                Refresh();
                return;
            }

            FirstDraw = true;
            PictureType = Type.EquipmentIcon;
            Equipment = equip;
            Cleared = false;

            Refresh();
        }

        /*
        public void AsyncLoadImage(Animator ani) { }
        public void AsyncLoadImage(EquipmentInfo equip) { }
        public void Clear() {}
        private void CallBackSetImage(ImageDownloader.DownloadedFile df) { }
        */







        /// <summary>
        /// For scrolling panel with mouse wheel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void OnMouseHover(object sender, EventArgs ev) { Parent.Focus(); }



        private void OnClick(object sender, EventArgs ev)
        {
            switch (PictureType)
            {
                case Type.FlowerIcon: Main.SelectFromSelector(Flower, false); return;
                case Type.EquipmentIcon: Main.SelectFromSelector(Equipment); return;
                default: return;
            }
        }



        private void OnDoubleClick(object sender, EventArgs ev)
        {
            switch (PictureType)
            {
                case Type.FlowerIcon: Main.SelectFromSelector(Flower, true); return;
                default: return;
            }
        }



        /// <summary>
        /// Drawing
        /// </summary>
        /// <param name="ev"></param>
        protected override void OnPaint(PaintEventArgs ev)
        {
            // Call the OnPaint method of the base class.  
            base.OnPaint(ev);

            if (Cleared || DesignMode)
            {
                Brush br = new SolidBrush(BackColor);
                ev.Graphics.FillRectangle(br, ev.ClipRectangle);
                br.Dispose();
                return;
            }

            if (FirstDraw)
            {
                FirstDraw = false;
                if (PictureType == Type.FlowerIcon) AtlasRect = Program.DB.FlowerIcons.GetPosition(Flower, this);
                if (PictureType == Type.EquipmentIcon) AtlasRect = Program.DB.EquipmentIcons.GetPosition(Equipment, this);
            }


            if (PictureType == Type.FlowerIcon) Program.DB.FlowerIcons.Draw(ev.Graphics, AtlasRect);
            if (PictureType == Type.EquipmentIcon) Program.DB.EquipmentIcons.Draw(ev.Graphics, AtlasRect);
        }
    }
}
