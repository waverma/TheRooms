using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Items
{
    public class Key : IItem
    {
        public Action<Game> GetAction()
        {
            return (Game game) => { game._playerStateBlock.Player.DoDamage(50); };
        }

        public string GetPictureDirectory()
        {
            return @"Images\Key.png";
        }

        public event Action StateChanged;
    }
}
