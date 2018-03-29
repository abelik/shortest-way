namespace TestTask.ShortestWay
{
    public class Location
    {
        public int X;
        public int Y;

        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(Location other) {
            if (other is null)
                return false;

            if (ReferenceEquals(other, this))
                return true;

            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) {
            return Equals(obj as Location);
        }
       
        public static bool operator == (Location left, Location right) {
            return Equals(left, right);
        }

        public static bool operator != (Location left, Location right) {
            return !(left == right);
        }

        public override int GetHashCode() => X ^ Y;
    }
}
