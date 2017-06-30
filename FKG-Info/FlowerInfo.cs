using System;



namespace FKG_Info
{
    public class FlowerInfo : IComparable
    {
        public ComplexName Name;
 
        public int ID { get; private set; }
        public int Rarity{ get; private set; }
        public int Nation { get; private set; }
        public int AttackType { get; private set; }
        public int FavoriteGift { get; private set; }
        public int Family { get; private set; }

        private int Skill;

        private int[] Ability1;
        private int[] Ability2;

        private int[] ImageID;


        public bool Game01, Game02, Game03;

        private int Evol;

        private FlowerStats[] Stats;

        private const int EVOL_NUM = 3;

        private int EvolMax;



        public static readonly string[] AttackTypes =
        {
            "None",
            "Slash",
            "Blunt",
            "Pierce",
            "Magic"
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



        public static string[] Gifts =
        {
            "None",
            "Gem",
            "Teddy Bear",
            "Cake",
            "Book"
        };

 
        public enum ImageTypes { IconSmall, IconMedium, IconLarge, Cutin, Bustup, Stand, StandSmall, Home };

        public struct Evolution
        {
            public const int Base = 0;
            public const int Awakened = 1;
            public const int Bloomed = 2;
        };



        public FlowerInfo()
        {
            ID = 0;
            EvolMax = 0;

            Name = new ComplexName();

            Ability1 = new int[EVOL_NUM];
            Ability2 = new int[EVOL_NUM];
            ImageID = new int[EVOL_NUM];

            Stats = new FlowerStats[EVOL_NUM];
        }



        public FlowerInfo(string[] masterData) : this()
        {
            if (masterData.Length < 54) return;

            int parsedValue;
            if (!int.TryParse(masterData[4], out parsedValue)) return;
            if (parsedValue < 1) return;
            if (parsedValue > 999) return;

            ID = parsedValue;
            Name.Kanji = masterData[45].Replace("\"", "");
            Name.AutoRomaji();


            int.TryParse(masterData[35], out Evol); Evol--;
            if ((Evol < 0) || (Evol > 2)) { ID = 0; return; }

            EvolMax = Evol;

            

            int.TryParse(masterData[0], out ImageID[Evol]);
            int.TryParse(masterData[10], out Ability1[Evol]);
            int.TryParse(masterData[11], out Ability2[Evol]);

            int.TryParse(masterData[2], out parsedValue); Family = parsedValue;
            int.TryParse(masterData[7], out parsedValue); Rarity = parsedValue;
            int.TryParse(masterData[3], out parsedValue); Nation = parsedValue;
            int.TryParse(masterData[8], out parsedValue); AttackType = parsedValue;
            int.TryParse(masterData[9], out parsedValue); FavoriteGift = parsedValue;


            int.TryParse(masterData[12], out Skill);

            Stats[Evol] = new FlowerStats(masterData);

            if ((Nation < 1) || (Nation > 7)) ID = 0;
        }



        //======================================================
        public void FillGrid(System.Windows.Forms.DataGridView view, int evol, bool translation = true)
        {
            if ((evol < 0) || (evol > EvolMax)) evol = EvolMax;

            int id;

            view.Rows.Clear();
            view.Rows.Add("Kanji", Name.Kanji);
            view.Rows.Add("Romaji", Name.Romaji);
            view.Rows.Add("Eng DMM", Name.EngDMM);
            view.Rows.Add("Eng Nutaku", Name.EngNutaku);
            view.Rows.Add("Rarity", GetStars());
            view.Rows.Add("Type", GetAttackType());
            view.Rows.Add("Nation", GetNation());
            view.Rows.Add("Favorite Gift", Gifts[FavoriteGift]);
            view.Rows.Add("Unique ID", ID);

            System.Windows.Forms.DataGridViewCellStyle statsStyle = new System.Windows.Forms.DataGridViewCellStyle();
            statsStyle.Font = new System.Drawing.Font("Consolas", 9);
            statsStyle.ForeColor = System.Drawing.Color.DarkRed;

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


        public int SelectEvolution(int evol)
        {
            if (evol < 0) return 0;
            if (evol > EvolMax) return EvolMax;
            return evol;
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


        
        public string GetImageName(int evol, ImageTypes type)
        {
            if (GetImageEvolID(evol) == 0) return null;

            return GetImageTypeName(type) + GetImageEvolID(evol).ToString();
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



        public int GetImageEvolID(int evol)
        {
            return ImageID[evol];
        }


 
        //======================================================
        // IComparable:
        //======================================================
        public int CompareTo(object obj)
        {
            FlowerInfo cmpFlower = obj as FlowerInfo;

            // Top rare first
            if (Rarity < cmpFlower.Rarity) return 1;
            if (Rarity > cmpFlower.Rarity) return -1;
            // Sl,Bl,Pr,Mg
            if (AttackType > cmpFlower.AttackType) return 1;
            if (AttackType < cmpFlower.AttackType) return -1;

            //string S0 = Name.Romaji.Substring(0, 2);
            //string S1 = Flower.Name.Romaji.Substring(0, 2);
            int Res = String.Compare(Name.Romaji, cmpFlower.Name.Romaji);
            if (Res != 0) return Res;
                 
            return 0;
        }


        // Sort by Skill Base RMS
        public static int BySkillBaseRMS(FlowerInfo fw1, FlowerInfo fw2)
        {
            if (fw1 == null || fw2 == null) return 0;

            //if (fw1.Skill.GetBaseRMS() == fw2.Skill.GetBaseRMS()) return fw1.CompareTo(fw2);

            //if (fw1.Skill.GetBaseRMS() < fw2.Skill.GetBaseRMS()) return 1; else return -1;

            return 0;
        }


        // Sort by Skill Raid RMS
        public static int BySkillRaidRMS(FlowerInfo fw1, FlowerInfo fw2)
        {
            if (fw1 == null || fw2 == null) return 0;

            //if (fw1.Skill.GetRaidRMS() == fw2.Skill.GetRaidRMS()) return fw1.CompareTo(fw2);

            //if (fw1.Skill.GetRaidRMS() < fw2.Skill.GetRaidRMS()) return 1; else return -1;
            return 0;
        }


        //public float GetSABaseRMS() { return Skill.GetBaseRMS() + Ability1.GetBaseRMS() + Ability2.GetBaseRMS(); }
        //public float GetSARaidRMS() { return Skill.GetRaidRMS() + Ability1.GetRaidRMS() + Ability2.GetRaidRMS(); }


        // Sort by Skill+Ability Base RMS
        public static int BySABaseRMS(FlowerInfo fw1, FlowerInfo fw2)
        {
            if (fw1 == null || fw2 == null) return 0;

            //float RMS1 = fw1.GetSABaseRMS();
            //float RMS2 = fw2.GetSABaseRMS();

            //if (RMS1 == RMS2) return fw1.CompareTo(fw2);

            //if (RMS1 < RMS2) return 1; else return -1;

            return 0;
        }


        // Sort by Skill+Ability Raid RMS
        public static int BySARaidRMS(FlowerInfo fw1, FlowerInfo fw2)
        {
            if (fw1 == null || fw2 == null) return 0;

            //float RMS1 = fw1.GetSARaidRMS();
            //float RMS2 = fw2.GetSARaidRMS();

            //if (RMS1 == RMS2) return fw1.CompareTo(fw2);

            //if (RMS1 < RMS2) return 1; else return -1;

            return 0;
        }


        /*
        public FlowerInfo(XmlNode node) : this()
        {
            if (node == null) return;


            Name.Kanji = XmlHelper.GetText(node, NodeName.KanjiName);
            Name.Romaji = XmlHelper.GetText(node, NodeName.RomajiName);
            Name.EngDMM = XmlHelper.GetText(node, NodeName.EngDMMName);
            Name.EngNutaku = XmlHelper.GetText(node, NodeName.EngNutakuName);


            AttackType = XmlHelper.GetInt32(node, NodeName.AttackType);
            Nation = XmlHelper.GetInt32(node, NodeName.Nation);
            Rarity = XmlHelper.GetInt32(node, NodeName.Rarity);

            //HitPoints = XmlHelper.GetInt32(node, NodeName.HitPoints);
            //Attack = XmlHelper.GetInt32(node, NodeName.Attack);
            //Defence = XmlHelper.GetInt32(node, NodeName.Defence);
            //Speed = XmlHelper.GetInt32(node, NodeName.Speed);


            //Skill = new SkillInfo(node["Skill"]);

            XmlNodeList xnl = node.SelectNodes("Ability");

            //Ability1 = new AbilityInfo(xnl[0]);
            //Ability2 = new AbilityInfo(xnl[1]);


            ImageBase = XmlHelper.GetInt32(node, NodeName.ImageBase);
            ImageAwakened = XmlHelper.GetInt32(node, NodeName.ImageAwakened);
            ImageBloomed = XmlHelper.GetInt32(node, NodeName.ImageBloomed);


            if (XmlHelper.GetText(node, NodeName.Game01) == "True") Game01 = true;
            if (XmlHelper.GetText(node, NodeName.Game02) == "True") Game02 = true;
            if (XmlHelper.GetText(node, NodeName.Game03) == "True") Game03 = true;

            
            GetID(Node);

            Name.Kanji = GetInnerText(Node[XmlTags.KanjiName]);
            Name.Romaji = GetInnerText(Node[XmlTags.RomajiName]);
            Name.EngNutaku = GetInnerText(Node[XmlTags.EngNutakuName]);
            Name.EngDMM = GetInnerText(Node[XmlTags.EngDMMName]);
            AttackType = GetInnerText(Node[XmlTags.AttackType]);
            Nationality = GetInnerText(Node[XmlTags.Nation]);

            InStockDMM = GetInnerText(Node[XmlTags.InStockDMM]) == "True";
            InStockNutaku = GetInnerText(Node[XmlTags.InStockNutaku]) == "True";

            Rarity = GetInnerInt32(Node[XmlTags.Rarity], 2);

            HitPoints = GetInnerInt32(Node[XmlTags.StatHP]);
            Attack = GetInnerInt32(Node[XmlTags.StatAtc]);
            Defence = GetInnerInt32(Node[XmlTags.StatDef]);
            Speed = GetInnerInt32(Node[XmlTags.StatSpd]);

            Skill.SetName(GetInnerText(Node[XmlTags.SkillName]));
            Skill.SetHitCount(GetInnerInt32(Node[XmlTags.SkillHits]));
            Skill.SetTargets(GetInnerInt32(Node[XmlTags.SkillTargets]));
            Skill.SetDamage(GetInnerInt32(Node[XmlTags.SkillDamage]));
            Skill.SetChance(GetInnerInt32(Node[XmlTags.SkillChance]));
            Skill.SetAbsorbHP(GetInnerText(Node[XmlTags.SkillHPAbsorb]) == "True");

            Ability1.SetInfo(GetInnerText(Node[XmlTags.Ability1Info]));
            Ability1.SetBasePower(GetInnerInt32(Node[XmlTags.Ability1BPower]));
            Ability1.SetRaidPower(GetInnerInt32(Node[XmlTags.Ability1RPower]));
            Ability1.AddSpecials(GetInnerText(Node[XmlTags.Ability1Spec]), ';');
            
            Ability2.SetInfo(GetInnerText(Node[XmlTags.Ability2Info]));
            Ability2.SetBasePower(GetInnerInt32(Node[XmlTags.Ability2BPower]));
            Ability2.SetRaidPower(GetInnerInt32(Node[XmlTags.Ability2RPower]));
            Ability2.AddSpecials(GetInnerText(Node[XmlTags.Ability2Spec]), ';');
            
        }
        */
        /*
        public string CreateXMLNode()
        {
            string node = "\t<Flower ID=\"" + ID + "\">" + Environment.NewLine;

            node += GetXmlFullString(XmlTags.KanjiName, Name.Kanji, 2);
            node += GetXmlFullString(XmlTags.RomajiName, Name.Romaji, 2);
            node += GetXmlFullString(XmlTags.EngNutakuName, Name.EngNutaku, 2);
            node += GetXmlFullString(XmlTags.EngDMMName, Name.EngDMM, 2);
            node += GetXmlFullString(XmlTags.AttackType, AttackType, 2);
            node += GetXmlFullString(XmlTags.Nation, Nationality, 2);
            node += GetXmlFullString(XmlTags.Rarity, Rarity.ToString(), 2);

            node += GetXmlFullString(XmlTags.InStockDMM, InStockDMM.ToString(), 2);
            node += GetXmlFullString(XmlTags.InStockNutaku, InStockNutaku.ToString(), 2);

            node += GetXmlFullString(XmlTags.StatHP, HitPoints.ToString(), 2);
            node += GetXmlFullString(XmlTags.StatAtc, Attack.ToString(), 2);
            node += GetXmlFullString(XmlTags.StatDef, Defence.ToString(), 2);
            node += GetXmlFullString(XmlTags.StatSpd, Speed.ToString(), 2);

            node += GetXmlFullString(XmlTags.SkillName, Skill.GetName(), 2);
            node += GetXmlFullString(XmlTags.SkillTargets, Skill.GetTargets().ToString(), 2);
            node += GetXmlFullString(XmlTags.SkillHits, Skill.GetHitCount().ToString(), 2);
            node += GetXmlFullString(XmlTags.SkillDamage, Skill.GetDamage().ToString(), 2);
            node += GetXmlFullString(XmlTags.SkillChance, Skill.GetChance().ToString(), 2);
            node += GetXmlFullString(XmlTags.SkillHPAbsorb, Skill.IsHPAbsorb().ToString(), 2);

            node += GetXmlFullString(XmlTags.Ability1Info, Ability1.GetInfo(), 2);
            node += GetXmlFullString(XmlTags.Ability1BPower, Ability1.GetBasePower().ToString(), 2);
            node += GetXmlFullString(XmlTags.Ability1RPower, Ability1.GetRaidPower().ToString(), 2);
            node += GetXmlFullString(XmlTags.Ability1Spec, Ability1.GetSpecial(";"), 2);

            node += GetXmlFullString(XmlTags.Ability2Info, Ability2.GetInfo(), 2);
            node += GetXmlFullString(XmlTags.Ability2BPower, Ability2.GetBasePower().ToString(), 2);
            node += GetXmlFullString(XmlTags.Ability2RPower, Ability2.GetRaidPower().ToString(), 2);
            node += GetXmlFullString(XmlTags.Ability2Spec, Ability2.GetSpecial(";"), 2);

            node += "\t</Flower>" + Environment.NewLine;
            return node;
        }
        */

        /*
        public void WriteXmlNode(XmlTextWriter wr)
        {
            wr.WriteStartElement("Flower");
            //wr.WriteAttributeString("ID", ID.ToString());

            wr.WriteElementString(NodeName.KanjiName, Name.Kanji);
            wr.WriteElementString(NodeName.RomajiName, Name.Romaji);
            wr.WriteElementString(NodeName.EngDMMName, Name.EngDMM);
            wr.WriteElementString(NodeName.EngNutakuName, Name.EngNutaku);
            wr.WriteElementString(NodeName.AttackType, AttackType.ToString());
            wr.WriteElementString(NodeName.Nation, Nation.ToString());
            wr.WriteElementString(NodeName.Rarity, Rarity.ToString());

            //wr.WriteElementString(NodeName.HitPoints, HitPoints.ToString());
            //wr.WriteElementString(NodeName.Attack, Attack.ToString());
            //wr.WriteElementString(NodeName.Defence, Defence.ToString());
            //wr.WriteElementString(NodeName.Speed, Speed.ToString());

            //Skill.WriteXmlNode(wr);
            //Ability1.WriteXmlNode(wr);
            //Ability2.WriteXmlNode(wr);

            wr.WriteElementString(NodeName.ImageBase, ImageBase.ToString());
            wr.WriteElementString(NodeName.ImageAwakened, ImageAwakened.ToString());
            wr.WriteElementString(NodeName.ImageBloomed, ImageBloomed.ToString());

            wr.WriteElementString(NodeName.Game01, Game01.ToString());
            wr.WriteElementString(NodeName.Game02, Game02.ToString());
            wr.WriteElementString(NodeName.Game03, Game03.ToString());

            wr.WriteEndElement();
        }
        */

        /*
        public FlowerInfo CopyTo(FlowerInfo dupe)
        {
            dupe.Name = Name;
            //dupe.Skill = Skill;
            //dupe.Ability1 = Ability1;
            //dupe.Ability2 = Ability2;


            dupe.Rarity = Rarity;
            dupe.Nation = Nation;
            dupe.AttackType = AttackType;

            //dupe.HitPoints = HitPoints;
            //dupe.Attack = Attack;
            //dupe.Defence = Defence;
            //dupe.Speed = Speed;

            dupe.Game01 = Game01;
            dupe.Game02 = Game02;
            dupe.Game03 = Game03;

            dupe.ImageBase = ImageBase;
            dupe.ImageAwakened = ImageAwakened;
            dupe.ImageBloomed = ImageBloomed;

            return dupe;
        }
        */

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
