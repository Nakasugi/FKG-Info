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

        public int Skill;

        public int AbilityBase;
        public int AbilityAwak;

        public int AbilityBloom1st;
        public int AbilityBloom2nd;

        public int ImageBase, ImageAwakened, ImageBloomed;


        public bool Game01, Game02, Game03;

        private int Evol;

        FlowerStats Stats;



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
            "nation_06",
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
        public enum Evolution { Base, Awakened, Bloomed };



        public FlowerInfo()
        {
            Name = new ComplexName();
        }



        public FlowerInfo(string masterData) : this()
        {
            string[] sfields = masterData.Split(',');
            if (sfields.Length < 50) return;

            int parsedValue;
            if (!int.TryParse(sfields[4], out parsedValue)) return;
            if (parsedValue < 1) return;
            if (parsedValue > 999) return;

            ID = parsedValue;
            Name.Kanji = sfields[45].Replace("\"", "");
            Name.AutoRomaji();


            int.TryParse(sfields[35], out Evol);

            switch (Evol)
            {
                case 1:
                    int.TryParse(sfields[0], out ImageBase);
                    int.TryParse(sfields[10], out AbilityBase);
                    break;
                case 2:
                    int.TryParse(sfields[0], out ImageAwakened);
                    int.TryParse(sfields[11], out AbilityAwak);
                    break;
                case 3:
                    int.TryParse(sfields[0], out ImageBloomed);
                    int.TryParse(sfields[10], out AbilityBloom1st);
                    int.TryParse(sfields[11], out AbilityBloom2nd);
                    break;
                default: break;
            }

            int.TryParse(sfields[7], out parsedValue); Rarity = parsedValue;
            int.TryParse(sfields[3], out parsedValue); Nation = parsedValue;
            int.TryParse(sfields[8], out parsedValue); AttackType = parsedValue;
            int.TryParse(sfields[9], out parsedValue); FavoriteGift = parsedValue;



            int.TryParse(sfields[12], out Skill);

            Stats = new FlowerStats(sfields);

            if ((Nation < 1) || (Nation > 7)) ID = 0;
        }



        //======================================================
        public void FillGrid(System.Windows.Forms.DataGridView view)
        {
            view.Rows.Clear();
            view.Rows.Add("Kanji", Name.Kanji);
            view.Rows.Add("Romaji", Name.Romaji);
            view.Rows.Add("English Nutaku", Name.EngNutaku);
            view.Rows.Add("English DMM", Name.EngDMM);
            view.Rows.Add("Rarity", GetStars());
            view.Rows.Add("Type", GetAttackType());
            view.Rows.Add("Nation", GetNation());
            view.Rows.Add("Favorite gift", Gifts[FavoriteGift]);
            view.Rows.Add("Unique ID", ID);

            view.Rows.Add("HitPoints", Stats.GetHitPointsInfo());
            view.Rows.Add("Attack", Stats.GetAttackInfo());
            view.Rows.Add("Defense", Stats.GetDefenseInfo());
        }


        public string GetAbilitiesInfo(bool translation, bool noBloomed = false)
        {
            string info = "";

            int ab1 = AbilityBase;
            int ab2 = AbilityAwak;

            if (noBloomed == false)
            {
                if ((AbilityBloom1st != 0) && (AbilityBloom2nd != 0))
                {
                    ab1 = AbilityBloom1st;
                    ab2 = AbilityBloom2nd;
                }
            }

            AbilityInfo ability;

            ability = Program.DataBase.GetAbility(ab1);
            if (ability != null) info += ability.GetInfo(translation);

            info += Environment.NewLine + Environment.NewLine;

            ability = Program.DataBase.GetAbility(ab2);
            if (ability != null) info += ability.GetInfo(translation);

            return info;
        }



        public string GetSkillInfo(bool translation)
        {
            SkillInfo skill = Program.DataBase.GetSkill(Skill);

            if (skill != null) return skill.GetInfo(translation);

            return null;
        }



        public bool CheckAbilityType(int type)
        {
            AbilityInfo ability;

            ability = Program.DataBase.GetAbility(AbilityBase);
            if (ability != null) if (ability.CheckTypeID(type)) return true;

            ability = Program.DataBase.GetAbility(AbilityAwak);
            if (ability != null) if (ability.CheckTypeID(type)) return true;

            ability = Program.DataBase.GetAbility(AbilityBloom1st);
            if (ability != null) if (ability.CheckTypeID(type)) return true;

            ability = Program.DataBase.GetAbility(AbilityBloom2nd);
            if (ability != null) if (ability.CheckTypeID(type)) return true;

            return false;
        }


        public bool CheckAbilityShortName(string shortName)
        {
            AbilityInfo ability;

            ability = Program.DataBase.GetAbility(AbilityBase);
            if (ability != null) if (ability.CheckAbilityShortName(shortName)) return true;

            ability = Program.DataBase.GetAbility(AbilityAwak);
            if (ability != null) if (ability.CheckAbilityShortName(shortName)) return true;

            ability = Program.DataBase.GetAbility(AbilityBloom1st);
            if (ability != null) if (ability.CheckAbilityShortName(shortName)) return true;

            ability = Program.DataBase.GetAbility(AbilityBloom2nd);
            if (ability != null) if (ability.CheckAbilityShortName(shortName)) return true;

            return false;
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


        
        public string GetImageName(Evolution evol, ImageTypes type)
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



        public int GetImageEvolID(Evolution evol)
        {
            switch (evol)
            {
                case Evolution.Base: return ImageBase;
                case Evolution.Awakened: return ImageAwakened;
                case Evolution.Bloomed: return ImageBloomed;
                default: return 0;
            }
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
            if (Evol < flower.Evol) Stats = flower.Stats;

            switch (flower.Evol)
            {
                case 1:
                    ImageBase = flower.ImageBase;
                    AbilityBase = flower.AbilityBase;
                    break;
                case 2:
                    ImageAwakened = flower.ImageAwakened;
                    AbilityAwak = flower.AbilityAwak;
                    break;
                case 3:
                    ImageBloomed = flower.ImageBloomed;
                    AbilityBloom1st = flower.AbilityBloom1st;
                    AbilityBloom2nd = flower.AbilityBloom2nd;
                    break;
                default: break;
            }
        }
    }
}
