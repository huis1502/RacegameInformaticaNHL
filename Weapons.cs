using System;
using System.Collections.Generic;
using System.Drawing;
using RaceGame.Structs;

namespace RaceGame
{
    public class Bullet
    {
        public float angle;
        public float speed;
        public int timeout;
        public int damage;
        public Player player;
        public DrawInfo bulletDrawInfo;

        public Bullet(Bitmap bitmap, int x, int y, int width, int height, float _angle = 0, float RotateX = 0f, float RotateY = 0f, bool AutoRemove = false, int Frames = 0)
        {
            bulletDrawInfo = new DrawInfo(bitmap, x, y, width, height, _angle, RotateX, RotateY, AutoRemove, Frames);
            Base.drawInfos.Add(bulletDrawInfo);

        }

        public void TrackBullet()
        {

            if (player == Base.currentGame.player1)
            {
                if (Math.Abs(bulletDrawInfo.x - Base.currentGame.player2.vehicle.topleft.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player2.vehicle.topleft.Y) < 20 || Math.Abs(bulletDrawInfo.x - Base.currentGame.player2.vehicle.topright.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player2.vehicle.topright.Y) < 20
                        || Math.Abs(bulletDrawInfo.x - Base.currentGame.player2.vehicle.backleft.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player2.vehicle.backleft.Y) < 20 || Math.Abs(bulletDrawInfo.x - Base.currentGame.player2.vehicle.backright.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player2.vehicle.backright.Y) < 20)
                {
                    Base.currentGame.player2.vehicle.drawInfo.angle = bulletDrawInfo.angle;
                    Base.currentGame.player2.vehicle.weaponDrawInfo.angle = bulletDrawInfo.angle;
                    Base.currentGame.player2.vehicle.health -= damage;
                    if (Base.currentGame.player2.vehicle.health < 0)
                    {
                        Base.currentGame.player2.vehicle.health = 0;
                    }
                    Console.WriteLine("damage gedaan: "+damage);
                    Console.WriteLine("helf remain iz: " + Base.currentGame.player2.vehicle.health);
                    timeout = 0;


                }
            }
            if (player == Base.currentGame.player2)
            {
                if (Math.Abs(bulletDrawInfo.x - Base.currentGame.player1.vehicle.topleft.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player1.vehicle.topleft.Y) < 20 || Math.Abs(bulletDrawInfo.x - Base.currentGame.player1.vehicle.topright.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player1.vehicle.topright.Y) < 20
                        || Math.Abs(bulletDrawInfo.x - Base.currentGame.player1.vehicle.backleft.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player1.vehicle.backleft.Y) < 20 || Math.Abs(bulletDrawInfo.x - Base.currentGame.player1.vehicle.backright.X) < 20 && Math.Abs(bulletDrawInfo.y - Base.currentGame.player1.vehicle.backright.Y) < 20)
                {
                    Base.currentGame.player1.vehicle.drawInfo.angle = bulletDrawInfo.angle;
                    Base.currentGame.player2.vehicle.weaponDrawInfo.angle = bulletDrawInfo.angle;
                    Base.currentGame.player1.vehicle.health -= damage;
                    if (Base.currentGame.player1.vehicle.health < 0)
                    {
                        Base.currentGame.player1.vehicle.health = 0;
                    }
                    Console.WriteLine(Base.currentGame.player1.vehicle.health);
                    timeout = 0;
                }
            }

            if (bulletDrawInfo.x <= 0 || bulletDrawInfo.x >= Base.currentGame.MapsizeX || bulletDrawInfo.y <= 0 || bulletDrawInfo.y >= Base.currentGame.MapsizeY)
            {
                Base.drawInfos.Remove(bulletDrawInfo);
                Base.gameTasks.Remove(TrackBullet);
                return;
            }

            bulletDrawInfo.x += (float)(Math.Cos(bulletDrawInfo.angle % 360 * (Math.PI / 180)) * speed);
            bulletDrawInfo.y += (float)(Math.Cos((90 - bulletDrawInfo.angle) % 360 * (Math.PI / 180)) * speed);
            
            if (timeout <= 0)
            {
                    Base.drawInfos.Remove(bulletDrawInfo);
                    Base.gameTasks.Remove(TrackBullet);
                return;
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
        public Player player;

        public static List<Bullet> Bullets = new List<Bullet>();
        public static int i;
        public int weaponReloading;

        public Weapons(Player t)
        {
            player = t;
        }

        virtual public void shoot()
        {
            Console.WriteLine("yo");
            if (weaponReloading == 0) //IK heb grote ballen
            {
                Bitmap koegmap = new Bitmap("koegel.png");
                Console.WriteLine("wtf");
                Bullets.Add(new Bullet(koegmap, (int)player.vehicle.drawInfo.x, (int)player.vehicle.drawInfo.y, 10, 10, player.vehicle.weaponDrawInfo.angle, 0f, 0f));
                i = Bullets.Count - 1;
                Bullets[i].speed = 30;
                Bullets[i].timeout = 300;
                Bullets[i].player = player;
                Console.WriteLine("damage: " + damage);
                Bullets[i].damage = damage;
                Base.gameTasks.Add(Bullets[i].TrackBullet);
                weaponReloading = fireRate;
                Base.gameTasks.Add(weaponReload);
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
        public TankWeapon(Player s) : base(s)
        {
            name = "Tank Cannon";
            type = "Cannon";
            spriteName = "loop.png";
            damage = 75;
            fireRate = 100;
            turnSpeed = 3f;
            turning = "false";
        }

        public override void shoot()
        {
            if (weaponReloading == 0) //IK heb grote ballen
            {
                Bitmap koegmap = new Bitmap("kannonbal.png");

                Bullets.Add(new Bullet(koegmap, (int)player.vehicle.drawInfo.x, (int)player.vehicle.drawInfo.y, 10, 10, player.vehicle.weaponDrawInfo.angle, 0f, 0f));
                i = Bullets.Count - 1;
                Bullets[i].speed = 30;
                Bullets[i].timeout = 300;
                Bullets[i].player = player;
                Bullets[i].damage = damage;
                Base.gameTasks.Add(Bullets[i].TrackBullet);
                weaponReloading = fireRate;
                Base.gameTasks.Add(weaponReload);
            }
        }
    }

    public class LAPVWeapon : Weapons
    {
        public LAPVWeapon(Player s) : base(s)
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
        public HorsePowerWeapon(Player s) : base(s)
        {
            name = "HorsePower Flamethrower";
            type = "Flamethrower";
            spriteName = "horsepowerweapon.png";
            damage = 5;
            fireRate = 10;
            turnSpeed = 3; 
        }

        public override void shoot()
        {
            if (weaponReloading == 0) //IK heb grote ballen
            {
                Bitmap koegmap = new Bitmap("vlam.png");

                Bullets.Add(new Bullet(koegmap, (int)player.vehicle.drawInfo.x, (int)player.vehicle.drawInfo.y, 30, 90, player.vehicle.weaponDrawInfo.angle, 0f, 0f, true, 300));
                i = Bullets.Count - 1;
                Bullets[i].speed = 30;
                Bullets[i].timeout = 300;
                Bullets[i].player = player;
                Bullets[i].damage = damage;
                Base.gameTasks.Add(Bullets[i].TrackBullet);
                weaponReloading = fireRate;
                Base.gameTasks.Add(weaponReload);
            }
        }
    }

    public class MotorfietsWeapon : Weapons
    {
        public MotorfietsWeapon(Player s) : base(s)
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
