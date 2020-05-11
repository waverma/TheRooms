using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRooms.interfaces
{
    public interface IKey : IItem
    {
        int DoorIndex { get; }
    }
}
