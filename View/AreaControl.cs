using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TheRooms.Domain;
using TheRooms.interfaces;
using TheRooms.MFUGE;
using Timer = System.Windows.Forms.Timer;

namespace TheRooms.View
{
    public partial class AreaControl : UserControl
    {
        private readonly Game game;
        private Area Area => game.AreaBlock.GetCurrentArea();
        private Vector PlayerLocation => Game.FromCellToPixel(new Size(CellSize.Width * Area.Width, CellSize.Width * Area.Height), game.AreaBlock.GetCurrentArea().Size, game.AreaBlock.GetCurrentArea().PlayerLocation);
        private Size CellSize
        {
            get
            {
                var cellW = Size.Width / Area.Width;
                var cellH = Size.Height / Area.Height;
                return cellW < cellH
                    ? new Size(cellW, cellW)
                    : new Size(cellH, cellH);
            }
        }

        private Point SizeShift => new Point((Size.Width - CellSize.Width * Area.Width) / 2,
            (Size.Height - CellSize.Height * Area.Height) / 2);

        private Image BlackSpaceImage => new Bitmap(@"Images\Path.png");
        private Image PlayerImage => throw new NotImplementedException();
        private Dictionary<string, Image> ReceivedImage = new Dictionary<string, Image>();

        private readonly Dictionary<int, Image> PlacedGround;
        private readonly Dictionary<int, Image> PlacedCreature;
        private readonly Dictionary<int, Image> PlacedSky;

        public AreaControl(Game save)
        {
            InitializeComponent();
            game = save;

            Size = new Size(1001, 528);

            Resize += HandleResize;
            Click += HandleClick;
            DoubleClick += HandleDoubleClick;
            Area.CellChanged += AreaOnCellChanged;

            var timer = new Timer { Interval = 41 };
            timer.Tick += (sender, args) =>
            {
                game.TickHandler.OnTick();
                Refresh();
            };
            timer.Start();
        }

        private void AreaOnCellChanged(Vector obj)
        {
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            BackColor = Color.Brown;
            DoubleBuffered = true;
            ShowArea(e.Graphics);
            ShowPlayer(e.Graphics);
        }

        private void HandleDoubleClick(object sender, EventArgs e)
        {
            //if (!(e is MouseEventArgs args)) return;
            //var clickLocation = new Vector(args.Location);
            //var cell = Game.FromPixelToCell(this.Size, new Size(Area.Width, Area.Height), clickLocation);
            //var currentCell = Area.Map[cell.X, cell.Y];
            //if (currentCell.Creature is null) return;
            //MessageBox.Show(currentCell.Creature.Health.ToString());
        }

        private void HandleClick(object sender, EventArgs e)
        {
            if (!(e is MouseEventArgs args)) return;
            var clickLocation = new Vector(args.Location.X - SizeShift.X, args.Location.Y - SizeShift.Y);
            var cell = Game.FromPixelToCell(new Size(CellSize.Width * Area.Width, CellSize.Width * Area.Height), new Size(Area.Width, Area.Height), clickLocation);
            game.TickHandler.Click(cell);
            Invalidate();
        }

        private void HandleResize(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ShowArea(Graphics g)
        {
            for (var x = 0; x < Area.Width; x++)
                for (var y = 0; y < Area.Height; y++)
                {
                    var currentCellLocation = new Point(x * CellSize.Width + SizeShift.X, y * CellSize.Height + SizeShift.Y);
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
                g.DrawImage(groundImage, new Rectangle(location, CellSize));
            if (creatureImage != null)
                g.DrawImage(creatureImage, new Rectangle(location, CellSize));
            if (skyImage != null)
                g.DrawImage(skyImage, new Rectangle(location, CellSize));
        }

        private void ShowPlayer(Graphics g)
        {
            var size = new Size(CellSize.Width * Area.Width, CellSize.Width * Area.Height);
            g.DrawImage(new Bitmap(game.PlayerStateBlock.Player.GetImageDirectory()),
                    new Rectangle(new Point(PlayerLocation.X - CellSize.Width / 2 + SizeShift.X, PlayerLocation.Y - CellSize.Height / 2 + SizeShift.Y), CellSize));
        }

        private void MovePlayer(IEnumerable<Vector> path)
        {
            foreach (var vector in path.Skip(1))
            {
                game.AreaBlock.GetCurrentArea().MovePlayer(vector);
                Refresh();
                Thread.Sleep(35);
            }
        }
    }
}
