using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRooms.MFUGE
{
    public class Inventory
    {
        private readonly int _capacity;
        private readonly List<IItem> _items;
        public int Count => _items.Count(item => item != null);

        public Inventory(int capacity)
        {
            _capacity = capacity;
            _items = new List<IItem>(_capacity);
            for (var i = 0; i < _capacity; i++)
                _items.Add(null);
        }

        public bool TryPutItem(IItem item)
        {
            if (_capacity == Count) return false;
            for (var i = 0; i < _capacity; i++)
                if (_items[i] == null)
                {
                    _items[i] = item;
                    break;
                }

            return true;
        }

        public IReadOnlyList<(IItem, int)> GetItems()
        {
            var result = new List<(IItem, int)>();

            for (var i = 0; i < _capacity; i++)
                if (_items[i] != null)
                    result.Add((_items[i], i));

            return result;
        }

        public IItem TryPopItem(int index)
        {
            if (index < 0 || index >= _capacity)
                return null;
            var result = _items[index];
            _items[index] = null;
            return result;
        }
    }

    public interface IItem
    {
        // Но это не точно.
        Action<Engine> GetAction();
        string GetPictureDirectory();
    }
}
