using System;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Items
{
    public class Key : IItem
    {
        public Action<Game> GetAction()
        {
            return (Game game) => { game.PlayerStateBlock.Player.DoDamage(50); };
        }

        public string GetPictureDirectory()
        {
            return @"Images\Key.png";
        }

        public event Action StateChanged;
    }
}
