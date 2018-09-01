using System.Collections.Generic;
using System.IO;
using System.Xml;




namespace FKG_Info
{
    public class SavingDataBlock
    {
        private List<int> Account1Refs;
        private List<int> Account2Refs;
        private List<int> NoBloomCGRefs;


        public const string XmlBlockName = "DataBlock";
        private const string XmlElementName = "CompressedData";



        public SavingDataBlock()
        {
            Account1Refs = new List<int>();
            Account2Refs = new List<int>();
            NoBloomCGRefs = new List<int>();
        }




        public SavingDataBlock(XmlDocument doc) : this()
        {
            string str_data;
            byte[] raw_data;

            try { str_data = doc.SelectSingleNode("//" + XmlBlockName)[XmlElementName].InnerText; } catch { return; }

            if (str_data == null) return;


            raw_data = Helper.Base64ToData(str_data);
            raw_data = Helper.DecompressData(raw_data);

            MemoryStream st = new MemoryStream(raw_data);
            BinaryReader rd = new BinaryReader(st);

            int i, cnt;

            cnt = rd.ReadInt16();
            for (i = 0; i < cnt; i++) Account1Refs.Add(rd.ReadInt16());
            cnt = rd.ReadInt16();
            for (i = 0; i < cnt; i++) Account2Refs.Add(rd.ReadInt16());
            cnt = rd.ReadInt16();
            for (i = 0; i < cnt; i++) NoBloomCGRefs.Add(rd.ReadInt16());

            rd.Close();
        }



        public void Save(XmlWriter xwr)
        {
            if (xwr == null) return;

            MemoryStream st = new MemoryStream();
            BinaryWriter wr = new BinaryWriter(st);

            wr.Write((System.Int16)Account1Refs.Count);
            foreach (int id in Account1Refs) wr.Write((System.Int16)id);
            wr.Write((System.Int16)Account2Refs.Count);
            foreach (int id in Account2Refs) wr.Write((System.Int16)id);
            wr.Write((System.Int16)NoBloomCGRefs.Count);
            foreach (int id in NoBloomCGRefs) wr.Write((System.Int16)id);

            byte[] raw_data;
            string str_data;

            raw_data = st.ToArray();
            raw_data = Helper.CompressData(raw_data);
            str_data = Helper.DataToBase64(raw_data);

            wr.Close();

            xwr.WriteStartElement(XmlBlockName);
            xwr.WriteElementString(XmlElementName, str_data);
            xwr.WriteEndElement();
        }



        public int[] GetAccRefs(int acc)
        {
            if (acc == 1) return Account1Refs.ToArray();
            if (acc == 2) return Account2Refs.ToArray();

            return null;
        }



        public List<int> GetNoBlomCGRefs() { return new List<int>(NoBloomCGRefs); }



        public bool CheckAccStatus(int refid, int acc)
        {
            if (acc == 1) { return Account1Refs.Contains(refid); }
            if (acc == 2) { return Account2Refs.Contains(refid); }

            return false;
        }



        public void AddToAccount(int refid, int acc)
        {
            if (acc == 1) { if (!Account1Refs.Contains(refid)) Account1Refs.Add(refid); }
            if (acc == 2) { if (!Account2Refs.Contains(refid)) Account2Refs.Add(refid); }
        }



        public void RemoveFromAccount(int refid, int acc)
        {
            if (acc == 1) Account1Refs.Remove(refid);
            if (acc == 2) Account2Refs.Remove(refid);
        }



        public void SetBloomCGData(int[] data)
        {
            NoBloomCGRefs.Clear();
            NoBloomCGRefs.AddRange(data);
        }
    }
}
