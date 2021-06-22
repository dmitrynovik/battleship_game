using System.Linq;
using System.Collections.Generic;

namespace BattleshipGame
{
    /// <summary>
    /// The fast an memory-greedy implementation of 2D board:
    /// - only allocates memory for points occupied by pieces (e.g. ships)
    /// - computes a unique hash key for occupied points to do fast O(1) lookup
    /// </summary>    
    public class FastBoard2D : SparseBoard2D
    {
        private IDictionary<uint, Point2D> _points = new Dictionary<uint, Point2D>();

        public FastBoard2D(uint width = 10, uint height = 10) : base(width, height) {  }

        public override IEnumerable<Point2D> GetPoints() => _points.Values;

        protected override bool TryAddImpl(Piece piece)
        {
            var piecePoints = piece.GetPoints()
                .ToDictionary(ComputeUniquePointKey, p => p);

            if (piecePoints.Keys.Any(key => _points.ContainsKey(key)))
            {
                // point is already occupied by another piece => can't add a piece
                return false;                
            }

            foreach (var kvp in piecePoints)
            {
                _points.Add(kvp.Key, kvp.Value);
            }
            return true;
        }

        /// <summary>
        /// Computes the unique hash key for the point so that we can find occupied points in O(1)
        /// </summary>
        private uint ComputeUniquePointKey(Point2D point) => Width >= Height ?
                (Width * point.X) + point.Y :
                (Height * point.Y) + point.X;
    }
}
