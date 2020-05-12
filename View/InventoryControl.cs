using System.Drawing;
using System.Windows.Forms;
using TheRooms.Domain;
using TheRooms.MFUGE;

namespace TheRooms.View
{
    public enum Orientation
    {
        Left,
        Right
    }

    public partial class InventoryControl : UserControl
    {
        private readonly Game _game;

        private readonly TableLayoutPanel _playerInventoryPanel;
        private readonly TableLayoutPanel _creatureInventoryPanel;
        private readonly TableLayoutPanel _inventoriesControls;

        private readonly Button AllToLeftButton;
        private readonly Button AllToRightButton;
        private readonly Button ToLeftButton;
        private readonly Button ToRightButton;

        private int SelectedLeftItem { get; set; } = -1;
        private int SelectedRightItem { get; set; } = -1;

        public InventoryControl(Game game)
        {
            game.InventoryBlock.InventoryBlockChanged += Refresh;
            _game = game;
            
            Size = new Size(115 * 11, 99);

            _playerInventoryPanel = new TableLayoutPanel { ColumnCount = 5, RowCount = 2 };
            for (var i = 0; i < 5; i++)
                _playerInventoryPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
            for (var i = 0; i < 2; i++)
                _playerInventoryPanel.RowStyles.Add(new RowStyle(SizeType.Percent));

            _creatureInventoryPanel = new TableLayoutPanel();
            for (var i = 0; i < 5; i++)
                _creatureInventoryPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
            for (var i = 0; i < 2; i++)
                _creatureInventoryPanel.RowStyles.Add(new RowStyle(SizeType.Percent));

            _inventoriesControls = new TableLayoutPanel();
            _inventoriesControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
            _inventoriesControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
            _inventoriesControls.RowStyles.Add(new RowStyle(SizeType.Percent));
            _inventoriesControls.RowStyles.Add(new RowStyle(SizeType.Percent));
            _inventoriesControls.RowStyles[0].Height = 50;
            _inventoriesControls.RowStyles[1].Height = 50;
            _inventoriesControls.ColumnStyles[0].Width = 50;
            _inventoriesControls.ColumnStyles[1].Width = 50;

            ToRightButton = new Button { Text = ">", BackColor = Color.Brown, Dock = DockStyle.Fill};
            ToRightButton.Click += (sender, args) =>
                _game.InventoryBlock.TryMoveItemToRightInventory(SelectedLeftItem);

            AllToRightButton = new Button { Text = ">>>", BackColor = Color.Brown, Dock = DockStyle.Fill };
            AllToRightButton.Click += (sender, args) =>
                _game.InventoryBlock.TryMoveAllItemsToRightInventory();

            AllToLeftButton = new Button { Text = "<<<", BackColor = Color.Brown, Dock = DockStyle.Fill };
            AllToLeftButton.Click += (sender, args) =>
                _game.InventoryBlock.TryMoveAllItemsToLeftInventory();

            ToLeftButton = new Button { Text = "<", BackColor = Color.Brown, Dock = DockStyle.Fill };
            ToLeftButton.Click += (sender, args) =>
                _game.InventoryBlock.TryMoveItemToLeftInventory(SelectedRightItem);

            // Когда я это переношу в OnPaint все начинает ломаться
            ToRightButton.Size = new Size(55, 22);
            ToRightButton.Location = new Point(11, 0);

            AllToRightButton.Size = new Size(55, 22);
            AllToRightButton.Location = Location = new Point(77, 0);

            AllToLeftButton.Size = new Size(55, 22);
            AllToLeftButton.Location = new Point(17 * 11, 0);

            ToLeftButton.Size = new Size(55, 22);
            ToLeftButton.Location = new Point(23 * 11, 0);
            Controls.Add(_playerInventoryPanel);
            Controls.Add(_creatureInventoryPanel);
            Controls.Add(_inventoriesControls);

            _inventoriesControls.Controls.Add(ToRightButton, 0, 0);
            _inventoriesControls.Controls.Add(AllToRightButton, 0, 1);
            _inventoriesControls.Controls.Add(AllToLeftButton, 1, 1);
            _inventoriesControls.Controls.Add(ToLeftButton, 1, 0);


            Draw();
            ShowInventory(_playerInventoryPanel, Orientation.Left);
            ShowInventory(_creatureInventoryPanel, Orientation.Right);

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DoubleBuffered = true;
            BackColor = Color.Brown;

            var i = 0;
            foreach (Control control in _playerInventoryPanel.Controls)
                control.Text = _game?.InventoryBlock?.LeftInventory?.Items[i++]?.GetType().Name;

            i = 0;
            foreach (Control control in _creatureInventoryPanel.Controls)
                control.Text = _game?.InventoryBlock?.RightInventory?.Items[i++]?.GetType().Name;

            Draw();
            _playerInventoryPanel.Visible = _game.InventoryBlock.LeftInventory != null;
            _creatureInventoryPanel.Visible = _game.InventoryBlock.RightInventory != null;
            _inventoriesControls.Visible = _game.InventoryBlock.RightInventory != null;
        }

