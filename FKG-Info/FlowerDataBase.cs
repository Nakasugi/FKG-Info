using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using FKG_Info.FKG_GameData;

namespace FKG_Info
{
    public class FlowerDataBase
    {
        public List<SkillInfo> Skills;
        public List<AbilityInfo> Abilities;
        public List<EquipmentInfo> Equipments;
        public List<SkinInfo> Skins;
        public GrowInfo GrownImageIDReplacer;

        public FlowersList Flowers { get; private set; }

        public MasterData Master;


        public string DMMURL;
        public string MobileURL;
        public string NutakuURL;
        public string ImagesFolder;
        public string DataFolder;
        public string SoundFolder;

        public int SoundVolume;

        public string Account1Name, Account2Name;


        private string DefaultFolder;

        public bool EnableDownloader;
        public bool SaveDownloaded;

        private bool NeedSave;



        public Translator TranslatorAbilities { get; private set; }
        public Translator TranslatorSkills { get; private set; }



        public System.Drawing.Image[] AbilityIcons { get; private set; }
        private const int AB_ICONS_COUNT = 64;



        struct NodeName
        {
            public const string DMMURL = "DMMURL";
            public const string DataFolder = "DataFolder";
            public const string ImagesFolder = "ImagesFolder";
            public const string SoundsFolder = "SoundsFolder";
            public const string EnableDownloader = "EnableDownloader";
            public const string SaveDownloaded = "SaveDownloaded";
            public const string SoundVolume = "SoundVolume";
            public const string Acc1Name = "Account1Name";
            public const string Acc2Name = "Account2Name";
            public const string VersionFlowers = "LastFlowerVersion";
        };



        struct FolderNames
        {
            public const string Data = "Data";
            public const string Images = "Images";
            public const string Icons = "Icons";
            public const string Equip = "Equipment";
            public const string Sounds = "Sounds";
        }



