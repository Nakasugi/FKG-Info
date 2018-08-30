namespace FKG_Info
{
    public class EquipmentInfo : BaseInfo
    {
        public string KName { get; private set; }
        public string KInfo { get; private set; }

        public int Rarity { get; private set; }
        public int Type { get; private set; }

        public int AttackMin { get; private set; }
        public int AttackMax { get; private set; }
        public int DefenseMin { get; private set; }
        public int DefenseMax { get; private set; }


        public int ESetID { get; private set; }
        public int ESetAtkMax { get; private set; }
        public int ESetDefMax { get; private set; }


        private int[] FlowerIDs;

        public int ImageID { get; private set; }

        //private static string URL;


        //http://dugrqaqinbtcq.cloudfront.net/product/images/item/100x100/

        public EquipmentInfo()
        {
            BaseType = ObjectType.Equipment;

            ESetID = 0;

            //FlowerIDs = new List<int>();
            //IsPersonalWeapon = false;
        }



        public EquipmentInfo(string[] masterData) : this()
        {
            if (masterData.Length < 32) return;


            int parsedValue;
            if (!int.TryParse(masterData[0], out parsedValue)) return;
            ID = parsedValue;
            KName = masterData[1];
            KInfo = masterData[25].Replace("\"", "");
            if (KInfo == "") KInfo = null;

            int.TryParse(masterData[2], out parsedValue); ImageID = parsedValue;

            int.TryParse(masterData[19], out parsedValue); Type = parsedValue;
            int.TryParse(masterData[22], out parsedValue); Rarity = parsedValue;

            int.TryParse(masterData[4], out parsedValue); AttackMin = parsedValue;
            int.TryParse(masterData[5], out parsedValue); DefenseMin = parsedValue;
            int.TryParse(masterData[7], out parsedValue); AttackMax = parsedValue;
            int.TryParse(masterData[8], out parsedValue); DefenseMax = parsedValue;

            string[] sIDs = masterData[21].Split('|');
            FlowerIDs = new int[sIDs.Length];
            for (int i = 0; i < sIDs.Length; i++)
                try { FlowerIDs[i] = int.Parse(sIDs[i]); }
                catch { FlowerIDs[i] = 0; }
            //int.TryParse(masterData[21], out FlowerID);
            //if (masterData[23] == "1") IsPersonalWeapon = true;
        }



        public bool ChekFlowerID(int flowerID)
        {
            if (System.Array.IndexOf(FlowerIDs, flowerID) == -1) return false;
            return true;
        }


        //public string GetDMMImageURL() { return ReplaceCharaToItem(Program.DB.DMMURL); }
        //public string GetNutakuImageURL() { return ReplaceCharaToItem(Program.DB.NutakuURL); }
        //public string GetPath() { return Program.DB.EquipFolder + "\\" + ImageID + ".png"; }
        public string GetImageName() { return ImageID.ToString(); }

        /*
        private string ReplaceCharaToItem(string st)
        {
            return st.Replace("character/", "") + "item/100x100/" + ImageID + ".png";
        }
        */


        public void FillGrid(System.Windows.Forms.DataGridView view, bool translation = true)
        {
            int id;

            view.Rows.Clear();
            view.Rows.Add("ImID", ImageID);
            view.Rows.Add("Kanji", KName);

            if (AttackMax > 0) view.Rows.Add("Attack", AttackMin + " .. " + AttackMax);
            if (DefenseMin > 0) view.Rows.Add("Defense", DefenseMin + " .. " + DefenseMax);

            int tmin = AttackMin + DefenseMin;
            int tmax = AttackMax + DefenseMax;
            if (tmax > 0) view.Rows.Add("In Total", tmin + " .. " + tmax);

            if (KInfo != null)
            {
                id = view.Rows.Add("Description", KInfo);
                view.Rows[id].Height = (view.Rows[id].Height - 2) * 4 + 1;
            }

            if (ESetID != 0)
            {
                view.Rows.Add("Set ID", ESetID.ToString());
                view.Rows.Add("Set Max Atk", ESetAtkMax.ToString());
                view.Rows.Add("Set Max Def", ESetDefMax.ToString());
                view.Rows.Add("Set Total", (ESetAtkMax + ESetDefMax).ToString());
            }
        }



        public override int CompareTo(object obj, SortBy sortType)
        {
            EquipmentInfo eq = obj as EquipmentInfo;

            if (eq == null) return 1;

            bool cmp1stStep = true;
            int cmp1, cmp2;

            switch (sortType)
            {
                case SortBy.Name:
                    cmp1 = KName.CompareTo(eq.KName);
                    if (cmp1 != 0) return cmp1;
                    break;
                case SortBy.Attack:
                    if (AttackMax < eq.AttackMax) return 1;
                    if (AttackMax > eq.AttackMax) return -1;
                    if (cmp1stStep) { cmp1stStep = false; goto case SortBy.OverallForce; }
                    break;
                case SortBy.Defense:
                    if (DefenseMax < eq.DefenseMax) return 1;
                    if (DefenseMax > eq.DefenseMax) return -1;
                    if (cmp1stStep) { cmp1stStep = false; goto case SortBy.OverallForce; }
                    break;
                case SortBy.OverallForce:
                    cmp1 = AttackMax + DefenseMax;
                    cmp2 = eq.AttackMax + eq.DefenseMax;
                    if (cmp1 < cmp2) return 1;
                    if (cmp1 > cmp2) return -1;
                    if (cmp1stStep) { cmp1stStep = false; goto case SortBy.Attack; }
                    break;
                case SortBy.SetAttack:
                    if (ESetAtkMax < eq.ESetAtkMax) return 1;
                    if (ESetAtkMax > eq.ESetAtkMax) return -1;
                    if (cmp1stStep) { cmp1stStep = false; goto case SortBy.SetTotalStats; }
                    break;
                case SortBy.SetDefense:
                    if (ESetDefMax < eq.ESetDefMax) return 1;
                    if (ESetDefMax > eq.ESetDefMax) return -1;
                    if (cmp1stStep) { cmp1stStep = false; goto case SortBy.SetTotalStats; }
                    break;
                case SortBy.SetTotalStats:
                    cmp1 = ESetAtkMax + ESetDefMax;
                    cmp2 = eq.ESetAtkMax + eq.ESetDefMax;
                    if (cmp1 < cmp2) return 1;
                    if (cmp1 > cmp2) return -1;
                    if (cmp1stStep) { cmp1stStep = false; goto case SortBy.SetAttack; }
                    break;
                default: break;
            }

            return base.CompareTo(obj, sortType);
        }



        /// <summary>
        /// Searching (by Name) equipment sets in equipment list
        /// </summary>
        /// <param name="equips"></param>
        public static void SearchSets(System.Collections.Generic.List<EquipmentInfo> equips)
        {
            int id = 1;
            string name, cmp;
            bool newid;

            foreach(EquipmentInfo eqA in equips)
            {
                //if (eqA.SetID != 0) idcr = eqA.SetID; else idcr = id;

                name = eqA.KName.Replace("指輪", "").Replace("腕輪", "").Replace("首飾り", "").Replace("耳飾り", "");

                newid = false;
                foreach (EquipmentInfo eqB in equips)
                {
                    cmp = eqB.KName.Replace("指輪", "").Replace("腕輪", "").Replace("首飾り", "").Replace("耳飾り", "");

                    if (name == cmp)
                    {
                        if (eqA.ESetID != 0)
                        {
                            eqB.ESetID = eqA.ESetID;
                        }
                        else
                        {
                            eqA.ESetID = id;
                            eqB.ESetID = id;
                            newid = true;
                        }
                    }
                }

                if (newid) id++;
            }


            System.Collections.Generic.List<EquipmentInfo> sets;
            int idmax = id, idcr = 1, maxAtk, maxDef;
            for (id = 1; id < idmax; id++)
            {
                sets = equips.FindAll(eq => eq.ESetID == id);

                if (sets.Count != 4)
                {
                    foreach (EquipmentInfo eq in sets) eq.ESetID = 0;
                }
                else
                {
                    maxAtk = 0; maxDef = 0;

                    foreach (EquipmentInfo eq in sets)
                    {
                        eq.ESetID = idcr;
                        maxAtk += eq.AttackMax;
                        maxDef += eq.DefenseMax;
                    }
                    
                    foreach (EquipmentInfo eq in sets)
                    {
                        eq.ESetAtkMax = maxAtk;
                        eq.ESetDefMax = maxDef;
                    }

                    idcr++;
                }
            }
        }
    }
}
