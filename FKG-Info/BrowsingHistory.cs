using System.Collections.Generic;



namespace FKG_Info
{
    public class BrowsingHistory
    {
        private List<int> History;

        private int CurrentIndex;



        public BrowsingHistory()
        {
            History = new List<int>();

            CurrentIndex = 0;
        }



        public void Clear() { History.Clear(); }



        public int Prev()
        {
            if (CurrentIndex + 1 >= History.Count) return -1;

            CurrentIndex++;
            return History[CurrentIndex];
        }



        public int Next()
        {
            if (CurrentIndex == 0) return -1;

            CurrentIndex--;
            return History[CurrentIndex];
        }



        public void Add(int num)
        {
            History.Insert(CurrentIndex, num);

            if (History.Count > 64)
            {
                if (CurrentIndex < 32)
                {
                    History.RemoveAt(History.Count - 1);
                }
                else
                {
                    History.RemoveAt(0);
                    CurrentIndex--;
                }
            }
        }
    }
}
