namespace FKG_Info
{
    public class AbilityInfo
    {
        public int ID { get; private set; }

        private string KInfo;

        private int[] Params;


        
        public AbilityInfo()
        {
            KInfo = null;
            Params = new int[8];
        }



        public AbilityInfo(string masterData) : this()
        {
            int parseValue;
            string[] sfields = masterData.Split(',');
            if (sfields.Length < 12) return;

            if (!int.TryParse(sfields[0], out parseValue)) { ID = 0; return; }
            ID = parseValue;

            KInfo = sfields[10];

            for (int i = 0; i < Params.Length; i++) int.TryParse(sfields[i + 2], out Params[i]);
        }



        public int GetTypeID(int n = 0)
        {
            if (n == 0) return Params[0];
            return Params[4];
        }



        public bool CheckTypeID(int type)
        {
            if (Params[0] == type) return true;
            if (Params[4] == type) return true;
            return false;
        }


        
        public bool CheckAbilityShortName(string shortName)
        {
            if (Program.DataBase.GetAbilityShortName(Params[0]) == shortName) return true;
            if (Program.DataBase.GetAbilityShortName(Params[4]) == shortName) return true;
            return false;
        }
        


        public string GetInfo(bool translation = true)
        {
            if (!translation) return KInfo;

            string info = "", tr;

            for (int k = 0; k < 8; k += 4)
            {
                if (Params[k] == 0) break;

                tr = Program.DataBase.GetAbilityTranslation(Params[k]);
                if (tr == null) return KInfo;

                info += string.Format(tr, Params[k + 1], Params[k + 2], Params[k + 3]) + " ";

                System.Text.StringBuilder sb = new System.Text.StringBuilder(info);

                sb.Replace("1th", "1st");
                sb.Replace("2th", "2nd");
                sb.Replace("3th", "3rd");

                sb.Replace("1type", FlowerInfo.AttackTypes[1]);
                sb.Replace("2type", FlowerInfo.AttackTypes[2]);
                sb.Replace("3type", FlowerInfo.AttackTypes[3]);
                sb.Replace("4type", FlowerInfo.AttackTypes[4]);

                info = sb.ToString();

                info = StringHelper.ReplaceSimpleArithmetic(info, '/');
            }

            if (info == "") return KInfo;

            return info;
        }
    }
}
