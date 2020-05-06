using NUnit.Framework;
using TheRooms.MFUGE;

namespace TheRooms.Tests
{
    public class VectorTests
    {
        public void TestOperation(
            Vector a, Vector b, int c, Vector expectedSum, 
            Vector expectedDiff, Vector expectedProduct, bool isEqual)
        {
            var sum = a + b;
            var diff = a - b;
            var product1 = a * c;
            var product2 = c * a;

            if (isEqual)
                Assert.AreEqual(a, b);
            else
                Assert.AreNotEqual(a, b);
            Assert.AreEqual(sum, expectedSum);
            Assert.AreEqual(diff, expectedDiff);
            Assert.AreEqual(product1, expectedProduct);
            Assert.AreEqual(product1, product2);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 1)]
        [TestCase(0, 1, 1)]
        [TestCase(-1, 0, 1)]
        [TestCase(0, -1, 1)]
        [TestCase(8, 6, 10)]
        [TestCase(6, 8, 10)]
        [TestCase(-8, 6, 10)]
        [TestCase(-6, 8, 10)]
        [TestCase(8, -6, 10)]
        [TestCase(6, -8, 10)]
        [TestCase(-8, -6, 10)]
        [TestCase(-6, -8, 10)]
        public void TestLength(int x, int y, double expectedLength)
        {
            Assert.AreEqual(new Vector(x, y).Length, expectedLength, 0.00001);
        }

        [TestCase(0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, true)]
        [TestCase(1, 0, 0, 1, 2, 1, 1, 1, -1, 2, 0, false)]
        public void TestC(
            int ax, int ay, int bx, int by, int c,
            int expectedSumX, int expectedSumY, int expectedDiffX, int expectedDiffY, 
            int expectedProductX, int expectedProductY, bool isEqual)
        {
            var a = new Vector(ax, ay);
            var b = new Vector(bx, by);
            var expectedSum = new Vector(expectedSumX, expectedSumY);
            var expectedDiff = new Vector(expectedDiffX, expectedDiffY);
            var expectedProduct = new Vector(expectedProductX, expectedProductY);

            TestOperation(a, b, c, expectedSum, expectedDiff, expectedProduct, isEqual);
        }
    }
}
