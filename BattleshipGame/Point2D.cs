using Functional.Maybe;

namespace BattleshipGame
{
    public record Point2D(uint X, uint Y, Maybe<Piece> Piece, bool IsHit = false)
    {
        public bool IsOccupied => Piece.IsSomething();

        public Point2D Hit() => new(X, Y, Piece, true);
    }
}
