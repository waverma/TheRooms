using NUnit.Framework;
using TheRooms.Domain.Items;
using TheRooms.MFUGE;

namespace TheRooms.Tests
{
    public class InventoryTests
    {
        private Inventory inventory;

        [SetUp]
        public void Setup()
        {
            inventory = new Inventory(2);
        }

        [Test]
        public void TestPut()
        {
            Assert.AreEqual(inventory.Count, 0);

            var popResult = inventory.TryPutItem(new Key());
            Assert.IsTrue(popResult);
            Assert.AreEqual(inventory.Count, 1);

            popResult = inventory.TryPutItem(new Key());
            Assert.IsTrue(popResult);
            Assert.AreEqual(inventory.Count, 2);

            popResult = inventory.TryPutItem(new Key());
            Assert.IsFalse(popResult);
            Assert.AreEqual(inventory.Count, 2);
        }

        [Test]
        public void TestPop()
        {
            var firstItem = new Key();
            var secondItem = new Key();
            inventory.TryPutItem(firstItem);
            inventory.TryPutItem(secondItem);

            Assert.AreEqual(inventory.TryPopItem(0), firstItem);
            Assert.AreEqual(inventory.Count, 1);
            Assert.IsNull(inventory.TryPopItem(0));
            Assert.AreEqual(inventory.Count, 1);
            Assert.AreEqual(inventory.TryPopItem(1), secondItem);
            Assert.AreEqual(inventory.Count, 0);
            Assert.IsNull(inventory.TryPopItem(1));
            Assert.AreEqual(inventory.Count, 0);
            Assert.IsNull(inventory.TryPopItem(-1));
            Assert.AreEqual(inventory.Count, 0);
            Assert.IsNull(inventory.TryPopItem(2));
            Assert.AreEqual(inventory.Count, 0);
        }

        [Test]
        public void TestGetItems()
        {
            var firstItem = new Key();
            var secondItem = new Key();
            inventory.TryPutItem(firstItem);
            inventory.TryPutItem(secondItem);

            var items = inventory.GetItems();

            Assert.AreEqual(items.Count, 2);
            Assert.AreEqual(inventory.TryPopItem(items[0].Item2), firstItem);
            Assert.AreEqual(inventory.TryPopItem(items[1].Item2), secondItem);
        }
    }
}
