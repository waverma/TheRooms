using System;
using TheRooms.Domain.Items;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public class Door : ICreature
    {
        public Inventory Inventory { get; set; }
        private readonly Vector location;
        private readonly Vector appearanceLocation;
        public readonly int AppearanceRoom;
        private bool isLock;
        public bool IsMortal => false;

        public double Health { get; private set; }

        public event Action<Vector> StateChanged;

        public Door(Vector location, int otherRoom, Vector otherRoomLocation, bool isLock)
        {
            this.location = location;
            appearanceLocation = otherRoomLocation;
            AppearanceRoom = otherRoom;
            this.isLock = isLock;
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
        { // TODO ПЕРЕДЕЛАТЬ
            return game =>
            {
                if (isLock)
                {
                    var keyIndex = -1;
                    foreach (var (item, index) in game.InventoryBlock.LeftInventory.GetItems())
                        if (item is Key)
                        {
                            keyIndex = index;
                            break;
                        }

                    if (keyIndex != -1)
                    {
                        game.InventoryBlock.LeftInventory.TryPopItem(keyIndex);
                        isLock = false;
                    }
                }

                if (isLock) return;
                var currentArea = game.AreaBlock.GetCurrentArea();
                if (game.AreaBlock.TryChangeArea(AppearanceRoom))
                {
                    game.AreaBlock.GetCurrentArea().MovePlayer(appearanceLocation);
                    foreach (var cell in game.AreaBlock.GetCurrentArea().Map)
                        if (cell?.Creature is Door door &&
                                ReferenceEquals(game.AreaBlock.GetArea(door.AppearanceRoom), currentArea))
                            door.isLock = false;
                }
            };
        }

        public Vector GetLocation()
        {
            return location;
        }

        public string GetPictureDirectory()
        {
            return @"Images\Door.png";
        }
    }
}