        public FlowerDataBase()
        {
            Flowers = new FlowersList();
            Skills = new List<SkillInfo>();
            Abilities = new List<AbilityInfo>();
            Equipments = new List<EquipmentInfo>();
            Skins = new List<SkinInfo>();
            GrownImageIDReplacer = new GrowInfo();


            DMMURL = "http://dugrqaqinbtcq.cloudfront.net/product/";
            MobileURL = "http://dugrqaqinbtcq.cloudfront.net/product/ynnFQcGDLfaUcGhp/assets/";
            NutakuURL = "http://cdn.flowerknight.nutaku.net/bin/commons/";


            /*
                http://dugrqaqinbtcq.cloudfront.net/product/images/character/
                http://dugrqaqinbtcq.cloudfront.net/product/ynnFQcGDLfaUcGhp/assets/medium/images/character/general/

                http://dugrqaqinbtcq.cloudfront.net/product/ynnFQcGDLfaUcGhp/assets/medium/images/character/general/i/md5(icon_l_<fkgID>).bin
                http://dugrqaqinbtcq.cloudfront.net/product/ynnFQcGDLfaUcGhp/assets/medium/images/character/general/s/md5(bustup_<fkgID>).bin
                http://dugrqaqinbtcq.cloudfront.net/product/ynnFQcGDLfaUcGhp/assets/medium/images/character/general/stand/md5(stand_<fkgID>).bin
                http://dugrqaqinbtcq.cloudfront.net/product/ynnFQcGDLfaUcGhp/assets/medium/images/character/general/stand_m/md5(stand_m_<fkgID>).bin
            */

            DefaultFolder = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            DataFolder = DefaultFolder + "\\" + FolderNames.Data;
            ImagesFolder = DefaultFolder + "\\" + FolderNames.Images;
            SoundFolder = DefaultFolder + "\\" + FolderNames.Sounds;

            SoundVolume = 25;

            Account1Name = "Account 1";
            Account2Name = "Account 2";

            EnableDownloader = true;
            SaveDownloaded = true;

            NeedSave = false;

            AbilityIcons = new System.Drawing.Image[AB_ICONS_COUNT];
            int x = 0, y = 0;
            for (int i = 0; i < AB_ICONS_COUNT; i++)
            {
                var rc = new System.Drawing.Rectangle(x, y, 40, 40);
                AbilityIcons[i] = Properties.Resources.ability_icons.Clone(rc, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                x += 40;
                if (x == 640) { x -= 640; y += 40; }
            }

            Flowers.Unselect();
        }



        private void Add(BaseInfo newInfo)
        {
            if (newInfo.ID == 0) return;

            switch (newInfo.BaseType)
            {
                case BaseInfo.ObjectType.Flower:
                    Flowers.Add((FlowerInfo)newInfo);
                    break;
                case BaseInfo.ObjectType.Skill:
                    SkillInfo skill = Skills.Find(sk => sk.ID == newInfo.ID);
                    if (skill == null) Skills.Add((SkillInfo)newInfo);
                    break;
                case BaseInfo.ObjectType.Ability:
                    AbilityInfo ability = Abilities.Find(ab => ab.ID == newInfo.ID);
                    if (ability == null) Abilities.Add((AbilityInfo)newInfo);
                    break;
                case BaseInfo.ObjectType.Equipment:
                    EquipmentInfo exist = Equipments.Find(eq => eq.ID == newInfo.ID);
                    if (exist == null) Equipments.Add((EquipmentInfo)newInfo);
                    break;
                case BaseInfo.ObjectType.Skin:
                    Skins.Add((SkinInfo)newInfo);
                    break;
                default: break;
            }

        }



        public EquipmentInfo[] GetFlowerEquipment(int flowerID)
        {
            return Equipments.FindAll(eq => eq.ChekFlowerID(flowerID)).ToArray();
        }



        public SkillInfo GetSkill(int id = -1)
        {
            //if (id == -1) id = Selected;
            if (id == -1) return null;

            return Skills.Find(s => s.ID == id);
        }



        public AbilityInfo GetAbility(int id = -1)
        {
            //if (id == -1) id = Selected;
            if (id == -1) return null;

            return Abilities.Find(s => s.ID == id);
        }



        public string[] GetAbilitiesTags()
        {

            List<int> atypes = new List<int>();

            int atype;
            string st;

            foreach (AbilityInfo ab in Abilities)
            {
                atype = ab.GetTypeID(0); atypes.Add(atype);
                atype = ab.GetTypeID(1); atypes.Add(atype);
            }

            
            atypes = atypes.Distinct().ToList();
            atypes.Remove(0);


            List<string> types = new List<string>();

            foreach (int tpid in atypes)
            {
                if (Flowers.Find(fw => fw.CheckAbilityType(tpid)) != null)
                {
                    st = TranslatorAbilities.GetTag(tpid);
                    if (!types.Contains(st)) types.Add(st);
                }
            }

            types.Sort();

            return types.ToArray();
        }



        /// <summary>
        /// Loading DataBase and Options
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static public FlowerDataBase Load(string fileName = "options.xml")
        {
            FlowerDataBase db = new FlowerDataBase();

            XmlDocument xmlData = new XmlDocument();


            if (File.Exists(fileName))
            {
                try { xmlData.Load(fileName); }
                catch { xmlData = null; }
            }


            if (xmlData != null)
            {
                string curver = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
                string optver;
                try { optver = xmlData.SelectSingleNode("//Version").InnerText; } catch { optver = null; }
                

                XmlNode opt = xmlData.SelectSingleNode("//Options");

                if (optver == curver)
                {
                    Helper.XmlGetText(opt, NodeName.DMMURL, ref db.DMMURL);
                }

                Helper.XmlGetText(opt, NodeName.DataFolder, ref db.DataFolder);
                Helper.XmlGetText(opt, NodeName.ImagesFolder, ref db.ImagesFolder);
                Helper.XmlGetText(opt, NodeName.SoundsFolder, ref db.SoundFolder);
                Helper.XmlGetInt32(opt, NodeName.SoundVolume, ref db.SoundVolume);
                Helper.XmlGetText(opt, NodeName.Acc1Name, ref db.Account1Name);
                Helper.XmlGetText(opt, NodeName.Acc2Name, ref db.Account2Name);

                Helper.XmlCheckNode(opt, NodeName.EnableDownloader, ref db.EnableDownloader);
                Helper.XmlCheckNode(opt, NodeName.SaveDownloaded, ref db.SaveDownloaded);

                db.Flowers.LoadSaving(xmlData);
            }


            if (!Helper.CheckFolder(db.DataFolder))
            {
                db.DataFolder = db.DefaultFolder + "\\" + FolderNames.Data;
                Helper.CheckFolder(db.DataFolder);
            }

            if (!Helper.CheckFolder(db.ImagesFolder))
            {
                db.ImagesFolder = db.DefaultFolder + "\\" + FolderNames.Images;
                Helper.CheckFolder(db.ImagesFolder);
            }


            db.Master = new MasterData(db.DataFolder + "\\dmmMaster.bin");
            if (db.Master.Ok)
            {
                db.LoadMasterData(db.Master.GetData("masterCharacter"), BaseInfo.ObjectType.Flower);
                db.LoadMasterData(db.Master.GetData("masterCharacterSkill"), BaseInfo.ObjectType.Skill);
                db.LoadMasterData(db.Master.GetData("masterCharacterLeaderSkill"), BaseInfo.ObjectType.Ability);
                db.LoadMasterData(db.Master.GetData("masterCharacterEquipment"), BaseInfo.ObjectType.Equipment);
                db.LoadMasterData(db.Master.GetData("masterCharacterSkin"), BaseInfo.ObjectType.Skin);

                db.GrownImageIDReplacer.ReadMasterData(db.Master.GetData("masterCharacterRarityEvolution"));

                db.LoadNutakuNames();

                db.TranslatorAbilities = new Translator(db.DataFolder + "\\en_abilities.txt");
                db.TranslatorSkills = new Translator(db.DataFolder + "\\en_skills.txt");
                db.LoadCharaNamesTranslation();
            }


            EquipmentInfo.SearchSets(db.Equipments);

            //db.Skins = db.Skins.FindAll(s => s.CheckActual());
            db.Flowers.UpdateSkins(db.Skins, db.GrownImageIDReplacer);

            return db;
        }



        public enum UrlType { CharaPC, CharaMobile, Equipment, Sound };

        /// <summary>
        /// Get absolute url
        /// </summary>
        public string GetUrl(string relurl, UrlType type)
        {
            if ((relurl == null) || (relurl == "")) return null;

            switch(type)
            {
                case UrlType.CharaPC: return DMMURL + "images/character/" + relurl;
                case UrlType.CharaMobile: return MobileURL + "medium/images/character/general/" + relurl;
                case UrlType.Equipment: return DMMURL + "images/item/100x100/" + relurl;
                case UrlType.Sound: return DMMURL + relurl;
                default: return null;
            }
        }



        public void SaveOptions(string fileName = "options.xml")
        {
            string bakName = "options_bak.xml";

            if (File.Exists(bakName)) File.Delete(bakName);
            if (File.Exists(fileName)) File.Move(fileName, bakName);

            XmlTextWriter xmlWriter = new XmlTextWriter(fileName, null);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.IndentChar = '\t';
            xmlWriter.Indentation = 1;

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("FlowerDB");

            Version version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            xmlWriter.WriteElementString("Version", version.ToString());

            xmlWriter.WriteStartElement("Options");
            xmlWriter.WriteElementString(NodeName.DMMURL, DMMURL);
            xmlWriter.WriteElementString(NodeName.DataFolder, DataFolder);
            xmlWriter.WriteElementString(NodeName.ImagesFolder, ImagesFolder);
            xmlWriter.WriteElementString(NodeName.SoundsFolder, SoundFolder);
            xmlWriter.WriteElementString(NodeName.EnableDownloader, EnableDownloader.ToString());
            xmlWriter.WriteElementString(NodeName.SaveDownloaded, SaveDownloaded.ToString());
            xmlWriter.WriteElementString(NodeName.SoundVolume, SoundVolume.ToString());
            xmlWriter.WriteElementString(NodeName.Acc1Name, Account1Name);
            xmlWriter.WriteElementString(NodeName.Acc2Name, Account2Name);
            xmlWriter.WriteEndElement();

            Flowers.SaveSaving(xmlWriter);

            xmlWriter.WriteEndElement();
            xmlWriter.Close();
        }



        /// <summary>
        /// Loading character names translation
        /// </summary>
        /// <param name="fname"></param>
        private void LoadCharaNamesTranslation(string fname = "en_names.txt")
        {
            FileStream fs;
            StreamReader rd;
            string st;

            string[] mName;

            List<FlowerInfo> flowers = new List<FlowerInfo>();

            try
            {
                fs = new FileStream(DataFolder + "\\" + fname, FileMode.Open);
                rd = new StreamReader(fs);
            }
            catch { return; }


            while (!rd.EndOfStream)
            {
                st = rd.ReadLine();
                mName = st.Split(';');

                flowers = Flowers.FindAll(fw => fw.ShortName == mName[0]);

                foreach (FlowerInfo flower in flowers)
                {
                    flower.Name.EngDMM = mName[1];
                    //flower.Name.EngNutaku = "";
                    //if (mName.Length > 2) flower.Name.EngNutaku = mName[2];
                }
            }

            rd.Close();
        }



        /// <summary>
        /// Loading Master Data
        /// </summary>
        /// <param name="masterData"></param>
        /// <param name="baseType"></param>
        private void LoadMasterData(string masterData, BaseInfo.ObjectType baseType)
        {
            string masterLine;
            string[] masterFields;

            StringReader rd = new StringReader(masterData);
            while (true)
            {
                masterLine = rd.ReadLine();
                if (masterLine == null) break;

                masterFields = masterLine.Split(',');

                switch (baseType)
                {
                    case BaseInfo.ObjectType.Flower:
                        Add(new FlowerInfo(masterFields));
                        break;
                    case BaseInfo.ObjectType.Skill:
                        Add(new SkillInfo(masterFields));
                        break;
                    case BaseInfo.ObjectType.Ability:
                        Add(new AbilityInfo(masterFields));
                        break;
                    case BaseInfo.ObjectType.Equipment:
                        Add(new EquipmentInfo(masterFields));
                        break;
                    case BaseInfo.ObjectType.Skin:
                        Add(new SkinInfo(masterFields));
                        break;
                    default: break;
                }
            }
            rd.Close();
        }



        /// <summary>
        /// Loading names from nutaku Master data
        /// </summary>
        private void LoadNutakuNames()
        {
            MasterData.LoadNutakuNames(Flowers, DataFolder + "\\nutakuMaster.bin");
        }



        public void ExportNames()
        {
            string folder = Program.DB.DataFolder + "\\Export";
            if (!Directory.Exists(folder)) try { Directory.CreateDirectory(folder); } catch { return; }

            string path = folder + "\\FlowerNames.txt";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            int maxID = 0;
            foreach (FlowerInfo fw in Flowers) if (maxID < fw.ID) maxID = fw.ID;
            maxID++;

            List<int> toExpot = new List<int>();

            for (int id = 0; id < maxID; id++)
            {
                FlowerInfo fw = Flowers.Find(f => f.ID == id);
                if (fw == null) continue;
                if (!fw.IsKnight) continue;

                bool repeat = false;

                foreach(int i in toExpot)
                {
                    FlowerInfo rfw = Flowers.Find(f => f.ID == i);
                    if (rfw == null) continue;
                    if (rfw.ShortName == fw.ShortName) { repeat = true; break; }
                }

                if (repeat) continue;

                toExpot.Add(id);
            }

            foreach(int id in toExpot)
            {
                FlowerInfo fw = Flowers.Find(f => f.ID == id);
                sw.WriteLine(fw.ShortName + ";" + fw.Name.EngDMM);
            }

            sw.Close();
        }



        public void OptionsChanged() { NeedSave = true; }
        public void SaveOptIfNeeded() { if (NeedSave || Flowers.NeedSave) SaveOptions(); }

        public void DeleteOldBloomCGs() { Flowers.DeleteOldBloomCGs(); }
    }
}
