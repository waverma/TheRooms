using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRooms.interfaces
{
    public interface IDoor
    {
        bool IsLock { get; }
        void UnLock(IKey key);
        void Lock(IKey Key);
    }
}
