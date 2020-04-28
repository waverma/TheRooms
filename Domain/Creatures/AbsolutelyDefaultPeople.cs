using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public class AbsolutelyDefaultPeople : IPeople
    {
        private double Health { get; set; }
        private readonly string _name;
        private readonly string _fileName = "Artyom.png";
        private readonly Dialog _dialog;

        private Vector Location { get; set; }

        public Inventory Inventory => new Inventory(4);

        public AbsolutelyDefaultPeople(Dialog dialog, string name, Vector location, string fileName = null)
        {
            Location = location;
            _dialog = dialog;
            _name = name;
            if (fileName != null) _fileName = fileName;
        }

        public event Action<Vector> CreatureDeath;

        public void DoDamage(double value)
        {
            Health -= value;
            if (Health > 0) return;
            CreatureDeath?.Invoke(GetLocation());
        }

        public Action<Game> GetAction()
        {
            return (Game game) => { };
        }

        public Action<Game> GetActionOnClick()
        {
            return (Game game) => game.SetTextForShow(GetDialog().GetNextLine());
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