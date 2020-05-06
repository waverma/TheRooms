using System;
using TheRooms.MFUGE;

namespace TheRooms.Domain.LogicBlocks
{
    public class InventoryBlock
    {
        public Inventory LeftInventory { get;}
        public Inventory RightInventory { get; private set; }

        public event Action InventoryBlockChanged;

        public InventoryBlock(Inventory inventory)
        {
            LeftInventory = inventory;
            LeftInventory.InventoryChanged += Inventory_InventoryChanged;
        }

        private void Inventory_InventoryChanged()
        {
            InventoryBlockChanged?.Invoke();
        }

        public void SetRightInventory(Inventory inventory)
        {
            RightInventory = inventory;
            RightInventory.InventoryChanged += Inventory_InventoryChanged;
            InventoryBlockChanged?.Invoke();
        }

        public void RemoveRightInventory()
        {
            if (RightInventory != null) RightInventory.InventoryChanged -= Inventory_InventoryChanged;
            RightInventory = null;
            InventoryBlockChanged?.Invoke();
        }

        public void TryMoveItemToRightInventory(int index)
        {
            LeftInventory.TryMoveItemTo(RightInventory, index);
            InventoryBlockChanged?.Invoke();
        }

        public void TryMoveItemToLeftInventory(int index)
        {
            RightInventory.TryMoveItemTo(LeftInventory, index);
            InventoryBlockChanged?.Invoke();
        }

        public void TryMoveAllItemsToRightInventory()
        {
            foreach (var item in LeftInventory.GetItems())
                TryMoveItemToRightInventory(item.Item2);
            InventoryBlockChanged?.Invoke();
        }

        public void TryMoveAllItemsToLeftInventory()
        {
            foreach (var item in RightInventory.GetItems())
                TryMoveItemToLeftInventory(item.Item2);
            InventoryBlockChanged?.Invoke();
        }
    }
}
