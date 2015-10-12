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
    }

    public class TankWeapon : Weapons
    {
        public TankWeapon()
        {
            name = "Tank Weapon";
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
            name = "LAPV Weapon";
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

        }
    }
}
