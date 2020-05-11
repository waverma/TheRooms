using TheRooms.Domain.Creatures;

namespace TheRooms.Domain.Rooms.Beginning
{
    public static class Doors
    {
        public static Door[] BegDoors = new Door[24];

        public static void CreateDoors()
        {
            for (var i = 0; i < 24; i++)
                BegDoors[i] = new Door(i);
        }
    }
}
