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



        public SkillInfo(string masterData) : this()
        {
            int parseValue;
            string[] sfields = masterData.Split(',');
            if (sfields.Length < 12) return;

            if (!int.TryParse(sfields[0], out parseValue)) { ID = 0; return; }
            ID = parseValue;

            KName = sfields[1];
            KInfo = sfields[6];

            int.TryParse(sfields[2], out SkillType);

            for (int i = 0; i < Params.Length; i++) int.TryParse(sfields[i + 3], out Params[i]);

            int.TryParse(sfields[7], out parseValue);
            ChanceMin = parseValue;
            int.TryParse(sfields[8], out parseValue);
            ChanceMax = parseValue;

            ChanceMax = ChanceMin + 5 * ChanceMax;
        }
        


        public string GetInfo(bool translation = true)
        {
            string info = "Name: " + KName + "\r\n", tr;

            FlowerDB db = Program.DataBase;

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
