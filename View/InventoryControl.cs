using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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

        private readonly Panel _playerInventoryPanel;
        private readonly Panel _creatureInventoryPanel;
        private readonly Panel _inventoriesControls;

        private readonly Button AllToLeftButton;
        private readonly Button AllToRightButton;
        private readonly Button ToLeftButton;
        private readonly Button ToRightButton;
        private readonly Button CloseButton;

        private int SelectedLeftItem { get; set; } = -1;
        private int SelectedRightItem { get; set; } = -1;

        public InventoryControl(Game game)
        {
            game._inventoryBlock.InventoryBlockChanged += Refresh;
            DoubleBuffered = true;
            _game = game;
            BackColor = Color.RoyalBlue;
            Size = new Size(115 * 11, 99);

            _playerInventoryPanel = new Panel();
            _creatureInventoryPanel = new Panel();
            _inventoriesControls = new Panel();

            ToRightButton = new Button { Text = ">" };
            ToRightButton.Click += (sender, args) =>
            {
                _game._inventoryBlock.TryMoveItemToRightInventory(SelectedLeftItem);
            };

            AllToRightButton = new Button { Text = ">>>" };
            AllToRightButton.Click += (sender, args) =>
            {
                _game._inventoryBlock.TryMoveAllItemsToRightInventory();
            };

            var c = new Button
            {
                Size = new Size(33, 33),
                Location = new Point(13 * 11, 0)
            };
            c.Click += (sender, args) =>
            {
                if (SelectedLeftItem == -1) return;
                var item = _game._inventoryBlock.LeftInventory.Items[SelectedLeftItem];
                if (item == null) return;
                var oldItem = _game._playerStateBlock.Player.PutInHand(item);
                _game._inventoryBlock.LeftInventory.TryPopItem(SelectedLeftItem);
                _game._inventoryBlock.LeftInventory.TryPutItem(oldItem);
                Refresh();
            };

            AllToLeftButton = new Button { Text = "<<<" };
            AllToLeftButton.Click += (sender, args) =>
            {
                _game._inventoryBlock.TryMoveAllItemsToLeftInventory();
            };

            ToLeftButton = new Button { Text = "<" };
            ToLeftButton.Click += (sender, args) =>
            {
                _game._inventoryBlock.TryMoveItemToLeftInventory(SelectedRightItem);
            };

            CloseButton = new Button { Text = "Close" };
            CloseButton.Click += (sender, args) =>
            {
                _game._inventoryBlock.RemoveRightInventory();
            };

            // Когда я это переношу в OnPaint все начинает ломаться
            ToRightButton.Size = new Size(55, 22);
            ToRightButton.Location = new Point(11, 0);

            AllToRightButton.Size = new Size(55, 22);
            AllToRightButton.Location = Location = new Point(77, 0);

            AllToLeftButton.Size = new Size(55, 22);
            AllToLeftButton.Location = new Point(17 * 11, 0);

            ToLeftButton.Size = new Size(55, 22);
            ToLeftButton.Location = new Point(23 * 11, 0);

            CloseButton.Size = new Size(55, 33);
            CloseButton.Location = new Point(23 * 11, 44);


            Controls.Add(_playerInventoryPanel);
            Controls.Add(_creatureInventoryPanel);
            Controls.Add(_inventoriesControls);

            _inventoriesControls.Controls.Add(ToRightButton);
            _inventoriesControls.Controls.Add(AllToRightButton);
            _inventoriesControls.Controls.Add(c);
            _inventoriesControls.Controls.Add(AllToLeftButton);
            _inventoriesControls.Controls.Add(ToLeftButton);
            _inventoriesControls.Controls.Add(CloseButton);

            Draw();
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var i = 0;
            foreach (Control control in _playerInventoryPanel.Controls)
            { // А ВОТ ЗДЕСЬ ЗАРАБОТАЛО
                control.Text = _game?._inventoryBlock?.LeftInventory?.Items[i++]?.GetType().Name;

                //var picturePath = _game?._inventoryBlock?.LeftInventory?.Items[i++]?.GetPictureDirectory();
                //if (picturePath != null)
                //    control.CreateGraphics().DrawImage(new Bitmap(picturePath), new Rectangle(Point.Empty, new Size(44, 22)));
            }

            i = 0;
            foreach (Control control in _creatureInventoryPanel.Controls)
            {
                control.Text = _game?._inventoryBlock?.RightInventory?.Items[i++]?.GetType().Name;

                //var picturePath = _game?._inventoryBlock?.RightInventory?.Items[i++]?.GetPictureDirectory();
                //if (picturePath != null)
                //    control.CreateGraphics().DrawImage(new Bitmap(picturePath), new Rectangle(Point.Empty, new Size(44, 22)));

                //Graphics.FromImage(((PictureBox)control).Image).DrawImage(new Bitmap(picturePath), new Rectangle(Point.Empty, new Size(44, 22)));
            }

            _playerInventoryPanel.Visible = _game._inventoryBlock.LeftInventory != null;
            _creatureInventoryPanel.Visible = _game._inventoryBlock.RightInventory != null;
            _inventoriesControls.Visible = _game._inventoryBlock.RightInventory != null;
        }

        private void Draw()
        {
            _playerInventoryPanel.Size = new Size(30 * 11, 77);
            _playerInventoryPanel.Location = new Point(13 * 11, 11);
            _playerInventoryPanel.BackColor = Color.Aqua;
            ShowInventory(_playerInventoryPanel, Orientation.Left);


            _creatureInventoryPanel.Size = new Size(30 * 11, 77);
            _inventoriesControls.Size = new Size(29 * 11, 77);

            _creatureInventoryPanel.Location = new Point(72 * 11, 11);
            _inventoriesControls.Location = new Point(43 * 11, 11);

            _creatureInventoryPanel.BackColor = Color.Aqua;
            _inventoriesControls.BackColor = Color.BlueViolet;

            ShowInventory(_creatureInventoryPanel, Orientation.Right);
        }

        private void ShowInventory(Panel panel, Orientation orientation)
        {
            var i = 0;
            var startPoint = new Vector(11, 11);
            for (var y = 0; y < 2; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    var picturePath = _game?._inventoryBlock?.RightInventory?.Items[i]?.GetPictureDirectory();

                    var itemPicture = new Button
                    {
                        Name = (i).ToString(),
                        //Enabled = false,
                        BackColor = Color.Chartreuse,
                        //Tag = i++,
                        Size = new Size(44, 22),
                        Location = startPoint.ToPoint()
                    };
                    //if (picturePath != null) itemPicture.Image = new Bitmap(picturePath);
                    i++;

                    startPoint += new Vector(66, 0);

                    itemPicture.Click += (sender, args) =>
                    {
                        if (SelectedLeftItem != -1)
                            foreach (var control in _playerInventoryPanel.Controls.Find(SelectedLeftItem.ToString(), false))
                                control.BackColor = Color.Chartreuse;
                        if (SelectedRightItem != -1)
                            foreach (var control in _creatureInventoryPanel.Controls.Find(SelectedRightItem.ToString(), false))
                                control.BackColor = Color.Chartreuse;

                        var picture = (Control)sender;
                        if (orientation == Orientation.Right)
                            SelectedRightItem = int.Parse(picture.Name);
                        else
                            SelectedLeftItem = int.Parse(picture.Name);
                        //picture.BackColor = Color.Black;
                    };

                    //itemPicture.DoubleClick += (sender, args) =>
                    //{
                    //    var picture = (Control)sender;
                    //    if (orientation == Orientation.Right)
                    //        SelectedRightItem = int.Parse(picture.Name);
                    //    else
                    //        SelectedLeftItem = int.Parse(picture.Name);

                    //    if (orientation == Orientation.Left)
                    //    {
                    //        _game._inventoryBlock.TryMoveItemToRightInventory(SelectedLeftItem);
                    //    }
                    //    else
                    //    {
                    //        _game._inventoryBlock.TryMoveItemToLeftInventory(SelectedRightItem);
                    //    }
                    //};

                    panel.Controls.Add(itemPicture);
                }
                startPoint += new Vector(-66 * 5, 33);
            }
        }
    }
}
