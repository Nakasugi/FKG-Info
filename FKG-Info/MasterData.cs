using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
//using Newtonsoft.Json;

namespace FKG_Info
{
    public class MasterData
    {
        public bool Ok { get; private set; }


        private class JsonStruct
        {
            public string Name;
            public string Data;
            public JsonStruct(string name, string data) { Name = name; Data = data; }
        }

        private List<JsonStruct> Data;



        public MasterData(string fname)
        {
            Ok = false;
            string json = LoadJson(fname); if (json == null) return;
            Ok = true;

            json = json.Substring(1, json.Length - 2);
            string[] jsonElements = json.Split(',');

            for (int i = 0; i < jsonElements.Length; i++) jsonElements[i] = jsonElements[i].Replace("\"", "");

            Data = new List<JsonStruct>();
            string s64;
            string[] el;

            foreach (string s in jsonElements)
            {
                el = s.Split(':');
                s64 = el[1];

                try
                {
                    s64 = s64.Replace("\\", "");
                    s64 = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(s64));
                }
                catch { }

                Data.Add(new JsonStruct(el[0], s64));
            }

        }



        public static void LoadNutakuNames(FlowersList flowers, string fname)
        {
            string json = LoadJson(fname); if (json == null) return;
            json = json.Substring(1, json.Length - 2);

            int pos = 0, sbcnt = 0, cut1, cut2;

            cut1 = json.IndexOf('[') + 1;

            while(true)
            {
                if (json[pos] == '[') sbcnt++;
                if (json[pos] == ']')
                {
                    sbcnt--;
                    if (sbcnt == 0) break;
                }
                pos++;
            }

            cut2 = pos - cut1;
            json = json.Substring(cut1 + 1, cut2 - 2);
            string[] jsonFlowers = json.Split(new string[] { "},{" }, StringSplitOptions.None);
            json = null;

            foreach(string fw_str in jsonFlowers)
            {
                string[] fw_elms = fw_str.Split(',');

                int.TryParse(fw_elms[0].Split(':')[1].Replace("\"", ""), out int id);
                string name = fw_elms[5];

                FlowerInfo flower = flowers.Find(fw => fw.ID == id);
                if (flower != null) flower.Name.EngNutaku = fw_elms[5].Split(':')[1].Replace("\"", "");
            }
        }
        


        private static string LoadJson(string fname)
        {
            Stream stInput;

            try
            {
                stInput = new FileStream(fname, FileMode.Open);
            }
            catch { return null; }

            if ((stInput.Length < 2) || (stInput.Length > 16000000)) { stInput.Close(); return null; }

            StreamReader rd = new StreamReader(stInput);
            string s64 = rd.ReadToEnd();

            s64 = s64.Replace("\\", "");
            byte[] bin;

            try
            {
                bin = Convert.FromBase64String(s64);
                rd.Close();
                stInput = new MemoryStream(bin);
            }
            catch { }

            stInput = Helper.DecompressStream(stInput, 2);
            stInput.Position = 0;
            rd = new StreamReader(stInput);
            string json = rd.ReadToEnd();
            rd.Close();

            return json;
        }

        

        public string GetData(string masterName)
        {
            return Data.Find(D => D.Name == masterName).Data;
        }



        public string[] GetNames()
        {
            string[] names = new string[Data.Count];

            for (int i = 0; i < Data.Count; i++) names[i] = Data[i].Name;

            return names;
        }



        public void Export(string masterName, string fileName)
        {
            File.WriteAllText(fileName, GetData(masterName));
        }



        public void ExportIDs()
        {
            string folder = Program.DB.DataFolder + "\\Export";
            if (!Helper.CheckFolder(folder)) return;

            string path = folder + "\\FlowerIDs.txt";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            string data = GetData("masterCharacter");
            StringReader sr = new StringReader(data);

            while (true)
            {
                string line = sr.ReadLine();
                if (line == null) break;
                sw.WriteLine(line.Split(',')[0]);
            }

            sr.Close();
            sw.Close();
        }



        public void ExportFlowerData(int refid)
        {
            string folder = Program.DB.DataFolder + "\\Export";
            if (!Helper.CheckFolder(folder)) return;

            string path = folder + "\\Flower" + refid.ToString("D4") + ".csv";
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            List<string[]> flowerData = new List<string[]>();

            string data = GetData("masterCharacter");

            StringReader sr = new StringReader(data);
            while (true)
            {
                string line = sr.ReadLine();
                if (line == null) break;

                string[] values = line.Split(',');
                if (values[37] != refid.ToString()) continue;

                flowerData.Add(values);
            }
            sr.Close();

            string[] fieldNames =
            {
                "ID",          "ID",            "Family",      "Nation",
                "unknown",     "ShortName",     "unknown",     "Rarity",
                "Type",        "FavGift",       "Ability1ID",  "Ability2ID",
                "SkillID",     "unknown",       "unknown",     "HPLvMin",
                "HPLvMax",     "AtkLvMin",      "AtkLvMax",    "DefLvMin",
                "DefLvMax",    "SpdLvMin",      "SpdLvMax",    "HpApm",
                "AtkAmp",      "DefAmp",        "HpApmEx",     "AtkAmpEx",
                "DefAmpEx",    "SellCost",      "SortCat",     "unknown",
                "isNotPreEvo", "IsKnight",      "HPAff1",      "AtkAff1",
                "DefAff1",     "RefID",         "Evolution",   "unknown",
                "HPAff2",      "AtkAff2",       "DefAff2",     "unknown",
                "unknown",     "IsBloomed",     "CanBloom",    "Name",
                "NoBloomCG",   "unknown",       "unknown",     "LibararyID",
                "unknown",     "IsEventKnight", "unknown",     "unknown",
                "unknown",     "unknown",       "GrownID",     "IsGrown",
                "CanGrow"
            };

            sw.WriteLine("CSV");
            if (flowerData.Count > 0)
            {
                for (int i = 0; i < flowerData[0].Length; i++)
                {
                    string line = i.ToString("D2") + ";";
                    if (i < fieldNames.Length) line += fieldNames[i];

                    foreach (string[] fdt in flowerData) line += ";" + fdt[i];

                    sw.WriteLine(line);
                }
            }

            sw.Close();

        }
    }
}
