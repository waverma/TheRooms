using System.Drawing;
using System.Windows.Forms;
using TheRooms.Domain;
using TheRooms.interfaces;

namespace TheRooms.View
{
    public partial class PlayerStateControl : UserControl
    {
        private readonly Game _game;
        private readonly ProgressBar _healthBar;
        private readonly ProgressBar _mindBar;
        private bool HandChanged { get; set; }
        //private readonly Label _bulletsCount;

        public PlayerStateControl(Game game)
        {
            _game = game;

            _game.PlayerStateBlock.PlayerStateBlockChanged += () =>
            {
                HandChanged = true;
                Refresh();
            };
            Size = new Size(132, 528);

            _healthBar = new ProgressBar();
            _mindBar = new ProgressBar();
            //_bulletsCount = new Label();

            Controls.Add(_healthBar);
            Controls.Add(_mindBar);
            //Controls.Add(_bulletsCount); 

            Resize += HandleResize;
            InitializeComponent();
            SetSize();
        }

        private void HandleResize(object sender, System.EventArgs e)
        {
            SetSize();
        }

        private void SetSize()
        {
            _healthBar.Size = new Size(10 * 11, 11 * 2);
            _healthBar.Location = new Point(11, 11);

            _mindBar.Size = new Size(10 * 11, 11 * 2);
            _mindBar.Location = new Point(11, 55);

            //var gun = _game?.PlayerStateBlock?.Player?.Hand as IGun;


            //if (gun != null)
            //    e.Graphics.DrawImage(new Bitmap(gun.GetPictureDirectory()), new Rectangle(new Point(11, 10 * 11), new Size(55, 33)));

            //_bulletsCount.Size = new Size(10 * 11, 11 * 2);
            //_bulletsCount.Location = new Point(11 * 7, 99);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BackColor = Color.Brown;
            _healthBar.ForeColor = Color.Red;
            _healthBar.Value = (int)_game.PlayerStateBlock.Health;

            _mindBar.ForeColor = Color.Blue;
            _mindBar.Value = (int)_game.PlayerStateBlock.Mind;

            var hand = _game?.PlayerStateBlock?.Player?.Hand;
            if (hand != null && HandChanged)
            {
                e.Graphics.DrawImage(new Bitmap(hand.GetPictureDirectory()),
                    new Rectangle(new Point(11, 10 * 11), new Size(55, 33)));
                HandChanged = false;
            }

            //_bulletsCount.Text = gun?.ShowShop().BulletsCount + @"\" + gun?.ShowShop().Capacity ?? "ПУСТАЯ РУКА ЫЫЫЫЫЫЫ";



        }
    }
}
