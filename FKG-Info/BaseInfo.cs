using System;

namespace FKG_Info
{
    public class BaseInfo : IComparable
    {
        public enum ObjectType { None, Flower, Skill, Ability, Equipment, Skin, GardenItem }
        public enum SortBy
        {
            Default, Name,
            OverallForce, Attack, Defense, HitPoints, Speed,
            SetTotalStats, SetAttack, SetDefense,
            Category
        }

        public ObjectType BaseType { get; protected set; }


        public int ID { get; protected set; }

        public bool Filter;
        public bool Updated;

        public int Version { get; private set; }




        protected BaseInfo()
        {
            BaseType = ObjectType.None;

            ID = 0;
            Filter = true;
            Version = 0;
            Updated = false;
        }



        /// <summary>
        /// Sort: (-1: this, obj) (1: obj, this)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int CompareTo(object obj)
        {
            BaseInfo bi = obj as BaseInfo;

            if (bi == null) return 1;

            if (ID < bi.ID) return -1;
            if (ID > bi.ID) return 1;

            return 0;
        }


        public virtual int CompareTo(object obj, SortBy sortType) { return CompareTo((BaseInfo)obj); }


        protected int SetVersion(string version)
        {
            if (version == null) return 0;

            Version = 0;

            try
            {
                string[] subvs = version.Split('.');
                Version = 1000000 * int.Parse(subvs[0]) + 1000 * int.Parse(subvs[1]) + int.Parse(subvs[2]);
            }
            catch { }

            return Version;
        }


        protected string GetVersion()
        {
            int l0 = Version / 1000000;
            int l1 = Version / 1000 - l0 * 1000;
            int l2 = Version - l0 * 1000000 - l1 * 1000;

            return l0 + "." + l1 + "." + l2;
        }
    }
}
