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

        //public FlowerInfo Flower { get; private set; }
        //public int RefID { get; private set; }

        public FlowerInfo Flower;
        public EquipmentInfo Equipment;



        private enum Type { Base, FlowerIcon, EquipmentIcon }

        private Type PictureType;
        private bool PartialIcon;



        private bool NeedImageDispose;

        public new System.Drawing.Image Image { get { return base.Image; } set { SetImage(value); } }

        public void SetImage(System.Drawing.Image image, bool needDispose = true)
        {
            if ((base.Image != null) && NeedImageDispose) base.Image.Dispose();
            NeedImageDispose = needDispose;
            base.Image = image;
        }




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


        /*
        new void Dispose()
        {
            if ((base.Image != null) && NeedImageDispose)
            {
                base.Image.Dispose();
                base.Image = null;
            }

            base.Dispose();
        }*/



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



        public void Clear() { lock (Locker) { LastName = null; SetImage(null); } }



        private void CallBackSetImage(ImageDownloader.DownloadedFile df)
        {
            lock (Locker)
            {
                if (df == null) { SetImage(null); return; }

                if (LastName != df.Name) return;

                // Lock! Lock!! Lock!!! Image protection need more Locks!
                // No more "Object is currently in use elsewhere"!
                lock (df.Locker) SetImage(df.Image, false);
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
