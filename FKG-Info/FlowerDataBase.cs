using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace FKG_Info
{
    public class FlowerDataBase
    {
        public List<FlowerInfo> Flowers;
        public List<SkillInfo> Skills;
        public List<AbilityInfo> Abilities;

        public MasterData Master;


        int Selected;


        public string DMMURL;
        public string NutakuURL;
        public string ImagesFolder;
        public string DataFolder;

        private string DefaultFolder;


        public string Game01Name;
        public string Game02Name;
        public string Game03Name;



        public enum ImageSources { Local, Nutaku, NutakuDMM, DMM, DMMNutaku }

        public ImageSources ImageSource;

        public bool StoreDownloaded;


        public int _masterCharaFields;
        public int _masterCharaLines;
        public int _masterSkillFields;
        public int _masterSkillLines;
        public int _masterAbililyFields;
        public int _masterAbilityLines;



        class TranslationInfo
        {
            public int ID;
            public string Text;
            public string Short;
        }

        List<TranslationInfo> TranslationAbilities;
        List<TranslationInfo> TranslationSkills;


        struct NodeName
        {
            public const string DMMURL = "DMMURL";
            public const string NutakuURL = "NutakuURL";
            public const string ImagesFolder = "ImagesFolder";
            public const string DataFolder = "DataFolder";
            public const string ImageSource = "ImageSource";
            public const string StoreDownloaded = "StoreDownloaded";
            public const string Game01Name = "Game01Name";
            public const string Game02Name = "Game02Name";
            public const string Game03Name = "Game03Name";
        };



        public FlowerDataBase()
        {
            Flowers = new List<FlowerInfo>();
            Skills = new List<SkillInfo>();
            Abilities = new List<AbilityInfo>();

            TranslationAbilities = new List<TranslationInfo>();

            DMMURL = "http://dugrqaqinbtcq.cloudfront.net/product/images/character/";
            NutakuURL = "http://cdn.flowerknight.nutaku.net/bin/commons/images/character/";

            DefaultFolder = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            ImagesFolder = DefaultFolder + "\\Images";
            DataFolder = DefaultFolder + "\\Data";

            ImageSource = ImageSources.Local;
            StoreDownloaded = false;


            _masterCharaLines = _masterCharaFields = 0;
            _masterSkillLines = _masterSkillFields = 0;
            _masterAbilityLines = _masterAbililyFields = 0;

            Unselect();
        }



        private void Add(FlowerInfo newFlower)
        {
            if (newFlower.ID == 0) return;

            FlowerInfo exist = Flowers.Find(f => f.ID == newFlower.ID);

            if (exist == null)
            {
                Flowers.Add(newFlower);
            }
            else
            {
                exist.Update(newFlower);
            }
        }



        private void Add(SkillInfo newSkill)
        {
            if (newSkill.ID == 0) return;

            SkillInfo exist = Skills.Find(f => f.ID == newSkill.ID);

            if (exist == null)
            {
                Skills.Add(newSkill);
            }
        }



        private void Add(AbilityInfo newAbility)
        {
            if (newAbility.ID == 0) return;

            AbilityInfo exist = Abilities.Find(f => f.ID == newAbility.ID);

            if (exist == null)
            {
                Abilities.Add(newAbility);
            }
        }

        
        
        public FlowerInfo GetSelected() { if (IsSelected()) return Flowers[Selected]; return null; }
        public void Select(int n) { Selected = n; }
        public void Select(FlowerInfo flower) { Selected = Flowers.IndexOf(flower); }
        public void Unselect() { Selected = -1; }
        public bool IsSelected() { return Selected != -1; }



        public SkillInfo GetSkill(int id = -1)
        {
            if (id == -1) id = Selected;
            if (id == -1) return null;

            return Skills.Find(s => s.ID == id);
        }



        public AbilityInfo GetAbility(int id = -1)
        {
            if (id == -1) id = Selected;
            if (id == -1) return null;

            return Abilities.Find(s => s.ID == id);
        }



        public string GetAbilityTranslation(int abilityType)
        {
            TranslationInfo tinfo = TranslationAbilities.Find(ta => ta.ID == abilityType);
            if (tinfo == null) return null;
            return tinfo.Text;
        }



        public string GetAbilityShortName(int abilityType)
        {
            TranslationInfo tinfo = TranslationAbilities.Find(ta => ta.ID == abilityType);
            if (tinfo == null) return abilityType.ToString("D4");
            return tinfo.Short;
        }



        public string GetSkillTranslation(int skillType)
        {
            TranslationInfo tinfo = TranslationSkills.Find(ts => ts.ID == skillType);
            if (tinfo == null) return null;
            return tinfo.Text;
        }



        public string[] GetAbilitiesShortNames()
        {
            List<int> atypes = new List<int>();

            int atype;
            string st;

            foreach (AbilityInfo ai in Abilities)
            {
                atype = ai.GetTypeID(0);
                if (!atypes.Contains(atype))
                    if (Flowers.Find(fw => fw.CheckAbilityType(atype)) != null) atypes.Add(atype);
                
                atype = ai.GetTypeID(1);
                if (!atypes.Contains(atype))
                    if (Flowers.Find(fw => fw.CheckAbilityType(atype)) != null) atypes.Add(atype);
            }

            atypes.Remove(0);

            List<string> types = new List<string>();

            TranslationInfo abtr;

            foreach (int tpid in atypes)
            {
                abtr = TranslationAbilities.Find(ta => ta.ID == tpid);

                if ((abtr == null) || (abtr.Short == ""))
                {
                    st = tpid.ToString("D4");
                }
                else
                {
                    st = abtr.Short;
                }

                if (!types.Contains(st)) types.Add(st);
            }

            types.Sort();

            return types.ToArray();
        }


        
        static public FlowerDataBase Load(string fileName = "info.xml")
        {
            FlowerDataBase db = new FlowerDataBase();

            XmlDocument xmlData = new XmlDocument();
            if (File.Exists(fileName))
            {
                xmlData.Load(fileName);


                XmlNode opt = xmlData.SelectSingleNode("//Options");

                db.DMMURL = XmlHelper.GetText(opt, NodeName.DMMURL);
                db.NutakuURL = XmlHelper.GetText(opt, NodeName.NutakuURL);
                db.ImagesFolder = XmlHelper.GetText(opt, NodeName.ImagesFolder);
                db.DataFolder = XmlHelper.GetText(opt, NodeName.DataFolder);

                db.Game01Name = XmlHelper.GetText(opt, NodeName.Game01Name);
                db.Game02Name = XmlHelper.GetText(opt, NodeName.Game02Name);
                db.Game03Name = XmlHelper.GetText(opt, NodeName.Game03Name);

                if (XmlHelper.GetText(opt, NodeName.StoreDownloaded) == "True") db.StoreDownloaded = true;

                try
                {
                    db.ImageSource = (ImageSources)Enum.Parse(typeof(ImageSources), XmlHelper.GetText(opt, NodeName.ImageSource));
                }
                catch
                {
                    db.ImageSource = ImageSources.Local;
                }

                /*
                XmlNodeList nodeList = xmlData.DocumentElement.SelectNodes("//Flower");
                foreach (XmlNode node in nodeList)
                {
                    db.FlowerList.Add(new FlowerInfo(node));
                }
                */


            }


            if (!Directory.Exists(db.DataFolder))
            {
                db.DataFolder = db.DefaultFolder + "\\Data";

                if (!Directory.Exists(db.DataFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(db.DataFolder);
                    }
                    catch (Exception exp)
                    {
                        System.Windows.Forms.MessageBox.Show(exp.Message, "Directory Error");
                    }
                }
            }

            if (!Directory.Exists(db.ImagesFolder))
            {
                db.ImagesFolder = db.DefaultFolder + "\\Images";

                if (!Directory.Exists(db.ImagesFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(db.ImagesFolder);
                    }
                    catch (Exception exp)
                    {
                        System.Windows.Forms.MessageBox.Show(exp.Message, "Directory Error");
                    }
                }
            }

            //db.LoadCharacters();
            //db.LoadSkills();
            //db.LoadAbilities();

            db.Master = new MasterData(db.DataFolder + "\\dmmMaster.bin");
            if (db.Master.Ok)
            {
                db.LoadCharacters(db.Master.GetMasterData("masterCharacter"));
                db.LoadSkills(db.Master.GetMasterData("masterCharacterSkill"));
                db.LoadAbilities(db.Master.GetMasterData("masterCharacterLeaderSkill"));

                db.LoadNutakuNames();
            }
           

            
            db.TranslationAbilities = LoadTranslation(db.DataFolder + "\\en_abilities.txt");
            db.TranslationSkills = LoadTranslation(db.DataFolder + "\\en_skills.txt");
            db.LoadCharaNamesTranslation();

            return db;
        }


        public void Save(string fileName = "info.xml")
        {
            string bakName = "info_bak.xml";

            if (File.Exists(bakName)) File.Delete(bakName);
            if (File.Exists(fileName)) File.Move(fileName, bakName);

            XmlTextWriter xmlWriter = new XmlTextWriter(fileName, null);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.IndentChar = '\t';
            xmlWriter.Indentation = 1;

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("FlowerDB");

            xmlWriter.WriteStartElement("Options");
            xmlWriter.WriteElementString(NodeName.DMMURL, DMMURL);
            xmlWriter.WriteElementString(NodeName.NutakuURL, NutakuURL);
            xmlWriter.WriteElementString(NodeName.ImagesFolder, ImagesFolder);
            xmlWriter.WriteElementString(NodeName.DataFolder, DataFolder);
            xmlWriter.WriteElementString(NodeName.ImageSource, ImageSource.ToString());
            xmlWriter.WriteElementString(NodeName.StoreDownloaded, StoreDownloaded.ToString());
            xmlWriter.WriteElementString(NodeName.Game01Name, Game01Name);
            xmlWriter.WriteElementString(NodeName.Game02Name, Game02Name);
            xmlWriter.WriteElementString(NodeName.Game03Name, Game03Name);
            xmlWriter.WriteEndElement();

            /*
            xmlWriter.WriteStartElement("FlowerKnights");
            foreach (FlowerInfo flower in FlowerList) flower.WriteXmlNode(xmlWriter);
            xmlWriter.WriteEndElement();
            */

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

            FlowerInfo flower;

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

                flower = Flowers.Find(fw => fw.Name.Kanji == mName[0]);

                if (flower != null) flower.Name.EngDMM = mName[1];
            }

            rd.Close();
        }



        /// <summary>
        /// Loading translation
        /// </summary>
        /// <param name="fpath">path to translation file</param>
        private static List<TranslationInfo> LoadTranslation(string fpath)
        {
            FileStream fs;
            StreamReader rd;
            string st;

            string[] sst;

            List<TranslationInfo> tinfo = new List<TranslationInfo>();

            try
            {
                fs = new FileStream(fpath, FileMode.Open);
            }
            catch { return tinfo; }

            rd = new StreamReader(fs);

            if (rd != null)
            {
                while (!rd.EndOfStream)
                {
                    st = rd.ReadLine();
                    sst = st.Split(';');

                    if (sst.Length < 2) continue;

                    TranslationInfo ti = new TranslationInfo();

                    if (int.TryParse(sst[0], out ti.ID))
                    {
                        ti.Text = sst[1];

                        if (sst.Length >= 3)
                        {
                            ti.Short = sst[2];
                        }
                        else
                        {
                            ti.Short = "";
                        }

                        tinfo.Add(ti);
                    }

                }

                rd.Close();
            }

            return tinfo;
        }



        /// <summary>
        /// Loading Characters from Master data
        /// </summary>
        /// <param name="masterData"></param>
        private void LoadCharacters(string masterData)
        {
            string masterLine;

            try
            {
                StringReader rd = new StringReader(masterData);
                while (true)
                {
                    masterLine = rd.ReadLine();
                    if (masterLine == null) break;
                    Add(new FlowerInfo(masterLine.Split(',')));

                    if (_masterCharaFields < masterLine.Length) _masterCharaFields = masterLine.Length;
                    _masterCharaLines++;
                }
                rd.Close();
            }
            catch { }
        }



        /// <summary>
        /// Loading Skills from Master data
        /// </summary>
        /// <param name="masterData"></param>
        private void LoadSkills(string masterData)
        {
            string masterLine;

            try
            {
                StringReader rd = new StringReader(masterData);
                while (true)
                {
                    masterLine = rd.ReadLine();
                    if (masterLine == null) break;
                    Add(new SkillInfo(masterLine.Split(',')));

                    if (_masterSkillFields < masterLine.Length) _masterSkillFields = masterLine.Length;
                    _masterSkillLines++;
                }
                rd.Close();
            }
            catch { }
        }



        /// <summary>
        /// Loading Abilities from Master data
        /// </summary>
        /// <param name="masterData"></param>
        private void LoadAbilities(string masterData)
        {
            string masterLine;

            try
            {
                StringReader rd = new StringReader(masterData);
                while (true)
                {
                    masterLine = rd.ReadLine();
                    if (masterLine == null) break;
                    Add(new AbilityInfo(masterLine.Split(',')));

                    if (_masterAbililyFields < masterLine.Length) _masterAbililyFields = masterLine.Length;
                    _masterAbilityLines++;
                }
                rd.Close();
            }
            catch { }
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
            string characters = nutaku.GetMasterData("masterCharacter");
            StringReader rd = new StringReader(characters);

            while (true)
            {
                chLine = rd.ReadLine();
                if (chLine == null) break;

                chFields = chLine.Split(',');
                int.TryParse(chFields[4], out chID);

                flower = Flowers.Find(f => f.ID == chID);

                if (flower != null) flower.Name.EngNutaku = chFields[5].Replace("\"", "");
            }
        }
    }
}
