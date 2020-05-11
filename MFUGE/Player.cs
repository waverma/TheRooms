using System;
using TheRooms.Domain;
using TheRooms.interfaces;

namespace TheRooms.MFUGE
{
    public class Player
    { // 
        public double Health { get; private set; }
        public double Mind { get; private set; }
        public readonly string Name;
        public Inventory Inventory { get; }
        public IItem Hand { get; private set; }

        public Path Path { get; private set; }

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
            {
                Health = 0;
                PlayerDeath?.Invoke();
            }
            else
                StateChanged?.Invoke();
        }

        public void DecreaseMind(double value)
        {
            Mind -= value;
            if (Mind <= 0)
            {
                Mind = 0;
                // TODO СДЕСЬ ДОЛЖНЫ БЫТЬ ГОЛЮНЫ
            }
            StateChanged?.Invoke();
        }

        public void SetPath(Path path)
        {
            this.Path = path;
        }

        public void PeeTo(Cell cell)
        {

        }

        public Action<Game> GetAction()
        {
            DecreaseMind(0.05);
            return game =>
            {
                var currentVector = Path?.GetNext();
                if (currentVector != null)
                    game.AreaBlock.GetCurrentArea().MovePlayer((Vector)currentVector);
            };
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
