using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TheRooms.MFUGE;

namespace Tests
{
    public class EngineTests
    {
        private Engine _engine;

        [Test]
        public void TestGetCurrentAreaWhenCurrentAreaWasSet()
        {
            var eb = new EngineBuilder();
            var area = Area.GetAreaForShow();
            eb = eb.AddArea(Area.GetAreaForTests());
            eb = eb.AddArea(area);
            eb = eb.SetPlayer(new Player("waverma", new Vector(0, 0)));
            eb = eb.SetCurrentArea(1);
            _engine = eb.Build();

            Assert.AreEqual(_engine.GetCurrentArea(), area);
        }

        [Test]
        public void TestGetCurrentAreaWhenCurrentNotSet()
        {
            var eb = new EngineBuilder();
            var area = Area.GetAreaForTests();
            eb = eb.AddArea(area);
            eb = eb.SetPlayer(new Player("waverma", new Vector(0, 0)));
            var engine = eb.Build();

            Assert.AreEqual(engine.GetCurrentArea(), area);
        }

        [Test]
        public void TestGetCurrentAreaWhenNoAreas()
        {
            var eb = new EngineBuilder();
            eb = eb.SetPlayer(new Player("waverma", new Vector(0, 0)));
            eb = eb.SetCurrentArea(1);
            var engine = eb.Build();

            Assert.IsNull(engine.GetCurrentArea());
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestTryChangeArea(int index)
        {
            var list = new List<Area>
            {
                Area.GetAreaForShow(),
                Area.GetAreaForTests()
            };

            var eb = new EngineBuilder();
            eb = eb.AddArea(list[0]);
            eb = eb.AddArea(list[1]);
            eb = eb.SetPlayer(new Player("waverma", new Vector(0, 0)));
            eb = eb.SetCurrentArea(0);
            var engine = eb.Build();

            var isSuccess = engine.TryChangeArea(index);
            if (index < 0 || index >= 2)
            {
                Assert.IsFalse(isSuccess);
                Assert.AreEqual(list[0], engine.GetCurrentArea());
            }
            else
            {
                Assert.IsTrue(isSuccess);
                Assert.AreEqual(list[index], engine.GetCurrentArea());
            }
        }

        [Test]
        public void TestFromPixelToCellWhenIndexIsCorrect()
        {
            throw new NotImplementedException();
        }

        //[Test]
        //public void TestFromPixelToCellWhenIndexIsIncorrect()
        //{
        //    throw new NotImplementedException();
        //}

        [Test]
        public void TestFromCellToPixelWhenIndexIsCorrect()
        {
            throw new NotImplementedException();
        }

        //[Test]
        //public void TestTryChangeAreaWhenIndexIsIncorrect()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
