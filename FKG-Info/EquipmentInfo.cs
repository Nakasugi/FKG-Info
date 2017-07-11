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


        private int FlowerID;
        private int ImageID;

        //private static string URL;


        //http://dugrqaqinbtcq.cloudfront.net/product/images/item/100x100/

        public EquipmentInfo()
        {
            BaseType = ObjectType.Equipment;

            ESetID = 0;
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

            int.TryParse(masterData[2], out ImageID);
            int.TryParse(masterData[21], out FlowerID);

            int.TryParse(masterData[19], out parsedValue); Type = parsedValue;
            int.TryParse(masterData[22], out parsedValue); Rarity = parsedValue;

            int.TryParse(masterData[4], out parsedValue); AttackMin = parsedValue;
            int.TryParse(masterData[5], out parsedValue); DefenseMin = parsedValue;
            int.TryParse(masterData[7], out parsedValue); AttackMax = parsedValue;
            int.TryParse(masterData[8], out parsedValue); DefenseMax = parsedValue;

            //if (masterData[23] == "1") IsPersonalWeapon = true;
        }



        public bool ChekFlowerID(int flowerID)
        {
            if (ID < 20000) return false;
            if (FlowerID != flowerID) return false;
            return true;
        }


        //public string GetDMMImageURL() { return ReplaceCharaToItem(Program.DB.DMMURL); }
        //public string GetNutakuImageURL() { return ReplaceCharaToItem(Program.DB.NutakuURL); }
        //public string GetPath() { return Program.DB.EquipFolder + "\\" + ImageID + ".png"; }
        public string GetImageName() { return ImageID.ToString(); }

        private string ReplaceCharaToItem(string st)
        {
            return st.Replace("character/", "") + "item/100x100/" + ImageID + ".png";
        }



        public void FillGrid(System.Windows.Forms.DataGridView view, bool translation = true)
        {
            int id;

            view.Rows.Clear();
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


        // ==================================================================================
        // Sort by Name
        public static int SortByName(EquipmentInfo eq1, EquipmentInfo eq2)
        {
            if (eq1 == null || eq2 == null) return 0;
            int res = eq1.KName.CompareTo(eq2.KName);
            if (res == 0) return eq1.CompareTo(eq2);
            return res;
        }

        // Sort by Attack
        public static int SortByAttack(EquipmentInfo eq1, EquipmentInfo eq2, bool def = true)
        {
            if (eq1 == null || eq2 == null) return 0;

            if (eq1.AttackMax < eq2.AttackMax) return 1;
            if (eq1.AttackMax > eq2.AttackMax) return -1;
            int res = 0;
            if (def) res = SortByDefense(eq1, eq2, false);
            if (res == 0) return eq1.CompareTo(eq2);
            return res;
        }

        // Sort by Defense
        public static int SortByDefense(EquipmentInfo eq1, EquipmentInfo eq2, bool atk = true)
        {
            if (eq1 == null || eq2 == null) return 0;

            if (eq1.DefenseMax < eq2.DefenseMax) return 1;
            if (eq1.DefenseMax > eq2.DefenseMax) return -1;
            int res = 0;
            if (atk) res = SortByAttack(eq1, eq2, false);
            if (res == 0) return eq1.CompareTo(eq2);
            return res;
        }

        // Sort by Total
        public static int SortByTotal(EquipmentInfo eq1, EquipmentInfo eq2)
        {
            if (eq1 == null || eq2 == null) return 0;

            int st1 = eq1.AttackMax + eq1.DefenseMax;
            int st2 = eq2.AttackMax + eq2.DefenseMax;

            if (st1 < st2) return 1;
            if (st1 > st2) return -1;
            return SortByAttack(eq1, eq2);
        }

        // Sort by Attack
        public static int SortBySetAttack(EquipmentInfo eq1, EquipmentInfo eq2, bool def = true)
        {
            if (eq1 == null || eq2 == null) return 0;

            if (eq1.ESetID == eq2.ESetID) return eq1.CompareTo(eq2);
            if ((eq1.ESetID == 0) && (eq2.ESetID == 0)) return eq1.CompareTo(eq2);
            if (eq1.ESetID == 0) return 1;
            if (eq2.ESetID == 0) return -1;

            if (eq1.ESetAtkMax < eq2.ESetAtkMax) return 1;
            if (eq1.ESetAtkMax > eq2.ESetAtkMax) return -1;
            int res = 0;
            if (def) res = SortBySetDefense(eq1, eq2, false);
            if (res == 0) return eq1.CompareTo(eq2);
            return res;
        }

        // Sort by Defense
        public static int SortBySetDefense(EquipmentInfo eq1, EquipmentInfo eq2, bool atk = true)
        {
            if (eq1 == null || eq2 == null) return 0;

            if (eq1.ESetID == eq2.ESetID) return eq1.CompareTo(eq2);
            if ((eq1.ESetID == 0) && (eq2.ESetID == 0)) return eq1.CompareTo(eq2);
            if (eq1.ESetID == 0) return 1;
            if (eq2.ESetID == 0) return -1;

            if (eq1.ESetDefMax < eq2.ESetDefMax) return 1;
            if (eq1.ESetDefMax > eq2.ESetDefMax) return -1;
            int res = 0;
            if (atk) res = SortBySetAttack(eq1, eq2, false);
            if (res == 0) return eq1.CompareTo(eq2);
            return res;
        }

        // Sort by Total
        public static int SortBySetTotal(EquipmentInfo eq1, EquipmentInfo eq2)
        {
            if (eq1 == null || eq2 == null) return 0;

            if (eq1.ESetID == eq2.ESetID) return eq1.CompareTo(eq2);
            if ((eq1.ESetID == 0) && (eq2.ESetID == 0)) return eq1.CompareTo(eq2);
            if (eq1.ESetID == 0) return 1;
            if (eq2.ESetID == 0) return -1;

            int st1 = eq1.ESetAtkMax + eq1.ESetDefMax;
            int st2 = eq2.ESetAtkMax + eq2.ESetDefMax;

            if (st1 < st2) return 1;
            if (st1 > st2) return -1;
            return SortBySetAttack(eq1, eq2);
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
