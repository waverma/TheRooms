using NUnit.Framework;
using TheRooms.Domain.Creatures;
using TheRooms.Domain.Grounds;
using TheRooms.MFUGE;

namespace Tests
{
    public class CellTests
    {
        private Cell _emptyCell;
        private Cell _cellWithContent;
        [SetUp]
        public void Setup()
        {
            _emptyCell = new Cell(null, null, null);
            _cellWithContent = new Cell(null, new Chest(), new Grass(new Vector(0, 0)));
        }


        [Test]
        public void TestAddCreatureWhenEmptyCell()
        {
            var chest = new Chest();
            _emptyCell = _emptyCell.AddCreature(chest);
            Assert.AreEqual(_emptyCell.Creature, chest);
        }

        //[Test]
        //public void TestAddSkyWhenEmptyCell()
        //{
        //    var chest = new Chest();
        //    _emptyCell = _emptyCell.AddCreature(chest);

        //    Assert.ReferenceEquals(_emptyCell.Creature, chest);
        //}

        [Test]
        public void TestAddGroundWhenEmptyCell()
        {
            var grass = new Grass(new Vector(0, 0));
            _emptyCell = _emptyCell.AddGround(grass);
            Assert.AreEqual(_emptyCell.Ground, grass);
        }

        [Test]
        public void TestAddCreatureWhenCreaturePlaced()
        {
            var chest = new Chest();
            var oldChest = _cellWithContent.Creature;
            _cellWithContent = _cellWithContent.AddCreature(chest);
            Assert.AreEqual(_cellWithContent.Creature, oldChest);
        }

        //[Test]
        //public void TestAddSkyWhenSkyPlaced()
        //{
        //    
        //}

        [Test]
        public void TestAddGroundWhenGroundPlaced()
        {
            var grass = new Grass(new Vector(0, 0));
            var oldGrass = _cellWithContent.Ground;
            _cellWithContent = _cellWithContent.AddGround(grass);
            Assert.AreEqual(_cellWithContent.Ground, oldGrass);
        }


        [Test]
        public void TestRemoveCreatureWhenEmptyCell()
        {
            _emptyCell = _emptyCell.RemoveCreature();
            Assert.IsNull(_emptyCell.Creature);
        }

        //[Test]
        //public void TestRemoveSkyWhenEmptyCell()
        //{
        //    var chest = new Chest();
        //    _emptyCell = _emptyCell.AddCreature(chest);

        //    Assert.ReferenceEquals(_emptyCell.Creature, chest);
        //}

        [Test]
        public void TestRemoveGroundWhenEmptyCell()
        {
            _emptyCell = _emptyCell.RemoveGround();
            Assert.IsNull(_emptyCell.Ground);
        }

        [Test]
        public void TestRemoveCreatureWhenCreaturePlaced()
        {
            _cellWithContent = _cellWithContent.RemoveCreature();
            Assert.IsNull(_cellWithContent.Creature);
        }

        //[Test]
        //public void TestRemoveSkyWhenSkyPlaced()
        //{
        //    
        //}

        [Test]
        public void TestRemoveGroundWhenGroundPlaced()
        {
            _cellWithContent = _cellWithContent.RemoveGround();
            Assert.IsNull(_cellWithContent.Ground);
        }
    }
}