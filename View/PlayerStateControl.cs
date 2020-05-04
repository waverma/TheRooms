using System.Drawing;
using System.Windows.Forms;
using TheRooms.Domain;
using TheRooms.Domain.Items;

namespace TheRooms.View
{
    public partial class PlayerStateControl : UserControl
    {
        private readonly Game _game;
        private readonly ProgressBar _healthBar;
        private readonly ProgressBar _mindBar;
        private readonly Label _bulletsCount;

        public PlayerStateControl(Game game)
        {
            _game = game;
            InitializeComponent();
            _game._playerStateBlock.PlayerStateBlockChanged += Refresh;
            Size = new Size(132, 528);

            _healthBar = new ProgressBar();
            _mindBar = new ProgressBar();
            _bulletsCount = new Label();

            Controls.Add(_healthBar);
            Controls.Add(_mindBar);
            Controls.Add(_bulletsCount);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //_healthBar.Style = ProgressBarStyle.Blocks;
            _healthBar.ForeColor = Color.Red;
            _healthBar.Value = (int)_game._playerStateBlock.Health;
            _healthBar.Size = new Size(10 * 11, 11 * 2);
            _healthBar.Location = new Point(11, 11);

            _mindBar.ForeColor = Color.Blue;
            _mindBar.Value = (int)_game._playerStateBlock.Mind;
            _mindBar.Size = new Size(10 * 11, 11 * 2);
            _mindBar.Location = new Point(11, 55);

            var gun = _game?._playerStateBlock?.Player?.Hand as IGun;
            _bulletsCount.Text = gun?.ShowShop().BulletsCount + @"\" + gun?.ShowShop().Capacity ?? "ПУСТАЯ РУКА ЫЫЫЫЫЫЫ";
            _bulletsCount.Size = new Size(10 * 11, 11 * 2);
            _bulletsCount.Location = new Point(11 * 7, 99);

            if (gun != null)
                e.Graphics.DrawImage(new Bitmap(gun.GetPictureDirectory()), new Rectangle(new Point(11, 10 * 11), new Size(55, 33)));
        }
    }
}
