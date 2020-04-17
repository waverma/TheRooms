using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TheRooms.Domain;
using TheRooms.Domain.Creatures;
using TheRooms.Domain.Grounds;

namespace TheRooms.MFUGE
{
    public class Area
    {
        public int Width { get; }
        public int Height { get; }
        public readonly Cell[,] Map;
        public Vector PlayerLocation { get; set; }

        public event Action<IReadOnlyList<Vector>> CellChanged;

        private Area(Cell[,] map, Vector playerLocation)
        {
            PlayerLocation = playerLocation;
            Map = map;
        }

        public void ChangeCell(Vector cellVector, Cell newCell)
        {
            throw new NotImplementedException();
        }

        public List<Vector> FindPathOrDefault(Vector start, Vector end)
        {
            throw new NotImplementedException();
        }

        public void MovePlayer(Vector newCell)
        {
            throw new NotImplementedException();
        }

        public bool InBounds(Point point)
        {
            throw new NotImplementedException();
            // new Rectangle(0, 0, Width, Height).Contains(point);
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
