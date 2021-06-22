using NUnit.Framework;
using FluentAssertions;

namespace BattleshipGame.Tests
{
    [TestFixture]
    public class Board2DTest
    {
        [Test]
        public void Can_Add_Horizontal_Ship_To_Board_Within_Board_Bounds() => MakeBoard()
            .TryAdd(new Ship(0, 0, Direction.Horizontal, 10))
            .Should().Be(true);

        [Test]
        public void Can_Add_Vertical_Ship_To_Board_Within_Board_Bounds() => MakeBoard()
            .TryAdd(new Ship(0, 0, Direction.Vertical, 10))
            .Should().Be(true);

        [Test]
        public void Cannot_Add_Horizontal_Ship_To_Board_OutOf_X_Bounds() => MakeBoard()
            .TryAdd(new Ship(1, 0, Direction.Horizontal, 10))
            .Should().Be(false);

        [Test]
        public void Cannot_Add_Vertical_Ship_To_Board_OutOf_X_Bounds() => MakeBoard()
            .TryAdd(new Ship(10, 0, Direction.Vertical, 10))
            .Should().Be(false);

        [Test]
        public void Cannot_Add_Horizontal_Ship_To_Board_OutOf_Y_Bounds() => MakeBoard()
            .TryAdd(new Ship(0, 10, Direction.Horizontal, 10))
            .Should().Be(false);

        [Test]
        public void Cannot_Add_Vertical_Ship_To_Board_OutOf_Y_Bounds() => MakeBoard()
            .TryAdd(new Ship(0, 1, Direction.Vertical, 10))
            .Should().Be(false);

        [Test]
        public void Can_Add_Ship_Which_DoesNot_Overlap_With_Another_Ship()
        {
            var board = MakeBoard();

            board.Add(new Ship(0, 0, Direction.Horizontal, 10));

            board.TryAdd(new Ship(2, 5, Direction.Vertical, 4))
                .Should().Be(true);
        }

        [Test]
        public void Cannot_Add_Ship_Which_Overlaps_With_Another_Ship()
        {
            var board = MakeBoard();

            board.Add(new Ship(0, 0, Direction.Horizontal, 10));

            board.TryAdd(new Ship(0, 5, Direction.Vertical, 10))
                .Should().Be(false);
        }

        [Test]
        public void When_Hit_All_Ships_But_One_Game_IsNot_Lost() => ((Board2D) MakeBoardWithShipsAndHitAllButOneCell())
            .IsGameLost.Should().Be(false);

        [Test]
        public void When_Hit_All_Ships_Game_Is_Lost() => ((Board2D) MakeBoardWithShipsAndHitAll())
            .IsGameLost.Should().Be(true);

        protected virtual Board2D MakeBoard() => new SparseBoard2D();

        private Board2D MakeBoardWithShips() => MakeBoard()
            .Add(new Ship(1, 1, Direction.Horizontal, 2))
            .Add(new Ship(1, 3, Direction.Vertical, 3));

        private Piece MakeBoardWithShipsAndHitAllButOneCell() => MakeBoardWithShips()
            .Hit(1, 1)
            .Hit(2, 1)
            .Hit(1, 3)
            .Hit(1, 4);

        private Piece MakeBoardWithShipsAndHitAll() => MakeBoardWithShipsAndHitAllButOneCell()
            .Hit(1, 5);
    }
}
