using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
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
            CurrentArea = currentArea > -1 && currentArea < Areas.Length
                ? currentArea : 0;

            if (Areas.Length == 0)
                CurrentArea = -1;
        }

        public Area GetCurrentArea()
        {
            if (CurrentArea < 0 || CurrentArea >= Areas.Length)
                return null;
            return Areas[CurrentArea];
        }

        public bool TryChangeArea(int index)
        {
            if (index <= -1 || index >= Areas.Length) return false;
            CurrentArea = index;
            return true;
        }

        public void ClickOnCell(Vector cellIndex)
        { // I have no idea what this method should do
            throw new NotImplementedException();
        }

        //
        // Возможно, стоит перенести в Area
        //
        public static Vector FromPixelToCell(Size pixelSize, Size cellSize, Vector pixel)
        { // TEST AND FIX ME
            if (pixelSize.Width % cellSize.Width != 0
                || pixelSize.Height % cellSize.Height != 0)
                throw new ArgumentException();

            var oneCellSize = new Size(pixelSize.Width / cellSize.Width,
                                pixelSize.Height / cellSize.Height);

            return new Vector(pixel.X / oneCellSize.Width, pixel.Y / oneCellSize.Height);
        }

        public static Vector FromCellToPixel(Size pixelSize, Size cellSize, Vector cell)
        { // TEST AND FIX ME
            if (pixelSize.Width % cellSize.Width != 0
                || pixelSize.Height % cellSize.Height != 0)
                throw new ArgumentException();

            var oneCellSize = new Size(pixelSize.Width / cellSize.Width,
                                       pixelSize.Height / cellSize.Height);

            return new Vector(cell.X * oneCellSize.Width + oneCellSize.Width / 2,
                              cell.Y * oneCellSize.Height + oneCellSize.Height / 2);
        }
    }

    public class EngineBuilder
    { // I don`t know how this test
        private readonly List<Area> _areas;
        private Player _player;
        private int CurrentArea { get; set; }

        public EngineBuilder()
        {
            _areas = new List<Area>();
        }

        public EngineBuilder AddArea(Area area)
        {
            _areas.Add(area);
            return this;
        }

        public EngineBuilder SetPlayer(Player player)
        {
            _player = player;
            return this;
        }

        public EngineBuilder SetCurrentArea(int areaIndex)
        {
            CurrentArea = areaIndex;
            return this;
        }

        public Engine Build()
        {
            var index = CurrentArea > -1
                        && CurrentArea < _areas.Count
                ? CurrentArea : 0;
            return new Engine(_areas.ToArray(), _player, index);
        }
    }
}