using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRooms.MFUGE
{
    public class Inventory
    {
        public void PutItem(IItem item)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IItem> GetItems()
        {
            throw new NotImplementedException();
        }

        public IItem PopItem(int index)
        {
            throw new NotImplementedException();
        }
    }

    public interface IItem
    {
        // Но это не точно.
        Action<Engine> GetAction();
        string GetPictureDirectory();
    }
}
