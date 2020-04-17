using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRooms.MFUGE
{
    public class Engine
    {
        /*
         * 1) Create Engine
         * 2) Add all area from folder
         *      1) CellBuilder in TextFromLines
         * 3) Set current area
         * 4) Add player
         *      1) set Location
         *      2) Add inventory
         *
         * + Set all event
         * + []add cellContent interacted
         *
         *
         *
         * Action:
         *  1) Move on Area +-
         *  2) Move Between Area +
         *  3) Click on ICreature +
         *  4) Put/Pop IItem on/from inventory +-
         *  5) Apply IItem to IC, IG, IS -
         *  6) All IC and so on must action on Tick (every Tick) +
         *  7) Interacting with self inventory and with self setting -+
         *  8) Create all module and control +-
         *  9) ...
         */

        private Area[] Areas;
        private int CurrentArea;
        private Player Player;

        public event Action StateChanged;

        public Engine(Area[] areas, Player player, int currentArea = 0)
        {
            Areas = areas;
            Player = player;
            CurrentArea = currentArea;
        }

        public Area GetCurrentArea() =>
            Areas[CurrentArea];

        private bool TryChangeArea()
        {
            throw new NotImplementedException();
        }

        public void ClickOnCell(Vector cellIndex)
        {
            throw new NotImplementedException();
        }

        //
        // Возможно, стоит перенести в Area
        //
        public static Vector FromPixelToCell(Size pixelSize, Size cellSize, Vector pixel)
        {
            throw new NotImplementedException();
        }

        public static Vector FromCellToPixel(Size pixelSize, Size cellSize, Vector cell)
        {
            throw new NotImplementedException();
        }
    }

    public class EngineBuilder
    {
        public EngineBuilder()
        {
            throw new NotImplementedException();
        }

        public EngineBuilder AddArea(Area area)
        {
            throw new NotImplementedException();
        }

        public EngineBuilder AddPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public EngineBuilder SetCurrentArea(int areaIndex)
        {
            throw new NotImplementedException();
        }

        public Engine Build()
        {
            throw new NotImplementedException();
        }
    }
}