using System;
using System.Collections.Generic;
using TheRooms.interfaces;

namespace TheRooms.MFUGE
{
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
