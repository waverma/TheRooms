using System;
using TheRooms.Domain;

namespace TheRooms.interfaces
{
    public interface IItem
    { //  TODO ПЕРЕНЕСТИ В ДРУГОЕ МЕТСО
        // Но это не точно.
        Action<Game> GetAction();
        string GetPictureDirectory();

        event Action StateChanged;
    }
}
