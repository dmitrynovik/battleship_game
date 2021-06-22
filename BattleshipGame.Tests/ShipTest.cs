using NUnit.Framework;
using FluentAssertions;

namespace BattleshipGame.Tests
{
    [TestFixture]
    public class ShipTest
    {
        [Test]
        public void Ship_Of_Size_2_Hit_Once_Should_Be_Alive() => MakeShip()
            .Hit(0, 0)
            .IsDead
            .Should().Be(false);

        [Test]
        public void Ship_Of_Size_2_Hit_Twice_Should_Be_Alive() => MakeShip()
            .Hit(0, 0)
            .Hit(1, 0)
            .IsDead
            .Should().Be(true);

        private Ship MakeShip() => new Ship(0, 0, Direction.Horizontal, 2);
    }
}