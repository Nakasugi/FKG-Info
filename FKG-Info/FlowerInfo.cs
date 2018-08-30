namespace FKG_Info
{
    public class FlowerInfo : BaseInfo
    {
        public ComplexName Name;

        public string ShortName { get; private set; }

        public int RefID { get; private set; }
        public int Rarity { get; private set; }
        public int Nation { get; private set; }
        public int AttackType { get; private set; }
        public int FavoriteGift { get; private set; }
        public int Family { get; private set; }

        public int Evolution { get; private set; }

        private int Skill;

        private int AbilityID1;
        private int AbilityID2;

        public bool CanBloom { get; private set; }
        private bool IsBloomed;
        private int GrownID;
        public bool CanGrow { get; private set; }
        public bool IsGrown { get; private set; }



        public bool NoBloomCG { get; private set; }
        public bool IsEventKnight { get; private set; }


        public int SortCategory { get; private set; }
        public bool IsKnight { get; private set; }

        private int ExclusiveSkinID;

        public FlowerStats Stats { get; private set; }

        private const int EVOL_NUM = 3;


        public int SelectedEvolution { get; private set; }
        public Animator.Type SelectedImage { get; private set; }
        public Animator.EmoType SelectedEmotion { get; private set; }

        
        
        private AbilityInfo Ability1
        {
            get
            {
                if (Ability1Value == null) Ability1Value = Program.DB.GetAbility(AbilityID1);
                return Ability1Value;
            }
        }

        private AbilityInfo Ability2
        {
            get
            {
                if (Ability2Value == null) Ability2Value = Program.DB.GetAbility(AbilityID2);
                return Ability2Value;
            }
        }

        private AbilityInfo Ability1Value;
        private AbilityInfo Ability2Value;




        private static class MrFields
        {
            public const int ID = 0;
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
            public const int isNotPreEvo = 32;
            public const int IsKnight = 33;
            public const int RefID = 37;
            public const int Evolution = 38;
            public const int IsBloomed = 45;
            public const int CanBloom = 46;
            public const int Name = 47;
            public const int NoBloomCG = 48;
            public const int LibararyID = 51;
            public const int IsEventKnight = 53;
            public const int Version = 57;
            public const int GrownID = 58;
            public const int IsGrown = 59;
            public const int CanGrow = 60;
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
            All_Knights, No_Bloom_Form, Has_Exclusive_Skin, Can_Grow,
            All_Units, All_Materials,
            Bloom_Materials, Skill_Materials, Equip_Materials,
            Other

        }


        public struct Variation
        {
            public const int Base = 0;
            public const int Evolved = 1;
            public const int Bloomed = 2;
            public const int GrownEvolved = 3;
            public const int GrownBloomed = 4;
            public const int Grown = 98;
            //public const int Count = 4;
        };

        public static readonly string[] VariationNames = 
            { "Base", "Evolved", "Bloomed", "Grown Evolved", "Grown Bloomed" };



        public FlowerInfo()
        {
            ID = 0;
            BaseType = ObjectType.Flower;

            Name = new ComplexName();

            Ability1Value = null;
            Ability2Value = null;
        }



        public FlowerInfo(string[] masterData) : this()
        {
            if (masterData.Length < 62) return;

            int parsedValue;
            if (!int.TryParse(masterData[MrFields.ID], out parsedValue)) return;
            ID = parsedValue;

            int.TryParse(masterData[MrFields.RefID], out parsedValue); RefID = parsedValue;
            if (RefID == 0) ID = 0;
            if (ID == 0) return;

            Name.Kanji = masterData[MrFields.Name].Replace("\"", "");
            Name.AutoRomaji();

            ShortName = masterData[MrFields.ShortName].Replace("\"", "");

            SetVersion(masterData[MrFields.Version]);


            //if ((Evol < 0) || (Evol > 2)) { ID = 0; return; }

            //EvolMax = Evol;


            if (masterData[MrFields.CanBloom] != "0") CanBloom = true;
            if (masterData[MrFields.IsBloomed] != "0") IsBloomed = true;
            if (masterData[MrFields.NoBloomCG] != "0") NoBloomCG = true;
            if (masterData[MrFields.IsEventKnight] != "0") IsEventKnight = true;
            if (masterData[MrFields.IsKnight] != "0") IsKnight = true;
            if (masterData[MrFields.CanGrow] != "0") CanGrow = true;
            if (masterData[MrFields.IsGrown] != "0") IsGrown = true;

            int.TryParse(masterData[MrFields.Evolution], out parsedValue); Evolution = parsedValue - 1;

            int.TryParse(masterData[MrFields.SkillID], out Skill);
            int.TryParse(masterData[MrFields.Ability1ID], out AbilityID1);
            int.TryParse(masterData[MrFields.Ability2ID], out AbilityID2);

            int.TryParse(masterData[MrFields.Family], out parsedValue); Family = parsedValue;
            int.TryParse(masterData[MrFields.Rarity], out parsedValue); Rarity = parsedValue;
            int.TryParse(masterData[MrFields.Nation], out parsedValue); Nation = parsedValue;
            int.TryParse(masterData[MrFields.Type], out parsedValue); AttackType = parsedValue;
            int.TryParse(masterData[MrFields.FavGift], out parsedValue); FavoriteGift = parsedValue;
           

            int.TryParse(masterData[MrFields.SortCat], out parsedValue); SortCategory = parsedValue;
            int.TryParse(masterData[MrFields.GrownID], out parsedValue); GrownID = parsedValue;


            Stats = new FlowerStats(masterData);

            if (ID >= 700000) ID = 0;
        }



        /// <summary>
        /// Fil DataGridView witn flower information
        /// </summary>
        /// <param name="view"></param>
        /// <param name="evol"></param>
        /// <param name="translation"></param>
        public void FillGrid(System.Windows.Forms.DataGridView view)
        {
            //evol = evol > EvolMax ? EvolMax : evol;

            int rowid;

            var blueFonfStyle = new System.Windows.Forms.DataGridViewCellStyle() { ForeColor = System.Drawing.Color.DarkViolet };

            view.Rows.Clear();

            view.Rows.Add("RefID:ImgID", RefID + ":" + ID);
            view.Rows.Add("Kanji", Name.Kanji);
            view.Rows.Add("Romaji", Name.Romaji);
            view.Rows.Add("DMM Wiki", Name.EngDMM);
            view.Rows.Add("Nutaku", Name.EngNutaku);
            view.Rows.Add("Type", GetAttackType());
            view.Rows.Add("Nation", GetNation());
            view.Rows.Add("Gift", Gifts[FavoriteGift]);

            FlowerStats fwst = Stats;
            if (fwst != null)
            {
                rowid = view.Rows.Add("HitPoints", fwst.GetHitPointsInfo());
                view.Rows[rowid].Cells[1].ToolTipText = fwst.GetHitPointsDetailedInfo();
                //view.Rows[rowid].Cells[1].
                view.Rows[rowid].Cells[1].Style = blueFonfStyle;
                rowid = view.Rows.Add("Attack", fwst.GetAttackInfo());
                view.Rows[rowid].Cells[1].ToolTipText = fwst.GetAttackDetailedInfo();
                view.Rows[rowid].Cells[1].Style = blueFonfStyle;
                rowid = view.Rows.Add("Defense", fwst.GetDefenseInfo());
                view.Rows[rowid].Cells[1].ToolTipText = fwst.GetDefenseDetailedInfo();
                view.Rows[rowid].Cells[1].Style = blueFonfStyle;

                view.Rows.Add("Speed", fwst.SpeedLvMax);
            }

            view.Rows.Add("Skill Name", GetSkillInfo(1));
            view.Rows.Add("Skill Chance", GetSkillInfo(2));
            view.Rows.Add("Skill Desc\r\nID:" + Skill, GetSkillInfo(3));

            for (int i = 0; i < 2 * AbilityInfo.SUBABL_NUM; i++)
            {
                var adata = GetAbilityData(i);
                if (adata == null) continue;
                if (adata[0] == 0) continue;

                var info = GetAbilitiesInfo(i);
                if (info == null) continue;

                var image = Program.DB.AbilityIcons[Program.DB.TranslatorAbilities.GetIconId(adata)];
                var ids = GetAbilitiesInfo(i, true);
                view.Rows.Add(Helper.CreateDGVRow(image, info, ids));
            }

            if (fwst != null)
            {
                rowid = view.Rows.Add("Overall Force", fwst.GetOverallForceInfo());
                view.Rows[rowid].Cells[1].ToolTipText = fwst.GetDetailedOverallForceInfo();
            }

            view.Rows.Add("Version", GetVersion());

            if (view.Rows.Count > 11)
            {
                view.Rows[0].DividerHeight = 1;
                view.Rows[4].DividerHeight = 1;
                view.Rows[11].DividerHeight = 1;
            }
        }



        public int[] GetAbilityData(int n)
        {
            if (Ability1 != null)
            {
                if (n < Ability1.Count)
                {
                    return Ability1.GetParams(n);
                }
                else
                {
                    n -= Ability1.Count;
                    if (Ability2 != null) return Ability2.GetParams(n);
                }
            }

            return null;
        }



        public int GetAbilityTypeID(int n)
        {
            return GetAbilityData(n)[0];
        }



        public string GetAbilitiesInfo(int n, bool getIDs = false)
        {


            int id = GetAbilityTypeID(n);

            if (id == 0) return null;

            if (Ability1 != null)
            {
                if (n < Ability1.Count)
                {
                    return Ability1.GetInfo(n, getIDs);
                }
                else
                {
                    n -= Ability1.Count;
                    if (Ability2 != null) return Ability2.GetInfo(n, getIDs);
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
            if (Ability1 != null) if (Ability1.CheckTypeID(type)) return true;
            if (Ability2 != null) if (Ability2.CheckTypeID(type)) return true;

            return false;
        }



        public bool CheckAbilityShortName(string shortName)
        {
            if (Ability1 != null) if (Ability1.CheckAbilityTags(shortName)) return true;
            if (Ability2 != null) if (Ability2.CheckAbilityTags(shortName)) return true;

            return false;
        }



        public bool CheckNames(string search)
        {
            bool res = false;

            string lowSearch = search.ToLower();

            if (Name.Kanji != null) res |= Name.Kanji.ToLower().Contains(lowSearch);
            if (Name.Romaji != null) res |= Name.Romaji.ToLower().Contains(lowSearch);
            if (Name.EngDMM != null) res |= Name.EngDMM.ToLower().Contains(lowSearch);
            if (Name.EngNutaku != null) res |= Name.EngNutaku.ToLower().Contains(lowSearch);

            res |= ID.ToString().Contains(search);

            if (lowSearch.Length > 3)
            {
                if (lowSearch.Substring(0, 3) == "id=")
                {
                    if (lowSearch.Substring(3) == RefID.ToString()) res = true;
                }
            }

            if (lowSearch.Length > 5)
            {
                if (lowSearch.Substring(0, 5) == "name=")
                {
                    if (search.Substring(5) == Name.Romaji) res = true;
                }
            }

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



        public int GetImageID(bool exskin = false)
        {
            if (exskin) return ExclusiveSkinID;

            if (IsGrown)
                return Program.DB.GrownImageIDReplacer.ReplaceImageID(GrownID);
            else
                return ID;
        }

        public string GetImageStringID(bool exskin = false)
        {
            return GetImageID(exskin).ToString();
        }



        public bool CheckIsEvent(bool invert = false)
        {
            if (!IsKnight) return false;

            return IsEventKnight ^ invert;
        }



        public bool CheckBloomForm(bool checkcg = false, bool invert = false)
        {
            if (!IsKnight) return false;

            bool res = true;

            if (!checkcg)
                res = CanBloom ^ invert;
            else
                res = (!NoBloomCG) ^ invert;

            return res;
        }



        public int GetBaseID()
        {
            if (!IsKnight) return ID;

            switch (Evolution)
            {
                case Variation.Evolved: return ID - 1;
                case Variation.Bloomed: return ID - 300000;
                case Variation.Grown:
                    if (CanBloom) return ID - 300000;
                    if (IsBloomed) return ID - 300001;
                    break;
                default: break;
            }

            return ID;
        }



        private int GetVariation()
        {
            if (Evolution == Variation.Grown)
            {
                if (CanBloom) return Variation.GrownEvolved;
                if (IsBloomed) return Variation.GrownBloomed;
            }
            return Evolution;
        }



        public int GetNextEvolutionID()
        {
            int nextid = -1;

            switch (Evolution)
            {
                case Variation.Base: nextid = ID + 1; break;
                case Variation.Evolved: nextid = ID + 300000 - 1; break;
                case Variation.Bloomed: nextid = ID + 1; break;
                case Variation.Grown: if (CanBloom) nextid = ID + 1; break;
                default: break;
            }

            return nextid;
        }



        public int GetPrevEvolutionID()
        {
            int previd = -1;

            switch (Evolution)
            {
                case Variation.Evolved: previd = ID - 1; break;
                case Variation.Bloomed: previd = ID - 300000 + 1; break;
                case Variation.Grown:
                    if (CanBloom)
                        previd = ID - 300000 + 1;
                    else
                        previd = ID - 1;
                    break;
                default: break;
            }

            return previd;
        }
        


        /// <summary>
        /// 0,1,2 - evol, 3 - grown env, 4 grown blm.
        /// </summary>
        /// <param name="variation"></param>
        /// <param name="invert"></param>
        /// <returns></returns>
        public bool CheckVariation(int variation, bool invert = false)
        {
            if (!IsKnight) return false;

            bool res = false;

            if (Evolution == variation) res = true;

            if (Evolution == 98)
            {
                if ((variation == 3) && (CanBloom)) res = true;
                if ((variation == 4) && (IsBloomed)) res = true;
            }

            return res ^ invert;
        }



        public bool CheckCategory(SpecFilter spec)
        {
            switch (spec)
            {
                case SpecFilter.All_Units: return true;
                case SpecFilter.All_Materials: return !IsKnight;
                case SpecFilter.Skill_Materials:
                    if ((SortCategory == 101) && (ID >= 200000)) return true;
                    break;
                case SpecFilter.Equip_Materials:
                    if ((SortCategory == 102) && (ID >= 200000)) return true;
                    break;
                case SpecFilter.Bloom_Materials:
                    if ((SortCategory == 400) || (SortCategory == 500)) return true;
                    break;
                case SpecFilter.Other:
                    if ((SortCategory == 300) || (SortCategory == 201)) return true;
                    if ((SortCategory > 100) && (SortCategory < 104) && (ID < 200000)) return true;
                    break;
                default: break;
            }
            return false;
        }



        public void FindExclusiveSkin(FlowerDataBase db)
        {
            SkinInfo skin;

            int id = ID;

            if (IsGrown) id = db.GrownImageIDReplacer.ReplaceImageID(GrownID);

            skin = db.Skins.Find(sk => sk.ID == id);
            if (skin == null) return;
            skin = db.Skins.Find(sk => sk.CheckExclusive(skin));
            if (skin == null) return;

            ExclusiveSkinID = skin.ID;
        }

        public bool HasExclusiveSkin() { return ExclusiveSkinID != 0; }



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

            if (RefID > cmpFlower.RefID) return 1;
            if (RefID < cmpFlower.RefID) return -1;

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
                    if (Stats.GetOverallForce() < fw.Stats.GetOverallForce()) return 1;
                    if (Stats.GetOverallForce() > fw.Stats.GetOverallForce()) return -1;
                    break;
                case SortBy.Attack:
                    if (Stats.GetAttack() < fw.Stats.GetAttack()) return 1;
                    if (Stats.GetAttack() > fw.Stats.GetAttack()) return -1;
                    break;
                case SortBy.Defense:
                    if (Stats.GetDefense() < fw.Stats.GetDefense()) return 1;
                    if (Stats.GetDefense() > fw.Stats.GetDefense()) return -1;
                    break;
                case SortBy.HitPoints:
                    if (Stats.GetHitPoints() < fw.Stats.GetHitPoints()) return 1;
                    if (Stats.GetHitPoints() > fw.Stats.GetHitPoints()) return -1;
                    break;
                case SortBy.Speed:
                    if (Stats.SpeedLvMax < fw.Stats.SpeedLvMax) return 1;
                    if (Stats.SpeedLvMax > fw.Stats.SpeedLvMax) return -1;
                    break;
                default: break;
            }

            return CompareTo(obj);
        }



        public int GetBloomCGStatusCode()
        {
            if (!IsBloomed) return 0;

            int code = ID << 8;

            return NoBloomCG ? code : code | 4;
        }
    }
}
