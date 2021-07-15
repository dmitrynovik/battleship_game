using Functional.Maybe;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipGame
{
    /// <summary>
    /// An abstract piece (shape), which is a collection of points, and can be added to a game board.
    /// </summary>    
    public abstract class Piece
    {
        public Piece(uint left, uint top)
        {
            Left = left;
            Top = top;
        }

        public uint Left { get; }
        public uint Top { get; }

        public abstract IEnumerable<Point2D> GetPoints();

        public abstract void AddOrReplacePoint(Point2D point);

        public virtual bool Attack(Point2D point)
        {
            if (point == null) 
                return false;

            var pt = GetPoints().FirstOrDefault(p => p.X == point.X && p.Y == point.Y);
            AddOrReplacePoint(pt?.Hit());
            return pt != null;
        }

        public bool Attack(uint x, uint y) => Attack(new Point2D(x, y, this.ToMaybe()));

        public Piece Hit(Point2D point)
        {
            Attack(point);
            return this;
        }

        public Piece Hit(uint x, uint y) => Hit(new Point2D(x, y, this.ToMaybe()));

        public bool IsDead => GetPoints().All(p => p.IsHit);
    }
}
