using System;
using System.Collections.Generic;



namespace FKG_Info
{
    public class FlowersList
    {
        private List<FlowerInfo> Flowers;
        private List<AccountLinks> AccLinks;

        //public List<FlowerUnitInfo> Units { get; private set; }

        private int SelectedID;

        public object Locker;

        private BrowsingHistory History;

        private int HighestVersion;



        private class AccountLinks
        {
            public int RefID;

            public bool Acc1;
            public bool Acc2;

            public AccountLinks(int refid) { RefID = refid; Acc1 = false; Acc2 = false; }

            public int GetAccStatus()
            {
                int res = 0;
                if (Acc1) res |= 0x01;
                if (Acc2) res |= 0x02;
                return res;
            }

            public void SetAccStatus(int status)
            {
                if ((status & 0x01) != 0) Acc1 = true;
                if ((status & 0x02) != 0) Acc2 = true;
            }
        }




        public FlowersList()
        {
            Locker = new object();

            Flowers = new List<FlowerInfo>();
            SelectedID = -1;

            AccLinks = new List<AccountLinks>();
            History = new BrowsingHistory();

            HighestVersion = 0;
        }



        public void Add(FlowerInfo flower)
        {
            if (flower.ID == 0) return;

            Flowers.Add(flower);

            if (HighestVersion < flower.Version) HighestVersion = flower.Version;

            if (AccLinks.Find(ac => ac.RefID == flower.RefID) == null)
                AccLinks.Add(new AccountLinks(flower.RefID));
        }



        public FlowerInfo GetSelected()
        {
            if (!IsSelected()) return null;
            return Flowers.Find(fw => fw.ID == SelectedID);
        }

        public FlowerInfo GetSelected(int variation)
        {
            FlowerInfo flower = GetSelected();
            if (flower == null) return null;

            return Flowers.Find(fw => ((fw.RefID == flower.RefID) && fw.CheckVariation(variation)));
        }

        public FlowerInfo GetPrev() { SelectedID = History.Prev(); return GetSelected(); }
        public FlowerInfo GetNext() { SelectedID = History.Next(); return GetSelected(); }

        /*
        public FlowerUnitInfo GetSelectedUnit()
        {
            if (!IsSelected()) return null;

            return GetUnit(SelectedRefID);
        }
        */
        //public FlowerUnitInfo GetUnit(int refid) { return Units.Find(u => u.RefID == refid); }



        public void Select(int id)
        {
            if (Flowers.Find(fw => fw.ID == id) == null)
            {
                SelectedID = -1;
                return;
            }

            SelectedID = id;
            History.Add(id);
        }

        public bool IsSelected() { return SelectedID != -1; }
        public void Unselect() { SelectedID = -1; }
        
        public int Count { get { return Flowers.Count; } }

        public FlowerInfo Find(Predicate<FlowerInfo> match) { return Flowers.Find(match); }
        public List<FlowerInfo> FindAll(Predicate<FlowerInfo> match) { return Flowers.FindAll(match); }

        public List<FlowerInfo>.Enumerator GetEnumerator() { return Flowers.GetEnumerator(); }




        public FlowerInfo GetNextEvolution(FlowerInfo flower)
        {
            if (flower == null) return null;

            int nextid = flower.GetNextEvolutionID();

            if (nextid == -1) return null;

            return Flowers.Find(fw => fw.ID == nextid);
        }

        public FlowerInfo GetPrevEvolution(FlowerInfo flower)
        {
            if (flower == null) return null;

            int previd = flower.GetPrevEvolutionID();

            if (previd == -1) return null;

            return Flowers.Find(fw => fw.ID == previd);
        }



        public void WriteXmlAccLinks(System.Xml.XmlTextWriter xw)
        {
            xw.WriteStartElement("HasFlowers");

            foreach (AccountLinks links in AccLinks)
            {
                int accStatus = links.GetAccStatus();
                if (accStatus != 0)
                    xw.WriteElementString("ID" + links.RefID.ToString("D4"), accStatus.ToString());
            }

            xw.WriteEndElement();
        }



        public void SetAccStatus(int refid, int status)
        {
            AccountLinks links = AccLinks.Find(al => al.RefID == refid);
            if (links == null) return;

            links.SetAccStatus(status);
        }

        public void SetAccStatus(int refid, int accnum, bool status)
        {
            AccountLinks links = AccLinks.Find(al => al.RefID == refid);
            if (links == null) return;

            if (accnum == 1) links.Acc1 = status;
            if (accnum == 2) links.Acc2 = status;
        }

        public bool CheckAccStatus(int refid, int accnum)
        {
            AccountLinks links = AccLinks.Find(al => al.RefID == refid);
            if (links == null) return false;

            if (accnum == 1) return links.Acc1;
            if (accnum == 2) return links.Acc2;

            return false;
        }



        public void ClearHistory() { History.Clear(); }



        public bool CheckVersions(ref int version)
        {
            if (version == 0)
            {
                version = HighestVersion;
                return true;
            }

            if (version == HighestVersion) return false;

            foreach (FlowerInfo flower in Flowers)
                if (flower.Version > version) flower.Updated = true;

            version = HighestVersion;
            return true;
        }
    }
}
