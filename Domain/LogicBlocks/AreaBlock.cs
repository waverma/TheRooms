using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TheRooms.MFUGE;

namespace TheRooms.Domain.LogicBlocks
{
    public class AreaBlock
    {
        private Area[] Areas;
        private int CurrentArea;

        public event Action<List<Vector>> AreaBlockChanged;

        public AreaBlock(Area[] areas, int currentArea = 0)
        {
            Areas = areas;
            CurrentArea = currentArea > -1 && currentArea < Areas.Length
                ? currentArea : 0;

            if (Areas.Length == 0)
                CurrentArea = -1;

            if (GetCurrentArea() == null) return;
            GetCurrentArea().CellChanged += AreaBlockChanged_Handler;
        }

        public Area GetCurrentArea()
        {
            if (CurrentArea < 0 || CurrentArea >= Areas.Length)
                return null;
            return Areas[CurrentArea];
        }

        public bool TryChangeArea(int index)
        {
            if (index <= -1 || index >= Areas.Length) return false;

            GetCurrentArea().CellChanged -= AreaBlockChanged_Handler;
            CurrentArea = index;
            AreaBlockChanged?.Invoke(Vector.GetVectorsArray(GetCurrentArea().Width, GetCurrentArea().Height).ToList());
            GetCurrentArea().CellChanged += AreaBlockChanged_Handler;

            return true;
        }

        private void AreaBlockChanged_Handler(Vector vector)
        {
            AreaBlockChanged?.Invoke(new List<Vector> { vector });
        }

        public Size GetSize()
        {
            throw new NotImplementedException();
        }
    }
}
