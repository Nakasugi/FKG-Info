using System;

namespace FKG_Info
{
    public class BaseInfo : IComparable
    {
        public enum ObjectType { None, Flower, Skill, Ability, Equipment, Skin }
        public enum SortBy
        {
            Default, Name,
            TotalStats, Attack, Defense, HitPoints, Speed,
            SetTotalStats, SetAttack, SetDefense,
            Category
        }

        public ObjectType BaseType { get; protected set; }


        public int ID { get; protected set; }

        public bool Filter;



        protected BaseInfo()
        {
            BaseType = ObjectType.None;

            ID = 0;
            Filter = true;
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
    }
}
