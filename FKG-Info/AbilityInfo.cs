namespace FKG_Info
{
    public class AbilityInfo : BaseInfo
    {
        //private string KInfo;

        private int[] Params;

        public int Count { get; private set; }



        public const int SUBABL_NUM = 3;
        public const int PARAMS_CNT = 6;
        public const int PARAMS_TOTAL = PARAMS_CNT * SUBABL_NUM;



        public AbilityInfo()
        {
            BaseType = ObjectType.Ability;

            //KInfo = null;
            Params = new int[PARAMS_TOTAL];
        }



        public AbilityInfo(string[] masterData) : this()
        {
            if (masterData == null) return;
            if (masterData.Length < 21) return;

            int parseValue;
            if (!int.TryParse(masterData[0], out parseValue)) { ID = 0; return; }
            ID = parseValue;

            //KInfo = masterData[14];

            for (int i = 0; i < Params.Length; i++) int.TryParse(masterData[i + 2], out Params[i]);

            for (int i = 0; i < SUBABL_NUM; i++) if (Params[PARAMS_CNT * i] != 0) Count = i + 1; else break;
        }



        /// <summary>
        /// Return ability type ID.
        /// </summary>
        /// <param name="n">Get Sub Ability type.</param>
        /// <returns></returns>
        public int GetTypeID(int n)
        {
            if (n < 0) return 0;
            if (n >= Count) return 0;

            return Params[PARAMS_CNT * n];
        }



        public bool CheckTypeID(int type)
        {
            for (int i = 0; i < SUBABL_NUM; i++) if (GetTypeID(i) == type) return true;
            return false;
        }



        public string GetListIDs()
        {
            string res = "";

            res += "ID:" + ID.ToString("D04");
            for (int i = 0; i < SUBABL_NUM; i++)
            {
                int ti = GetTypeID(i); if (ti == 0) break;
                res += "\r\nA" + i + ":" + ti.ToString("D04");
            }

            return res;
        }



        public bool CheckAbilityTags(string tag)
        {
            for (int i = 0; i < SUBABL_NUM; i++)
                if (Program.DB.TranslatorAbilities.GetTag(GetTypeID(i)) == tag) return true;

            return false;
        }



        public int[] GetParams(int n)
        {
            int[] prs = new int[PARAMS_CNT];

            if (n < SUBABL_NUM) System.Array.Copy(Params, PARAMS_CNT * n, prs, 0, PARAMS_CNT);

            return prs;
        }



        public string GetInfo(int n, bool getIDs = false)
        {
            int ti = GetTypeID(n);
            if (ti != 0)
            {
                if (getIDs) return "ID: " + ID.ToString("D04") + " - " + ti.ToString("D04");

                return GetInfo(GetParams(n));
            }

            return null;
        }



        private string GetInfo(int[] prms) { return Program.DB.TranslatorAbilities.GetTranslation(prms); }

        //public int GetAbilityIconID(int[] prms) { return Program.DB.TranslatorAbilities.GetIconId(prms); }
    }
}
