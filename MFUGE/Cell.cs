using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRooms.MFUGE
{
    public class Cell
    {
        public readonly ISky Sky;
        public readonly ICreature Creature;
        public readonly IGround Ground;

        public Cell(ISky sky, ICreature creature, IGround ground)
        {
            Sky = sky;
            Creature = creature;
            Ground = ground;
        }

        public Cell AddCreature(ICreature creature)
        {
            throw new NotImplementedException();
        }

        public Cell AddSky(ISky sky)
        {
            throw new NotImplementedException();
        }

        public Cell AddGround(IGround ground)
        {
            throw new NotImplementedException();
        }

        public Cell RemoveSky()
        {
            throw new NotImplementedException();
        }

        public Cell RemoveCreature()
        {
            throw new NotImplementedException();
        }

        public Cell RemoveGround()
        {
            throw new NotImplementedException();
        }

        public static Cell GetRandomCell()
        {
            throw new NotImplementedException();
        }
    }

    public class CellBuilder
    {
        public CellBuilder()
        {
            throw new NotImplementedException();
        }

        public Cell AddSky(ISky sky)
        {
            throw new NotImplementedException();
        }

        public Cell AddCreature(ICreature creature)
        {
            throw new NotImplementedException();
        }

        public Cell AddCurrentGround(IGround ground)
        {
            throw new NotImplementedException();
        }

        public Cell Build()
        {
            throw new NotImplementedException();
        }
    }
}