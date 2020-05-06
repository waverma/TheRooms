using NUnit.Framework;
using TheRooms.Domain.Creatures;
using TheRooms.Domain.Rooms;
using TheRooms.MFUGE;

namespace TheRooms.Tests
{
    public class AreaTests
    {
        private Area area;

        [SetUp]
        public void Setup()
        {
            area = AreasForShowAndTests.GetAreaForTests();
        }

        [Test]
        public void TestChangeCellWhenOldCellIsNull()
        {
            var newCell = new Cell(null, null, null);
            area.ChangeCell(new Vector(1, 0), newCell);
            Assert.IsNull(area.Map[1, 0]);
        }

        [Test]
        public void TestChangeCellWhenOldCellPlaced()
        {
            var newCell = new Cell(null, new Chest(new Inventory(10), new Vector(0, 0)), null);
            area.ChangeCell(new Vector(0, 0), newCell);
            Assert.AreEqual(area.Map[0, 0], newCell);
        }

        [Test]
        public void TestMovePlayer()
        {
            area.MovePlayer(new Vector(0, 1));
            Assert.AreEqual(area.PlayerLocation, new Vector(0, 1));
        }

        [Test]
        public void TestMovePlayerWhenIndexOutOfRange()
        {
            area.MovePlayer(new Vector(1, 0));
            Assert.AreEqual(area.PlayerLocation, new Vector(0, 0));
        }
        
        [Test]
        public void TestInBounds()
        {
            Assert.IsTrue(area.InBounds(new Vector(0,0)));
            Assert.IsTrue(area.InBounds(new Vector(0, 1)));
            Assert.IsTrue(area.InBounds(new Vector(1, 1)));

            Assert.IsFalse(area.InBounds(new Vector(1, 0)));
            Assert.IsFalse(area.InBounds(new Vector(-1, 0)));
            Assert.IsFalse(area.InBounds(new Vector(2, 0)));
            Assert.IsFalse(area.InBounds(new Vector(1, 2)));
        }
    }
}