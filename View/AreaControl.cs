using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheRooms.Domain;
using TheRooms.MFUGE;

namespace TheRooms.View
{
    public partial class AreaControl : UserControl
    {
        private readonly Game Game;
        private Area Area => Game.Save.GetCurrentArea();
        private Vector PlayerLocation { get; set; }

        private Image BlackSpaceImage => throw new NotImplementedException();
        private Image PlayerImage => throw new NotImplementedException();
        private Dictionary<string, Image> ReceivedImage = new Dictionary<string, Image>();

        private readonly Dictionary<int, Image> PlacedGround;
        private readonly Dictionary<int, Image> PlacedCreature;
        private readonly Dictionary<int, Image> PlacedSky;

        public AreaControl(Game save)
        {
            InitializeComponent();
            Game = save;
            DoubleBuffered = true;
            Size = new Size(1001, 528);

            Resize += HandleResize;
            Click += HandleClick;
            DoubleClick += HandleDoubleClick;
            Area.CellChanged += AreaOnCellChanged;
        }

        private void AreaOnCellChanged(IReadOnlyList<Vector> obj)
        {
            throw new NotImplementedException();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ShowArea(e.Graphics);
            ShowPlayer(e.Graphics);
        }

        private void HandleDoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HandleClick(object sender, EventArgs e)
        {
            if (!(e is MouseEventArgs args)) return;
            var clickLocation = new Vector(args.Location);
            var cell = Engine.FromPixelToCell(this.Size, new Size(Area.Width, Area.Height), clickLocation);

            var currentCell = Area.Map[cell.X, cell.Y];
            if (currentCell is null) return;
            if (currentCell.Creature is null)
            {
                var path = Area.FindPathOrDefault(PlayerLocation, cell);

                if (path == null) return;
                MovePlayer(path);
                Area.MovePlayer(cell);
                return;
            }
            if (Area.PlayerLocation.IsNeighboringVector(cell))
            {// интерактирование с существом
                var creatureAction = Area.Map[cell.X, cell.Y].Creature.GetActionOnClick();
                creatureAction(Game);
            }
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
                        PaintCell(currentCellLocation, BlackSpaceImage, null, null);
                        continue;
                    }

                    var groundName = Area.Map[x, y].Ground.GetPictureDirectory();
                    var creatureName = Area.Map[x, y].Creature.GetPictureDirectory();
                    var skyName = Area.Map[x, y].Sky.GetPictureDirectory();

                    //
                    // Можно вынести в метод
                    //
                    var groundImage = ReceivedImage.ContainsKey(groundName)
                        ? ReceivedImage[groundName]
                        : FileHandler.GetImage(groundName);

                    var creatureImage = ReceivedImage.ContainsKey(creatureName)
                        ? ReceivedImage[creatureName]
                        : FileHandler.GetImage(creatureName);

                    var skyImage = ReceivedImage.ContainsKey(skyName)
                        ? ReceivedImage[skyName]
                        : FileHandler.GetImage(skyName);

                    PaintCell(currentCellLocation, groundImage, creatureImage, skyImage);
                }
        }

        private void PaintCell(Point location, Image groundImage, Image creatureImage, Image skyImage)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private void MovePlayer(List<Vector> path)
        {
            throw new NotImplementedException();
        }
    }
}
