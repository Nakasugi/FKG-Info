namespace FKG_Info
{
    public class FlowerStats
    {
        public int HitPointsLvMin;
        public int AttackLvMin;
        public int DefenseLvMin;
        public int SpeedLvMin;

        public int HitPointsLvMax;
        public int AttackLvMax;
        public int DefenseLvMax;
        public int SpeedLvMax;

        public int HitPointAmpules;
        public int AttackAmpules;
        public int DefenseAmpules;

        private int HitPointAffectionScaling1;
        private int AttackAffectionScaling1;
        private int DefenseAffectionScaling1;

        private int HitPointAffectionScaling2;
        private int AttackAffectionScaling2;
        private int DefenseAffectionScaling2;


        const string Pattern = "{0,-5} +{1,-4}  max={2}";


        public FlowerStats(string[] masterData)
        {
            int.TryParse(masterData[15], out HitPointsLvMin);
            int.TryParse(masterData[17], out AttackLvMin);
            int.TryParse(masterData[19], out DefenseLvMin);
            int.TryParse(masterData[21], out SpeedLvMin);

            int.TryParse(masterData[16], out HitPointsLvMax);
            int.TryParse(masterData[18], out AttackLvMax);
            int.TryParse(masterData[20], out DefenseLvMax);
            int.TryParse(masterData[22], out SpeedLvMax);

            int.TryParse(masterData[23], out HitPointAmpules);
            int.TryParse(masterData[24], out AttackAmpules);
            int.TryParse(masterData[25], out DefenseAmpules);

            int.TryParse(masterData[31], out HitPointAffectionScaling1);
            int.TryParse(masterData[32], out AttackAffectionScaling1);
            int.TryParse(masterData[33], out DefenseAffectionScaling1);
            int.TryParse(masterData[38], out HitPointAffectionScaling2);
            int.TryParse(masterData[39], out AttackAffectionScaling2);
            int.TryParse(masterData[40], out DefenseAffectionScaling2);
        }



        public int GetHitPointsAffectionBonus(int affection)
        {
            int[] aff = SplitAffection(affection);
            return HitPointAffectionScaling1 * 12 * aff[0] / 1000 + HitPointAffectionScaling2 * 12 * aff[1] / 1000;
        }

        public int GetAttackAffectionBonus(int affection)
        {
            int[] aff = SplitAffection(affection);
            return AttackAffectionScaling1 * 12 * aff[0] / 1000 + AttackAffectionScaling2 * 12 * aff[1] / 1000;
        }

        public int GetDefenseAffectionBonus(int affection)
        {
            int[] aff = SplitAffection(affection);
            return DefenseAffectionScaling1 * 12 * aff[0] / 1000 + DefenseAffectionScaling2 * 12 * aff[1] / 1000;
        }



        public int GetHitPointsMax()
        {
            return HitPointsLvMax + HitPointAmpules + GetHitPointsAffectionBonus(200);
        }

        public int GetAttackMax()
        {
            return AttackLvMax + AttackAmpules + GetAttackAffectionBonus(200);
        }

        public int GetDefenseMax()
        {
            return DefenseLvMax + DefenseAmpules + GetDefenseAffectionBonus(200);
        }

        public int GetTotalMax()
        {
            return GetHitPointsMax() + GetAttackMax() + GetDefenseMax() + SpeedLvMax;
        }



        public string GetHitPointsInfo()
        {
            return string.Format(Pattern, HitPointsLvMax, GetHitPointsAffectionBonus(200), GetHitPointsMax());
        }

        public string GetAttackInfo()
        {
            return string.Format(Pattern, AttackLvMax, GetAttackAffectionBonus(200), GetAttackMax());
        }

        public string GetDefenseInfo()
        {
            return string.Format(Pattern, DefenseLvMax, GetDefenseAffectionBonus(200), GetDefenseMax());
        }



        private int[] SplitAffection(int affection)
        {
            int[] aff = new int[2];

            if (affection > 100)
            {
                aff[0] = 100;
                aff[1] = affection - 100;

                if (aff[1] > 100) aff[1] = 100;
            }
            else
            {
                aff[0] = affection;
                aff[1] = 0;
            }


            return aff;
        }
    }
}
