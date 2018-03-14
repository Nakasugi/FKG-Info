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

        public int HitPointAmpulesEx;
        public int AttackAmpulesEx;
        public int DefenseAmpulesEx;


        private int HitPointAffectionScaling1;
        private int AttackAffectionScaling1;
        private int DefenseAffectionScaling1;

        private int HitPointAffectionScaling2;
        private int AttackAffectionScaling2;
        private int DefenseAffectionScaling2;


        //const string Pattern = "{0,-5} +{1,-4}  max={2}";


        public const int STAT_LOW = 0x00;
        public const int STAT_BASE = 0x01;
        public const int STAT_AFFECTION = 0x02;
        public const int STAT_AMP = 0x04;
        public const int STAT_AMPEX = 0x08;
        public const int STAT_MAX = STAT_BASE | STAT_AFFECTION | STAT_AMP | STAT_AMPEX;


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
            public const int HpApmEx = 23;
            public const int AtkAmpEx = 24;
            public const int DefAmpEx = 25;
            public const int HPAff1 = 34;
            public const int AtkAff1 = 35;
            public const int DefAff1 = 36;
            public const int HPAff2 = 40;
            public const int AtkAff2 = 41;
            public const int DefAff2 = 42;
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

            int.TryParse(masterData[MrFields.HpApmEx], out HitPointAmpulesEx);
            int.TryParse(masterData[MrFields.AtkAmpEx], out AttackAmpulesEx);
            int.TryParse(masterData[MrFields.DefAmpEx], out DefenseAmpulesEx);

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



        public int GetHitPoints(int attr = STAT_MAX)
        {
            if (attr == STAT_LOW) return HitPointsLvMin;

            int res = 0;
            if ((attr & STAT_BASE) != 0) res += HitPointsLvMax;
            if ((attr & STAT_AFFECTION) != 0) res += GetHitPointsAffectionBonus(200);
            if ((attr & STAT_AMP) != 0) res += HitPointAmpules;
            if ((attr & STAT_AMPEX) != 0) res += HitPointAmpulesEx;
            return res;
        }

        public int GetAttack(int attr = STAT_MAX)
        {
            if (attr == STAT_LOW) return AttackLvMin;

            int res = 0;
            if ((attr & STAT_BASE) != 0) res += AttackLvMax;
            if ((attr & STAT_AFFECTION) != 0) res += GetAttackAffectionBonus(200);
            if ((attr & STAT_AMP) != 0) res += AttackAmpules;
            if ((attr & STAT_AMPEX) != 0) res += AttackAmpulesEx;
            return res;
        }

        public int GetDefense(int attr = STAT_MAX)
        {
            if (attr == STAT_LOW) return DefenseLvMin;

            int res = 0;
            if ((attr & STAT_BASE) != 0) res += DefenseLvMax;
            if ((attr & STAT_AFFECTION) != 0) res += GetDefenseAffectionBonus(200);
            if ((attr & STAT_AMP) != 0) res += DefenseAmpules;
            if ((attr & STAT_AMPEX) != 0) res += DefenseAmpulesEx;
            return res;
        }


        public int GetOverallForce(int attr = STAT_MAX)
        {
            return GetHitPoints(attr) + GetAttack(attr) + GetDefense(attr);
        }

        public string GetDetailedOverallForceInfo()
        {
            string info = "";
            info += "Lvl Max = " + GetOverallForce(STAT_BASE) + "\r\n";
            info += "+Affection = " + GetOverallForce(STAT_BASE|STAT_AFFECTION) + "\r\n";
            info += "+Ampules = " + GetOverallForce(STAT_BASE|STAT_AFFECTION|STAT_AMP) + "\r\n";
            info += "+AmpulesEx = " + GetOverallForce();
            return info;
        }



        public string GetHitPointsDetailedInfo()
        {
            string info = "";
            info += "Lvl Max = " + HitPointsLvMax + "\r\n";
            info += "Affection = +" + GetHitPointsAffectionBonus(200) + "\r\n";
            info += "Ampules = +" + HitPointAmpules + "\r\n";
            info += "AmpulesEx = +" + HitPointAmpulesEx + "\r\n";
            info += "Total = " + GetHitPoints();
            return info;
        }

        public string GetAttackDetailedInfo()
        {
            string info = "";
            info += "Lvl Max = " + AttackLvMax + "\r\n";
            info += "Affection = +" + GetAttackAffectionBonus(200) + "\r\n";
            info += "Ampules = +" + AttackAmpules + "\r\n";
            info += "AmpulesEx = +" + AttackAmpulesEx + "\r\n";
            info += "Total = " + GetAttack();
            return info;
        }

        public string GetDefenseDetailedInfo()
        {
            string info = "";
            info += "Lvl Max = " + DefenseLvMax + "\r\n";
            info += "Affection = +" + GetHitPointsAffectionBonus(200) + "\r\n";
            info += "Ampules = +" + DefenseAmpules + "\r\n";
            info += "AmpulesEx = +" + DefenseAmpulesEx + "\r\n";
            info += "Total = " + GetDefense();
            return info;
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
