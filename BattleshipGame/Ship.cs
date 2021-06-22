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

            _points =  Enumerable.Range(0, Convert.ToInt32(Size))
                .Select(n => Orientation == Direction.Horizontal ?
                    new Point2D((uint)(left + n), top, this) :
                    new Point2D(left, (uint)(top + n), this)
                )
                .ToArray();
        }

        public uint Size { get; }
        public Direction Orientation { get ;}

        public override IEnumerable<Point2D> GetPoints() => _points;

        public override string ToString() => $"{nameof(Ship)} (Left={Left},Top={Top},Size={Size},Orientation={Orientation})";
    }
}
