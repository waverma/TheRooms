using System;
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
        {   // TODO ALARM ALARM ALARM   CHANGE EVENT LOGIC
            Player = player;
            Player.StateChanged += () => PlayerStateBlockChanged?.Invoke();
        }
    }
}
