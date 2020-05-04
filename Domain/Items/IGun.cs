using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Items
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
