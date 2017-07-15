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



        public void AsyncLoadChImage(FlowerInfo flower)
        {
            string name = flower.GetImageName();
            
            lock (Locker) LastName = name;

            Program.ImageLoader.GetImage(flower, CallBackSetImage);
        }



        public void AsyncLoadEqImage(EquipmentInfo equip)
        {
            string name = equip.GetImageName();

            lock (Locker) LastName = name;

            Program.ImageLoader.GetImage(equip, CallBackSetImage);
        }



        public void Clear() { LastName = null; Image = null; }



        private void CallBackSetImage(ImageDownloader.DownloadedFile ifile)
        {
            if (ifile == null) { Image = null; return; }

            lock (Locker) if (LastName != ifile.Name) return;

            int i = 0;

            while (i < 3)
            {
                try
                {
                    Image = ifile.Image;
                    return;
                }
                catch
                {
                    i++;
                    System.Threading.Thread.Sleep(50);
                }
            }
        }



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
