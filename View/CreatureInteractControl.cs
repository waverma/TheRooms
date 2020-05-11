using System;
using System.Drawing;
using System.Windows.Forms;
using TheRooms.Domain;
using TheRooms.Domain.Creatures;

namespace TheRooms.View
{
    public partial class CreatureInteractControl : UserControl
    {
        private readonly Game _game;

        private readonly Label _playerText = new Label();
        private readonly Label _creatureText = new Label();
        private readonly Button _prevButton = new Button();
        private readonly Button _nextButton = new Button();
        private readonly Button _closeButton = new Button();

        public CreatureInteractControl(Game game)
        {
            _prevButton.Text = "<";
            _nextButton.Text = ">";
            _closeButton.Text = "}{";

            InitializeComponent();
            Resize += HandleResize;
            game.DialogBlock.DialogBlockChanged += DialogBlock_DialogBlockChanged;

            _game = game;
            Size = new Size(115 * 11, 88);

            _prevButton.Click += (sender, args) =>
            {
                var (textOwner, text) = _game.DialogBlock.Dialog.GetPreviousLine();
                if (textOwner == CurrentTextOwner.Player)
                {
                    _playerText.Text = text;
                    _creatureText.Text = null;
                }
                else
                {
                    _creatureText.Text = text;
                    _playerText.Text = null;
                }
            };
            _nextButton.Click += (sender, args) =>
            {
                var (textOwner, text) = _game.DialogBlock.Dialog.GetNextLine();
                if (textOwner == CurrentTextOwner.Player)
                {
                    _playerText.Text = text;
                    _creatureText.Text = null;
                }
                else
                {
                    _creatureText.Text = text;
                    _playerText.Text = null;
                }
            };
            _closeButton.Click += (sender, args) =>
            {
                _game.DialogBlock.ChangeDialog(null);
            };

            Controls.Add(_playerText);
            Controls.Add(_creatureText);
            Controls.Add(_prevButton);
            Controls.Add(_nextButton);
            Controls.Add(_closeButton);

            DialogBlock_DialogBlockChanged();
        }

        private void DialogBlock_DialogBlockChanged()
        {
            var stateFlag = false;
            if (_game.DialogBlock.Dialog != null)
            {
                stateFlag = true;

                var (textOwner, text) = _game.DialogBlock.Dialog.GetNextLine();
                if (textOwner == CurrentTextOwner.Player)
                {
                    _playerText.Text = text;
                    _creatureText.Text = null;
                }
                else
                {
                    _creatureText.Text = text;
                    _playerText.Text = null;
                }
            }

            _creatureText.Visible = stateFlag;
            _playerText.Visible = stateFlag;

            _prevButton.Visible = stateFlag;
            _nextButton.Visible = stateFlag;
            _closeButton.Visible = stateFlag;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BackColor = Color.Brown;

            _playerText.BackColor = Color.Transparent;
            _creatureText.BackColor = Color.Transparent;
            _prevButton.BackColor = Color.Brown;
            _nextButton.BackColor = Color.Brown;
            _closeButton.BackColor = Color.Brown;

            _creatureText.BorderStyle = BorderStyle.None;
            _playerText.BorderStyle = BorderStyle.None;
            _prevButton.FlatStyle = FlatStyle.Flat;
            _nextButton.FlatStyle = FlatStyle.Flat;
            _closeButton.FlatStyle = FlatStyle.Flat;

            SetSize();
        }

        private void HandleResize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void SetSize()
        {
            var dialogFieldSize = new Size((int)(ClientSize.Width / 1.4556962), 33);
            var nextButtonSize = new Size((int)(ClientSize.Width / 57.5), 33);
            var closeButtonSize = new Size((int)(ClientSize.Width / 28.75), 33);

            _playerText.Size = dialogFieldSize;
            _playerText.Location = new Point((int)(ClientSize.Width / 6.05263157), 11);

            _creatureText.Size = dialogFieldSize;
            _creatureText.Location = new Point((int)(ClientSize.Width / 6.05263157), 11 * 4);

            _prevButton.Size = nextButtonSize;
            _prevButton.Location = new Point((int)(ClientSize.Width / 7.66666666), 11);

            _nextButton.Size = nextButtonSize;
            _nextButton.Location = new Point((int)(ClientSize.Width / 1.16161616161616), 11);

            _closeButton.Size = closeButtonSize;
            _closeButton.Location = new Point((int)(ClientSize.Width / 1.1274509), 11);
        }
    }
}
