namespace FKG_Info
{
    class LifeContcol
    {
        private bool Life;

        public bool IsLife { get { return Life; } }
        public bool IsDead { get { return !Life; } }


        public LifeContcol() { Life = true; }
      

        public void Kill() { Life = false; }
    }
}
