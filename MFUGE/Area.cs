using System;
using System.Collections.Generic;
using System.Drawing;
using TheRooms.Domain;
using TheRooms.Domain.Creatures;
using TheRooms.Domain.Grounds;

namespace TheRooms.MFUGE
{
    public class Area
    {
        public int Width => Map.GetLength(0);
        public int Height => Map.GetLength(1);

        public readonly Cell[,] Map;
        public Vector PlayerLocation { get; private set; }

        public event Action<Vector> CellChanged;

        private Area(Cell[,] map, Vector playerLocation)
        {
            PlayerLocation = playerLocation;
            Map = map;
        }

        public void ChangeCell(Vector cellVector, Cell newCell)
        {
            if (!InBounds(cellVector)) return;
            Map[cellVector.X, cellVector.Y] = newCell;
            CellChanged?.Invoke(cellVector);
        }

        public IReadOnlyList<Vector> FindPathOrDefault(Vector start, Vector end)
        { // TEST ME
            var ordinaryPath = PathFinder.GetOrdinaryPath(this, start, end).ToReadOnlyList();
            if (ordinaryPath == null) return null;
            var flattenedPath = PathFinder.GetFlattenedPath(this, ordinaryPath);
            return flattenedPath;
        }

        public void MovePlayer(Vector newCell)
        {
            if (InBounds(newCell))
                PlayerLocation = newCell;
        }

        public bool InBounds(Vector point)
        {
            return point.X > -1
                   && point.Y > -1
                   && point.X < Width
                   && point.Y < Height
                   && Map[point.X, point.Y] != null;
        }

        public static Area GetAreaForShow()
        {
            var cells = new Cell[20, 20];
            var text = new List<string>
            {
                "Первыя строка",
                "Вторая строка",
                "Третья строка",
                "Четвертоя строка",
                "Пятая строка"
            };
            var dialog = new Dialog(text);

            var creatures = new Dictionary<Point, ICreature>
            {
                [new Point(1, 1)] = new AbsolutelyDefaultPeople(dialog, "Ортем", new Vector(1, 1)),
            };

            for (var x = 1; x < 19; x++)
                for (var y = 1; y < 19; y++)
                {
                    cells[x, y] = new Cell(
                        null,
                        creatures.ContainsKey(new Point(x, y)) ? creatures[new Point(x, y)] : null,
                        new Grass(new Vector(x, y)));
                }

            return new Area(cells, new Vector(5, 5));
        }

        public static Area GetAreaForTests()
        {
            var map = new Cell[2, 2];

            map[0, 0] = new Cell(null, null, null);
            map[0, 1] = new Cell(null, new Chest(), null);
            //map[1, 0] = new Cell(null, null, new Grass(new Vector(1, 1)));
            map[1, 1] = new Cell(null, new Chest(), new Grass(new Vector(1, 1)));

            return new Area(map, new Vector(0, 0));
        }
    }

    public static class AreaBuilder
    {
        public static Area FromText(string text)
        {
            throw new NotImplementedException();
        }

        public static Area FromLines(string[] skyLines, string[] creatureLines, string[] groundLines)
        {
            throw new NotImplementedException();
        }

        public static string ToLine(Area area)
        {
            throw new NotImplementedException();
        }

        public static string ToText(string[] skyLines, string[] creatureLines, string[] groundLines)
        {
            throw new NotImplementedException();
        }
    }
}
