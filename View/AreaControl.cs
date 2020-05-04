using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TheRooms.Domain;
using TheRooms.Domain.Items;
using TheRooms.Domain.LogicBlocks;
using TheRooms.MFUGE;

namespace TheRooms.View
{
    public partial class AreaControl : UserControl
    {
        private readonly Game _game;//избавиться
        private Area Area => _game._areaBlock.GetCurrentArea();
        private Vector PlayerLocation => Game.FromCellToPixel(Size, _game._areaBlock.GetCurrentArea().Size, _game._areaBlock.GetCurrentArea().PlayerLocation);
        private Size _cellSize => new Size(this.Size.Width / _game._areaBlock.GetCurrentArea().Width, this.Size.Height / _game._areaBlock.GetCurrentArea().Height);

        private Image BlackSpaceImage => new Bitmap(@"Images\Path.png");
        private Image PlayerImage => throw new NotImplementedException();
        private Dictionary<string, Image> ReceivedImage = new Dictionary<string, Image>();

        private readonly Dictionary<int, Image> PlacedGround;
        private readonly Dictionary<int, Image> PlacedCreature;
        private readonly Dictionary<int, Image> PlacedSky;

        private readonly Graphics G;

        public AreaControl(Game save)
        {
            BackColor = Color.Aquamarine;

            InitializeComponent();
            _game = save;
            DoubleBuffered = true;
            Size = new Size(1001, 528);

            Resize += HandleResize;
            Click += HandleClick;
            DoubleClick += HandleDoubleClick;
            Area.CellChanged += AreaOnCellChanged;
        }

        private void AreaOnCellChanged(Vector obj)
        {
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ShowArea(e.Graphics);
            ShowPlayer(e.Graphics);
        }

        private void HandleDoubleClick(object sender, EventArgs e)
        {
            if (!(e is MouseEventArgs args)) return;
            var clickLocation = new Vector(args.Location);
            var cell = Game.FromPixelToCell(this.Size, new Size(Area.Width, Area.Height), clickLocation);
            var currentCell = Area.Map[cell.X, cell.Y];
            if (currentCell.Creature is null) return;
            MessageBox.Show(currentCell.Creature.Health.ToString());
        }

        private void HandleClick(object sender, EventArgs e)
        {
            if (!(e is MouseEventArgs args)) return;
            var clickLocation = new Vector(args.Location);
            var cell = Game.FromPixelToCell(this.Size, new Size(Area.Width, Area.Height), clickLocation);

            if (_game._areaBlock.GetCurrentArea().PlayerLocation == cell)
                return;

            var currentCell = Area.Map[cell.X, cell.Y];
            if (currentCell is null) return;
            if (currentCell.Creature is null)
            {
                _game._inventoryBlock.RemoveRightInventory();
                var path = Area.FindPathOrDefault(_game._areaBlock.GetCurrentArea().PlayerLocation, cell);

                if (path == null) return;
                MovePlayer(path);
                //Area.MovePlayer(cell);
                return;
            }

            if (!Area.PlayerLocation.IsNeighboringVector(cell))
            {
                if (currentCell.Creature == null)
                    return;
                if (_game._playerStateBlock.Player.Hand is IGun gun)
                    gun.TryShot(currentCell);
                return;
            }
            // интерактирование с существом
            var creatureAction = Area.Map[cell.X, cell.Y].Creature.GetActionOnClick();
            creatureAction(_game);

            Invalidate();
        }

        private void HandleResize(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShowArea(Graphics g)
        {
            var cellSize = new Size(Size.Width / Area.Width, Size.Height / Area.Height);

            for (var x = 0; x < Area.Width; x++)
                for (var y = 0; y < Area.Height; y++)
                {
                    var currentCellLocation = new Point(x * cellSize.Width, y * cellSize.Height);
                    if (Area.Map[x, y] is null)
                    {
                        PaintCell(g, currentCellLocation, BlackSpaceImage, null, null);
                        continue;
                    }

                    var groundName = Area.Map[x, y].Ground?.GetPictureDirectory();
                    var creatureName = Area.Map[x, y].Creature?.GetPictureDirectory();
                    var skyName = Area.Map[x, y].Sky?.GetPictureDirectory();

                    //
                    // Можно вынести в метод
                    //
                    var groundImage = groundName != null
                        ? ReceivedImage.ContainsKey(groundName)
                            ? ReceivedImage[groundName]
                            : new Bitmap(groundName)
                        : null;
                    if (groundName != null)
                        ReceivedImage[groundName] = groundImage;

                    var creatureImage = creatureName != null
                        ? ReceivedImage.ContainsKey(creatureName)
                            ? ReceivedImage[creatureName]
                            : new Bitmap(creatureName)
                        : null;
                    if (creatureName != null)
                        ReceivedImage[creatureName] = creatureImage;

                    var skyImage = skyName != null
                        ? ReceivedImage.ContainsKey(skyName)
                            ? ReceivedImage[skyName]
                            : new Bitmap(skyName)
                        : null;
                    if (skyName != null)
                        ReceivedImage[skyName] = skyImage;

                    PaintCell(g, currentCellLocation, groundImage, creatureImage, skyImage);
                }
        }

        private void PaintCell(Graphics g, Point location, Image groundImage, Image creatureImage, Image skyImage)
        {
            if (groundImage != null)
                g.DrawImage(groundImage, new Rectangle(location, _cellSize));
            if (creatureImage != null)
                g.DrawImage(creatureImage, new Rectangle(location, _cellSize));
            if (skyImage != null)
                g.DrawImage(skyImage, new Rectangle(location, _cellSize));
        }

        private void PlaceGround(string pictureName, Point location)
        {
            throw new NotImplementedException();
        }

        private void PlaceCreature(string pictureName, Point location)
        {
            throw new NotImplementedException();
        }

        private void PlaceSky(string pictureName, Point location)
        {
            throw new NotImplementedException();
        }

        private void ShowPlayer(Graphics g)
        {
            g.DrawImage(new Bitmap(_game._playerStateBlock.Player.GetImageDirectory()),
                    new Rectangle(new Point(PlayerLocation.X - _cellSize.Width / 2, PlayerLocation.Y - _cellSize.Height / 2), _cellSize));
        }

        private void MovePlayer(IEnumerable<Vector> path)
        {
            foreach (var vector in path.Skip(1))
            {
                _game._areaBlock.GetCurrentArea().MovePlayer(vector);
                Refresh();
                Thread.Sleep(10);
            }
        }
    }
}
