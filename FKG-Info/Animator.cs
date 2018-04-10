using System;

namespace FKG_Info
{
    public class Animator
    {
        public enum Type { None, IconSmall, IconMedium, IconLarge, Cutin, Bustup, Stand, StandSmall, Home }
        public enum EmoType
        {
            Normal, Happy, Angry, Sad, Shy, Surprised,
            NormalClear, HappyClear, AngryClear, SadClear, ShyClear, SurprisedClear,
            EyesHalf, EyesClose
        }

        public FlowerInfo Flower;
        public Type ImageType;
        public EmoType Emotion;
        public bool Exclusive;
        public bool RawImage;
        public int ExID;


        private System.Windows.Forms.Timer AnimationTimer;
        private int CurrentFrame;
        private int LastFrameTime;
        private AdvPictureBox AnimPicBox;
        private Random RNG;
        

        private const int MAX_FRTIME = 5000;


        public Animator()
        {
            Flower = null;
            ImageType = Type.Stand;
            Emotion = EmoType.Normal;
            Exclusive = false;
            RawImage = false;
            ExID = 0;
        }



        public Animator(Animator ani)
        {
            Flower = ani.Flower;
            ImageType = ani.ImageType;
            Emotion = ani.Emotion;
            Exclusive = ani.Exclusive;
            RawImage = ani.RawImage;
            ExID = ani.ExID;
        }



        public void InitializeAnimation(AdvPictureBox picBox)
        {
            AnimPicBox = picBox;
            CurrentFrame = 0;
            LastFrameTime = 0;

            AnimationTimer = new System.Windows.Forms.Timer();
            AnimationTimer.Interval = MAX_FRTIME;
            AnimationTimer.Tick += (s, e) => AnimationTick();
            AnimationTimer.Start();

            RNG = new Random();
        }



        public bool IsIcon()
        {
            if (ImageType == Type.IconSmall) return true;
            if (ImageType == Type.IconMedium) return true;
            if (ImageType == Type.IconLarge) return true;
            return false;
        }



        public string GetImageName()
        {
            if (Flower == null) return null;

            string name = Flower.GetImageStringID(Exclusive);
            name = GetPrefix(ImageType) + name;

            if ((ImageType == Type.Bustup) || (ImageType == Type.Home)) name += GetSuffix(Emotion);

            return name;
        }



        public FlowerInfo GetFlower() { return Flower; }



        public static string GetPrefix(Type type)
        {
            string res = null;

            switch (type)
            {
                case Type.IconSmall: res = "icon_s_"; break;
                case Type.IconMedium: res = "icon_m_"; break;
                case Type.IconLarge: res = "icon_l_"; break;
                case Type.Cutin: res = "cutin_"; break;
                case Type.Bustup: res = "bustup_"; break;
                case Type.Stand: res = "stand_"; break;
                case Type.StandSmall: res = "stand_s_"; break;
                case Type.Home: res = "home_"; break;
                default: return null;
            }

            return res;
        }



        public static string GetSuffix(EmoType emo)
        {
            string res = null;

            switch (emo)
            {
                case EmoType.Happy: res += "_02"; break;
                case EmoType.Angry: res += "_03"; break;
                case EmoType.Sad: res += "_04"; break;
                case EmoType.Shy: res += "_05"; break;
                case EmoType.Surprised: res += "_06"; break;

                case EmoType.NormalClear: res += "_2"; break;
                case EmoType.HappyClear: res += "_2_02"; break;
                case EmoType.AngryClear: res += "_2_03"; break;
                case EmoType.SadClear: res += "_2_04"; break;
                case EmoType.ShyClear: res += "_2_05"; break;
                case EmoType.SurprisedClear: res += "_2_06"; break;

                case EmoType.EyesHalf: res += "_50"; break;
                case EmoType.EyesClose: res += "_90"; break;
                default: break;
            }

            return res;
        }



        /// <summary>
        /// Names of pose types for image selection menu.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetMenuName(Type type)
        {
            string res = null;

            switch (type)
            {
                case Type.IconSmall: res = "Icon Small"; break;
                case Type.IconMedium: res = "Icon Medium"; break;
                case Type.IconLarge: res = "Icon Large"; break;
                case Type.Cutin: res = "Skill"; break;
                case Type.Bustup: res = "Bustup"; break;
                case Type.Stand: res = "Stand"; break;
                case Type.StandSmall: res = "Stand Small"; break;
                case Type.Home: res = "Home"; break;
                default: return null;
            }

            return res;
        }



        /// <summary>
        /// Names of emotion types for image selection menu.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetMenuName(EmoType type)
        {
            string res = null;

            switch (type)
            {
                case EmoType.Normal: res = "Normal"; break;
                case EmoType.Happy: res = "Happy"; break;
                case EmoType.Angry: res = "Angry"; break;
                case EmoType.Sad: res = "Sad"; break;
                case EmoType.Shy: res = "Shy"; break;
                case EmoType.Surprised: res = "Surprised"; break;

                case EmoType.NormalClear: res = "Normal Clear"; break;
                case EmoType.HappyClear: res = "Happy Clear"; break;
                case EmoType.AngryClear: res = "Angry Clear"; break;
                case EmoType.SadClear: res = "Sad Clear"; break;
                case EmoType.ShyClear: res = "Shy Clear"; break;
                case EmoType.SurprisedClear: res = "Surprised Clear"; break;

                default: return null;
            }

            return res;
        }




