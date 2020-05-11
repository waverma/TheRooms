using System;
using System.Collections.Generic;
using NUnit.Framework;
using TheRooms.Domain.Rooms;
using TheRooms.MFUGE;

namespace TheRooms.Tests
{
    public class PathFinderTest
    {
        private readonly Area area = AreasForShowAndTests.GetAreaForTests();

        [TestCase(0, 3, 3, false)]
        [TestCase(1, 3, 2, false)]
        [TestCase(3, 3, 1, false)]
        [TestCase(2, 3, 0, true)]
        [TestCase(4, 3, 0, true)]
        [TestCase(3, 4, 0, true)]
        [TestCase(3, 2, 0, true)]
        public void TestOrdinaryPathFinder(int start, int end, int length, bool isNull)
        {
            var vectors = new List<Vector>
            {
                new Vector(0, 0),
                new Vector(0, 1),
                new Vector(1, 0),
                new Vector(1, 1),
                new Vector(1, 2)
            };

            var path = Path.GetOrdinaryPath(area, vectors[start], vectors[end]);

            if (isNull)
                Assert.IsNull(path);
            else
                Assert.AreEqual(path.Length, length);
        }

        [Test]
        public void TestFlattenedPath()
        {
            throw new NotImplementedException();
        }
    }
}
