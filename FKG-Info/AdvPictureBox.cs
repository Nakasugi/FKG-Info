using System;
using System.Windows.Forms;

namespace FKG_Info
{
    class AdvPictureBox : PictureBox, IComparable
    {
        private string LastName;
        private object Locker;

        private MainForm Main;

        public FlowerInfo Flower { get; private set; }
        public EquipmentInfo Equipment { get; private set; }



        private enum Type { Base, FlowerIcon, EquipmentIcon }

        Type PictureType;



        public AdvPictureBox() : base()
        {
            PictureType = Type.Base;
            Locker = new object();
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



        public void AsyncLoadChImage(FlowerInfo flower, int evol, FlowerInfo.ImageTypes itype)
        {
            string name = flower.GetImageName(evol, itype);
            lock (Locker) LastName = name;

            Program.ImageLoader.GetChImage(name, CallBacksetImage);
        }



        public void AsyncLoadEqImage(EquipmentInfo equip)
        {
            string name = equip.GetImageName();
            lock (Locker) LastName = name;

            Program.ImageLoader.GetEqImage(name, CallBacksetImage);
        }



        public void Clear() { LastName = null; Image = null; }



        private void CallBacksetImage(ImageDownloader.DownloadedFile ifile)
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



        public int CompareTo(object obj)
        {
            switch (PictureType)
            {
                case Type.FlowerIcon: return Flower.CompareTo(((AdvPictureBox)obj).Flower);
                case Type.EquipmentIcon: return Equipment.CompareTo(((AdvPictureBox)obj).Equipment);
                default: return 0;
            }
        }



        public static int ByEqName(AdvPictureBox pb1, AdvPictureBox pb2) { return EquipmentInfo.SortByName(pb1.Equipment, pb2.Equipment); }
        public static int ByEqAttack(AdvPictureBox pb1, AdvPictureBox pb2) { return EquipmentInfo.SortByAttack(pb1.Equipment, pb2.Equipment); }
        public static int ByEqDefense(AdvPictureBox pb1, AdvPictureBox pb2) { return EquipmentInfo.SortByDefense(pb1.Equipment, pb2.Equipment); }
        public static int ByEqTotal(AdvPictureBox pb1, AdvPictureBox pb2) { return EquipmentInfo.SortByTotal(pb1.Equipment, pb2.Equipment); }
        public static int ByEqSetAttack(AdvPictureBox pb1, AdvPictureBox pb2) { return EquipmentInfo.SortBySetAttack(pb1.Equipment, pb2.Equipment); }
        public static int ByEqSetDefense(AdvPictureBox pb1, AdvPictureBox pb2) { return EquipmentInfo.SortBySetDefense(pb1.Equipment, pb2.Equipment); }
        public static int ByEqSetTotal(AdvPictureBox pb1, AdvPictureBox pb2) { return EquipmentInfo.SortBySetTotal(pb1.Equipment, pb2.Equipment); }



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
