using TheRooms.MFUGE;

namespace TheRooms.Domain.Creatures
{
    public interface IPeople : ICreature
    {
        string GetName();

        Dialog GetDialog();
    }
}
