namespace AOC.Common.Models
{
    public class Position(int x, int y)
    {
        public int X { get; protected set; } = x;
        public int Y { get; protected set; } = y;

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is Position)) return false;

            var pos = (Position)obj;
            return pos.X == X && pos.Y == Y;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }

    public enum Direction
    {
        North = 10,
        East = 20,
        South = 30,
        West = 40,
    }
}
