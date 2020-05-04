using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public class Chest : ICreature
    {
        public double Health { get; private set; }
        private bool IsLock { get; set; }
        private Vector _location { get; set; }
        public bool IsMortal => false;

        public Inventory Inventory { get; set; }


        public event Action<Vector> StateChanged;

        public Chest(Inventory inventory, Vector location)
        {
            IsLock = true;
            _location = location;
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
            throw new NotImplementedException();
        }

        public Action<Game> GetActionOnClick()
        {
            return (Game game) =>
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
            return _location;
        }

        public string GetPictureDirectory()
        {
            return IsLock 
                ? @"Images\chest-closed.png" 
                : @"Images\chest-opened.png";
        }
    }
}
