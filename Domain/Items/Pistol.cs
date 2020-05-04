using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheRooms.MFUGE;

namespace TheRooms.Domain.Items
{
    public class Bullet
    { // NoTests
        public double Damage { get; }

        private Bullet(double damage)
        {
            Damage = damage;
        }

        public static Bullet GetPistolBullet()
        {
            return new Bullet(40);
        }
    }

    public class GunShop
    { // NoTests
        public readonly int Capacity;
        private readonly Stack<Bullet> _bullets = new Stack<Bullet>();
        public int BulletsCount => _bullets.Count;

        private GunShop(int capacity)
        {
            Capacity = capacity;
        }

        public Bullet PopBullet()
        {
            return BulletsCount == 0 
                ? null 
                : _bullets.Pop();
        }

        public bool TryPushBullet(Bullet bullet)
        {
            if (Capacity == BulletsCount) return false;
            _bullets.Push(bullet);
            return true;
        }

        public static GunShop GetPistolShop(int bulletCount = 0)
        {
            var shop = new GunShop(8);
            for (var i = 0; i < bulletCount; i++)
                shop.TryPushBullet(Bullet.GetPistolBullet());

            return shop;
        }
    }

    public class Pistol : IGun
    { // NoTests
        // УБИРАТЬ ПУЛЮ ТОЛЬКО ПОСЛЕ ПОЛНОГО ЗАВЕРШЕНИЯ ВЫСТРЕЛА ИЛИ НЕТ
        private GunShop Shop { get; set; }

        public event Action StateChanged;

        public Action<Game> GetAction()
        {
            return (Game game) => { };
        }

        public int GetBulletCount()
        {
            return Shop.BulletsCount;
        }

        public double GetCoolDown()
        {
            return 3.5;
        }

        public double GetDamage()
        {
            var b = Shop.PopBullet();
            var damage = b.Damage;
            Shop.TryPushBullet(b);
            return damage;
        }

        public string GetPictureDirectory()
        {
            return @"Images\Pistol.png";
        }

        public double GetRange()
        {
            return 8;
        }

        public bool TryShot(Cell cell)
        {
            var currentBullet = Shop.PopBullet();
            if (currentBullet == null) return false;
            cell.Creature?.DoDamage(currentBullet.Damage);

            StateChanged?.Invoke();
            return true;
        }

        public GunShop Reload(GunShop shop)
        {
            var oldShop = Shop;
            Shop = shop;
            StateChanged?.Invoke();
            return oldShop;
        }

        public GunShop ShowShop()
        { // Опасно
            return Shop;
        }

        public static Pistol GetRandomPistol()
        {
            var pistol = new Pistol();
            var shop = GunShop.GetPistolShop(8);
            for (var i = 0; i < new Random().Next(1, 8); i++)
                shop.TryPushBullet(Bullet.GetPistolBullet());
            pistol.Reload(shop);
            return pistol;
        }
    }
}
