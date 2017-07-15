using System;



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


        //public bool Game01, Game02, Game03;

        private int Evol;
        private int EvolMax;
        private bool NoBloomCG;

        public int SortCategory { get; private set; }
        public bool NoKnight { get; private set; }
      

        private FlowerStats[] Stats;

        private const int EVOL_NUM = 3;



        private object Locker;
        private bool Waiting;

        public int SelectedEvolution { get; private set; }
        public ImageTypes SelectedImage { get; private set; }



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

 
        public enum ImageTypes { IconSmall, IconMedium, IconLarge, Cutin, Bustup, Stand, StandSmall, Home };
        public enum SpecFilter
        {
            All_Knights, Has_Bloom_Form, Has_Bloom_CG, No_Bloom_CG,
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
            Locker = new object();
            Waiting = false;

            BaseType = ObjectType.Flower;

            ID = 0;
            EvolMax = 0;

            Name = new ComplexName();

            Ability1 = new int[EVOL_NUM];
            Ability2 = new int[EVOL_NUM];
            ImageID = new int[EVOL_NUM];

            NoBloomCG = false;
            NoKnight = false;

            Stats = new FlowerStats[EVOL_NUM];
        }



        public FlowerInfo(string[] masterData) : this()
        {
            if (masterData.Length < 54) return;

            int parsedValue;
            if (!int.TryParse(masterData[34], out parsedValue)) return;

            ID = parsedValue;
            Name.Kanji = masterData[45].Replace("\"", "");
            Name.AutoRomaji();

            ShortName = masterData[5].Replace("\"", "");

            int.TryParse(masterData[35], out Evol); Evol--;
            if ((Evol < 0) || (Evol > 2)) { ID = 0; return; }

            EvolMax = Evol;

            if (masterData[46] == "1") NoBloomCG = true;

            int.TryParse(masterData[0], out ImageID[Evol]);
            int.TryParse(masterData[10], out Ability1[Evol]);
            int.TryParse(masterData[11], out Ability2[Evol]);

            int.TryParse(masterData[2], out parsedValue); Family = parsedValue;
            int.TryParse(masterData[7], out parsedValue); Rarity = parsedValue;
            int.TryParse(masterData[3], out parsedValue); Nation = parsedValue;
            int.TryParse(masterData[8], out parsedValue); AttackType = parsedValue;
            int.TryParse(masterData[9], out parsedValue); FavoriteGift = parsedValue;

            int.TryParse(masterData[12], out Skill);

            if (Evol == 0)
            {
                int.TryParse(masterData[27], out parsedValue); SortCategory = parsedValue;
                if (masterData[29] != "0") NoKnight = true;
            }

            Stats[Evol] = new FlowerStats(masterData);

            if (ImageID[Evol] >= 700000) ID = 0;
            //if ((Nation < 1) || (Nation > 7)) ID = 0;
        }



        //======================================================
        public void FillGrid(System.Windows.Forms.DataGridView view, int evol, bool translation = true)
        {
            evol = evol > EvolMax ? EvolMax : evol;

            int id;

            view.Rows.Clear();

            System.Windows.Forms.DataGridViewCellStyle statsStyle = new System.Windows.Forms.DataGridViewCellStyle();
            statsStyle.Font = new System.Drawing.Font("Consolas", 9);
            statsStyle.ForeColor = System.Drawing.Color.DarkBlue;
            id = view.Rows.Add("Ev:ID:ImID", evol + ":" + ID + ":" + ImageID[evol]);
            view.Rows[id].DefaultCellStyle = statsStyle;

            view.Rows.Add("Kanji", Name.Kanji);
            view.Rows.Add("Romaji", Name.Romaji);
            view.Rows.Add("Eng DMM", Name.EngDMM);
            view.Rows.Add("Eng Nutaku", Name.EngNutaku);
            view.Rows.Add("Rarity", GetStars());
            view.Rows.Add("Type", GetAttackType());
            view.Rows.Add("Nation", GetNation());
            view.Rows.Add("Favorite Gift", Gifts[FavoriteGift]);

            //statsStyle.ForeColor = System.Drawing.Color.DarkRed;
            id = view.Rows.Add("HitPoints", Stats[evol].GetHitPointsInfo());
            view.Rows[id].DefaultCellStyle = statsStyle;
            id = view.Rows.Add("Attack", Stats[evol].GetAttackInfo());
            view.Rows[id].DefaultCellStyle = statsStyle;
            id = view.Rows.Add("Defense", Stats[evol].GetDefenseInfo());
            view.Rows[id].DefaultCellStyle = statsStyle;

            view.Rows.Add("Skill Name", GetSkillInfo(1));
            view.Rows.Add("Skill Chance", GetSkillInfo(2));
            id = view.Rows.Add("Skill Desc\r\nID " + Skill, GetSkillInfo(3, translation));
            view.Rows[id].Height = (view.Rows[id].Height - 2) * 3 + 1;

            id = view.Rows.Add("Ability 1\r\n\r\nID " + Ability1[evol], GetAbilitiesInfo(1, evol, translation));
            view.Rows[id].Height = (view.Rows[id].Height - 2) * 5;

            id = view.Rows.Add("Ability 2\r\n\r\nID " + Ability2[evol], GetAbilitiesInfo(2, evol, translation));
            view.Rows[id].Height = (view.Rows[id].Height - 2) * 5;

            EquipmentInfo eq = Program.DB.GetFlowerEquipment(ID);
            if (eq != null) view.Rows.Add("Equipment", eq.KName);
            
        }


        /// <summary>
        /// Return (string) ablilties description
        /// </summary>
        /// <param name="mode">0=Both, 1=1st, 2=2nd</param>
        /// <param name="evol"></param>
        /// <param name="translation"></param>
        /// <returns></returns>
        public string GetAbilitiesInfo(int mode, int evol, bool translation = true)
        {
            if ((evol < 0) || (evol > EvolMax)) evol = EvolMax;

            if ((mode < 0) || (mode > 2))
                return GetAbilitiesInfo(0, evol, translation);

            if (mode == 0)
                return GetAbilitiesInfo(1, evol, translation) + "\r\n\r\n" + GetAbilitiesInfo(2, evol, translation);

            int id = 0;
            if (mode == 1) id = Ability1[evol];
            if (mode == 2) id = Ability2[evol];

            AbilityInfo ability = Program.DB.GetAbility(id);
            if (ability != null) return ability.GetInfo(translation);

            return null;
        }


        /// <summary>
        /// Return (string) skill description.
        /// </summary>
        /// <param name="mode">0=Full, 1=Name, 2=Chance, 3=Full Desc. By default Full.</param>
        /// <param name="translation"></param>
        /// <returns></returns>
        public string GetSkillInfo(int mode = 0, bool translation = true)
        {
            SkillInfo skill = Program.DB.GetSkill(Skill);

            if (skill != null) return skill.GetInfo(mode, translation);

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
                if (ability != null) if (ability.CheckAbilityShortName(shortName)) return true;
            }

            foreach (int ab2 in Ability2)
            {
                ability = Program.DB.GetAbility(ab2);
                if (ability != null) if (ability.CheckAbilityShortName(shortName)) return true;
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
            return res;
        }


        /*
        public int SelectEvolution(int evol)
        {
            if (evol < 0) return 0;
            if (evol > EvolMax) return EvolMax;
            return evol;
        }
        */


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


        
        public string GetImageName()
        {
            if (GetImageEvolID(SelectedEvolution) == 0) return null;

            return GetImageTypeName(SelectedImage) + GetImageEvolID(SelectedEvolution).ToString();
        }
        


        public string GetImageTypeName(ImageTypes type)
        {
            switch (type)
            {
                case ImageTypes.IconSmall: return "icon_s_";
                case ImageTypes.IconMedium: return "icon_m_";
                case ImageTypes.IconLarge: return "icon_l_";
                case ImageTypes.Cutin: return "cutin_";
                case ImageTypes.Bustup: return "bustup_";
                case ImageTypes.Stand: return "stand_";
                case ImageTypes.StandSmall: return "stand_s_";
                case ImageTypes.Home: return "home_";

                default: return "";
            }
        }



        /// <summary>
        /// Select image and lock selection by default
        /// </summary>
        /// <param name="evol"></param>
        /// <param name="type"></param>
        /// <param name="Sync"></param>
        public void SelectImageType(int evol, ImageTypes type, bool Sync = true)
        {
            if (Sync)
            {
                int t = 0;
                while (Waiting)
                {
                    System.Threading.Thread.Sleep(10);
                    t++;
                    if (t > 100)
                    {
                        System.Windows.Forms.MessageBox.Show("Threads synchronization error.", "Error");
                        break;
                    }
                }

                lock (Locker) Waiting = true;
            }

            SelectedEvolution = evol;
            SelectedImage = type;
        }

        public void UnlockSelection() { lock (Locker) Waiting = false; }



        public int GetImageEvolID(int evol)
        {
            return evol > EvolMax ? 0 : ImageID[evol];
        }


        public int CheckEvolutionValue(int evol) { return evol > EvolMax ? EvolMax : evol; }


        /// <summary>
        /// Check is flower has bloom form
        /// </summary>
        /// <param name="cgCheck">Check CG, return true if: 1=Has, 2=No</param>
        /// <returns></returns>
        public bool HasBloomForm(int cgCheck = 0)
        {
            if (NoKnight) return false;

            switch (cgCheck)
            {
                case 0: if (EvolMax > 1) return true; break;
                case 1: if (!NoBloomCG) goto case 0; break;
                case 2: if (NoBloomCG) goto case 0; break;
                default: break;
            }
            return false;
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
                default: break;
            }

            return CompareTo(obj);
        }




        public void Update(FlowerInfo flower)
        {
            int evol = flower.Evol;

            if (EvolMax < evol) EvolMax = evol;

            ImageID[evol] = flower.ImageID[evol];

            Ability1[evol] = flower.Ability1[evol];
            Ability2[evol] = flower.Ability2[evol];

            Stats[evol] = flower.Stats[evol];
        }
    }
}
