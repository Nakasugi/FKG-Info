﻿using System;
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
    }
}