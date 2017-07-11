using System;

namespace FKG_Info
{
    public class BaseInfo : IComparable
    {
        public enum ObjectType { Flower, Skill, Ability, Equipment }

        public ObjectType BaseType { get; protected set; }


        public int ID { get; protected set; }

        public bool Filter;


        protected BaseInfo()
        {
            ID = 0;
            Filter = true;
        }


        public virtual int CompareTo(object obj)
        {
            BaseInfo bi = obj as BaseInfo;

            if (ID < bi.ID) return -1;
            if (ID > bi.ID) return 1;

            return 0;
        }
    }
}
