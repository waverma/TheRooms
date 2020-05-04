using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRooms.Domain.Items;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public class Door : ICreature
    {
        public Inventory Inventory { get; set; }
        private readonly Vector _location;
        private readonly Vector _appearanceLocation;
        private readonly int _appearanceRoom;
        private bool IsLock;
        public bool IsMortal => false;

        public double Health { get; private set; }

        public event Action<Vector> StateChanged;

        public Door(Vector location, int otherRoom, Vector otherRoomLocation, bool isLock)
        {
            _location = location;
            _appearanceLocation = otherRoomLocation;
            _appearanceRoom = otherRoom;
            IsLock = isLock;
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
        { // переделать логику удаления ключa
            return (Game game) =>
            {
                if (IsLock)
                {
                    var keyIndex = -1;
                    foreach (var item in game._inventoryBlock.LeftInventory.GetItems())
                        if (item.Item1 is Key)
                        {
                            keyIndex = item.Item2;
                            break;
                        }

                    if (keyIndex != -1)
                    {
                        game._inventoryBlock.LeftInventory.TryPopItem(keyIndex);
                        IsLock = false;
                    }
                }

                if (!IsLock)
                {
                    if (game._areaBlock.TryChangeArea(_appearanceRoom))
                        game._areaBlock.GetCurrentArea().MovePlayer(_appearanceLocation);
                }
            };
        }

        public Vector GetLocation()
        {
            return _location;
        }

        public string GetPictureDirectory()
        {
            return @"Images\Door.png";
        }
    }
}
