using System.Collections.Generic;
using System.Linq;

namespace FKG_Info
{
    public class SkinsList
    {
        public class Skin
        {
            public int ID { get; private set; }
            public string Name { get; private set; }

            public Skin(int id, string name) { ID = id; Name = name; }
        }


        public int RefID;

        private List<Skin> Skins;

        public int Main { get { return Skins[0].ID; } }



        public SkinsList(int mainSkinId)
        {
            RefID = 0;
            Skins = new List<Skin>();

            Skins.Add(new Skin(mainSkinId, "Basic"));
        }



        public void AddSkin(int id, string name)
        {
            if ((id == 0) || (name == "") || (name == null)) return;

            if (id == Main)
            {
                Skins[0] = new Skin(id, name);
                return;
            }

            Skins.Add(new Skin(id, name));
        }



        public string[] GetNames()
        {
            return Skins.Select(s => s.Name).ToArray();
        }



        public int GetByName(string name)
        {
            Skin skin = Skins.Find(s => s.Name == name);

            if (skin == null) return Main;

            return skin.ID;
        }



        public int GetByIndex(int index)
        {
            try
            {
                return Skins[index].ID;
            }
            catch
            {
                return Main;
            }
        }


        public bool HasAdditionalSkin() { return Skins.Count > 1; }
    }
}
