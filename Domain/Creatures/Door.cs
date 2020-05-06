using System;
using System.Security.Cryptography.X509Certificates;
using TheRooms.Domain.Items;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public class Door : ICreature
    {
        public Inventory Inventory { get; set; }
        private readonly Vector _location;
        private readonly Vector _appearanceLocation;
        public readonly int AppearanceRoom;
        private bool _isLock;
        public bool IsMortal => false;

        public double Health { get; private set; }

        public event Action<Vector> StateChanged;

        public Door(Vector location, int otherRoom, Vector otherRoomLocation, bool isLock)
        {
            _location = location;
            _appearanceLocation = otherRoomLocation;
            AppearanceRoom = otherRoom;
            _isLock = isLock;
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
        { // ПЕРЕДЕЛАТЬ
            return game =>
            {
                if (_isLock)
                {
                    var keyIndex = -1;
                    foreach (var (item, index) in game._inventoryBlock.LeftInventory.GetItems())
                        if (item is Key)
                        {
                            keyIndex = index;
                            break;
                        }

                    if (keyIndex != -1)
                    {
                        game._inventoryBlock.LeftInventory.TryPopItem(keyIndex);
                        _isLock = false;
                    }
                }

                if (_isLock) return;
                var currentArea = game._areaBlock.GetCurrentArea();
                if (game._areaBlock.TryChangeArea(AppearanceRoom))
                {
                    game._areaBlock.GetCurrentArea().MovePlayer(_appearanceLocation);
                    foreach (var cell in game._areaBlock.GetCurrentArea().Map)
                        if (cell?.Creature is Door door &&
                                ReferenceEquals(game._areaBlock.GetArea(door.AppearanceRoom), currentArea))
                            door._isLock = false;
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
