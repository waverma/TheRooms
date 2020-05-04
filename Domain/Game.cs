using System;
using System.Drawing;
using TheRooms.Domain.LogicBlocks;
using TheRooms.MFUGE;

namespace TheRooms.Domain
{
    public enum GameState
    {

    }

    public class Game
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


        public readonly AreaBlock _areaBlock;
        public readonly InventoryBlock _inventoryBlock;
        public readonly DialogBlock _dialogBlock;
        public readonly MenuBlock _menuBlock;
        public readonly PlayerStateBlock _playerStateBlock;

        public event Action<GameState> StateChanged;

        public Game()
            : this(GetAreas(), 0, new Player("Admin", new Inventory(10)))
        {
        }

        public Game(Area[] areas, int currentArea = 0, Player player = null)
        {
            if (player == null)
                player = new Player("Unknown", new Inventory(10));

            _areaBlock = new AreaBlock(areas, currentArea);
            _inventoryBlock = new InventoryBlock(player.Inventory);
            _dialogBlock = new DialogBlock();
            _menuBlock = new MenuBlock();
            _playerStateBlock = new PlayerStateBlock(player);
        }

        private static Area[] GetAreas() // загрузка карт из вне
        {
            var area = Area.GetAreaForShow();
            var area2 = Area.GetAreaForShow2();
            return new Area[2] { area, area2 };
        }


        //
        //
        //
        public static Vector FromPixelToCell(Size pixelSize, Size cellSize, Vector pixel)
        {
            if (pixel.X < 0 || pixel.Y < 0 
                            || pixel.X >= pixelSize.Width 
                            || pixel.Y >= pixelSize.Height) 
                return new Vector(-1, -1);

            var oneCellSize = new Size(pixelSize.Width / cellSize.Width,
                pixelSize.Height / cellSize.Height);

            return new Vector(pixel.X / oneCellSize.Width, pixel.Y / oneCellSize.Height);
        }

        public static Vector FromCellToPixel(Size pixelSize, Size cellSize, Vector cell)
        {
            if (cell.X < 0 || cell.Y < 0
                           || cell.X >= cellSize.Width 
                           || cell.Y >= cellSize.Height) 
                return new Vector(-1, -1);

            var oneCellSize = new Size(pixelSize.Width / cellSize.Width,
                pixelSize.Height / cellSize.Height);

            return new Vector(cell.X * oneCellSize.Width + oneCellSize.Width / 2,
                cell.Y * oneCellSize.Height + oneCellSize.Height / 2);
        }
    }
}
