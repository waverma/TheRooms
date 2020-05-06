using System;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public class AbsolutelyDefaultPeople : IPeople
    {
        public double Health { get; private set; }
        private readonly string _name;
        private readonly string _fileName = @"Images\Artyom.png";
        private readonly Dialog _dialog;
        public bool IsMortal => true;

        private Vector Location { get; set; }

        public Inventory Inventory => new Inventory(10);

        public AbsolutelyDefaultPeople(Dialog dialog, string name, Vector location, string fileName = null)
        {
            Health = 100;
            Location = location;
            _dialog = dialog;
            _name = name;
            if (fileName != null) _fileName = fileName;
        }

        public event Action<Vector> StateChanged;

        public void DoDamage(double value)
        {
            Health -= value;
            if (Health > 0) return;
            StateChanged?.Invoke(GetLocation());
        }

        public Action<Game> GetAction()
        {
            return (Game game) => { };
        }

        public Action<Game> GetActionOnClick()
        {
            return (Game game) => game.DialogBlock.ChangeCreatureDialog(GetDialog());
        }

        public Dialog GetDialog()
        {
            return _dialog;
        }

        public Vector GetLocation()
        {
            return Location;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetPictureDirectory()
        {
            return _fileName;
        }
    }
}