using System;
using TheRooms.MFUGE;

namespace TheRooms.Domain.LogicBlocks
{
    public class PlayerStateBlock
    { // TODO ДОБАВИТЬ НОРМАЛЬНУЮ РАБОТУ С РУКАМИ И НОВОЙ МЕХАНИКОЙ 
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
