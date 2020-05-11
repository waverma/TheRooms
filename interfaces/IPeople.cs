using TheRooms.Domain.Creatures;

namespace TheRooms.interfaces
{
    public interface IPeople : ICreature
    {
        string GetName();

        Dialog GetDialog();
    }
}
