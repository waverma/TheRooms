using System;
using System.Collections.Generic;
using System.Drawing;
using TheRooms.Domain;
using TheRooms.Domain.Creatures;
using TheRooms.Domain.Grounds;
using TheRooms.Domain.Items;

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

        private Area(Cell[,] map, Vector playerLocation)
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
        { // TEST ME
            var ordinaryPath = PathFinder.GetOrdinaryPath(this, start, end).ToReadOnlyList();
            if (ordinaryPath == null) return null;
            //var flattenedPath = PathFinder.GetFlattenedPath(this, ordinaryPath);
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

            var inv1 = new Inventory(10);
            inv1.TryPutItem(new Key());
            inv1.TryPutItem(Pistol.GetRandomPistol());

            var inv2 = new Inventory(10);
            inv2.TryPutItem(new Key());
            inv2.TryPutItem(new Key());

            var creatures = new Dictionary<Point, ICreature>
            {
                [new Point(1, 1)] = new AbsolutelyDefaultPeople(dialog, "Ортем", new Vector(1, 1)),
                [new Point(18, 18)] = new Chest(inv1, new Vector(18, 18)),
                [new Point(11, 11)] = new Chest(inv2, new Vector(11, 11))
                //[new Point(15, 0)] = new Door(new Vector(15, 0), 1, new Vector(15, 2), true)
            };

            for (var x = 1; x < 19; x++)
                for (var y = 1; y < 19; y++)
                {
                    cells[x, y] = new Cell(
                        null,
                        creatures.ContainsKey(new Point(x, y)) ? creatures[new Point(x, y)] : null,
                        new Grass(new Vector(x, y)));
                }
            cells[15, 0] = new Cell(null, new Door(new Vector(15, 0), 1, new Vector(16, 2), true), new Grass(new Vector(15, 0)));

            return new Area(cells, new Vector(5, 5));
        }

        public static Area GetAreaForShow2()
        {
            var cells = new Cell[20, 20];

            var inv1 = new Inventory(10);
            inv1.TryPutItem(new Key());

            var inv2 = new Inventory(10);
            inv2.TryPutItem(new Key());
            inv2.TryPutItem(new Key());

            var creatures = new Dictionary<Point, ICreature>
            {
                [new Point(18, 18)] = new Chest(inv1, new Vector(18, 18)),
                [new Point(11, 11)] = new Chest(inv2, new Vector(11, 11))
                //[new Point(15, 0)] = new Door(new Vector(15, 0), 1, new Vector(15, 2), true)
            };

            for (var x = 1; x < 19; x++)
            for (var y = 1; y < 19; y++)
            {
                cells[x, y] = new Cell(
                    null,
                    creatures.ContainsKey(new Point(x, y)) ? creatures[new Point(x, y)] : null,
                    new Grass(new Vector(x, y)));
            }
            cells[16, 0] = new Cell(null, new Door(new Vector(16, 0), 0, new Vector(15, 2), false), new Grass(new Vector(16, 0)));

            return new Area(cells, new Vector(5, 5));
        }

        public static Area GetAreaForTests()
        {
            var map = new Cell[2, 2];

            map[0, 0] = new Cell(null, null, null);
            map[0, 1] = new Cell(null, null, null);
            //map[1, 0] = new Cell(null, null, new Grass(new Vector(1, 1)));
            map[1, 1] = new Cell(null, null, new Grass(new Vector(1, 1)));

            return new Area(map, new Vector(0, 0));
        }
    }
}