        public delegate void MenuClick(Type type, EmoType emo = EmoType.Normal);



        public static void FillMenu(System.Windows.Forms.ToolStripItemCollection items, MenuClick menuClickFn)
        {
            System.Windows.Forms.ToolStripMenuItem imi, emi;

            string itemName;

            foreach (Type itp in Enum.GetValues(typeof(Type)))
            {
                itemName = GetMenuName(itp);
                if (itemName == null) continue;
                if (itemName.Substring(0, 4) == "Icon") continue;

                imi = new System.Windows.Forms.ToolStripMenuItem(itemName, null, (s, e) => menuClickFn(itp));
                imi.BackColor = System.Drawing.SystemColors.Menu;
                imi.Name = itemName;
                items.Add(imi);

                if (itp == Type.Bustup)
                {
                    foreach (EmoType etp in Enum.GetValues(typeof(EmoType)))
                    {
                        itemName = GetMenuName(etp);
                        if (itemName == null) continue;

                        emi = new System.Windows.Forms.ToolStripMenuItem(itemName, null, (s, e) => menuClickFn(itp, etp));
                        emi.BackColor = System.Drawing.SystemColors.Menu;
                        emi.Name = itemName;
                        imi.DropDownItems.Add(emi);
                    }
                }
            }
        }



        public static void FillMenu(System.Windows.Forms.Menu.MenuItemCollection items, MenuClick menuClickFn)
        {
            string itemName;

            System.Windows.Forms.MenuItem mi, emi;

            foreach (Type itp in Enum.GetValues(typeof(Type)))
            {
                itemName = GetMenuName(itp);
                if (itemName == null) continue;
                if (itemName.Substring(0, 4) == "Icon") continue;

                mi = items.Add(itemName, (s, e) => menuClickFn(itp));
                mi.Name = itemName;

                if (itp == Type.Bustup)
                {
                    foreach (EmoType etp in Enum.GetValues(typeof(EmoType)))
                    {
                        itemName = GetMenuName(etp);
                        if (itemName == null) continue;

                        emi = mi.MenuItems.Add(itemName, (s, e) => menuClickFn(itp, etp));
                        emi.Name = itemName;
                    }
                }
            }
        }



        /// <summary>
        /// Eyes animation for "Home" images.
        /// </summary>
        private void AnimationTick()
        {
            if (ImageType != Type.Home) { AnimationTimer.Interval = MAX_FRTIME; return; }

            AnimationTimer.Stop();

            Animator ani = new Animator(this);

            switch (CurrentFrame)
            {
                case 0:
                    ani.Emotion = EmoType.EyesHalf;
                    AnimPicBox.AsyncLoadImage(ani);
                    AnimationTimer.Interval = 30;
                    break;
                case 1:
                    ani.Emotion = EmoType.EyesClose;
                    AnimPicBox.AsyncLoadImage(ani);
                    AnimationTimer.Interval = 50;
                    break;
                case 2:
                    ani.Emotion = EmoType.EyesHalf;
                    AnimPicBox.AsyncLoadImage(ani);
                    AnimationTimer.Interval = 50;
                    break;
                default:
                    ani.Emotion = EmoType.Normal;
                    AnimPicBox.AsyncLoadImage(ani);

                    int minTime = 100, maxTime = MAX_FRTIME;
                    if (LastFrameTime > 0.75 * MAX_FRTIME) { maxTime = 200; }
                    if (LastFrameTime < 0.25 * MAX_FRTIME) { minTime = MAX_FRTIME / 2; }
                    LastFrameTime = RNG.Next(minTime, maxTime);
                    AnimationTimer.Interval = LastFrameTime;
                    break;
            }

            CurrentFrame++;
            if (CurrentFrame > 3) CurrentFrame = 0;
            AnimationTimer.Start();
        }



        /// <summary>
        /// OnClick animation for "Home" images.
        /// </summary>
        public void AnimationClick()
        {
            if (ImageType != Type.Home) return;

            AnimationTimer.Stop();

            Animator ani = new Animator(this);

            int frid = RNG.Next(0, 5);

            switch (frid)
            {
                case 0: ani.Emotion = EmoType.Happy; break;
                case 1: ani.Emotion = EmoType.Angry; break;
                case 2: ani.Emotion = EmoType.Sad; break;
                case 3: ani.Emotion = EmoType.Shy; break;
                case 4: ani.Emotion = EmoType.Surprised; break;
            }

            AnimPicBox.AsyncLoadImage(ani);
            AnimationTimer.Interval = 1700;
            AnimationTimer.Start();
            LastFrameTime = MAX_FRTIME / 2;
            CurrentFrame = 4;
        }
    }
}
