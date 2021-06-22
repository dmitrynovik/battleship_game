using Functional.Maybe;

namespace BattleshipGame
{
    public class Point2D
    {
        public Point2D(uint x, uint y, Piece piece = null)
        {
            X = x;
            Y = y;
            Piece = piece == null ? Maybe<Piece>.Nothing : piece.ToMaybe();
        }

        public uint X { get; }
        public uint Y { get; }
        public Maybe<Piece> Piece { get; }
        public bool IsOccupied => Piece.IsSomething();
        public bool IsHit { get; private set;}

        public void Hit() => IsHit = true;

        public override int GetHashCode()
        {
            unchecked
            {
                return (1931 * X.GetHashCode()) ^ (2333 * Y.GetHashCode());
            }
        }

        public override bool Equals(object obj) => obj is Point2D pt && pt.X == X && pt.Y == Y;

        public override string ToString() => $"{nameof(Point2D)} ({X},{Y})";
    }
}
