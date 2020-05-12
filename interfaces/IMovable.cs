using TheRooms.MFUGE;

namespace TheRooms.interfaces
{
    public interface IMovable
    {
        double CellPerSecond { get; }
        Path Path { get; }
        MoveOrientation GetMoveOrientation();

        double GetCellShift();
    }

    public enum MoveOrientation
    {
        None = 0,
        Up = 1,
        Right = 2,
        Down = 3,
        Left = 4
    }
}
