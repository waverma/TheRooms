using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TheRooms.MFUGE;

namespace TheRooms.Domain.LogicBlocks
{
    public class AreaBlock
    {
        private readonly Area[] _areas;
        private int _currentArea;

        public event Action<List<Vector>> AreaBlockChanged;

        public AreaBlock(Area[] areas, int currentArea = 0)
        {
            _areas = areas;
            _currentArea = currentArea > -1 && currentArea < _areas.Length
                ? currentArea : 0;

            if (_areas.Length == 0)
                _currentArea = -1;

            if (GetCurrentArea() == null) return;
            GetCurrentArea().CellChanged += AreaBlockChanged_Handler;
        }

        public Area GetCurrentArea()
        {
            if (_currentArea < 0 || _currentArea >= _areas.Length)
                return null;
            return _areas[_currentArea];
        }

        public bool TryChangeArea(int index)
        {
            if (index <= -1 || index >= _areas.Length) return false;

            GetCurrentArea().CellChanged -= AreaBlockChanged_Handler;
            _currentArea = index;
            AreaBlockChanged?.Invoke(Vector.GetVectorsArray(GetCurrentArea().Width, GetCurrentArea().Height).ToList());
            GetCurrentArea().CellChanged += AreaBlockChanged_Handler;

            return true;
        }

        private void AreaBlockChanged_Handler(Vector vector)
        {
            AreaBlockChanged?.Invoke(new List<Vector> { vector });
        }

        public Area GetArea(int index)
        { // ОПАСНО
            return _areas[index];
        }

        public Size GetSize()
        { // Зачeм?
            throw new NotImplementedException();
        }
    }
}
