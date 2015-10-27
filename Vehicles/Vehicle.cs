using RaceGame.Enums;
using System.Drawing;
using System;

namespace RaceGame
{
    public abstract class Vehicle
    {
        public string path;
        public DrawInfo drawInfo;
        public DrawInfo weaponDrawInfo;
        public Bitmap bitmap;
        public Bitmap weaponSprite;
        public VehicleType vehicletype;
        public int StartPositionX;
        public int StartPositionY;
        public int fuelCapacity;
        public float maxSpeed;
        public float acceleration;
        public float deceleration;
        public float turnSpeed;
        public string name;
        public Weapons weapon;
        public System.Drawing.Point relativeWeaponPos;
        public int maxHealth;
        public int ramDamage;
        public float sideDamageMultiplier;
        public float grassMultiplier;

        float deltaTime = 1.00f;
        float prevDelta = 1.00f;
        float speed = 0f;
        float prevSpeed = 0f;
        float prevSpeedrev = 0f;
        public int i = 0;
        public int j = 0;
        private bool go = false;
        public string turning = "false";
        public bool throttle = false;
        public bool brake = false;

        public Vehicle(int x, int y)
        {
            StartPositionX = x;
            StartPositionY = y;
        }
        public Vehicle(int x, int y, string path)
        {
            StartPositionX = x;
            StartPositionY = y;
            this.path = path;
        }
        public Vehicle(int x, int y,VehicleType t)
        {
            vehicletype = t;
            StartPositionX = x;
            StartPositionY = y;
        }
        #region Drawing and stopping drawingappelnoot
        public void StartDraw(int x, int y)
        {
            bitmap = new Bitmap(path);
            drawInfo = new DrawInfo(bitmap, x,y,50,50, 0f,0f,0f);
            Base.drawInfos.Add(drawInfo);
        }
        
        public void StartDraw()
        {
            bitmap = new Bitmap(path);
            drawInfo = new DrawInfo(bitmap, StartPositionX, StartPositionY, 50, 50, 0f,0f,0f);
            Base.drawInfos.Add(drawInfo);
        }

        public void StartWeaponDraw()
        {
            weaponSprite = new Bitmap(weapon.spriteName);
            weaponDrawInfo = new DrawInfo(weaponSprite, StartPositionX, StartPositionY, 50, 100, 0f, 0f, drawInfo.angle);
            Base.drawInfos.Add(weaponDrawInfo);
        }
        public void StopDraw()
        {
            Base.drawInfos.Remove(drawInfo);
        }
        #endregion

        #region Movement
        public virtual void Shoot() { }
        #endregion

