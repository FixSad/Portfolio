using Space_Invaders.Enums;

namespace Space_Invaders
{
    internal class GameObject
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public Bump CurrentBump { get; set; }
        public bool IsAlive { get; set; }

        public GameObject(int x, int y)
        {
            IsAlive = true;
            X = x;
            Y = y;
            CurrentBump = Bump.None;
        }

        public virtual Bump CheckBump()
        {
            if (X == 0 || Y == 0 || X == 19 || Y == 39)
                return Bump.Border;
            else
                return Bump.None;
        }

        public virtual void TryToMove(){}

        public override string ToString()
        {
            return $"{this.GetType().Name}. X: {X}, Y: {Y}. Is Alive: {IsAlive}";
        }
    }
}