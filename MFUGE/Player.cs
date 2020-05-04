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
        public double Health { get; private set; }
        public double Mind { get; private set; }
        public readonly string Name;
        public Inventory Inventory { get; }
        public IItem Hand { get; private set; }

        public event Action PlayerDeath;
        public event Action StateChanged;

        public Player(string name, Inventory inventory = null)
        {
            Mind = Health = 100;
            Name = name;
            Inventory = inventory ?? new Inventory(10);
        }

        public void DoDamage(double value)
        {
            Health -= value;
            if (Health <= 0)
                PlayerDeath?.Invoke(); 
            else
                StateChanged?.Invoke();
        }

        public string GetImageDirectory()
        {
            //return @"Images\Player.png";
            return @"Images\KillMe.png";
        }

        public IItem PutInHand(IItem item)
        {
            var oldItem = Hand;
            Hand = item;
            if (Hand != null)
                Hand.StateChanged += () => StateChanged?.Invoke();
            StateChanged?.Invoke();
            return oldItem;
        }
    }
}
