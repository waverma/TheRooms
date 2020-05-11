using System;
using TheRooms.interfaces;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Grounds
{
    public class Grass : IGround
    {
        private readonly Vector location;
        public Grass(Vector location)
        {
            this.location = location;
        }

        public Action<Game> GetAction()
        {
            return (Game game) => { };
        }

        public Vector GetLocation()
        {
            return location;
        }

        public string GetPictureDirectory()
        {
            return @"Images\Grass.png";
        }
    }
}