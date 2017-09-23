using System.Linq;



namespace FKG_Info
{
    public class AbilityInfo : BaseInfo
    {
        private string KInfo;

        private int[] Params;

        public int Count { get; private set; }



        public const int SUB_NUM = 3;
        public const int PARAMS_NUM = 4 * SUB_NUM;



        public AbilityInfo()
        {
            BaseType = ObjectType.Ability;

            KInfo = null;
            Params = new int[PARAMS_NUM];
        }



        public AbilityInfo(string[] masterData) : this()
        {
            if (masterData.Length < 15) return;

            int parseValue;
            if (!int.TryParse(masterData[0], out parseValue)) { ID = 0; return; }
            ID = parseValue;

            KInfo = masterData[14];

            for (int i = 0; i < Params.Length; i++) int.TryParse(masterData[i + 2], out Params[i]);

            for (int i = 0; i < SUB_NUM; i++) if (Params[4 * i] != 0) Count = i + 1; else break;
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

            return Params[4 * n];
        }



        public bool CheckTypeID(int type)
        {
            for (int i = 0; i < SUB_NUM; i++) if (GetTypeID(i) == type) return true;
            return false;
        }



        public string GetListIDs()
        {
            string res = "";

            res += "ID:" + ID.ToString("D04");
            for (int i = 0; i < SUB_NUM; i++)
            {
                int ti = GetTypeID(i); if (ti == 0) break;
                res += "\r\nA" + i + ":" + ti.ToString("D04");
            }

            return res;
        }



        public bool CheckAbilityTags(string tag)
        {
            for (int i = 0; i < SUB_NUM; i++)
                if (Program.DB.TranslatorAbilities.GetTag(GetTypeID(i)) == tag) return true;

            return false;
        }



        public int[] GetParams(int n)
        {
            int[] prs = new int[4];

            if (n < SUB_NUM) System.Array.Copy(Params, 4 * n, prs, 0, 4);

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



        public string GetInfo(int[] prms) { return Program.DB.TranslatorAbilities.GetTranslation(prms); }




        public static int GetAbilityIconID(int[] prms)
        {
            if (prms[0] == 1205) return 11 + prms[1];

            int l = IconIDs.GetLength(0);

            for (int i = 0; i < l; i++) if (IconIDs[i, 0] == prms[0]) return IconIDs[i, 1];

            return 0;
        }



        private static readonly int[,] IconIDs =
        {
            {0001, 1},
            {0002, 1},
            {0003, 11},
            {0005, 3},
            {0006, 6},
            {0008, 16},
            {0010, 4},
            {0011, 7},
            {0101, 21},
            {0201, 27},
            {0301, 2},
            {0302, 1},
            {0303, 21},
            {0401, 26},
            {0501, 26},
            {0602, 34},
            {0701, 9},
            {0801, 26},
            {0901, 33},
            {0902, 34},
            {0903, 38},
            {0904, 34},
            {1002, 29},
            {1101, 17},
            {1102, 16},
            {1104, 16},
            {1105, 16},
            {1106, 0},
            {1201, 24},
            {1202, 22},
            {1203, 36},
            {1204, 23},
            {1205, 13},
            {1206, 22},
            {1207, 0},
            {1209, 16},
            {1301, 30},
            {1302, 31},
            {1303, 32},
            {1401, 8},
            {1402, 10},
            {1501, 3},
            {1701, 18},
            {1702, 18},
            {1801, 35},
            {1802, 0},
            {1803, 37},
            {1901, 37},
            {2001, 19},
            {2002, 20},
            {2003, 0},
            {2101, 0},
            {2102, 0},
            {3001, 0},
            {3002, 0},
            {3003, 25},
            {4001, 0},
            {5001, 19}
        };
    }
}
