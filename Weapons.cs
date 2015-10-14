using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceGame
{
    public abstract class Weapons
    {
        public string name;
        public string type;
        public string spriteName;
        public int damage;
        public int fireRate;

        virtual public void shoot()
        { 

        }
    }

    public class TankWeapon : Weapons
    {
        public TankWeapon()
        {
            name = "Tank Cannon";
            type = "Cannon";
            spriteName = "tankcannon.png";
            damage = 75;
            fireRate = 120;
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
        }
    }
}
