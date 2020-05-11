using TheRooms.Domain.Creatures;
using TheRooms.Domain.Grounds;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Rooms.Beginning
{
    public static class Room3
    {
        public static Area GetRoom()
        {
            var map = new Cell[25, 6];

            for (var x = 1; x < 24; x++)
            for (var y = 1; y < 5; y++)
                map[x, y] = new Cell(null, null, new Grass(new Vector(x, y)));

            // двери
            var v = new Vector(15, 0);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 1, new Vector(15, 8), Doors.BegDoors[1]), new Grass(v));

            v = new Vector(2, 5);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 3, new Vector(2, 1), Doors.BegDoors[3]), new Grass(v));

            v = new Vector(22, 5);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 4, new Vector(8, 1), Doors.BegDoors[4]), new Grass(v));


            return new Area(map, new Vector(1, 7));
        }
    }
}
