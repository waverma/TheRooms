using NUnit.Framework;
using TheRooms.Domain.Creatures;
using TheRooms.MFUGE;

namespace Tests
{
    public class AreaTests
    {
        private Area _area;

        [SetUp]
        public void Setup()
        {
            _area = Area.GetAreaForTests();
        }

        [Test]
        public void TestChangeCellWhenOldCellIsNull()
        {
            var newCell = new Cell(null, null, null);
            _area.ChangeCell(new Vector(1, 0), newCell);
            Assert.IsNull(_area.Map[1, 0]);
        }

        [Test]
        public void TestChangeCellWhenOldCellPlaced()
        {
            var newCell = new Cell(null, new Chest(new Inventory(10), new Vector(0, 0)), null);
            _area.ChangeCell(new Vector(0, 0), newCell);
            Assert.AreEqual(_area.Map[0, 0], newCell);
        }

        [Test]
        public void TestChangeCellWhenIndexOutOfRange()
        {
            // PASS // ﬂ ’«,  ¿  ›“Œ “≈—“»“‹
        }

        [Test]
        public void TestMovePlayer()
        {
            _area.MovePlayer(new Vector(0, 1));
            Assert.AreEqual(_area.PlayerLocation, new Vector(0, 1));
        }

        [Test]
        public void TestMovePlayerWhenIndexOutOfRange()
        {
            _area.MovePlayer(new Vector(1, 0));
            Assert.AreEqual(_area.PlayerLocation, new Vector(0, 0));
        }
        
        [Test]
        public void TestInBounds()
        {
            Assert.IsTrue(_area.InBounds(new Vector(0,0)));
            Assert.IsTrue(_area.InBounds(new Vector(0, 1)));
            Assert.IsTrue(_area.InBounds(new Vector(1, 1)));

            Assert.IsFalse(_area.InBounds(new Vector(1, 0)));
            Assert.IsFalse(_area.InBounds(new Vector(-1, 0)));
            Assert.IsFalse(_area.InBounds(new Vector(2, 0)));
            Assert.IsFalse(_area.InBounds(new Vector(1, 2)));
        }

        public void TestFindPath()
        {
            // PASS
        }
    }
}