using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TheRooms.Domain;

namespace TheRooms.View
{
    public partial class MenuControl : UserControl
    {
        private readonly Game _game;
        private readonly IReadOnlyDictionary<string, Action<Game>> _buttonsFunction;
        private Size CurrentSize => new Size(132, 528);
        private Size ButtonSize => new Size(88, 55);

        private readonly Button _playPauseButton;
        private readonly Button _settingsButton;
        private readonly Button _saveButton;
        private readonly Button _showSavesPauseButton;
        private readonly Button _exitButton;

        private string _playPauseButtonText = "Pause";
        private readonly string _settingsButtonText = "settings";
        private readonly string _saveButtonText = "save";
        private readonly string _showSavesPauseButtonText = "saves";
        private readonly string _exitButtonText = "exit";

        public MenuControl(Game game)
        {
            InitializeComponent();
            _game = game;

            Size = CurrentSize;
            _buttonsFunction = _game.GetGameMenuButtonContent();

            _playPauseButton = new Button()
            {
                Text = _playPauseButtonText
            };

            _settingsButton = new Button()
            {
                Text = _settingsButtonText
            };

            _saveButton = new Button()
            {
                Text = _saveButtonText
            };

            _showSavesPauseButton = new Button()
            {
                Text = _showSavesPauseButtonText
            };

            _exitButton = new Button()
            {
                Text = _exitButtonText
            };

            _playPauseButton.Click += _playPauseButton_Click;
            _settingsButton.Click += _settingsButton_Click;
            _saveButton.Click += _saveButton_Click;
            _showSavesPauseButton.Click += _showSavesPauseButton_Click;
            _exitButton.Click += _ExitButton_Click;
        }

        private void _ExitButton_Click(object sender, EventArgs e)
        {
            _buttonsFunction[_exitButtonText](_game);
        }

        private void _showSavesPauseButton_Click(object sender, EventArgs e)
        {
            _buttonsFunction[_showSavesPauseButtonText](_game);
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _buttonsFunction[_saveButtonText](_game);
        }

        private void _settingsButton_Click(object sender, EventArgs e)
        {
            _buttonsFunction[_settingsButtonText](_game);
        }

        private void _playPauseButton_Click(object sender, EventArgs e)
        {
            _buttonsFunction["Play/Pause"](_game);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _playPauseButton.Size = ButtonSize;
            _settingsButton.Size = ButtonSize;
            _saveButton.Size = ButtonSize;
            _showSavesPauseButton.Size = ButtonSize;
            _exitButton.Size = ButtonSize;

            _playPauseButton.Location = new Point(22, 11);
            _settingsButton.Location = new Point(22, 88);
            _saveButton.Location = new Point(22, 165);
            _showSavesPauseButton.Location = new Point(22, 242);
            _exitButton.Location = new Point(22, 451);
        }
    }
}