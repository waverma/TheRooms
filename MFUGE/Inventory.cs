using System;
using System.Collections.Generic;
using System.Linq;
using TheRooms.Domain;

namespace TheRooms.MFUGE
{
    public class Inventory
    {// TODO Обновить методы на основе новых свойств
        private readonly int _capacity;
        public List<IItem> Items { get; }
        public int Count => Items.Count(item => item != null);

        public bool IsEmpty => Count == 0;
        public bool IsFull => _capacity == Count;

        public event Action InventoryChanged;

        public Inventory(int capacity)
        {
            _capacity = capacity;
            Items = new List<IItem>(_capacity);
            for (var i = 0; i < _capacity; i++)
                Items.Add(null);
        }

        public bool TryPutItem(IItem item)
        {
            if (_capacity == Count) return false;
            for (var i = 0; i < _capacity; i++)
                if (Items[i] == null)
                {
                    Items[i] = item;
                    break;
                }

            InventoryChanged?.Invoke();
            return true;
        }

        public IReadOnlyList<(IItem, int)> GetItems()
        {
            var result = new List<(IItem, int)>();

            for (var i = 0; i < _capacity; i++)
                if (Items[i] != null)
                    result.Add((Items[i], i));

            return result;
        }

        public IItem TryPopItem(int index)
        {
            if (index < 0 || index >= _capacity)
                return null;
            var result = Items[index];
            Items[index] = null;
            InventoryChanged?.Invoke();
            return result;
        }

        public bool TryMoveItemTo(Inventory other, int itemIndex)
        {// TODO No tests
            if (other.IsFull) return false;
            var item = this.TryPopItem(itemIndex);
            if (item == null) return false;
            other.TryPutItem(item);
            return true;
        }
    }

    public interface IItem
    { //  TODO ПЕРЕНЕСТИ В ДРУГОЕ МЕТСО
        // Но это не точно.
        Action<Game> GetAction();
        string GetPictureDirectory();

        event Action StateChanged;
    }
}
