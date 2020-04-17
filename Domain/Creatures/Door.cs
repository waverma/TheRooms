using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public class Door : ICreature
    {
        public Inventory Inventory
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public event Action<Vector> CreatureDeath;

        public void DoDamage(double value)
        {
            throw new NotImplementedException();
        }

        public Action<Engine> GetAction()
        {
            throw new NotImplementedException();
        }

        public Action<Engine> GetActionOnClick()
        {
            throw new NotImplementedException();
        }

        public Vector GetLocation()
        {
            throw new NotImplementedException();
        }

        public string GetPictureDirectory()
        {
            throw new NotImplementedException();
        }

        Action<Game> ICreature.GetAction()
        {
            throw new NotImplementedException();
        }

        Action<Game> ICreature.GetActionOnClick()
        {
            throw new NotImplementedException();
        }
    }
}
