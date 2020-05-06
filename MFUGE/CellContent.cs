using System;
using System.Collections.Generic;
using TheRooms.Domain;

namespace TheRooms.MFUGE
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

    public interface IGround
    {
        Action<Game> GetAction();
        string GetPictureDirectory();
        Vector GetLocation();
    }

    public interface ISky
    {
        Action<Game> GetAction();
        void DoDamage(double value);
        string GetPictureDirectory();
        Vector GetLocation();
    }

    public class CellContent
    {
        private static Dictionary<string, Func<ISky>> SkyKind;
        private static Dictionary<string, Func<ICreature>> SkyCreature;
        private static Dictionary<string, Func<IGround>> SkyGround;

        public static bool TryAddSky(string line, Func<ISky> newSky)
        {
            throw new NotImplementedException();
        }

        public static bool TryAddCreature(string line, Func<ICreature> newCreature)
        {
            throw new NotImplementedException();
        }

        public static bool TryAddGround(string line, Func<IGround> newGround)
        {
            throw new NotImplementedException();
        }

        public static bool TryGetSky(string line)
        {
            throw new NotImplementedException();
        }

        public static bool TryGetCreature(string line)
        {
            throw new NotImplementedException();
        }

        public static bool TryGetGround(string line)
        {
            throw new NotImplementedException();
        }
    }
}
