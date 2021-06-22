using System.Linq;

namespace BattleshipGame
{
    /// <summary>
    /// An abstract 2D game board.
    ///
    /// For the simplicity of implementation, a board can be also viewed as a piece, which is a Union of all pieces (shapes) it holds. 
    /// </summary>    
    public abstract class Board2D : Piece
    {
        public Board2D(uint width = 10, uint height = 10) : base(0, 0)
        {
            Height = height;
            Width = width;
        }

        public uint Height { get; }
        public uint Width { get; }  

        public bool TryAdd(Piece piece)
        {
            if (piece == null)
                return false;

            if (piece.GetPoints().Any(p => p.X >= Width || p.Y >= Height))
            {
                // out of bounds:
                return false;
            }

            return TryAddImpl(piece);
        }

        public Board2D Add(Piece piece)
        {
            TryAdd(piece);
            return this;
        }

        protected abstract bool TryAddImpl(Piece piece);

        public bool IsGameLost => IsDead;

        public override string ToString() => $"{GetType()} {Width}x{Height}";

        // See the base Piece class for the Attack method
    }
}
