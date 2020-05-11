using System.IO;
using TheRooms.Domain.Creatures;
using TheRooms.Domain.Grounds;
using TheRooms.Domain.Items;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Rooms.Beginning
{
    public static class Room2
    {
        public static Area GetRoom()
        {
            var map = new Cell[11, 10];

            for (var x = 1; x < 10; x++)
                for (var y = 1; y < 9; y++)
                    map[x, y] = new Cell(null, null, new Grass(new Vector(x, y)));

            // сундуки
            var v = new Vector(9, 4);
            var inv = new Inventory(10);
            inv.TryPutItem(new Key(1));
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new Chest(inv, v));

            v = new Vector(5, 8);
            inv = new Inventory(10);
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new Chest(inv, v));

            // люди
            v = new Vector(5, 1);
            var dialog = File.ReadAllLines(Game.Dialogs + "R1P3.txt");
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new AbsolutelyDefaultPeople(new Dialog(dialog, "Bread"), "Bread", v));

            // двери
            v = new Vector(0, 3);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 0, new Vector(13, 3), Doors.BegDoors[0]), new Grass(v));

            v = new Vector(1, 9);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 2, new Vector(15, 1), Doors.BegDoors[1]), new Grass(v));

            v = new Vector(10, 8);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 3, new Vector(1, 8), Doors.BegDoors[2]), new Grass(v));


            return new Area(map, new Vector(1, 3));
        }
    }
}
