using System;
using TheRooms.Domain;
using TheRooms.MFUGE;

namespace TheRooms.interfaces
{
    public interface ICreature
    {// TODO Разнести интерфейсы на абстракции
        event Action<Vector> StateChanged;
        double Health { get; }
        bool IsMortal { get; }

        Inventory Inventory { get; }
        Action<Game> GetActionOnClick();
        Action<Game> GetAction();
        void DoDamage(double value);
        string GetPictureDirectory();
        Vector GetLocation();
    }
}
