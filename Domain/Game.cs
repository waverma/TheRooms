using System;
using System.Drawing;
using TheRooms.Domain.LogicBlocks;
using TheRooms.Domain.Rooms.Beginning;
using TheRooms.MFUGE;

namespace TheRooms.Domain
{
    public enum GameState
    {

    }

    public class Game
    {
        public const string Images = @"Images\";
        public const string Dialogs = @"Dialogs\";
        public const string Saves = @"Saves\";

        public readonly AreaBlock AreaBlock;
        public readonly InventoryBlock InventoryBlock;
        public readonly DialogBlock DialogBlock;
        public readonly MenuBlock MenuBlock;
        public readonly PlayerStateBlock PlayerStateBlock;

        public event Action<GameState> StateChanged;

        public Game()
            : this(GetAreas(), 0, new Player("Admin", new Inventory(10)))
        {
        }

        public Game(Area[] areas, int currentArea = 0, Player player = null)
        {
            if (player == null)
                player = new Player("Unknown", new Inventory(10));

            AreaBlock = new AreaBlock(areas, currentArea);
            InventoryBlock = new InventoryBlock(player.Inventory);
            DialogBlock = new DialogBlock();
            MenuBlock = new MenuBlock();
            PlayerStateBlock = new PlayerStateBlock(player);
        }

        private static Area[] GetAreas()
        {
            var area = Room1.GetRoom();
            return new Area[1] { area };
        }

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
