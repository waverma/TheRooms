using System;
using TheRooms.interfaces;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public class DoorJacket : ICreature
    {
        public Inventory Inventory { get; set; }
        private readonly Vector location;
        private readonly Vector appearanceLocation;
        public readonly int AppearanceRoom;
        public bool IsMortal => false;
        private readonly IDoor door;

        public double Health { get; private set; }

        public event Action<Vector> StateChanged;

        public DoorJacket(Vector location, int otherRoom, Vector otherRoomLocation, IDoor door)
        {
            this.location = location;
            appearanceLocation = otherRoomLocation;
            AppearanceRoom = otherRoom;
            this.door = door;
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
                if (door.IsLock)
                {
                    var item = game.PlayerStateBlock.Player.Hand;
                    if (!(item is IKey)) return;
                    door.UnLock((IKey)item);
                    if (door.IsLock) return;
                }

                if (game.AreaBlock.TryChangeArea(AppearanceRoom))
                    game.AreaBlock.GetCurrentArea().MovePlayer(appearanceLocation);
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

    public class Door : IDoor
    {
        public bool IsLock { get; private set; }
        public int Index { get; }

        public Door(int index, bool isLock = true)
        {
            IsLock = isLock;
            Index = index;
        }

        public void UnLock(IKey key)
        {
            if (Index != key.DoorIndex) return;
            IsLock = false;
        }

        public void Lock(IKey key)
        {
            if (Index != key.DoorIndex) return;
            IsLock = true;
        }
    }
}
