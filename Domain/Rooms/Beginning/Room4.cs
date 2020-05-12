using System.IO;
using TheRooms.Domain.Creatures;
using TheRooms.Domain.Grounds;
using TheRooms.Domain.Items;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Rooms.Beginning
{
    public class Room4
    { // TODO не выставлены точки спавна
        public static Area GetRoom()
        {
            var map = new Cell[29, 37];

            for (var x = 1; x < 28; x++)
                for (var y = 1; y < 36; y++)
                    map[x, y] = new Cell(null, null, new Grass(new Vector(x, y)));
            for (var i = 1; i <= 24; i++)
                for (var j = 1; j <= 28; j++)
                    map[i, j] = null;

            // двери
            var v = new Vector(24, 8);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 1, new Vector(9, 8), Doors.BegDoors[2]), new Grass(v));

            v = new Vector(28, 8);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 6, new Vector(1, 3), Doors.BegDoors[5]), new Grass(v));

            v = new Vector(28, 17);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 7, new Vector(1, 3), Doors.BegDoors[6]), new Grass(v));

            v = new Vector(28, 27);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 8, new Vector(1, 3), Doors.BegDoors[7]), new Grass(v));

            v = new Vector(14, 36);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 11, new Vector(1, 3), Doors.BegDoors[11]), new Grass(v));


            return new Area(map, new Vector(1, 7));
        }
    }
}
