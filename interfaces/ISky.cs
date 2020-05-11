using System;
using TheRooms.Domain;
using TheRooms.MFUGE;

namespace TheRooms.interfaces
{
    public interface ISky
    {
        Action<Game> GetAction();
        void DoDamage(double value);
        string GetPictureDirectory();
        Vector GetLocation();
    }
}
