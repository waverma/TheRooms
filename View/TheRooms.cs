using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheRooms.Domain;
using TheRooms.View;

namespace TheRooms
{
    public partial class TheRooms : Form
    {
        private readonly CreatureInteractControl _creatureInteractControl;
        private readonly AreaControl _areaControl;
        private readonly MenuControl _menuControl;
        private readonly InventoryControl _inventoryControl;
        private readonly PlayerStateControl _playerStateControl;

        private readonly Game _game;
        private readonly Timer _timer;

        public TheRooms(Game game)
        {
            _creatureInteractControl = new CreatureInteractControl(game);
            _areaControl = new AreaControl(game);
            _menuControl = new MenuControl(game);
            _inventoryControl = new InventoryControl(game);
            _playerStateControl = new PlayerStateControl(game);

            // InitializeComponent();
            Size = new Size(1265 + 16, 715 + 11 + 28);

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

        //private void GameOnStateChanged(GameState state)
        //{
        //    throw new NotImplementedException();
        //}

        //private void TimerTick(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override void OnLoad(EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            _creatureInteractControl.Location = new Point(0, 0);
            _areaControl.Location = new Point(12 * 11, 8 * 11);
            _menuControl.Location = new Point(0, 8 * 11);
            _inventoryControl.Location = new Point(0, 11 * 56);
            _playerStateControl.Location = new Point(103 * 11, 8 * 11);
        }

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override void OnKeyUp(KeyEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
