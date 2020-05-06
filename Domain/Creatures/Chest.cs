using System;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public class Chest : ICreature
    {
        public double Health { get; private set; }
        private bool IsLock { get; set; }
        private Vector Location { get; set; }
        public bool IsMortal => false;

        public Inventory Inventory { get; set; }


        public event Action<Vector> StateChanged;

        public Chest(Inventory inventory, Vector location)
        {
            IsLock = true;
            Location = location;
            Inventory = inventory;
            Health = 100;
        }

        public void DoDamage(double value)
        {
            Health -= value;
            if (Health <= 0)
                StateChanged?.Invoke(GetLocation());
        }

        public Action<Game> GetAction()
        {
            return game => { };
        }

        public Action<Game> GetActionOnClick()
        {
            return game =>
            {
                void Lock()
                {
                    IsLock = true;
                    StateChanged?.Invoke(GetLocation());
                }

                game._inventoryBlock.InventoryBlockChanged -= Lock;
                IsLock = false;
                StateChanged?.Invoke(GetLocation());
                game._inventoryBlock.SetRightInventory(Inventory);
                game._inventoryBlock.InventoryBlockChanged += Lock;
            };
        }

        public Vector GetLocation()
        {
            return Location;
        }

        public string GetPictureDirectory()
        {
            return IsLock 
                ? @"Images\chest-closed.png" 
                : @"Images\chest-opened.png";
        }
    }
}
