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
        public string spriteName;
        public float turnSpeed;
        public string name;
        public Weapons weapon;
        public Point relativeWeaponPos;
        public int maxHealth;
        public int ramDamage;
        public float sideDamageMultiplier;

    }

    public class Tank : Cars
    {
        public Tank()
        {
            fuelCapacity = 95;
            topSpeed = 0.5f;
            acceleration = 0.2f;
            spriteName = "tank.png";
            turnSpeed = 0.6f;
            name = "Tank";
            weapon = new TankWeapon();
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 200;
            ramDamage = 100;
            sideDamageMultiplier = 1.1f;
        }
    }

    public class Jackass : Cars
    {
        public Jackass()
        {
            fuelCapacity = 120;
            topSpeed = 1.2f;
            acceleration = 0.6f;
            spriteName = "jackass.png";
            turnSpeed = 0.8f;
            weapon = null;
            maxHealth = 75;
            ramDamage = 200;
            sideDamageMultiplier = 2f;
        }
    }

    public class LAPV : Cars
    {
        public LAPV()
        {
            fuelCapacity = 100;
            topSpeed = 0.7f;
            acceleration = 0.3f;
            spriteName = "lapv.png";
            turnSpeed = 0.9f;
            weapon = new LAPVWeapon();
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 120;
            ramDamage = 80;
            sideDamageMultiplier = 1.1f;
        }
    }

    public class HorsePower : Cars
    {
        public HorsePower()
        {
            fuelCapacity = 140;
            topSpeed = 1;
            acceleration = 0.5f;
            spriteName = "horsepower.png";
            turnSpeed = 1;
            weapon = new LAPVWeapon();
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 90;
            ramDamage = 40;
            sideDamageMultiplier = 1.5f;

        }
    }
}
