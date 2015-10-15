using System;
using System.Drawing;
using RaceGame.Structs;

namespace RaceGame
{
    public class Bullet
    {
        public float angle;
        public float speed;
        public int timeout;
        public string player;

        public void TrackBullet()
        {
            Console.WriteLine(Base.currentGame.player1.vehicle.weapon.bulletDrawInfo.x);
            Base.currentGame.player1.vehicle.weapon.bulletDrawInfo.x += (float)(Math.Cos(Base.currentGame.player1.vehicle.weaponDrawInfo.angle % 360 * (Math.PI / 180)) * speed);
            Base.currentGame.player1.vehicle.weapon.bulletDrawInfo.y += (float)(Math.Cos((90 - Base.currentGame.player1.vehicle.weaponDrawInfo.angle) % 360 * (Math.PI / 180)) * speed);

            if (timeout <= 0)
            {
                if(player == "player1")
                {
                    Base.drawInfos.Remove(Base.currentGame.player1.vehicle.weapon.bulletDrawInfo);
                    Base.gameTasks.Remove(TrackBullet);
                }
            }

            timeout--;
        }
    }

    public abstract class Weapons
    {
        public string name;
        public string type;
        public string spriteName;
        public int damage;
        public int fireRate;
        public float turnSpeed;
        public string turning;
        public DrawInfo bulletDrawInfo;
        public int weaponReloading;

        virtual public void shoot(string Player)
        {
            if (weaponReloading == 0) //IK heb grote ballen
            {
                if (Player == "player1")
                {
                    Console.WriteLine(Base.currentGame.player1.vehicle.weaponDrawInfo.angle);
                    //Big Dicks Over The Road Racing
                    Bitmap koegmap = new Bitmap("koegel.png");
                    bulletDrawInfo = new DrawInfo(koegmap, (int)Base.currentGame.player1.vehicle.drawInfo.x, (int)Base.currentGame.player1.vehicle.drawInfo.y, 10, 10, 0f, 0f, 0f, true, 200);
                    Base.drawInfos.Add(bulletDrawInfo);
                    Bullet Bullet = new Bullet();
                    Bullet.angle = Base.currentGame.player1.vehicle.weaponDrawInfo.angle;
                    Bullet.speed = 5;
                    Bullet.timeout = 120;
                    Bullet.player = Player;
                    Base.gameTasks.Add(Bullet.TrackBullet);
                    weaponReloading = fireRate;
                    Base.gameTasks.Add(weaponReload);
                }
            }
        }

        public void weaponReload()
        {
            weaponReloading--;
            if (weaponReloading == 0)
            {
                Base.gameTasks.Remove(weaponReload);
            }
        }
    }

    public class TankWeapon : Weapons
    {
        public TankWeapon()
        {
            name = "Tank Cannon";
            type = "Cannon";
            spriteName = "loop.png";
            damage = 75;
            fireRate = 20;
            turnSpeed = 3f;
            turning = "false";
        }
    }

    public class LAPVWeapon : Weapons
    {
        public LAPVWeapon()
        {
            name = "LAPV Turret";
            type = "MachineGun";
            spriteName = "lapvweapon.png";
            damage = 15;
            fireRate = 25;
            turnSpeed = 3;
        }
    }

    public class HorsePowerWeapon : Weapons
    {
        public HorsePowerWeapon()
        {
            name = "HorsePower Flamethrower";
            type = "Flamethrower";
            spriteName = "horsepowerweapon.png";
            damage = 5;
            fireRate = 10;
            turnSpeed = 3;
        }
    }

    public class MotorfietsWeapon : Weapons
    {
        public MotorfietsWeapon()
        {
            name = "Motorfiets SMG";
            type = "MachineGun";
            spriteName = "motorfietsweapon.png";
            damage = 10;
            fireRate = 20;
            turnSpeed = 4;
        }
    }
}
