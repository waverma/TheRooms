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

namespace TheRooms
{
    public partial class TheRooms : Form
    {
        private readonly Game Game;
        private readonly Timer timer;

        public TheRooms(Game game)
        {
            // InitializeComponent();
            Size = new Size(1265, 715);

            Game = game;
            Game.StateChanged += GameOnStateChanged;

            timer = new Timer { Interval = 10 };
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void GameOnStateChanged(GameState state)
        {
            throw new NotImplementedException();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnLoad(EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
