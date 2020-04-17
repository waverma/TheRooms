using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public interface IPeople : ICreature
    {
        string GetName();

        Dialog GetDialog();
    }
}
