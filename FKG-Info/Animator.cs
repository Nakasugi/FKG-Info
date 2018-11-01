using System;
using System.Collections.Generic;




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
        public int SkinIndex;
        public bool Mobile;

        /// <summary>
        /// If true then download original, no additional elements will placed (like icon BG, type, etc)
        /// </summary>
        public bool RawImage;

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
            SkinIndex = 0;
            Mobile = false;
            RawImage = false;
        }



        public Animator(FlowerInfo flower) : this()
        {
            Flower = flower;
        }



        public Animator(Animator ani)
        {
            Flower = ani.Flower;
            ImageType = ani.ImageType;
            Emotion = ani.Emotion;
            SkinIndex = ani.SkinIndex;
            Mobile = ani.Mobile;
            RawImage = ani.RawImage;
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



        public bool IsIcon() { return IsIcon(ImageType); }

        public static bool IsIcon(Type type)
        {
            if (type == Type.IconSmall) return true;
            if (type == Type.IconMedium) return true;
            if (type == Type.IconLarge) return true;
            return false;
        }


        public string GetPath()
        {
            string path;

            path = Program.DB.ImagesFolder; if (Mobile) path += "\\Mobile";
            path += "\\" + GetImageName() + ".png";

            return path;
        }


        public string GetUrlSubFolder()
        {
            switch (ImageType)
            {
                case Type.IconSmall:
                case Type.IconMedium:
                case Type.IconLarge:
                    return "i/";

                case Type.Stand: return "stand/";

                case Type.StandSmall:
                    if (!Mobile)
                        return "stand_s/";
                    else
                        return "stand_m/";

                case Type.Cutin:
                    if (Mobile) return "cutin/";
                    goto default;

                case Type.Home:
                    if (Mobile) return "home/";
                    goto default;

                default: return "s/";
            }
        }



        public string GetImageName()
        {
            if (Flower == null) return null;

            string name = Flower.GetImageStringID(SkinIndex);
            name = GetPrefix() + name;

            if ((ImageType == Type.Bustup) || (ImageType == Type.Home)) name += GetSuffix(Emotion);

            return name;
        }



        public string GetPrefix()
        {
            string res = null;

            switch (ImageType)
            {
                case Type.IconSmall: res = "icon_s_"; break;
                case Type.IconMedium: res = "icon_m_"; break;
                case Type.IconLarge: res = "icon_l_"; break;
                case Type.Cutin: res = "cutin_"; break;
                case Type.Bustup: res = "bustup_"; break;
                case Type.Stand: res = "stand_"; break;
                case Type.StandSmall: if (Mobile) res = "stand_m_"; else res = "stand_s_"; break;
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



        public static List<Animator> GetAllFrames(FlowerInfo flower)
        {
            if (flower == null) return null;

            List<Animator> frames = new List<Animator>();

            Animator ani;

            ani = new Animator(flower);
            frames.Add(ani);

            ani = new Animator(flower);
            ani.ImageType = Type.StandSmall;
            frames.Add(ani);

            ani = new Animator(flower);
            ani.ImageType = Type.Cutin;
            frames.Add(ani);

            foreach (EmoType etp in GetEmotionsForType(Type.Bustup))
            {
                ani = new Animator(flower);
                ani.ImageType = Type.Bustup;
                ani.Emotion = etp;
                frames.Add(ani);
            }

            foreach (EmoType etp in GetEmotionsForType(Type.Home))
            {
                ani = new Animator(flower);
                ani.ImageType = Type.Home;
                ani.Emotion = etp;
                frames.Add(ani);
            }

            List<Animator> mframes = new List<Animator>(frames);
            foreach (Animator fr in mframes) fr.Mobile = true;

            frames.AddRange(mframes);

            return frames;
        }



        private static List<EmoType> GetEmotionsForType(Type type)
        {
            List<EmoType> emos = new List<EmoType>();

            if ((type != Type.Bustup) && (type != Type.Home))
            {
                emos.Add(EmoType.Normal);
                return emos;
            }

            emos.AddRange((EmoType[])Enum.GetValues(typeof(EmoType)));

            if (type == Type.Bustup)
            {
                emos.Remove(EmoType.EyesClose);
                emos.Remove(EmoType.EyesHalf);
            }

            if (type == Type.Home)
            {
                emos.Remove(EmoType.NormalClear);
                emos.Remove(EmoType.HappyClear);
                emos.Remove(EmoType.AngryClear);
                emos.Remove(EmoType.SadClear);
                emos.Remove(EmoType.ShyClear);
                emos.Remove(EmoType.SurprisedClear);
            }

            return emos;
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
