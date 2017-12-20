namespace FKG_Info
{
    class GardenItemInfo : BaseInfo
    {
        // http://dugrqaqinbtcq.cloudfront.net/product/images/item/garden/dwk2vtekomujuwd9/objet/604309.png?1.57.0
        // http://dugrqaqinbtcq.cloudfront.net/product/images/item/100x100/601011.png?1.57.0
        // http://dugrqaqinbtcq.cloudfront.net/product/images/item/garden/dwk2vtekomujuwd9/background/601024.jpg?1.57.0
        // http://dugrqaqinbtcq.cloudfront.net/product/images/item/garden/dwk2vtekomujuwd9/azumaya/602021.png?1.57.0
        // http://dugrqaqinbtcq.cloudfront.net/product/images/item/garden/dwk2vtekomujuwd9/gardentree/603021.png?1.57.0
        // http://dugrqaqinbtcq.cloudfront.net/product/images/item/garden/dwk2vtekomujuwd9/objet/604140.png?1.57.0

        private int ImageID;

        private string KName;
        private string KDesc;

        GardenItemInfo()
        {
            BaseType = ObjectType.GardenItem;
        }


        public GardenItemInfo(string[] masterData) : this()
        {
            if (masterData.Length < 17) return;

            //int parsedValue;

            int.TryParse(masterData[0], out ImageID);

            KName = masterData[2];
            KDesc = masterData[3];

            // [7] need molotok?
            // [12] set?
        }
    }
}
