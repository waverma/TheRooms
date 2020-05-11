using System;
using TheRooms.interfaces;

namespace TheRooms.Domain.Items
{
    public class Key : IKey
    {
        public int DoorIndex { get; }
        
        public event Action StateChanged;

        public Key(int index = 0)
        {
            DoorIndex = index;
        }

        public Action<Game> GetAction()
        {
            return (Game game) => { game.PlayerStateBlock.Player.DoDamage(1); };
        }

        public string GetPictureDirectory()
        {
            return @"Images\Key.png";
        }
    }
}
