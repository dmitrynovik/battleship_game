using Functional.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipGame
{
    public class Ship : Piece
    {
        private readonly Point2D[] _points;

        public Ship(uint left, uint top, Direction orientation, uint size) : base(left, top)
        {
            if (size <= 0)
                throw new ArgumentException("Size must be a positive number", nameof(size));

            Size = size;
            Orientation = orientation;

            _points =  Enumerable
                .Range(0, Convert.ToInt32(Size))
                .Select(n => Orientation == Direction.Horizontal ?
                    new Point2D((uint)(left + n), top, this.ToMaybe<Piece>()) :
                    new Point2D(left, (uint)(top + n), this.ToMaybe<Piece>())
                )
                .ToArray();
        }

        public uint Size { get; }

        public Direction Orientation { get ;}

        public override void AddOrReplacePoint(Point2D point)
        {
            for (var i = 0; i < _points.Length; ++i)
            {
                if (_points[i].X == point.X && _points[i].Y == point.Y)
                {
                    _points[i] = point;
                    return;
                }
            }
        }

        public override IEnumerable<Point2D> GetPoints() => _points;

        public override string ToString() => $"{nameof(Ship)} (Left={Left},Top={Top},Size={Size},Orientation={Orientation})";
    }
}
