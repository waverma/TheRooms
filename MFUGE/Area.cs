using System;
using System.Collections.Generic;
using System.Drawing;

namespace TheRooms.MFUGE
{
    public class Area
    {
        public int Width => Map.GetLength(0);
        public int Height => Map.GetLength(1);
        public Size Size => new Size(Width, Height);

        public readonly Cell[,] Map;
        public Vector PlayerLocation { get; private set; }

        public event Action<Vector> CellChanged;

        public Area(Cell[,] map, Vector playerLocation)
        {
            PlayerLocation = playerLocation;
            Map = map;

            foreach (var cell in map)
                if (cell?.Creature != null)
                    cell.Creature.StateChanged += (Vector vector) =>
                    {
                        if (cell.Creature.Health <= 0 && cell.Creature.IsMortal)
                            ChangeCell(vector, cell.RemoveCreature());
                        CellChanged?.Invoke(vector);
                    };
        }

        public void ChangeCell(Vector cellVector, Cell newCell)
        {
            if (!InBounds(cellVector)) return;
            Map[cellVector.X, cellVector.Y] = newCell;
            CellChanged?.Invoke(cellVector);
        }

        public IReadOnlyList<Vector> FindPathOrDefault(Vector start, Vector end)
        { // TODO TEST ME
            var ordinaryPath = Path.GetOrdinaryPath(this, start, end)?.ToReadOnlyList();
            //var flattenedPath = Path.GetFlattenedPath(this, ordinaryPath);
            return ordinaryPath;
        }

        public void MovePlayer(Vector newCell)
        {
            if (InBounds(newCell))
                PlayerLocation = newCell;
        }

        public static bool InBounds(Area area, Vector point)
        {
            return point.X > -1
                   && point.Y > -1
                   && point.X < area.Width
                   && point.Y < area.Height
                   && area.Map[point.X, point.Y] != null;
        }

        public bool InBounds(Vector point)
        {
            return Area.InBounds(this, point);
        }
    }
}
