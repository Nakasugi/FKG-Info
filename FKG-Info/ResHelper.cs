namespace FKG_Info
{
    public class ResHelper
    {
        public enum IconElement { Background, Frame, Type, Evolution }


        public static System.Drawing.Image GetIconElement(IconElement el, int num)
        {
            switch (el)
            {
                case IconElement.Background:
                    switch (num)
                    {
                        case 1: return Properties.Resources.icon_bg1;
                        case 2: return Properties.Resources.icon_bg2;
                        case 3: return Properties.Resources.icon_bg3;
                        case 4: return Properties.Resources.icon_bg4;
                        case 5: return Properties.Resources.icon_bg5;
                        case 6: return Properties.Resources.icon_bg6;
                        default: break;
                    }
                    break;
                case IconElement.Frame:
                    switch (num)
                    {
                        case 1: return Properties.Resources.icon_frame1;
                        case 2: return Properties.Resources.icon_frame2;
                        case 3: return Properties.Resources.icon_frame3;
                        case 4: return Properties.Resources.icon_frame4;
                        case 5: return Properties.Resources.icon_frame5;
                        case 6: return Properties.Resources.icon_frame6;
                        default: break;
                    }
                    break;
                case IconElement.Type:
                    switch (num)
                    {
                        case 1: return Properties.Resources.icon_type1;
                        case 2: return Properties.Resources.icon_type2;
                        case 3: return Properties.Resources.icon_type3;
                        case 4: return Properties.Resources.icon_type4;
                        default: break;
                    }
                    break;
                case IconElement.Evolution:
                    switch (num)
                    {
                        case 1: return Properties.Resources.icon_evol1;
                        case 2: return Properties.Resources.icon_evol2;
                        case 3: return Properties.Resources.icon_evol3;
                        default: break;
                    }
                    break;
                default: break;
            }

            return Properties.Resources.icon_default;
        }
    }
}
