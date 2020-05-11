using System;
using System.Drawing;
using System.Windows.Forms;
using TheRooms.Domain;

namespace TheRooms.View
{
    public partial class TheRooms : Form
    {
        private const int DefaultCellSize = 11;

        private readonly CreatureInteractControl _creatureInteractControl;
        private readonly AreaControl _areaControl;
        private readonly MenuControl _menuControl;
        private readonly InventoryControl _inventoryControl;
        private readonly PlayerStateControl _playerStateControl;

        private readonly Game _game;
        private readonly Timer _timer;

        public TheRooms(Game game)
        {
            game.StateChanged += state =>
            {
                if (state == GameState.Exit)
                    Close();
            };

            _creatureInteractControl = new CreatureInteractControl(game);
            _areaControl = new AreaControl(game);
            _menuControl = new MenuControl(game);
            _inventoryControl = new InventoryControl(game);
            _playerStateControl = new PlayerStateControl(game);

            // InitializeComponent();
            Size = new Size(1265 + 16, 715 + 11 + 28);
            Resize += HandleResize;
            _game = game;
            //_game.StateChanged += GameOnStateChanged;

            _timer = new Timer { Interval = 10 };
            //timer.Tick += TimerTick;
            _timer.Start();

            Controls.Add(_creatureInteractControl);
            Controls.Add(_areaControl);
            Controls.Add(_menuControl);
            Controls.Add(_inventoryControl);
            Controls.Add(_playerStateControl);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            SetSize();
        }

        private void HandleResize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void SetSize()
        {
            _creatureInteractControl.Location = new Point(0, 0);
            _creatureInteractControl.Size = new Size(ClientSize.Width, 8 * DefaultCellSize);

            _menuControl.Location = new Point(0, 8 * DefaultCellSize);
            _menuControl.Size = new Size(DefaultCellSize * 12, ClientSize.Height - 9 * DefaultCellSize - 8 * DefaultCellSize);

            _areaControl.Location = new Point(DefaultCellSize * 12, 8 * DefaultCellSize);
            _areaControl.Size = new Size(ClientSize.Width - DefaultCellSize * 12 * 2, ClientSize.Height - 9 * DefaultCellSize - 8 * DefaultCellSize);

            _playerStateControl.Location = new Point(ClientSize.Width - DefaultCellSize * 12, 8 * DefaultCellSize);
            _playerStateControl.Size = new Size(DefaultCellSize * 12, ClientSize.Height - 9 * DefaultCellSize - 8 * DefaultCellSize);

            _inventoryControl.Location = new Point(0, ClientSize.Height - 9 * DefaultCellSize);
            _inventoryControl.Size = new Size(ClientSize.Width, 9 * DefaultCellSize);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TheRooms
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "TheRooms";
            this.ResumeLayout(false);

        }
    }
}
