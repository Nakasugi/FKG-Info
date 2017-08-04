using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FKG_Info
{
    public partial class AdvPictureBox : PictureBox
    {
        private string LastName;
        private object Locker;

        private MainForm Main;

        public FlowerInfo Flower { get; private set; }
        public EquipmentInfo Equipment { get; private set; }



        private enum Type { Base, FlowerIcon, EquipmentIcon }

        private Type PictureType;
        private bool PartialIcon;



        public AdvPictureBox() : base()
        {
            InitializeComponent();

            Locker = new object();
            PictureType = Type.Base;
        }



        public AdvPictureBox(IContainer container) : this()
        {
            container.Add(this);
        }



        public AdvPictureBox(bool partialIcon = false) : this()
        {
            PartialIcon = partialIcon;
        }



        public AdvPictureBox(MainForm main, FlowerInfo flower) : this()
        {
            PictureType = Type.FlowerIcon;
            Main = main;
            Flower = flower;

            MouseHover += OnMouseHover;
            Click += OnClick;
            DoubleClick += OnDoubleClick;
        }



        public AdvPictureBox(MainForm main, EquipmentInfo equip) : this()
        {
            PictureType = Type.EquipmentIcon;
            Main = main;
            Equipment = equip;

            MouseHover += OnMouseHover;
            Click += OnClick;
            DoubleClick += OnDoubleClick;
        }



        public void AsyncLoadImage(Animator ani)
        {
            string name = ani.GetImageName();

            lock (Locker) LastName = name;

            Program.ImageLoader.GetImage(ani, CallBackSetImage);
        }




        public void AsyncLoadImage(EquipmentInfo equip)
        {
            string name = equip.GetImageName();

            lock (Locker) LastName = name;

            Program.ImageLoader.GetImage(equip, CallBackSetImage);
        }



        public void Clear() { lock (Locker) { LastName = null; Image = null; } }



        private void CallBackSetImage(ImageDownloader.DownloadedFile df)
        {
            lock (Locker)
            {
                if (df == null) { Image = null; return; }

                if (LastName != df.Name) return;

                // Lock! Lock!! Lock!!! Image protection need more Locks!
                // No more "Object is currently in use elsewhere"!
                lock (df.Locker) Image = df.Image;
            }
        }



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
    }
}
