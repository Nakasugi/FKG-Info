using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;



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



        public IconsAtlas FlowerIcons;



        public string DMMURL;
        public string NutakuURL;
        public string ImagesFolder;
        public string DataFolder;
        public string IconsFolder;
        public string EquipFolder;
        public string SoundFolder;

        public int SoundVolume;

        public string Account1Name, Account2Name;


        private string DefaultFolder;

        private int LastFlowerVersion;


        public enum ImageSources { Local, Nutaku, NutakuDMM, DMM, DMMNutaku }

        public ImageSources ImageSource;

        public bool StoreDownloaded;

        private bool NeedSaveOptions;



        public Translator TranslatorAbilities { get; private set; }
        public Translator TranslatorSkills { get; private set; }



        public System.Drawing.Image[] AbilityIcons { get; private set; }
        private const int AB_ICONS_COUNT = 64;



        struct NodeName
        {
            public const string DMMURL = "DMMURL";
            public const string NutakuURL = "NutakuURL";
            public const string DataFolder = "DataFolder";
            public const string ImagesFolder = "ImagesFolder";
            public const string SoundsFolder = "SoundsFolder";
            public const string ImageSource = "ImageSource";
            public const string StoreDownloaded = "StoreDownloaded";
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
            NutakuURL = "http://cdn.flowerknight.nutaku.net/bin/commons/";

            DefaultFolder = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            DataFolder = DefaultFolder + "\\" + FolderNames.Data;
            ImagesFolder = DefaultFolder + "\\" + FolderNames.Images;
            IconsFolder = ImagesFolder + "\\" + FolderNames.Icons;
            EquipFolder = ImagesFolder + "\\" + FolderNames.Equip;
            SoundFolder = DefaultFolder + "\\" + FolderNames.Sounds;

            SoundVolume = 25;

            Account1Name = "Account 1";
            Account2Name = "Account 2";

            ImageSource = ImageSources.DMM;
            StoreDownloaded = true;

            NeedSaveOptions = false;

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
                    /*
                    FlowerInfo flower = Flowers.Find(f => f.ID == newInfo.ID);
                    if (flower == null)
                    {
                        Flowers.Add((FlowerInfo)newInfo);
                    }
                    else
                    {
                        flower.Update((FlowerInfo)newInfo);
                    }*/
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



        //public FlowerInfo GetSelected() { if (IsSelected()) return Flowers[Selected]; return null; }
        //public void Select(int n) { Selected = n; }
        //public void Select(FlowerInfo flower) { Selected = Flowers.IndexOf(flower); }
        //public void Unselect() { Selected = -1; }
        //public bool IsSelected() { return Selected != -1; }



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
                atype = ab.GetTypeID(0);
                if (!atypes.Contains(atype))
                    if (Flowers.Find(fw => fw.CheckAbilityType(atype)) != null) atypes.Add(atype);

                atype = ab.GetTypeID(1);
                if (!atypes.Contains(atype))
                    if (Flowers.Find(fw => fw.CheckAbilityType(atype)) != null) atypes.Add(atype);
            }

            atypes.Remove(0);

            List<string> types = new List<string>();

            foreach (int tpid in atypes)
            {
                st = TranslatorAbilities.GetTag(tpid);
                if (!types.Contains(st)) types.Add(st);
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
            XmlNodeList accHasList = null;
            if (File.Exists(fileName))
            {
                xmlData.Load(fileName);

                string curver = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
                string optver;
                try { optver = xmlData.SelectSingleNode("//Version").InnerText; } catch { optver = null; }
                

                XmlNode opt = xmlData.SelectSingleNode("//Options");
                XmlNode has = xmlData.SelectSingleNode("//HasFlowers");
                if (has != null) accHasList = has.ChildNodes;

                if (optver == curver)
                {
                    Helper.XmlGetText(opt, NodeName.DMMURL, ref db.DMMURL);
                    Helper.XmlGetText(opt, NodeName.NutakuURL, ref db.NutakuURL);
                }

                Helper.XmlGetText(opt, NodeName.DataFolder, ref db.DataFolder);
                Helper.XmlGetText(opt, NodeName.ImagesFolder, ref db.ImagesFolder);
                Helper.XmlGetText(opt, NodeName.SoundsFolder, ref db.SoundFolder);
                Helper.XmlGetInt32(opt, NodeName.SoundVolume, ref db.SoundVolume);
                Helper.XmlGetText(opt, NodeName.Acc1Name, ref db.Account1Name);
                Helper.XmlGetText(opt, NodeName.Acc2Name, ref db.Account2Name);

                Helper.XmlGetInt32(opt, NodeName.VersionFlowers, ref db.LastFlowerVersion);

                db.StoreDownloaded = Helper.XmlCheckNode(opt, NodeName.StoreDownloaded, "True");

                string enumText = db.ImageSource.ToString();
                Helper.XmlGetText(opt, NodeName.ImageSource, ref enumText);
                db.ImageSource = (ImageSources)Enum.Parse(typeof(ImageSources), enumText);
                
                db.IconsFolder = db.ImagesFolder + "\\" + FolderNames.Icons;
                db.EquipFolder = db.ImagesFolder + "\\" + FolderNames.Equip;
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

            if (!Helper.CheckFolder(db.IconsFolder))
            {
                db.IconsFolder = db.ImagesFolder + "\\" + FolderNames.Icons;
                Helper.CheckFolder(db.IconsFolder);
            }

            if (!Helper.CheckFolder(db.EquipFolder))
            {
                db.EquipFolder = db.ImagesFolder + "\\" + FolderNames.Equip;
                Helper.CheckFolder(db.EquipFolder);
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
            }

            //db.Flowers.UpdateLinks();

            db.TranslatorAbilities = new Translator(db.DataFolder + "\\en_abilities.txt");
            db.TranslatorSkills = new Translator(db.DataFolder + "\\en_skills.txt");
            db.LoadCharaNamesTranslation();


            EquipmentInfo.SearchSets(db.Equipments);

            foreach (FlowerInfo fw in db.Flowers) fw.FindExclusiveSkin(db);

            if (accHasList != null)
            {
                foreach (XmlNode hasNode in accHasList)
                {
                    bool error = false;
                    error |= !int.TryParse(hasNode.Name.Substring(2), out int refId);
                    error |= !int.TryParse(hasNode.InnerText, out int accStatus);
                    if (error) continue;

                    db.Flowers.SetAccStatus(refId, accStatus);
                }
            }

            return db;
        }



        /// <summary>
        /// Get Nutaku or DMM urls
        /// </summary>
        /// <param name="type">1 = 1st, 2 = 2nd</param>
        /// <returns></returns>
        public string GetUrl(int type = 0, string relurl = null)
        {
            switch (ImageSource)
            {
                case ImageSources.Nutaku:
                    if (type == 1) return NutakuURL + relurl;
                    break;
                case ImageSources.NutakuDMM:
                    if (type == 1) return NutakuURL + relurl;
                    if (type == 2) return DMMURL + relurl;
                    break;
                case ImageSources.DMM:
                    if (type == 1) return DMMURL + relurl;
                    break;
                case ImageSources.DMMNutaku:
                    if (type == 1) return DMMURL + relurl;
                    if (type == 2) return NutakuURL + relurl;
                    break;
                default: break;
            }

            return null;
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
            xmlWriter.WriteElementString(NodeName.NutakuURL, NutakuURL);
            xmlWriter.WriteElementString(NodeName.DataFolder, DataFolder);
            xmlWriter.WriteElementString(NodeName.ImagesFolder, ImagesFolder);
            xmlWriter.WriteElementString(NodeName.SoundsFolder, SoundFolder);
            xmlWriter.WriteElementString(NodeName.ImageSource, ImageSource.ToString());
            xmlWriter.WriteElementString(NodeName.StoreDownloaded, StoreDownloaded.ToString());
            xmlWriter.WriteElementString(NodeName.SoundVolume, SoundVolume.ToString());
            xmlWriter.WriteElementString(NodeName.Acc1Name, Account1Name);
            xmlWriter.WriteElementString(NodeName.Acc2Name, Account2Name);
            xmlWriter.WriteElementString(NodeName.VersionFlowers, LastFlowerVersion.ToString());
            xmlWriter.WriteEndElement();

            Flowers.WriteXmlAccLinks(xmlWriter);

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

                foreach (FlowerInfo flower in flowers) flower.Name.EngDMM = mName[1];
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
            MasterData nutaku = new MasterData(DataFolder + "\\nutakuMaster.bin");
            if (!nutaku.Ok) return;

            string[] chFields;

            FlowerInfo flower;
            int chID;
            string chLine;
            string characters = nutaku.GetData("masterCharacter");
            StringReader rd = new StringReader(characters);

            while (true)
            {
                chLine = rd.ReadLine();
                if (chLine == null) break;

                chFields = chLine.Split(',');
                int.TryParse(chFields[0], out chID);

                flower = Flowers.Find(fw => fw.ID == chID);

                if (flower != null) flower.Name.EngNutaku = chFields[5].Replace("\"", "");
            }
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



        public void OptionsChanged() { NeedSaveOptions = true; }
        public void SaveOptIfNeeded() { if (NeedSaveOptions) SaveOptions(); }



        public void UpdateVersions()
        {
            if (Flowers.CheckVersions(ref LastFlowerVersion)) NeedSaveOptions = true;

            foreach (FlowerInfo flower in Flowers)
            {
                if (flower.Updated)
                {
                    flower.Updated = false;
                    Program.ImageLoader.DeleteImages(flower.ID);
                }
            }
        }
    }
}
