using System;
using TheRooms.Domain;
using TheRooms.interfaces;

namespace TheRooms.MFUGE
{
    public class Player : IMovable
    { // 
        public double Health { get; private set; }
        public double Mind { get; private set; }
        public readonly string Name;
        public Inventory Inventory { get; }
        public IItem Hand { get; private set; }

        public Path Path { get; private set; }
        private MoveOrientation ori { get; set; }
        private int stepCount { get; set; }

        private int currentStep;

        public double CellPerSecond => 3;

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
            DecreaseMind(0.0005);
            return game =>
            {
                if (Path?.IsEmpty ?? true)
                    return;
                if (currentStep == 1)
                {
                    stepCount = 0;
                    var currentVector = Path?.GetNext();
                    if (currentVector != null)
                        game.AreaBlock.GetCurrentArea().MovePlayer((Vector)currentVector);
                }
                if (currentStep == 0)
                {
                    ori = GetRelativeOrientation(game.AreaBlock.GetCurrentArea().PlayerLocation, Path.Peek);
                    if (ori == MoveOrientation.None)
                    {
                        Path.GetNext();
                        ori = GetRelativeOrientation(game.AreaBlock.GetCurrentArea().PlayerLocation, Path.Peek);
                    }
                    stepCount = (int)(1000 / CellPerSecond) / game.TickHandler.TickInterval;
                    currentStep = stepCount;
                    return;
                }
                currentStep--;
            };
        }

        private static MoveOrientation GetRelativeOrientation(Vector first, Vector second)
        {
            if (first + new Vector(1, 0) == second)
                return MoveOrientation.Left;
            if (first + new Vector(0, 1) == second)
                return MoveOrientation.Down;
            if (first + new Vector(-1, 0) == second)
                return MoveOrientation.Right;
            if (first + new Vector(0, -1) == second)
                return MoveOrientation.Up;
            return MoveOrientation.None;
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

        public MoveOrientation GetMoveOrientation()
        {
            return ori;
        }

        public double GetCellShift()
        {
            if (stepCount == 0 || currentStep == 0) return 0;
            var result = 1.0 - (double)currentStep / stepCount;
            return result;
        }
    }
}
