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

namespace TheRooms.View
{
    public partial class CreatureInteractControl : UserControl
    {
        private readonly Game _game;

        private readonly Size _dialogFieldSize = new Size(79 * 11, 33);
        private readonly Size _closeButtonSize = new Size(66, 33);
        private readonly Size _nextButtonSize = new Size(22, 33);

        private readonly Label _playerText = new Label();
        private readonly Label _creatureText = new Label();
        private readonly Button _prevButton = new Button();
        private readonly Button _nextButton = new Button();
        private readonly Button _closeButton = new Button();

        public CreatureInteractControl(Game game)
        {
            BackColor = Color.RoyalBlue;

            InitializeComponent();
            game.DialogBlock.DialogBlockChanged += DialogBlock_DialogBlockChanged;

            _game = game;
            Size = new Size(115 * 11, 88);

            _prevButton.Click += (sender, args) =>
            {
                _creatureText.Text = _game.DialogBlock.GetPreviousCreatureString();
            };
            _nextButton.Click += (sender, args) =>
            {
                _creatureText.Text = _game.DialogBlock.GetNextCreatureString();
            };
            _closeButton.Click += (sender, args) =>
            {
                _game.DialogBlock.ChangeCreatureDialog(null);
                _game.DialogBlock.ChangePlayerDialog(null);
            };

            Controls.Add(_playerText);
            Controls.Add(_creatureText);
            Controls.Add(_prevButton);
            Controls.Add(_nextButton);
            Controls.Add(_closeButton);
        }

        private void DialogBlock_DialogBlockChanged()
        {
            var stateFlag = false;
            if (_game.DialogBlock.CreatureDialog != null)
                //&& _game._dialogBlock.PlayerDialog != null)
            {
                stateFlag = true;
                _creatureText.Text = _game.DialogBlock.GetNextCreatureString();
                _playerText.Text = _game.DialogBlock.GetNextPlayerString();
            }

            _creatureText.Visible = stateFlag;
            _playerText.Visible = stateFlag;

            _prevButton.Enabled = stateFlag;
            _nextButton.Enabled = stateFlag;
            _closeButton.Enabled = stateFlag;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            _playerText.BackColor = Color.Azure;
            _creatureText.BackColor = Color.Azure;
            _prevButton.BackColor = Color.Azure;
            _nextButton.BackColor = Color.Azure;
            _closeButton.BackColor = Color.Azure;

            _playerText.BorderStyle = BorderStyle.Fixed3D;
            _creatureText.BorderStyle = BorderStyle.Fixed3D;
            _prevButton.FlatStyle = FlatStyle.Flat;
            _nextButton.FlatStyle = FlatStyle.Flat;
            _closeButton.FlatStyle = FlatStyle.Flat;

            _playerText.Size = _dialogFieldSize;
            _playerText.Location = new Point(11 * 18, 11);

            _creatureText.Size = _dialogFieldSize;
            _creatureText.Location = new Point(11 * 18, 11 * 4);

            _prevButton.Size = _nextButtonSize;
            _prevButton.Location = new Point(11 * 14, 11);

            _nextButton.Size = _nextButtonSize;
            _nextButton.Location = new Point(11 * 98, 11);

            _closeButton.Size = _closeButtonSize;
            _closeButton.Location = new Point(11 * 101, 11);
        }
    }
}
