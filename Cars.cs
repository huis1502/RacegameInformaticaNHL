using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace RaceGame
{
    public abstract class Cars
    {
        public int fuelCapacity;
        public float topSpeed;
        public float acceleration;
        public float deceleration;
        public string spriteName;
        public float turnSpeed;
        public string name;
        public Weapons weapon;
        public Point relativeWeaponPos;
        public int maxHealth;
        public int ramDamage;
        public float sideDamageMultiplier;
        public float grassMultiplier;

    }

    public class Tank : Cars
    {
        public Tank()
        {
            fuelCapacity = 95;
            topSpeed = 0.5f;
            acceleration = 0.01f;
            deceleration = 0.02f;
            spriteName = "tank.png";
            turnSpeed = 2;
            name = "Tank";
            weapon = new TankWeapon();
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 200;
            ramDamage = 100;
            sideDamageMultiplier = 1.1f;
            grassMultiplier = 0.9f;
        }
    }

    public class Jackass : Cars
    {
        public Jackass()
        {
            fuelCapacity = 120;
            topSpeed = 1.2f;
            acceleration = 0.03f;
            deceleration = 0.05f;
            spriteName = "jackass.png";
            turnSpeed = 1.3f;
            weapon = null;
            maxHealth = 75;
            ramDamage = 200;
            sideDamageMultiplier = 2f;
            grassMultiplier = 0.4f;
        }
    }

    public class LAPV : Cars
    {
        public LAPV()
        {
            fuelCapacity = 100;
            topSpeed = 0.7f;
            acceleration = 0.015f;
            deceleration = 0.03f;
            spriteName = "lapv.png";
            turnSpeed = 2.4f;
            weapon = new LAPVWeapon();
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 120;
            ramDamage = 80;
            sideDamageMultiplier = 1.1f;
            grassMultiplier = 0.8f;
        }
    }

    public class HorsePower : Cars
    {
        public HorsePower()
        {
            fuelCapacity = 140;
            topSpeed = 1;
            acceleration = 0.025f;
            deceleration = 0.05f;
            spriteName = "horsepower.png";
            turnSpeed = 2.3f;
            weapon = new HorsePowerWeapon();
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 90;
            ramDamage = 40;
            sideDamageMultiplier = 1.5f;
            grassMultiplier = 0.6f;

        }
    }

    public class Motorfiets : Cars
    {
        public Motorfiets()
        {
            fuelCapacity = 70;
            topSpeed = 1.1f;
            acceleration = 0.035f;
            deceleration = 0.07f;
            spriteName = "Motorfiets.png";
            turnSpeed = 3.4f;
            weapon = new LAPVWeapon();
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 60;
            ramDamage = 30;
            sideDamageMultiplier = 2;
            grassMultiplier = 0.4f;
        }
    }
}
