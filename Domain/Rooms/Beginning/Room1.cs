using System.IO;
using TheRooms.Domain.Creatures;
using TheRooms.Domain.Grounds;
using TheRooms.Domain.Items;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Rooms.Beginning
{
    public static class Room1
    {
        public static Area GetRoom()
        {
            var map = new Cell[15, 10];
            var playerLocation = new Vector(2, 2);

            for (var x = 1; x < 14; x++)
                for (var y = 1; y < 9; y++)
                    map[x, y] = new Cell(null, null, new Grass(new Vector(x, y)));

            // сундуки
            var v = new Vector(5, 1);
            var inv = new Inventory(10);
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new Chest(inv, v));

            v = new Vector(7, 1);
            inv = new Inventory(10);
            inv.TryPutItem(new Key(2));
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new Chest(inv, v));

            v = new Vector(9, 1);
            inv = new Inventory(10);
            inv.TryPutItem(new Pistol());
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new Chest(inv, v));

            v = new Vector(8, 8);
            inv = new Inventory(10);
            inv.TryPutItem(new Key(0));
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new Chest(inv, v));

            v = new Vector(10, 8);
            inv = new Inventory(10);
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new Chest(inv, v));
             
            v = new Vector(12, 8);
            inv = new Inventory(10);
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new Chest(inv, v));

            // люди
            v = new Vector(1, 8);
            var dialog = File.ReadAllLines(Game.Dialogs + "R1P1.txt");
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new AbsolutelyDefaultPeople(new Dialog(dialog, "Vincent"), "Vincent", v));

            v = new Vector(13, 1);
            dialog = File.ReadAllLines(Game.Dialogs + "R1P2.txt");
            map[v.X, v.Y] = map[v.X, v.Y].AddCreature(new AbsolutelyDefaultPeople(new Dialog(dialog, "Jules"), "Jules", v));

            // двери
            v = new Vector(14, 3);
            map[v.X, v.Y] = new Cell(null, new DoorJacket(v, 1, new Vector(1, 3), Doors.BegDoors[0]), new Grass(v));


            return new Area(map, playerLocation);
        }
    }
}
