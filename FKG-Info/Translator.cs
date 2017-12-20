using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FKG_Info
{
    /// <summary>
    /// Translation Skill and Abilities description
    /// 
    /// [section1]
    /// id0, text, tag
    /// id1, text, tag
    /// ...
    /// [section2]
    /// id0 ..
    /// ...
    /// </summary>
    public class Translator
    {
        private class Section
        {
            public string Name;

            private List<TranslationUnit> Units;

            public Section() { Units = new List<TranslationUnit>(); }
            public void Add(TranslationUnit unit) { Units.Add(unit); }
            public TranslationUnit Find(int id) { return Units.Find(u => u.ID == id); }
        }



        private class TranslationUnit
        {
            public int ID;

            public string Translation;
            public string Tag;
            public string Icon;

            public TranslationUnit() { ID = 0; Translation = null; Tag = null; Icon = "0"; }
        }



        private List<Section> Sections;

        //private int[] CurrentData;





        public Translator(string fname)
        {
            FileStream fs = null;
            StreamReader rd = null;

            Regex pattern = null;
            MatchCollection matches = null;

            Section currentSection = null;

            string sline = null;

            try
            {
                fs = new FileStream(fname, FileMode.Open);

            }
            catch { return; }

            rd = new StreamReader(fs); if (rd == null) return;


            Sections = new List<Section>();


            while (!rd.EndOfStream)
            {
                sline = rd.ReadLine();

                // Check Section
                pattern = new Regex(@"(?<=\[)\w+(?=\])");
                matches = pattern.Matches(sline);

                if (matches.Count != 0)
                {
                    currentSection = new Section();
                    currentSection.Name = matches[0].Value;
                    Sections.Add(currentSection);
                    continue;
                }


                if (currentSection == null) continue;

                pattern = new Regex(@"[0-9a-zA-Z:,' /\{\}\(\)\%\.~-]+");
                matches = pattern.Matches(sline);

                if (matches.Count > 1)
                {
                    var tu = new TranslationUnit();

                    try
                    {
                        tu.ID = int.Parse(matches[0].Value);
                    }
                    catch { continue; }

                    tu.Translation = matches[1].Value;

                    if (matches.Count > 2) tu.Tag = matches[2].Value;
                    if (matches.Count > 3) tu.Icon = matches[3].Value.Replace("{", "").Replace("}", "");

                    currentSection.Add(tu);
                }
            }

            fs.Close();
        }



        /// <summary>
        /// Translation for int array [p0,p1,p2,..]
        /// p0 = is for main section
        /// pN = ids from {pID}
        /// </summary>
        /// <param name="data">0=id, 1=p0, 2=p1...</param>
        /// <returns></returns>
        public string GetTranslation(int[] data)
        {
            if (data.Length < 4) return null;

            switch (data[0])
            {
                case 0: return null;
                case 1: if (data[1] == 0) return null; break;
                default: break;
            }


            TranslationUnit tu = GetTranslationUnit("Main", data[0]);
            if (tu == null) return null;


            string tr = tu.Translation;

            var pattern = new Regex(@"(?<=\{)[:\d\w]+(?=\})");
            var matches = pattern.Matches(tr);

            foreach (Match m in matches)
                tr = tr.Replace("{" + m.Value + "}", GetParamValue(m.Value, data));

            return tr;
        }



        public string GetTag(int id)
        {
            string res = id.ToString("D04");

            TranslationUnit tu = GetTranslationUnit("Main", id);
            if ((tu == null) || ((tu.Tag == null))) return res;

            return tu.Tag;
        }



        public int GetIconId(int[] data)
        {
            TranslationUnit tu = GetTranslationUnit("Main", data[0]);
            if (tu == null) return 0;

            string sValue = GetParamValue(tu.Icon, data);
            int.TryParse(sValue, out int iValue);

            return iValue;
        }



        private string GetSectionString(string sectionName, int id)
        {
            string res = sectionName + ":" + id.ToString();

            TranslationUnit tu = GetTranslationUnit(sectionName, id);
            if (tu == null) return res;

            return tu.Translation;
        }



        private TranslationUnit GetTranslationUnit(string sectionName, int id)
        {
            Section sc = Sections.Find(s => s.Name == sectionName);
            if (sc == null) return null;

            return sc.Find(id);
        }



        /// <summary>
        /// Recursive calculator
        /// :op1:val1:op2:val2:..:
        /// res = value op1 val1;
        /// res =   res op2 val2;
        /// res =   res po3 val3;
        /// ...
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="data"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string CalcParamValue(List<string> keys, int[] data, double value = 0)
        {
            if (keys.Count == 0) return value.ToString();

            if (keys.Count == 1)
            {
                return GetSectionString(keys[0], (int)value);
            }

            int tvalue;

            if (!GetParamValue(keys[1], out tvalue, data)) return value.ToString();

            switch (keys[0])
            {
                case "add": value += tvalue; break;
                case "sub": value -= tvalue; break;
                case "mul": value *= tvalue; break;
                case "div": value /= tvalue; break;
                default: break;
            }

            keys.RemoveRange(0, 2);

            return CalcParamValue(keys, data, value);
        }



        /// <summary>
        /// Store to value data[key] if key has prefix 'p', or key.ToString if no prefix.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool GetParamValue(string key, out int value, int[] data)
        {
            value = 0;
            
            if (key[0] == 'p')
            {
                key = key.Substring(1, key.Length - 1);
                if (!int.TryParse(key, out value)) return false;
                value++;
                if (data.Length <= value) return false;
                value = data[value];
                return true;
            }
            else
            {
                return int.TryParse(key, out value);
            }
        }



        private string GetParamValue(string sparam, int[] data)
        {
            List<string> keys = new List<string>();
            keys.AddRange(sparam.Split(':'));

            if (!GetParamValue(keys[0], out int value, data)) return sparam;

            keys.RemoveAt(0);
            return CalcParamValue(keys, data, value);
        }
    }
}
