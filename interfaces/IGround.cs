using System;
using TheRooms.Domain;
using TheRooms.MFUGE;

namespace TheRooms.interfaces
{
    public interface IGround
    {
        Action<Game> GetAction();
        string GetPictureDirectory();
        Vector GetLocation();
    }
}
