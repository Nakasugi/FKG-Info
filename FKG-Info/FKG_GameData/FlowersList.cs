using System;
using System.Collections.Generic;
using System.Linq;



namespace FKG_Info.FKG_GameData
{
    public class FlowersList
    {
        private List<FlowerInfo> Flowers;

        private int SelectedID;


        public object Locker;

        private BrowsingHistory History;


        private SavingDataBlock SavingData;
        public bool NeedSave { get; private set; }



        public FlowersList()
        {
            Locker = new object();

            Flowers = new List<FlowerInfo>();
            SelectedID = -1;

            History = new BrowsingHistory();
            SavingData = new SavingDataBlock();

            NeedSave = false;
        }



        public void Add(FlowerInfo flower)
        {
            if (flower.ID == 0) return;

            Flowers.Add(flower);
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



        private List<int> CreateNoBloomCGData()
        {
            List<int> ids = new List<int>();

            foreach (FlowerInfo flower in Flowers)
                if ((flower.Evolution == FlowerInfo.Variation.Bloomed) && flower.NoBloomCG) ids.Add(flower.RefID);

            return ids;
        }



        public void LoadSaving(System.Xml.XmlDocument doc) { SavingData = new SavingDataBlock(doc); }



        public void SaveSaving(System.Xml.XmlWriter xwr)
        {
            SavingData.SetBloomCGData(CreateNoBloomCGData().ToArray());
            SavingData.Save(xwr);
        }



        public bool CheckAccStatus(int refid, int acc) { return SavingData.CheckAccStatus(refid, acc); }
        public void AddToAccount(int refid, int acc) { SavingData.AddToAccount(refid, acc); NeedSave = true; }
        public void RemoveFromAccount(int refid, int acc) { SavingData.RemoveFromAccount(refid, acc); NeedSave = true; }



        public void ClearHistory() { History.Clear(); }



        public void DeleteOldBloomCGs()
        {
            List<int> newids = CreateNoBloomCGData();
            List<int> oldids = SavingData.GetNoBlomCGRefs();

            if (oldids.SequenceEqual(newids)) return;

            NeedSave = true;

            foreach (int id in newids) oldids.Remove(id);

            if (oldids.Count == 0) return;


            List<int> iconids = new List<int>();

            foreach(int id in oldids)
            {
                FlowerInfo flower = Flowers.Find(fw => fw.RefID == id && fw.Evolution == FlowerInfo.Variation.Bloomed);

                if (flower == null) continue;

                Program.ImageLoader.DeleteImages(flower.ID);
                Program.FlowerIcons.UpdateIconImage(flower.ID);
            }
        }



        public void UpdateSkins(List<SkinInfo> skins, GrowInfo grow)
        {
            List<SkinInfo> sklist = skins.ToList();

            foreach (FlowerInfo flower in Flowers)
            {
                SkinInfo skin = sklist.Find(sk => sk.ID == flower.ID);
                if (skin == null) continue;
                flower.SkinsRefID = skin.RefID;
                sklist.Remove(skin);
            }

            sklist = sklist.FindAll(sk => sk.IsExclusive || sk.IsBaseReplace);

            foreach (FlowerInfo flower in Flowers)
            {
                if (flower.SkinsRefID == 0) continue;

                foreach (SkinInfo skin in sklist) if (skin.RefID == flower.SkinsRefID) flower.UpdateSkin(skin, grow);
            }
        }
    }
}