        public void Appelnoot()
        {
            if (Base.currentGame.player1.vehicle.throttle == true)
            {
                if (Math.Abs(0 - Base.currentGame.player1.vehicle.speed) <= 0.01)
                {
                    Base.currentGame.player1.vehicle.go = true;
                }
                if (Base.currentGame.player1.vehicle.speed > 0 || Base.currentGame.player1.vehicle.go)
                {
                    if (Math.Abs((((-(float)Math.Pow(Base.currentGame.player1.vehicle.acceleration * (Base.currentGame.player1.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player1.vehicle.maxSpeed), 2)) + Base.currentGame.player1.vehicle.maxSpeed) * Base.currentGame.player1.vehicle.prevDelta) - Base.currentGame.player1.vehicle.prevSpeed) <= .5 || Base.currentGame.player1.vehicle.go)
                    {
                        Base.currentGame.player1.vehicle.go = false;
                        Base.currentGame.player1.vehicle.speed = ((-(float)Math.Pow(Base.currentGame.player1.vehicle.acceleration * (Base.currentGame.player1.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player1.vehicle.maxSpeed), 2)) + Base.currentGame.player1.vehicle.maxSpeed) * Base.currentGame.player1.vehicle.deltaTime;
                        Base.currentGame.player1.vehicle.prevSpeed = Base.currentGame.player1.vehicle.speed;
                        Base.currentGame.player1.vehicle.prevDelta = Base.currentGame.player1.vehicle.deltaTime;
                        if (Math.Abs(1 - Base.currentGame.player1.vehicle.speed / Base.currentGame.player1.vehicle.maxSpeed) < 0.01)
                        {
                            Base.currentGame.player1.vehicle.speed = Base.currentGame.player1.vehicle.maxSpeed * Base.currentGame.player1.vehicle.deltaTime;
                        }
                        else
                        {
                            Base.currentGame.player1.vehicle.i++;
                        }
                    }
                    else
                    {
                        Base.currentGame.player1.vehicle.i = 0;
                        while (Math.Abs((((-(float)Math.Pow(Base.currentGame.player1.vehicle.acceleration * (Base.currentGame.player1.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player1.vehicle.maxSpeed), 2)) + Base.currentGame.player1.vehicle.maxSpeed) * Base.currentGame.player1.vehicle.deltaTime) - Base.currentGame.player1.vehicle.speed) > .5)
                        {
                            Base.currentGame.player1.vehicle.i++;
                            if (Math.Abs((((-(float)Math.Pow(Base.currentGame.player1.vehicle.acceleration * (i / 2) - Math.Sqrt(Base.currentGame.player1.vehicle.maxSpeed), 2)) + Base.currentGame.player1.vehicle.maxSpeed) * Base.currentGame.player1.vehicle.deltaTime) - Base.currentGame.player1.vehicle.speed) <= .5)
                            {
                                Base.currentGame.player1.vehicle.go = true;
                            }
                        }
                    }
                }
                else
                {
                    Base.currentGame.player1.vehicle.speed = (1 * (float)Math.Pow(Base.currentGame.player1.vehicle.deceleration * j, 2) + Base.currentGame.player1.vehicle.speed) * Base.currentGame.player1.vehicle.deltaTime;
                    if (Math.Abs(0 - Base.currentGame.player1.vehicle.speed) <= 0.01)
                    {
                        Base.currentGame.player1.vehicle.speed = 0;
                    }
                    else
                    {
                        Base.currentGame.player1.vehicle.j++;
                    }
                }
                //formule acceleratie
                //y=(-(acceleration*(x/2)-sqrt(topspeed))^2)+topspeed
            }
            else
            {
                if (Base.currentGame.player1.vehicle.speed > 0)
                {
                    if (Base.currentGame.player1.vehicle.speed < 0.05)
                    {
                        Base.currentGame.player1.vehicle.speed = 0;
                    }
                    else
                    {
                        Base.currentGame.player1.vehicle.speed -= Base.currentGame.player1.vehicle.deceleration * .5f * Base.currentGame.player1.vehicle.deltaTime;
                    }
                    Base.currentGame.player1.vehicle.i = 0;
                }
            }


            if (Base.currentGame.player1.vehicle.brake == true)
            {
                if (Math.Abs(0 - Base.currentGame.player1.vehicle.speed) <= 0.0002)
                {
                    Base.currentGame.player1.vehicle.go = true;
                }
                if (Base.currentGame.player1.vehicle.speed <= 0 || Base.currentGame.player1.vehicle.go)
                {
                    if (Math.Abs((-0.75f * ((-(float)Math.Pow(Base.currentGame.player1.vehicle.acceleration * (Base.currentGame.player1.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player1.vehicle.maxSpeed), 2)) + Base.currentGame.player1.vehicle.maxSpeed) * Base.currentGame.player1.vehicle.prevDelta) - Base.currentGame.player1.vehicle.prevSpeedrev) <= .5 || Base.currentGame.player1.vehicle.go)
                    {
                        Base.currentGame.player1.vehicle.go = false;
                        Base.currentGame.player1.vehicle.speed = -0.75f * ((-(float)Math.Pow(Base.currentGame.player1.vehicle.acceleration * (i / 2) - Math.Sqrt(Base.currentGame.player1.vehicle.maxSpeed), 2)) + Base.currentGame.player1.vehicle.maxSpeed) * Base.currentGame.player1.vehicle.deltaTime;
                        Base.currentGame.player1.vehicle.prevSpeedrev = Base.currentGame.player1.vehicle.speed;
                        Base.currentGame.player1.vehicle.prevDelta = Base.currentGame.player1.vehicle.deltaTime;
                        if (Math.Abs(1 - Base.currentGame.player1.vehicle.speed / (Base.currentGame.player1.vehicle.maxSpeed * -0.75f)) < 0.01f)
                        {
                            Base.currentGame.player1.vehicle.speed = Base.currentGame.player1.vehicle.maxSpeed * -.75f * Base.currentGame.player1.vehicle.deltaTime;
                        }
                        else
                        {
                            Base.currentGame.player1.vehicle.i++;
                        }
                    }
                    else
                    {
                        Base.currentGame.player1.vehicle.i = 0;
                        while (Math.Abs((((-(float)Math.Pow(Base.currentGame.player1.vehicle.acceleration * (Base.currentGame.player1.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player1.vehicle.maxSpeed), 2)) + Base.currentGame.player1.vehicle.maxSpeed) * Base.currentGame.player1.vehicle.deltaTime) - Base.currentGame.player1.vehicle.speed) > .5)
                        {
                            Base.currentGame.player1.vehicle.i++;
                            if (Math.Abs((((-(float)Math.Pow(Base.currentGame.player1.vehicle.acceleration * (Base.currentGame.player1.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player1.vehicle.maxSpeed), 2)) + Base.currentGame.player1.vehicle.maxSpeed) * Base.currentGame.player1.vehicle.deltaTime) - Base.currentGame.player1.vehicle.speed) <= .5)
                            {
                                Base.currentGame.player1.vehicle.go = true;
                            }
                        }
                    }
                }
                else
                {
                    //remmen
                    Base.currentGame.player1.vehicle.speed = (-1 * (float)Math.Pow(Base.currentGame.player1.vehicle.deceleration * Base.currentGame.player1.vehicle.j, 2) + Base.currentGame.player1.vehicle.speed) * Base.currentGame.player1.vehicle.deltaTime;
                    if (Base.currentGame.player1.vehicle.speed <= 0.01)
                    {
                        Base.currentGame.player1.vehicle.speed = 0;
                    }
                    else
                    {
                        Base.currentGame.player1.vehicle.j++;
                    }
                    //formule acceleratie
                    //speed=-deceleration x ^ 2 + speed
                }
            }
            else
            {
                if (Base.currentGame.player1.vehicle.speed < 0)
                {
                    if (Base.currentGame.player1.vehicle.speed > -0.05)
                    {
                        Base.currentGame.player1.vehicle.speed = 0;
                    }
                    else
                    {
                        Base.currentGame.player1.vehicle.speed += Base.currentGame.player1.vehicle.deceleration * .5f * Base.currentGame.player1.vehicle.deltaTime;
                    }
                    Base.currentGame.player1.vehicle.i = 0;
                }
            }

            if (Base.currentGame.player1.vehicle.turning == "right")
            {
                if (Base.currentGame.player1.vehicle.vehicletype != VehicleType.Tank)
                {
                    if (Base.currentGame.player1.vehicle.speed > 3)
                    {
                        float rotationPlus = (Base.currentGame.player1.vehicle.turnSpeed * 3 / (float)Math.Pow(Base.currentGame.player1.vehicle.speed, 1 / 3)) * Base.currentGame.player1.vehicle.deltaTime * Base.currentGame.player1.vehicle.speed / (float)Math.Pow(Base.currentGame.player1.vehicle.speed, 2.25);
                        Base.currentGame.player1.vehicle.drawInfo.angle += rotationPlus;
                        Base.currentGame.player1.vehicle.weaponDrawInfo.angle += rotationPlus;
                    }
                    else
                    {
                        float rotationPlus = (Base.currentGame.player1.vehicle.turnSpeed * 3 / (float)Math.Pow(Base.currentGame.player1.vehicle.speed, 1 / 3)) * Base.currentGame.player1.vehicle.deltaTime * Base.currentGame.player1.vehicle.speed / 15;
                        Base.currentGame.player1.vehicle.drawInfo.angle += rotationPlus;
                        Base.currentGame.player1.vehicle.weaponDrawInfo.angle += rotationPlus;
                    }
                }
                else
                {
                    float rotationPlus = Base.currentGame.player1.vehicle.turnSpeed / (float)Math.Pow(Base.currentGame.player1.vehicle.speed, 1 / 5) * Base.currentGame.player1.vehicle.deltaTime;
                    Base.currentGame.player1.vehicle.drawInfo.angle += rotationPlus;
                    Base.currentGame.player1.vehicle.weaponDrawInfo.angle += rotationPlus;
                }
            }
            else if (Base.currentGame.player1.vehicle.turning == "left")
            {
                if (Base.currentGame.player1.vehicle.vehicletype != VehicleType.Tank)
                {
                    if (Base.currentGame.player1.vehicle.speed > 3)
                    {
                        float rotationPlus = (Base.currentGame.player1.vehicle.turnSpeed * 3 / (float)Math.Pow(Base.currentGame.player1.vehicle.speed, 1 / 3)) * Base.currentGame.player1.vehicle.deltaTime * Base.currentGame.player1.vehicle.speed / (float)Math.Pow(Base.currentGame.player1.vehicle.speed, 2.25);
                        Base.currentGame.player1.vehicle.drawInfo.angle -= rotationPlus;
                        Base.currentGame.player1.vehicle.weaponDrawInfo.angle -= rotationPlus;
                    }
                    else
                    {
                        Base.currentGame.player1.vehicle.drawInfo.angle -= (Base.currentGame.player1.vehicle.turnSpeed * 3 / (float)Math.Pow(Base.currentGame.player1.vehicle.speed, 1 / 3)) * Base.currentGame.player1.vehicle.deltaTime * Base.currentGame.player1.vehicle.speed / 15;
                        Base.currentGame.player1.vehicle.weaponDrawInfo.angle -= (Base.currentGame.player1.vehicle.turnSpeed * 3 / (float)Math.Pow(Base.currentGame.player1.vehicle.speed, 1 / 3)) * Base.currentGame.player1.vehicle.deltaTime * Base.currentGame.player1.vehicle.speed / 15;
                    }
                }
                else
                {
                    float rotationPlus = Base.currentGame.player1.vehicle.turnSpeed / (float)Math.Pow(Base.currentGame.player1.vehicle.speed, 1 / 5) * Base.currentGame.player1.vehicle.deltaTime;
                    Base.currentGame.player1.vehicle.drawInfo.angle -= rotationPlus;
                    Base.currentGame.player1.vehicle.weaponDrawInfo.angle -= rotationPlus;
                }
            }
            else if (Base.currentGame.player1.vehicle.weapon.turning == "left")
            {
                Base.currentGame.player1.vehicle.weaponDrawInfo.angle -= Base.currentGame.player1.vehicle.weapon.turnSpeed;
            }
            else if (Base.currentGame.player1.vehicle.weapon.turning == "right")
            {
                Base.currentGame.player1.vehicle.weaponDrawInfo.angle += Base.currentGame.player1.vehicle.weapon.turnSpeed;
            }
            Base.currentGame.player1.vehicle.drawInfo.x += (float)(Math.Cos(Base.currentGame.player1.vehicle.drawInfo.angle * (Math.PI / 180)) * Base.currentGame.player1.vehicle.speed);
            Base.currentGame.player1.vehicle.drawInfo.y += (float)(Math.Cos((90 - Base.currentGame.player1.vehicle.drawInfo.angle) * (Math.PI / 180)) * Base.currentGame.player1.vehicle.speed);

            Base.currentGame.player1.vehicle.weaponDrawInfo.x = Base.currentGame.player1.vehicle.drawInfo.x;
            Base.currentGame.player1.vehicle.weaponDrawInfo.y = Base.currentGame.player1.vehicle.drawInfo.y;

            Base.currentGame.player1.vehicle.deltaTime = 1;
            // if (orig.GetPixel((int)BallPos.X - BallSizeW / 2, (int)BallPos.Y - BallSizeH / 2).G != 0)
            // {
            //     deltaTime = grassMultiplier;
            //   }

            if (Base.currentGame.player2.vehicle.throttle == true)
            {
                if (Math.Abs(0 - Base.currentGame.player2.vehicle.speed) <= 0.01)
                {
                    Base.currentGame.player2.vehicle.go = true;
                }
                if (Base.currentGame.player2.vehicle.speed > 0 || Base.currentGame.player2.vehicle.go)
                {
                    if (Math.Abs((((-(float)Math.Pow(Base.currentGame.player2.vehicle.acceleration * (Base.currentGame.player2.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player2.vehicle.maxSpeed), 2)) + Base.currentGame.player2.vehicle.maxSpeed) * Base.currentGame.player2.vehicle.prevDelta) - Base.currentGame.player2.vehicle.prevSpeed) <= .5 || Base.currentGame.player2.vehicle.go)
                    {
                        Base.currentGame.player2.vehicle.go = false;
                        Base.currentGame.player2.vehicle.speed = ((-(float)Math.Pow(Base.currentGame.player2.vehicle.acceleration * (Base.currentGame.player2.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player2.vehicle.maxSpeed), 2)) + Base.currentGame.player2.vehicle.maxSpeed) * Base.currentGame.player2.vehicle.deltaTime;
                        Base.currentGame.player2.vehicle.prevSpeed = Base.currentGame.player2.vehicle.speed;
                        Base.currentGame.player2.vehicle.prevDelta = Base.currentGame.player2.vehicle.deltaTime;
                        if (Math.Abs(1 - Base.currentGame.player2.vehicle.speed / Base.currentGame.player2.vehicle.maxSpeed) < 0.01)
                        {
                            Base.currentGame.player2.vehicle.speed = Base.currentGame.player2.vehicle.maxSpeed * Base.currentGame.player2.vehicle.deltaTime;
                        }
                        else
                        {
                            Base.currentGame.player2.vehicle.i++;
                        }
                    }
                    else
                    {
                        Base.currentGame.player2.vehicle.i = 0;
                        while (Math.Abs((((-(float)Math.Pow(Base.currentGame.player2.vehicle.acceleration * (Base.currentGame.player2.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player2.vehicle.maxSpeed), 2)) + Base.currentGame.player2.vehicle.maxSpeed) * Base.currentGame.player2.vehicle.deltaTime) - Base.currentGame.player2.vehicle.speed) > .5)
                        {
                            Base.currentGame.player2.vehicle.i++;
                            if (Math.Abs((((-(float)Math.Pow(Base.currentGame.player2.vehicle.acceleration * (i / 2) - Math.Sqrt(Base.currentGame.player2.vehicle.maxSpeed), 2)) + Base.currentGame.player2.vehicle.maxSpeed) * Base.currentGame.player2.vehicle.deltaTime) - Base.currentGame.player2.vehicle.speed) <= .5)
                            {
                                Base.currentGame.player2.vehicle.go = true;
                            }
                        }
                    }
                }
                else
                {
                    Base.currentGame.player2.vehicle.speed = (1 * (float)Math.Pow(Base.currentGame.player2.vehicle.deceleration * j, 2) + Base.currentGame.player2.vehicle.speed) * Base.currentGame.player2.vehicle.deltaTime;
                    if (Math.Abs(0 - Base.currentGame.player2.vehicle.speed) <= 0.01)
                    {
                        Base.currentGame.player2.vehicle.speed = 0;
                    }
                    else
                    {
                        Base.currentGame.player2.vehicle.j++;
                    }
                }
                //formule acceleratie
                //y=(-(acceleration*(x/2)-sqrt(topspeed))^2)+topspeed
            }
            else
            {
                if (Base.currentGame.player2.vehicle.speed > 0)
                {
                    if (Base.currentGame.player2.vehicle.speed < 0.05)
                    {
                        Base.currentGame.player2.vehicle.speed = 0;
                    }
                    else
                    {
                        Base.currentGame.player2.vehicle.speed -= Base.currentGame.player2.vehicle.deceleration * .5f * Base.currentGame.player2.vehicle.deltaTime;
                    }
                    Base.currentGame.player2.vehicle.i = 0;
                }
            }


            if (Base.currentGame.player2.vehicle.brake == true)
            {
                if (Math.Abs(0 - Base.currentGame.player2.vehicle.speed) <= 0.0002)
                {
                    Base.currentGame.player2.vehicle.go = true;
                }
                if (Base.currentGame.player2.vehicle.speed <= 0 || Base.currentGame.player2.vehicle.go)
                {
                    if (Math.Abs((-0.75f * ((-(float)Math.Pow(Base.currentGame.player2.vehicle.acceleration * (Base.currentGame.player2.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player2.vehicle.maxSpeed), 2)) + Base.currentGame.player2.vehicle.maxSpeed) * Base.currentGame.player2.vehicle.prevDelta) - Base.currentGame.player2.vehicle.prevSpeedrev) <= .5 || Base.currentGame.player2.vehicle.go)
                    {
                        Base.currentGame.player2.vehicle.go = false;
                        Base.currentGame.player2.vehicle.speed = -0.75f * ((-(float)Math.Pow(Base.currentGame.player2.vehicle.acceleration * (i / 2) - Math.Sqrt(Base.currentGame.player2.vehicle.maxSpeed), 2)) + Base.currentGame.player2.vehicle.maxSpeed) * Base.currentGame.player2.vehicle.deltaTime;
                        Base.currentGame.player2.vehicle.prevSpeedrev = Base.currentGame.player2.vehicle.speed;
                        Base.currentGame.player2.vehicle.prevDelta = Base.currentGame.player2.vehicle.deltaTime;
                        if (Math.Abs(1 - Base.currentGame.player2.vehicle.speed / (Base.currentGame.player2.vehicle.maxSpeed * -0.75f)) < 0.01f)
                        {
                            Base.currentGame.player2.vehicle.speed = Base.currentGame.player2.vehicle.maxSpeed * -.75f * Base.currentGame.player2.vehicle.deltaTime;
                        }
                        else
                        {
                            Base.currentGame.player2.vehicle.i++;
                        }
                    }
                    else
                    {
                        Base.currentGame.player2.vehicle.i = 0;
                        while (Math.Abs((((-(float)Math.Pow(Base.currentGame.player2.vehicle.acceleration * (Base.currentGame.player2.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player2.vehicle.maxSpeed), 2)) + Base.currentGame.player2.vehicle.maxSpeed) * Base.currentGame.player2.vehicle.deltaTime) - Base.currentGame.player2.vehicle.speed) > .5)
                        {
                            Base.currentGame.player2.vehicle.i++;
                            if (Math.Abs((((-(float)Math.Pow(Base.currentGame.player2.vehicle.acceleration * (Base.currentGame.player2.vehicle.i / 2) - Math.Sqrt(Base.currentGame.player2.vehicle.maxSpeed), 2)) + Base.currentGame.player2.vehicle.maxSpeed) * Base.currentGame.player2.vehicle.deltaTime) - Base.currentGame.player2.vehicle.speed) <= .5)
                            {
                                Base.currentGame.player2.vehicle.go = true;
                            }
                        }
                    }
                }
                else
                {
                    //remmen
                    Base.currentGame.player2.vehicle.speed = (-1 * (float)Math.Pow(Base.currentGame.player2.vehicle.deceleration * Base.currentGame.player2.vehicle.j, 2) + Base.currentGame.player2.vehicle.speed) * Base.currentGame.player2.vehicle.deltaTime;
                    if (Base.currentGame.player2.vehicle.speed <= 0.01)
                    {
                        Base.currentGame.player2.vehicle.speed = 0;
                    }
                    else
                    {
                        Base.currentGame.player2.vehicle.j++;
                    }
                    //formule acceleratie
                    //speed=-deceleration x ^ 2 + speed
                }
            }
            else
            {
                if (Base.currentGame.player2.vehicle.speed < 0)
                {
                    if (Base.currentGame.player2.vehicle.speed > -0.05)
                    {
                        Base.currentGame.player2.vehicle.speed = 0;
                    }
                    else
                    {
                        Base.currentGame.player2.vehicle.speed += Base.currentGame.player2.vehicle.deceleration * .5f * Base.currentGame.player2.vehicle.deltaTime;
                    }
                    Base.currentGame.player2.vehicle.i = 0;
                }
            }

            if (Base.currentGame.player2.vehicle.turning == "right")
            {
                if (Base.currentGame.player2.vehicle.vehicletype != VehicleType.Tank)
                {
                    if (Base.currentGame.player2.vehicle.speed > 3)
                    {
                        float rotationPlus = (Base.currentGame.player2.vehicle.turnSpeed * 3 / (float)Math.Pow(Base.currentGame.player2.vehicle.speed, 1 / 3)) * Base.currentGame.player2.vehicle.deltaTime * Base.currentGame.player2.vehicle.speed / (float)Math.Pow(Base.currentGame.player2.vehicle.speed, 2.25);
                        Base.currentGame.player2.vehicle.drawInfo.angle += rotationPlus;
                        Base.currentGame.player2.vehicle.weaponDrawInfo.angle += rotationPlus;
                    }
                    else
                    {
                        float rotationPlus = (Base.currentGame.player2.vehicle.turnSpeed * 3 / (float)Math.Pow(Base.currentGame.player2.vehicle.speed, 1 / 3)) * Base.currentGame.player2.vehicle.deltaTime * Base.currentGame.player2.vehicle.speed / 15;
                        Base.currentGame.player2.vehicle.drawInfo.angle += rotationPlus;
                        Base.currentGame.player2.vehicle.weaponDrawInfo.angle += rotationPlus;
                    }
                }
                else
                {
                    float rotationPlus = Base.currentGame.player2.vehicle.turnSpeed / (float)Math.Pow(Base.currentGame.player2.vehicle.speed, 1 / 5) * Base.currentGame.player2.vehicle.deltaTime;
                    Base.currentGame.player2.vehicle.drawInfo.angle += rotationPlus;
                    Base.currentGame.player2.vehicle.weaponDrawInfo.angle += rotationPlus;
                }
            }
            else if (Base.currentGame.player2.vehicle.turning == "left")
            {
                if (Base.currentGame.player2.vehicle.vehicletype != VehicleType.Tank)
                {
                    if (Base.currentGame.player2.vehicle.speed > 3)
                    {
                        float rotationPlus = (Base.currentGame.player2.vehicle.turnSpeed * 3 / (float)Math.Pow(Base.currentGame.player2.vehicle.speed, 1 / 3)) * Base.currentGame.player2.vehicle.deltaTime * Base.currentGame.player2.vehicle.speed / (float)Math.Pow(Base.currentGame.player2.vehicle.speed, 2.25);
                        Base.currentGame.player2.vehicle.drawInfo.angle -= rotationPlus;
                        Base.currentGame.player2.vehicle.weaponDrawInfo.angle -= rotationPlus;
                    }
                    else
                    {
                        Base.currentGame.player2.vehicle.drawInfo.angle -= (Base.currentGame.player2.vehicle.turnSpeed * 3 / (float)Math.Pow(Base.currentGame.player2.vehicle.speed, 1 / 3)) * Base.currentGame.player2.vehicle.deltaTime * Base.currentGame.player2.vehicle.speed / 15;
                        Base.currentGame.player2.vehicle.weaponDrawInfo.angle -= (Base.currentGame.player2.vehicle.turnSpeed * 3 / (float)Math.Pow(Base.currentGame.player2.vehicle.speed, 1 / 3)) * Base.currentGame.player2.vehicle.deltaTime * Base.currentGame.player2.vehicle.speed / 15;
                    }
                }
                else
                {
                    float rotationPlus = Base.currentGame.player2.vehicle.turnSpeed / (float)Math.Pow(Base.currentGame.player2.vehicle.speed, 1 / 5) * Base.currentGame.player2.vehicle.deltaTime;
                    Base.currentGame.player2.vehicle.drawInfo.angle -= rotationPlus;
                    Base.currentGame.player2.vehicle.weaponDrawInfo.angle -= rotationPlus;
                }
            }
            else if (Base.currentGame.player2.vehicle.weapon.turning == "left")
            {
                Base.currentGame.player2.vehicle.weaponDrawInfo.angle -= Base.currentGame.player2.vehicle.weapon.turnSpeed;
            }
            else if (Base.currentGame.player2.vehicle.weapon.turning == "right")
            {
                Base.currentGame.player2.vehicle.weaponDrawInfo.angle += Base.currentGame.player2.vehicle.weapon.turnSpeed;
            }
            Base.currentGame.player2.vehicle.drawInfo.x += (float)(Math.Cos(Base.currentGame.player2.vehicle.drawInfo.angle * (Math.PI / 180)) * Base.currentGame.player2.vehicle.speed);
            Base.currentGame.player2.vehicle.drawInfo.y += (float)(Math.Cos((90 - Base.currentGame.player2.vehicle.drawInfo.angle) * (Math.PI / 180)) * Base.currentGame.player2.vehicle.speed);

            Base.currentGame.player2.vehicle.weaponDrawInfo.x = Base.currentGame.player2.vehicle.drawInfo.x;
            Base.currentGame.player2.vehicle.weaponDrawInfo.y = Base.currentGame.player2.vehicle.drawInfo.y;

            Base.currentGame.player2.vehicle.deltaTime = 1;
            // if (orig.GetPixel((int)BallPos.X - BallSizeW / 2, (int)BallPos.Y - BallSizeH / 2).G != 0)
            // {
            //     deltaTime = grassMultiplier;
            //   }

        }

    }
}
