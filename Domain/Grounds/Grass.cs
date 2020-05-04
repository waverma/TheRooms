using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Grounds
{
    public class Grass : IGround
    {
        private readonly Vector _location;
        public Grass(Vector location)
        {
            _location = location;
        }

        public Action<Game> GetAction()
        {
            return (Game game) => { };
        }

        public Vector GetLocation()
        {
            return _location;
        }

        public string GetPictureDirectory()
        {
            return @"Images\Grass.png";
        }
    }
}