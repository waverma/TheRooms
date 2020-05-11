using TheRooms.Domain.Items;
using TheRooms.MFUGE;

namespace TheRooms.interfaces
{
    public interface IGun : IItem
    {
        GunShop ShowShop();
        double GetRange();
        double GetDamage();
        double GetCoolDown();
        int GetBulletCount();
        bool TryShot(Cell cell);
        GunShop Reload(GunShop shop);
    }
}
