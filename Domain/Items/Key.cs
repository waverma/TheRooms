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
        public Action<Engine> GetAction()
        {
            throw new NotImplementedException();
        }

        public string GetPictureDirectory()
        {
            throw new NotImplementedException();
        }
    }
}
