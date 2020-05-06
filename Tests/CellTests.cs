using NUnit.Framework;
using TheRooms.Domain.Creatures;
using TheRooms.Domain.Grounds;
using TheRooms.MFUGE;

namespace TheRooms.Tests
{
    public class CellTests
    {
        private Cell emptyCell;
        private Cell cellWithContent;
        [SetUp]
        public void Setup()
        {
            emptyCell = new Cell(null, null, null);
            cellWithContent = new Cell(null, new Chest(new Inventory(10), new Vector(0, 0)), new Grass(new Vector(0, 0)));
        }

        [Test]
        public void TestAddCreatureWhenEmptyCell()
        {
            var chest = new Chest(new Inventory(10), new Vector(0, 0));
            emptyCell = emptyCell.AddCreature(chest);
            Assert.AreEqual(emptyCell.Creature, chest);
        }

        [Test]
        public void TestAddGroundWhenEmptyCell()
        {
            var grass = new Grass(new Vector(0, 0));
            emptyCell = emptyCell.AddGround(grass);
            Assert.AreEqual(emptyCell.Ground, grass);
        }

        [Test]
        public void TestAddCreatureWhenCreaturePlaced()
        {
            var chest = new Chest(new Inventory(10), new Vector(0, 0));
            var oldChest = cellWithContent.Creature;
            cellWithContent = cellWithContent.AddCreature(chest);
            Assert.AreEqual(cellWithContent.Creature, oldChest);
        }

        [Test]
        public void TestAddGroundWhenGroundPlaced()
        {
            var grass = new Grass(new Vector(0, 0));
            var oldGrass = cellWithContent.Ground;
            cellWithContent = cellWithContent.AddGround(grass);
            Assert.AreEqual(cellWithContent.Ground, oldGrass);
        }

        [Test]
        public void TestRemoveCreatureWhenEmptyCell()
        {
            emptyCell = emptyCell.RemoveCreature();
            Assert.IsNull(emptyCell.Creature);
        }

        [Test]
        public void TestRemoveGroundWhenEmptyCell()
        {
            emptyCell = emptyCell.RemoveGround();
            Assert.IsNull(emptyCell.Ground);
        }

        [Test]
        public void TestRemoveCreatureWhenCreaturePlaced()
        {
            cellWithContent = cellWithContent.RemoveCreature();
            Assert.IsNull(cellWithContent.Creature);
        }

        [Test]
        public void TestRemoveGroundWhenGroundPlaced()
        {
            cellWithContent = cellWithContent.RemoveGround();
            Assert.IsNull(cellWithContent.Ground);
        }
    }
}
