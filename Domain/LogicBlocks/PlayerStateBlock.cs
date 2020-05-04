using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRooms.MFUGE;

namespace TheRooms.Domain.LogicBlocks
{
    public class PlayerStateBlock
    {
        public double Health => Player.Health;
        public double Mind => Player.Mind;
        public Player Player { get; private set; }

        public event Action PlayerStateBlockChanged;

        public PlayerStateBlock(Player player)
        {   
            Player = player;
            Player.StateChanged += () => PlayerStateBlockChanged?.Invoke();
        }
    }
}
