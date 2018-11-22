namespace FKG_Info.FKG_GameData
{
    public class SkinInfo : BaseInfo
    {
        public string Name { get; private set; }

        public int ReplaceID { get; private set; }
        public int RefID { get; private set; }

        /// <summary>
        /// True for skins, False for characters forms.
        /// </summary>
        public bool IsSkin { get; private set; }
        /// <summary>
        /// No exclusive skin, just replace with ImageRep.
        /// </summary>
        public bool IsBaseReplace { get; private set; }
        /// <summary>
        /// True for exclusive skins.
        /// </summary>
        public bool IsExclusive { get; private set; }

        /// <summary>
        /// Position in skin selection window.
        /// </summary>
        public int Position { get; private set; }



        SkinInfo()
        {
            BaseType = ObjectType.Skin;

            IsSkin = false;
            IsBaseReplace = false;
            IsExclusive = false;
        }



        public SkinInfo(string[] masterData) : this()
        {
            int parsedValue;
            if (!int.TryParse(masterData[1], out parsedValue)) return;

            RefID = parsedValue;

            Name = masterData[6];

            int.TryParse(masterData[0], out parsedValue); ID = parsedValue;
            int.TryParse(masterData[2], out parsedValue); ReplaceID = parsedValue;
            int.TryParse(masterData[7], out parsedValue); Position = parsedValue;

            if (masterData[3] == "1") IsSkin = true;
            if (masterData[4] == "1") IsBaseReplace = true;
            if (masterData[5] == "1") IsExclusive = true;
        }
    }
}
