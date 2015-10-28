﻿using RaceGame.Enums;

namespace RaceGame
{
    public class Tank : Vehicle
    {
        public Tank(int x, int y, Player _player) : base(x,y, VehicleType.Tank, _player)
        {
            fuelCapacity = 95;
            fuel = fuelCapacity;
            maxSpeed = 4f;
            acceleration = 0.05f;
            deceleration = 0.05f;
            path = "tankbody.png";
            turnSpeed = 2;
            name = "Tank";
            weapon = new TankWeapon(_player);
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 200;
            health = maxHealth;
            ramDamage = 100;
            sideDamageMultiplier = 1.1f;
            grassMultiplier = 0.9f;
        }
    }

    public class Jackass : Vehicle
    {
        public Jackass(int x, int y, Player _player) : base(x,y,VehicleType.Jackass, _player)
        {
            fuelCapacity = 120;
            fuel = fuelCapacity;
            maxSpeed = 1.2f;
            acceleration = 0.03f;
            deceleration = 0.05f;
            path = "jackass.png";
            turnSpeed = 1.3f;
            weapon = null;
            maxHealth = 75;
            ramDamage = 200;
            sideDamageMultiplier = 2f;
            grassMultiplier = 0.4f;
        }
    }

    public class LAPV : Vehicle
    {
        public LAPV(int x, int y, Player _player) : base(x, y, VehicleType.LAPV, _player)
        {
            fuelCapacity = 100;
            fuel = fuelCapacity;
            maxSpeed = 0.7f;
            acceleration = 0.015f;
            deceleration = 0.03f;
            path = "lapv.png";
            turnSpeed = 2.4f;
            weapon = new LAPVWeapon(_player);
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 120;
            ramDamage = 80;
            sideDamageMultiplier = 1.1f;
            grassMultiplier = 0.8f;
        }
    }

    public class HorsePower : Vehicle
    {
        public HorsePower(int x, int y, Player _player) : base(x, y, VehicleType.HorsePower, _player)
        {
            fuelCapacity = 140;
            fuel = fuelCapacity;
            maxSpeed = 1;
            acceleration = 0.025f;
            deceleration = 0.05f;
            path = "horsepower.png";
            turnSpeed = 2.3f;
            weapon = new HorsePowerWeapon(_player);
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 90;
            ramDamage = 40;
            sideDamageMultiplier = 1.5f;
            grassMultiplier = 0.6f;

        }
    }

    public class Motorfiets : Vehicle
    {
        public Motorfiets(int x, int y, Player _player) : base(x, y, VehicleType.Motorfiets, _player)
        {
            fuelCapacity = 70;
            fuel = fuelCapacity;
            maxSpeed = 1.1f;
            acceleration = 0.035f;
            deceleration = 0.07f;
            path = "Motorfiets.png";
            turnSpeed = 3.4f;
            weapon = new LAPVWeapon(_player);
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 60;
            ramDamage = 30;
            sideDamageMultiplier = 2;
            grassMultiplier = 0.4f;
        }
    }
}
