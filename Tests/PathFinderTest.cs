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

            var path = PathFinder.GetOrdinaryPath(_area, vectors[start], vectors[end]);

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
