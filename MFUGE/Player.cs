using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRooms.MFUGE
{
    public class Player
    {
        public readonly string Name;
        public Inventory Inventory { get; }
        public Vector Location { get; set; }

        public Player(string name, Vector location, Inventory inventory = null)
        {
            Name = name;
            Location = location;
            Inventory = inventory ?? new Inventory(24);
        }
    }
}
