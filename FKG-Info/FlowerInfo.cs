namespace FKG_Info
{
    public class FlowerInfo : BaseInfo
    {
        public ComplexName Name;

        public string ShortName { get; private set; }

        public int Rarity{ get; private set; }
        public int Nation { get; private set; }
        public int AttackType { get; private set; }
        public int FavoriteGift { get; private set; }
        public int Family { get; private set; }

        private int Skill;

        private int[] Ability1;
        private int[] Ability2;

        private int[] ImageID;

        private int ExclusiveSkinID;


        private int Evol;
        private int EvolMax;
        private bool NoBloomCG;
        private bool IsEventKnight;


        public int SortCategory { get; private set; }
        public bool NoKnight { get; private set; }

        
        private FlowerStats[] Stats;

        private const int EVOL_NUM = 3;


        public int SelectedEvolution { get; private set; }
        public Animator.Type SelectedImage { get; private set; }
        public Animator.EmoType SelectedEmotion { get; private set; }


        public bool Account1, Account2;



        private static class MrFields
        {
            public const int ImageID = 0;
            public const int Family = 2;
            public const int Nation = 3;
            public const int ShortName = 5;
            public const int Rarity = 7;
            public const int Type = 8;
            public const int FavGift = 9;
            public const int Ability1ID = 10;
            public const int Ability2ID = 11;
            public const int SkillID = 12;
            public const int SortCat = 30;
            public const int NoKnight = 32;
            public const int ID = 37;
            public const int Evol = 38;
            public const int Name = 47;
            public const int NoBloomCG = 48;
            public const int LibararyID = 51;
            public const int IsEventKnight = 53;
        }



        public static readonly string[] AttackTypes =
        {
            "None",
            "Slash",
            "Blunt",
            "Pierce",
            "Magic",
            "Universal"
        };

        public string GetAttackType() { return AttackTypes[AttackType]; }



        public static readonly string[] Nations =
        {
            "None",
            "Winter Rose",
            "Banana Ocean",
            "Blossom Hill",
            "Bergamot Valley",
            "Lily Wood",
            "Kodaibana",
            "Lotus Lake"
        };

        public string GetNation() { return Nations[Nation]; }



        public static readonly string[] Gifts =
        {
            "None",
            "Gem",
            "Teddy Bear",
            "Cake",
            "Book"
        };

 
        public enum SpecFilter
        {
            All_Knights, Has_Bloom_Form, No_Bloom_Form, Has_Exclusive_Skin,
            All_Units, All_Materials,
            Bloom_Materials, Skill_Materials, Equip_Materials,
            Other
        }


        public struct Evolution
        {
            public const int Base = 0;
            public const int Awakened = 1;
            public const int Bloomed = 2;
        };



        public FlowerInfo()
        {
            BaseType = ObjectType.Flower;

            ID = 0;
            EvolMax = 0;

            Name = new ComplexName();

            Ability1 = new int[EVOL_NUM];
            Ability2 = new int[EVOL_NUM];
            ImageID = new int[EVOL_NUM];

            NoBloomCG = false;
            NoKnight = false;

            Account1 = false;
            Account2 = false;

            Stats = new FlowerStats[EVOL_NUM];
        }



        public FlowerInfo(string[] masterData) : this()
        {
            if (masterData.Length < 54) return;

            int parsedValue;
            if (!int.TryParse(masterData[MrFields.ID], out parsedValue)) return;

            ID = parsedValue;
            Name.Kanji = masterData[MrFields.Name].Replace("\"", "");
            Name.AutoRomaji();

            ShortName = masterData[MrFields.ShortName].Replace("\"", "");

            int.TryParse(masterData[MrFields.Evol], out Evol); Evol--;
            if ((Evol < 0) || (Evol > 2)) { ID = 0; return; }

            EvolMax = Evol;

            if (masterData[MrFields.NoBloomCG] == "1") NoBloomCG = true;
            if (masterData[MrFields.IsEventKnight] == "1") IsEventKnight = true;

            int.TryParse(masterData[MrFields.ImageID], out ImageID[Evol]);
            int.TryParse(masterData[MrFields.Ability1ID], out Ability1[Evol]);
            int.TryParse(masterData[MrFields.Ability2ID], out Ability2[Evol]);

            int.TryParse(masterData[MrFields.Family], out parsedValue); Family = parsedValue;
            int.TryParse(masterData[MrFields.Rarity], out parsedValue); Rarity = parsedValue;
            int.TryParse(masterData[MrFields.Nation], out parsedValue); Nation = parsedValue;
            int.TryParse(masterData[MrFields.Type], out parsedValue); AttackType = parsedValue;
            int.TryParse(masterData[MrFields.FavGift], out parsedValue); FavoriteGift = parsedValue;

            int.TryParse(masterData[MrFields.SkillID], out Skill);

            if (Evol == 0)
            {
                int.TryParse(masterData[MrFields.SortCat], out parsedValue); SortCategory = parsedValue;
                if (masterData[MrFields.NoKnight] != "0") NoKnight = true;
            }

            Stats[Evol] = new FlowerStats(masterData);

            if (ImageID[Evol] >= 700000) ID = 0;
        }



        /// <summary>
        /// Fil DataGridView witn flower information
        /// </summary>
        /// <param name="view"></param>
        /// <param name="evol"></param>
        /// <param name="translation"></param>
        public void FillGrid(System.Windows.Forms.DataGridView view, int evol)
        {
            evol = evol > EvolMax ? EvolMax : evol;

            int rowid;

            var blueFonfStyle= new System.Windows.Forms.DataGridViewCellStyle() { ForeColor = System.Drawing.Color.DarkViolet };

            view.Rows.Clear();

            view.Rows.Add("Ev:ID:ImID", evol + ":" + ID + ":" + ImageID[evol]);
            view.Rows.Add("Kanji", Name.Kanji);
            view.Rows.Add("Romaji", Name.Romaji);
            view.Rows.Add("DMM Wiki", Name.EngDMM);
            view.Rows.Add("Nutaku", Name.EngNutaku);
            view.Rows.Add("Type", GetAttackType());
            view.Rows.Add("Nation", GetNation());
            view.Rows.Add("Gift", Gifts[FavoriteGift]);

            FlowerStats fwst = Stats[evol];
            if (fwst != null)
            {
                rowid = view.Rows.Add("HitPoints", fwst.GetHitPoints());
                view.Rows[rowid].Cells[1].ToolTipText = fwst.GetHitPointsDetailedInfo();
                view.Rows[rowid].Cells[1].Style = blueFonfStyle;
                rowid = view.Rows.Add("Attack", fwst.GetAttack());
                view.Rows[rowid].Cells[1].ToolTipText = fwst.GetAttackDetailedInfo();
                view.Rows[rowid].Cells[1].Style = blueFonfStyle;
                rowid = view.Rows.Add("Defense", fwst.GetDefense());
                view.Rows[rowid].Cells[1].ToolTipText = fwst.GetDefenseDetailedInfo();
                view.Rows[rowid].Cells[1].Style = blueFonfStyle;

                view.Rows.Add("Speed", fwst.SpeedLvMax);
            }

            view.Rows.Add("Skill Name", GetSkillInfo(1));
            view.Rows.Add("Skill Chance", GetSkillInfo(2));
            view.Rows.Add("Skill Desc\r\nID:" + Skill, GetSkillInfo(3));

            for (int i = 0; i < 2 * AbilityInfo.SUBABL_NUM; i++)
            {
                var adata = GetAbilityData(i, evol);
                if (adata == null) continue;
                if (adata[0] == 0) continue;

                var info = GetAbilitiesInfo(i, evol);
                if (info == null) continue;

                var image = Program.DB.AbilityIcons[Program.DB.TranslatorAbilities.GetIconId(adata)];
                var ids = GetAbilitiesInfo(i, evol, true);
                view.Rows.Add(Helper.CreateDGVRow(image, info, ids));
            }

            if (fwst != null)
            {
                rowid = view.Rows.Add("Overall Force", fwst.GetOverallForce());
                view.Rows[rowid].Cells[1].ToolTipText = fwst.GetDetailedOverallForceInfo();
            }

            if (view.Rows.Count > 11)
            {
                view.Rows[0].DividerHeight = 1;
                view.Rows[4].DividerHeight = 1;
                view.Rows[11].DividerHeight = 1;
            }
        }



        public int[] GetAbilityData(int n, int evol)
        {
            evol = CheckEvolutionValue(evol);

            AbilityInfo ab = Program.DB.GetAbility(Ability1[evol]);

            if (ab != null)
            {
                if (n < ab.Count)
                {
                    return ab.GetParams(n);
                }
                else
                {
                    n -= ab.Count;
                    ab = Program.DB.GetAbility(Ability2[evol]);
                    if (ab != null) return ab.GetParams(n);
                }
            }

            return null;
        }



        public int GetAbilityTypeID(int n, int evol)
        {
            return GetAbilityData(n, evol)[0];
        }



        public string GetAbilitiesInfo(int n, int evol, bool getIDs = false)
        {


            int id = GetAbilityTypeID(n, evol);

            if (id == 0) return null;

            evol = CheckEvolutionValue(evol);

            AbilityInfo ab = Program.DB.GetAbility(Ability1[evol]);

            if (ab != null)
            {
                if (n < ab.Count)
                {
                    return ab.GetInfo(n, getIDs);
                }
                else
                {
                    n -= ab.Count;
                    ab = Program.DB.GetAbility(Ability2[evol]);
                    if (ab != null) return ab.GetInfo(n, getIDs);
                }
            }

            return null;
        }



        public string GetSkillInfo(int mode = 0)
        {
            SkillInfo skill = Program.DB.GetSkill(Skill);

            if (skill != null) return skill.GetInfo(mode);

            return null;
        }



        public bool CheckAbilityType(int type)
        {
            AbilityInfo ability;

            foreach (int ab1 in Ability1)
            {
                ability = Program.DB.GetAbility(ab1);
                if (ability != null) if (ability.CheckTypeID(type)) return true;
            }

            foreach (int ab2 in Ability2)
            {
                ability = Program.DB.GetAbility(ab2);
                if (ability != null) if (ability.CheckTypeID(type)) return true;
            }

            return false;
        }



        public bool CheckAbilityShortName(string shortName)
        {
            AbilityInfo ability;

            foreach (int ab1 in Ability1)
            {
                ability = Program.DB.GetAbility(ab1);
                if (ability != null) if (ability.CheckAbilityTags(shortName)) return true;
            }

            foreach (int ab2 in Ability2)
            {
                ability = Program.DB.GetAbility(ab2);
                if (ability != null) if (ability.CheckAbilityTags(shortName)) return true;
            }

            return false;
        }



        public bool CheckNames(string search)
        {
            bool res = false;

            search = search.ToLower();
            if (Name.Kanji != null) res |= Name.Kanji.ToLower().Contains(search);
            if (Name.Romaji != null) res |= Name.Romaji.ToLower().Contains(search);
            if (Name.EngDMM != null) res |= Name.EngDMM.ToLower().Contains(search);
            if (Name.EngNutaku != null) res |= Name.EngNutaku.ToLower().Contains(search);

            for (int i = 0; i < EVOL_NUM; i++) if (ImageID[i] != 0) res |= ImageID[i].ToString().Contains(search);

            return res;
        }



        string GetStars()
        {
            switch (Rarity)
            {
                case 1: return "★";
                case 2: return "★★";
                case 3: return "★★★";
                case 4: return "★★★★";
                case 5: return "★★★★★";
                case 6: return "★★★★★★";
                default: return "";
            }
        }



        /// <summary>
        /// Image ID
        /// </summary>
        /// <param name="evol">Evolution tear: 0 = base, 1 = evol, 2 = bloom.</param>
        /// <param name="ex">If true, return exclusive skin ID.</param>
        /// <returns></returns>
        public int GetImageEvolID(int evol, bool ex = false)
        {
            if ((ex) && (ExclusiveSkinID != 0)) return ExclusiveSkinID;

            return evol > EvolMax ? 0 : ImageID[evol];
        }


        public int CheckEvolutionValue(int evol) { return evol > EvolMax ? EvolMax : evol; }


        /// <summary>
        /// Check is flower has bloom form or bloom CG.
        /// </summary>
        /// <param name="checkcg"></param>
        /// <param name="invert"></param>
        /// <returns></returns>
        public bool CheckBloomForm(bool checkcg = false, bool invert = false)
        {
            if (NoKnight) return false;

            bool res;

            if(!checkcg)
                res = (EvolMax > 1) ^ invert;
            else
                res = (!NoBloomCG) ^ invert;

            return res;
        }


        /// <summary>
        /// Check is event knight, based on isEvent param.
        /// If invert = true, return true only for no event.
        /// </summary>
        /// <param name="invert"></param>
        /// <returns></returns>
        public bool CheckIsEvent(bool invert = false)
        {
            if (NoKnight) return false;

            return IsEventKnight ^ invert;
        }



        public bool CheckCategory(SpecFilter spec)
        {
            switch (spec)
            {
                case SpecFilter.All_Units: return true;
                case SpecFilter.All_Materials: return NoKnight;
                case SpecFilter.Skill_Materials:
                    if ((SortCategory == 101) && (ImageID[0] >= 200000)) return true;
                    break;
                case SpecFilter.Equip_Materials:
                    if ((SortCategory == 102) && (ImageID[0] >= 200000)) return true;
                    break;
                case SpecFilter.Bloom_Materials:
                    if ((SortCategory == 400) || (SortCategory == 500)) return true;
                    break;
                case SpecFilter.Other:
                    if ((SortCategory == 300) || (SortCategory == 201)) return true;
                    if ((SortCategory > 100) && (SortCategory < 104) && (ImageID[0] < 200000)) return true;
                    break;
                default: break;
            }
            return false;
        }

        

        /// <summary>
        /// Sort: (-1: this, obj) (1: obj, this)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int CompareTo(object obj)
        {
            FlowerInfo cmpFlower = obj as FlowerInfo;

            if (cmpFlower == null) return 0;

            // Top rare first
            if (Rarity < cmpFlower.Rarity) return 1;
            if (Rarity > cmpFlower.Rarity) return -1;
            // Sl,Bl,Pr,Mg
            if (AttackType > cmpFlower.AttackType) return 1;
            if (AttackType < cmpFlower.AttackType) return -1;

            //string S0 = Name.Romaji.Substring(0, 2);
            //string S1 = Flower.Name.Romaji.Substring(0, 2);
            //int Res = String.Compare(Name.Romaji, cmpFlower.Name.Romaji);

            return base.CompareTo(obj);
        }




        public override int CompareTo(object obj, SortBy sortType)
        {
            FlowerInfo fw = obj as FlowerInfo;

            if (fw == null) return 1;

            switch (sortType)
            {
                case SortBy.Category:
                    if (SortCategory < fw.SortCategory) return 1;
                    if (SortCategory > fw.SortCategory) return -1;
                    break;
                case SortBy.OverallForce:
                    if (Stats[EvolMax].GetOverallForce() < fw.Stats[fw.EvolMax].GetOverallForce()) return 1;
                    if (Stats[EvolMax].GetOverallForce() > fw.Stats[fw.EvolMax].GetOverallForce()) return -1;
                    break;
                case SortBy.Attack:
                    if (Stats[EvolMax].GetAttack() < fw.Stats[fw.EvolMax].GetAttack()) return 1;
                    if (Stats[EvolMax].GetAttack() > fw.Stats[fw.EvolMax].GetAttack()) return -1;
                    break;
                case SortBy.Defense:
                    if (Stats[EvolMax].GetDefense() < fw.Stats[fw.EvolMax].GetDefense()) return 1;
                    if (Stats[EvolMax].GetDefense() > fw.Stats[fw.EvolMax].GetDefense()) return -1;
                    break;
                case SortBy.HitPoints:
                    if (Stats[EvolMax].GetHitPoints() < fw.Stats[fw.EvolMax].GetHitPoints()) return 1;
                    if (Stats[EvolMax].GetHitPoints() > fw.Stats[fw.EvolMax].GetHitPoints()) return -1;
                    break;
                case SortBy.Speed:
                    if (Stats[EvolMax].SpeedLvMax < fw.Stats[fw.EvolMax].SpeedLvMax) return 1;
                    if (Stats[EvolMax].SpeedLvMax > fw.Stats[fw.EvolMax].SpeedLvMax) return -1;
                    break;
                default: break;
            }

            return CompareTo(obj);
        }



        public void FindExclusiveSkin(System.Collections.Generic.List<SkinInfo> skins)
        {
            SkinInfo skin;
            
            skin = skins.Find(sk => sk.ID == ImageID[0]);
            if (skin == null) return;

            skin = skins.Find(sk => sk.CheckExclusive(skin));
            if (skin == null) return;

            ExclusiveSkinID = skin.ID;
        }

        public bool HasExclusiveSkin() { return ExclusiveSkinID != 0; }



        public void Update(FlowerInfo flower)
        {
            int evol = flower.Evol;

            if (EvolMax < evol) EvolMax = evol;

            ImageID[evol] = flower.ImageID[evol];

            Ability1[evol] = flower.Ability1[evol];
            Ability2[evol] = flower.Ability2[evol];

            Stats[evol] = flower.Stats[evol];
        }



        public int GetAccStatus() { return ((Account1 ? 1 : 0) | ((Account2 ? 1 : 0) << 1)); }

        public void SetAccStatus(int status)
        {
            if ((status & 0x01) != 0) Account1 = true;
            if ((status & 0x02) != 0) Account2 = true;
        }
    }
}
