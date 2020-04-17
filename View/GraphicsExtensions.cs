using System.Drawing;

namespace TheRooms.View
{
    public static class GraphicsExtensions
    {
        private static Brush backgroundBrush = new SolidBrush(Color.FromArgb(0xF0, 0xF8, 0xFF));
        private static Pen cellBorderPen = new Pen(Color.FromArgb(0x77, 0xAA, 0xEE), 2);

        public static void DrawBackground(this Graphics graphics, int width, int height)
        {
            graphics.FillRectangle(backgroundBrush, 0, 0, width, height);
        }

        public static void DrawEmptyCell(this Graphics graphics, Rectangle rectangle)
        {
            graphics.DrawRectangle(cellBorderPen, rectangle);
        }
    }
}