        private void Draw()
        { // 3.8 // 9.5
            _playerInventoryPanel.Size = new Size((int)(Size.Width / 3.8), 77);
            _playerInventoryPanel.Location = new Point((int)(Size.Width / 9.5), 11);
            _playerInventoryPanel.BackColor = Color.Transparent;

            _creatureInventoryPanel.Size = new Size((int)(Size.Width / 3.8), 77);
            _inventoriesControls.Size = new Size(Size.Width - ((int)(Size.Width / 3.8) + (int)(Size.Width / 9.5)) * 2, 77);

            _creatureInventoryPanel.Location = new Point(Size.Width - (int)(Size.Width / 3.8) - (int)(Size.Width / 9.5), 11);
            _inventoriesControls.Location = new Point((int)(Size.Width / 3.8) + (int)(Size.Width / 9.5), 11);

            _creatureInventoryPanel.BackColor = Color.Transparent;
            _inventoriesControls.BackColor = Color.Transparent;

            for (var j = 0; j < _playerInventoryPanel.RowStyles.Count; j++)
            {
                _playerInventoryPanel.RowStyles[j].SizeType = SizeType.Percent;
                _playerInventoryPanel.RowStyles[j].Height = 50;

                _creatureInventoryPanel.RowStyles[j].SizeType = SizeType.Percent;
                _creatureInventoryPanel.RowStyles[j].Height = 50;
            }

            for (var j = 0; j < _playerInventoryPanel.ColumnStyles.Count; j++)
            {
                _playerInventoryPanel.ColumnStyles[j].SizeType = SizeType.Percent;
                _playerInventoryPanel.ColumnStyles[j].Width = 20;

                _creatureInventoryPanel.ColumnStyles[j].SizeType = SizeType.Percent;
                _creatureInventoryPanel.ColumnStyles[j].Width = 20;
            }
        }

        private void ShowInventory(TableLayoutPanel panel, Orientation orientation)
        {
            var i = 0;
            var startPoint = new Vector(11, 11);
            for (var y = 0; y < 2; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    var picturePath = _game?.InventoryBlock?.RightInventory?.Items[i]?.GetPictureDirectory();

                    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));

                    var itemPicture = new Label
                    {
                        Name = (i).ToString(),
                        BackColor = Color.OrangeRed,
                        BorderStyle = BorderStyle.FixedSingle,
                        //Location = startPoint.ToPoint(),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    i++;

                    startPoint += new Vector(66, 0);

                    itemPicture.Click += (sender, args) =>
                    {
                        var picture = (Control)sender;
                        if (orientation == Orientation.Right)
                            SelectedRightItem = int.Parse(picture.Name);
                        else
                            SelectedLeftItem = int.Parse(picture.Name);
                    };

                    itemPicture.DoubleClick += (sender, args) =>
                    {
                        var picture = (Control)sender;
                        int currentSelectedItem;
                        Inventory currentInventory;
                        if (orientation == Orientation.Right)
                        {
                            currentSelectedItem = SelectedRightItem = int.Parse(picture.Name);
                            currentInventory = _game.InventoryBlock.RightInventory;
                        }
                        else
                        {
                            currentSelectedItem = SelectedLeftItem = int.Parse(picture.Name);
                            currentInventory = _game.InventoryBlock.LeftInventory;
                        }

                        if (currentSelectedItem == -1) return;
                        var item = currentInventory.Items[currentSelectedItem];
                        if (item == null) return;
                        var oldItem = _game.PlayerStateBlock.Player.PutInHand(item);
                        currentInventory.TryPopItem(currentSelectedItem);
                        currentInventory.TryPutItem(oldItem);
                        Refresh();
                    };

                    panel.Controls.Add(itemPicture, x, y);
                }
                startPoint += new Vector(-66 * 5, 33);
            }
        }
    }
}
