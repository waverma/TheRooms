using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TheRooms.MFUGE;

namespace Tests
{
    public class PathFinderTest
    {
        private readonly Area _area = Area.GetAreaForTests();

        [TestCase(0, 3, 3)]
        [TestCase(1, 3, 2)]
        [TestCase(3, 3, 1)]
        [TestCase(2, 3, 0)]
        [TestCase(4, 3, 0)]
        [TestCase(3, 4, 0)]
        [TestCase(3, 2, 0)]
        public void TestOrdinaryPathFinder(int start, int end, int length)
        {
            var vectors = new List<Vector>
            {
                new Vector(0, 0),
                new Vector(0, 1),
                new Vector(1, 0),
                new Vector(1, 1),
                new Vector(1, 2)
            };

            var path = PathFinder.GetOrdinaryPath(_area, vectors[start], vectors[end]);
            Assert.AreEqual(path.Length, length);
        }

        [Test]
        public void TestFlattenedPath()
        {
            throw new NotImplementedException();
        }
    }
}
