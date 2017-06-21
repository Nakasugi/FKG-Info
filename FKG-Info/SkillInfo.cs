namespace FKG_Info
{
    public class SkillInfo
    {
        public int ID { get; private set; }

        public string KName;
        public string KInfo;

        private int SkillType;

        private int[] Params;

        public int ChanceMin { get; private set; }
        public int ChanceMax { get; private set; }



        public SkillInfo()
        {
            Params = new int[3];
        }



        public SkillInfo(string[] masterData) : this()
        {
            if (masterData.Length < 14) return;

            int parseValue;
            if (!int.TryParse(masterData[0], out parseValue)) { ID = 0; return; }
            ID = parseValue;

            KName = masterData[1];
            KInfo = masterData[6];

            int.TryParse(masterData[2], out SkillType);

            for (int i = 0; i < Params.Length; i++) int.TryParse(masterData[i + 3], out Params[i]);

            int.TryParse(masterData[7], out parseValue);
            ChanceMin = parseValue;
            int.TryParse(masterData[8], out parseValue);
            ChanceMax = parseValue;

            ChanceMax = ChanceMin + 5 * ChanceMax;
        }
        


        public string GetInfo(bool translation = true)
        {
            string info = "Name: " + KName + "\r\n", tr;

            FlowerDataBase db = Program.DB;

            info += "Chance: " + ChanceMin + " .. " + ChanceMax + "%\r\n";

            tr = db.GetSkillTranslation(SkillType);

            if ((tr != null) && (translation))
            {
                info += string.Format(tr, Params[0], Params[1], Params[2]);
            }
            else
            {
                info += KInfo;
            }


            info = StringHelper.ReplaceSimpleArithmetic(info, '+');

            return info;
        }
    }
}
