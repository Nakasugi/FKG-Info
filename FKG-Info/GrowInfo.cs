using System.Collections.Generic;
using System.IO;



namespace FKG_Info
{
    public class GrowInfo
    {
        private class GrowUnit
        {
            public int MainID;
            public int ReplaceID;

            public GrowUnit(string[] masterData)
            {
                int parsedValue;

                int.TryParse(masterData[0], out parsedValue); MainID = parsedValue;
                int.TryParse(masterData[4], out parsedValue); ReplaceID = parsedValue;
            }
        }



        List<GrowUnit> Units;



        public GrowInfo()
        {
            Units = new List<GrowUnit>();
        }



        public void ReadMasterData(string masterData)
        {
            string masterLine;
            string[] masterFields;

            StringReader rd = new StringReader(masterData);
            while (true)
            {
                masterLine = rd.ReadLine();
                if (masterLine == null) break;

                masterFields = masterLine.Split(',');

                Units.Add(new GrowUnit(masterFields));
            }
            rd.Close();
        }


        public int ReplaceImageID(int id)
        {
            GrowUnit unit = Units.Find(u => u.MainID == id);
            if (unit == null) return id;
            return unit.ReplaceID;
        }
    }
}
