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
            Stream stInput;

            Ok = false;

            try
            {
                stInput = new FileStream(fname, FileMode.Open);
            }
            catch { return; }

            if ((stInput.Length < 2) || (stInput.Length > 10000000)) { stInput.Close(); return; }

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

            DeflateStream zip = new DeflateStream(stInput, CompressionMode.Decompress);
            stInput.Position = 2;
            Stream stJson = new MemoryStream();

            try
            {
                zip.CopyTo(stJson);
                zip.Close();
            }
            catch (Exception exp)
            {
                zip.Close();
                System.Windows.Forms.MessageBox.Show(exp.Message,"Unzip Error");
                return;
            }

            Ok = true;

            stJson.Position = 0;
            rd = new StreamReader(stJson);
            string json = rd.ReadToEnd();
            rd.Close();

            json = json.Substring(1, json.Length - 2);
            string[] jsonElements = json.Split(',');

            for (int i = 0; i < jsonElements.Length; i++) jsonElements[i] = jsonElements[i].Replace("\"", "");

            Data = new List<JsonStruct>();
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
