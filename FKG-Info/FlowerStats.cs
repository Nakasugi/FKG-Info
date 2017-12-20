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


        private static class MrFields
        {
            public const int HPLvMin = 15;
            public const int HPLvMax = 16;
            public const int AtkLvMin = 17;
            public const int AtkLvMax = 18;
            public const int DefLvMin = 19;
            public const int DefLvMax = 20;
            public const int SpdLvMin = 21;
            public const int SpdLvMax = 22;
            public const int HpApm = 23;
            public const int AtkAmp = 24;
            public const int DefAmp = 25;
            public const int HPAff1 = 31;
            public const int AtkAff1 = 32;
            public const int DefAff1 = 33;
            public const int HPAff2 = 37;
            public const int AtkAff2 = 38;
            public const int DefAff2 = 39;
        }


        public FlowerStats(string[] masterData)
        {
            int.TryParse(masterData[MrFields.HPLvMin], out HitPointsLvMin);
            int.TryParse(masterData[MrFields.AtkLvMin], out AttackLvMin);
            int.TryParse(masterData[MrFields.DefLvMin], out DefenseLvMin);
            int.TryParse(masterData[MrFields.SpdLvMin], out SpeedLvMin);

            int.TryParse(masterData[MrFields.HPLvMax], out HitPointsLvMax);
            int.TryParse(masterData[MrFields.AtkLvMax], out AttackLvMax);
            int.TryParse(masterData[MrFields.DefLvMax], out DefenseLvMax);
            int.TryParse(masterData[MrFields.SpdLvMax], out SpeedLvMax);

            int.TryParse(masterData[MrFields.HpApm], out HitPointAmpules);
            int.TryParse(masterData[MrFields.AtkAmp], out AttackAmpules);
            int.TryParse(masterData[MrFields.DefAmp], out DefenseAmpules);

            int.TryParse(masterData[MrFields.HPAff1], out HitPointAffectionScaling1);
            int.TryParse(masterData[MrFields.AtkAff1], out AttackAffectionScaling1);
            int.TryParse(masterData[MrFields.DefAff1], out DefenseAffectionScaling1);
            int.TryParse(masterData[MrFields.HPAff2], out HitPointAffectionScaling2);
            int.TryParse(masterData[MrFields.AtkAff2], out AttackAffectionScaling2);
            int.TryParse(masterData[MrFields.DefAff2], out DefenseAffectionScaling2);
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

        public int GetOverallForce()
        {
            return GetHitPointsMax() + GetAttackMax() + GetDefenseMax();
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
