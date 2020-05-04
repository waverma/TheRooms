using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using NUnit.Framework;
using TheRooms.Domain;
using TheRooms.MFUGE;

namespace Tests
{
    public class EngineTests
    {
        private Game _engine;

        [Test]
        public void TestGetCurrentAreaWhenCurrentAreaWasSet()
        {
            var area = Area.GetAreaForShow();
            _engine = new Game(new[] { Area.GetAreaForShow(), area }, 1);

            Assert.AreEqual(_engine._areaBlock.GetCurrentArea(), area);
        }

        [Test]
        public void TestGetCurrentAreaWhenCurrentNotSet()
        {

            var area = Area.GetAreaForShow();
            _engine = new Game(new[] { area });

            Assert.AreEqual(_engine._areaBlock.GetCurrentArea(), area);
        }

        [Test]
        public void TestGetCurrentAreaWhenNoAreas()
        {
            _engine = new Game(new Area[0]);

            Assert.IsNull(_engine._areaBlock.GetCurrentArea());
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

            _engine = new Game(list.ToArray(), 0);

            var isSuccess = _engine._areaBlock.TryChangeArea(index);
            if (index < 0 || index >= 2)
            {
                Assert.IsFalse(isSuccess);
                Assert.AreEqual(list[0], _engine._areaBlock.GetCurrentArea());
            }
            else
            {
                Assert.IsTrue(isSuccess);
                Assert.AreEqual(list[index], _engine._areaBlock.GetCurrentArea());
            }
        }

        [TestCase(5, 3, 20, 12, 200, 72, 0, 0)]
        [TestCase(195, 3, 20, 12, 200, 72, 19, 0)]
        [TestCase(5, 69, 20, 12, 200, 72, 0, 11)]
        [TestCase(195, 69, 20, 12, 200, 72, 19, 11)]
        [TestCase(-1, -1, 20, 12, 200, 72, -1, -1)]
        public void TestFromPixelToCellWhenIndexIsCorrect(int pixelX, int pixelY,
            int cellsWight, int cellsHeight, int pixelsWight, int pixelsHeight,
            int cellX, int cellY)
        {
            var pixel = new Vector(pixelX, pixelY);
            var cells = new Size(cellsWight, cellsHeight);
            var pixels = new Size(pixelsWight, pixelsHeight);
            var cell = new Vector(cellX, cellY);

            var result = Game.FromPixelToCell(pixels, cells, pixel);

            Assert.AreEqual(cell, result);
        }

        [TestCase(0, 0, 20, 12, 200, 72, 5, 3)]
        [TestCase(19, 0, 20, 12, 200, 72, 195, 3)]
        [TestCase(0, 11, 20, 12, 200, 72, 5, 69)]
        [TestCase(19, 11, 20, 12, 200, 72, 195, 69)]
        [TestCase(-1, 0, 20, 12, 200, 72, -1, -1)]
        [TestCase(0, -1, 20, 12, 200, 72, -1, -1)]
        [TestCase(20, 0, 20, 12, 200, 72, -1, -1)]
        [TestCase(0, 12, 20, 12, 200, 72, -1, -1)]
        [TestCase(19, -1, 20, 12, 200, 72, -1, -1)]
        [TestCase(-1, 11, 20, 12, 200, 72, -1, -1)]
        [TestCase(20, 11, 20, 12, 200, 72, -1, -1)]
        [TestCase(12, 19, 20, 12, 200, 72, -1, -1)]
        public void TestFromCellToPixelWhenIndexIsCorrect(int cellX, int cellY,
            int cellsWight, int cellsHeight, int pixelsWight, int pixelsHeight,
            int pixelX, int pixelY)
        {
            var pixel = new Vector(pixelX, pixelY);
            var cells = new Size(cellsWight, cellsHeight);
            var pixels = new Size(pixelsWight, pixelsHeight);
            var cell = new Vector(cellX, cellY);

            var result = Game.FromCellToPixel(pixels, cells, cell);

            Assert.AreEqual(pixel, result);
        }
    }
}
