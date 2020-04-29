using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TheRooms.Domain.Items;
using TheRooms.MFUGE;

namespace Tests
{
    public class InventoryTests
    {
        private Inventory _inventory;

        [SetUp]
        public void Setup()
        {
            _inventory = new Inventory(2);
        }

        [Test]
        public void TestPut()
        {
            Assert.AreEqual(_inventory.Count, 0);

            var popResult = _inventory.TryPutItem(new Key());
            Assert.IsTrue(popResult);
            Assert.AreEqual(_inventory.Count, 1);

            popResult = _inventory.TryPutItem(new Key());
            Assert.IsTrue(popResult);
            Assert.AreEqual(_inventory.Count, 2);

            popResult = _inventory.TryPutItem(new Key());
            Assert.IsFalse(popResult);
            Assert.AreEqual(_inventory.Count, 2);
        }

        [Test]
        public void TestPop()
        {
            var firstItem = new Key();
            var secondItem = new Key();
            _inventory.TryPutItem(firstItem);
            _inventory.TryPutItem(secondItem);

            Assert.AreEqual(_inventory.TryPopItem(0), firstItem);
            Assert.AreEqual(_inventory.Count, 1);
            Assert.IsNull(_inventory.TryPopItem(0));
            Assert.AreEqual(_inventory.Count, 1);
            Assert.AreEqual(_inventory.TryPopItem(1), secondItem);
            Assert.AreEqual(_inventory.Count, 0);
            Assert.IsNull(_inventory.TryPopItem(1));
            Assert.AreEqual(_inventory.Count, 0);
            Assert.IsNull(_inventory.TryPopItem(-1));
            Assert.AreEqual(_inventory.Count, 0);
            Assert.IsNull(_inventory.TryPopItem(2));
            Assert.AreEqual(_inventory.Count, 0);
        }

        [Test]
        public void TestGetItems()
        {
            var firstItem = new Key();
            var secondItem = new Key();
            _inventory.TryPutItem(firstItem);
            _inventory.TryPutItem(secondItem);

            var items = _inventory.GetItems();

            Assert.AreEqual(items.Count, 2);
            Assert.AreEqual(_inventory.TryPopItem(items[0].Item2), firstItem);
            Assert.AreEqual(_inventory.TryPopItem(items[1].Item2), secondItem);
        }
    }
}
