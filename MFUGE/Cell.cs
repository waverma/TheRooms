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

        public bool IsEmpty()
        {
            return Creature == null;
        }

        public Cell AddCreature(ICreature creature)
        {
            return Creature != null
                ? this
                : new Cell(Sky, creature, Ground);
        }

        public Cell AddSky(ISky sky)
        {
            return Sky != null 
                ? this 
                : new Cell(sky, Creature, Ground);
        }

        public Cell AddGround(IGround ground)
        {
            return Ground != null
                ? this
                : new Cell(Sky, Creature, ground);
        }

        public Cell RemoveSky()
        {
            return new Cell(null, Creature, Ground);
        }

        public Cell RemoveCreature()
        {
            return new Cell(Sky, null, Ground);
        }

        public Cell RemoveGround()
        {
            return new Cell(Sky, Creature, null);
        }

        public static Cell GetRandomCell()
        {
            throw new NotImplementedException();
        }
    }
}