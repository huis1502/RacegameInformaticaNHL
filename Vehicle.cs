using RaceGame.Structs;
using RaceGame.Enums;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace RaceGame
{
    public abstract class Vehicle
    {
        public string path;
        public DrawInfo drawInfo;
        public DrawInfo WeaponDrawInfo;
        public Bitmap bitmap;
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
        static float speed = 0f;
        static float prevSpeed = 0f;
        static float prevSpeedrev = 0f;
        public int i = 0;
        public int j = 0;
        private bool go = false;
        public string turning = "false";
        public bool throttle = false;
        public bool brake = false;
        public bool eDown = false;
        public bool qDown = false;

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
            drawInfo = new DrawInfo(bitmap, x,y,50,50, 0f,0f,0f,true, 240);
            Base.drawInfos.Add(drawInfo);
        }


        public void StartDraw()
        {
            bitmap = new Bitmap(path);
            drawInfo = new DrawInfo(bitmap, StartPositionX, StartPositionY, 50, 50, 0f,0f,0f);
            Base.drawInfos.Add(drawInfo);
        }
        /*

        public void StartDraw(DrawInfo _drawInfo)
        {
            bitmap = new Bitmap(Application.StartupPath + path);
            Base.drawInfos.Add(_drawInfo);
        }*/
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
            if (throttle == true)
            {
                if (Math.Abs(0 - speed) <= 0.01)
                {
                    go = true;
                }
                if (speed > 0 || go)
                {
                    if (Math.Abs((((-(float)Math.Pow(acceleration * (i / 2) - Math.Sqrt(maxSpeed), 2)) + maxSpeed) * prevDelta) - prevSpeed) <= .5 || go)
                    {
                        go = false;
                        speed = ((-(float)Math.Pow(acceleration * (i / 2) - Math.Sqrt(maxSpeed), 2)) + maxSpeed) * deltaTime;
                        prevSpeed = speed;
                        prevDelta = deltaTime;
                        if (Math.Abs(1 - speed / maxSpeed) < 0.01)
                        {
                            speed = maxSpeed * deltaTime;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        i = 0;
                        while (Math.Abs((((-(float)Math.Pow(acceleration * (i / 2) - Math.Sqrt(maxSpeed), 2)) + maxSpeed) * deltaTime) - speed) > .5)
                        {
                            i++;
                            if (Math.Abs((((-(float)Math.Pow(acceleration * (i / 2) - Math.Sqrt(maxSpeed), 2)) + maxSpeed) * deltaTime) - speed) <= .5)
                            {
                                go = true;
                            }
                        }
                    }
                }
                else
                {
                    speed = (1 * (float)Math.Pow(deceleration * j, 2) + speed) * deltaTime;
                    if (Math.Abs(0 - speed) <= 0.01)
                    {
                        speed = 0;
                    }
                    else
                    {
                        j++;
                    }
                }
                //formule acceleratie
                //y=(-(acceleration*(x/2)-sqrt(topspeed))^2)+topspeed
            }
            else
            {
                if (speed > 0)
                {
                    if (speed < 0.05)
                    {
                        speed = 0;
                    }
                    else
                    {
                        speed -= deceleration * .5f * deltaTime;
                    }
                    i = 0;
                }
            }


            if (brake == true)
            {
                if (Math.Abs(0 - speed) <= 0.0002)
                {
                    go = true;
                }
                if (speed <= 0 || go)
                {
                    if (Math.Abs((-0.75f * ((-(float)Math.Pow(acceleration * (i / 2) - Math.Sqrt(maxSpeed), 2)) + maxSpeed) * prevDelta) - prevSpeedrev) <= .5 || go)
                    {
                        go = false;
                        speed = -0.75f * ((-(float)Math.Pow(acceleration * (i / 2) - Math.Sqrt(maxSpeed), 2)) + maxSpeed) * deltaTime;
                        prevSpeedrev = speed;
                        prevDelta = deltaTime;
                        if (Math.Abs(1 - speed / (maxSpeed * -0.75f)) < 0.01f)
                        {
                            speed = maxSpeed * -.75f * deltaTime;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        i = 0;
                        while (Math.Abs((((-(float)Math.Pow(acceleration * (i / 2) - Math.Sqrt(maxSpeed), 2)) + maxSpeed) * deltaTime) - speed) > .5)
                        {
                            i++;
                            if (Math.Abs((((-(float)Math.Pow(acceleration * (i / 2) - Math.Sqrt(maxSpeed), 2)) + maxSpeed) * deltaTime) - speed) <= .5)
                            {
                                go = true;
                            }
                        }
                    }
                }
                else
                {
                    //remmen
                    speed = (-1 * (float)Math.Pow(deceleration * j, 2) + speed) * deltaTime;
                    if (speed <= 0.01)
                    {
                        speed = 0;
                    }
                    else
                    {
                        j++;
                    }
                    //formule acceleratie
                    //speed=-deceleration x ^ 2 + speed
                }
            }
            else
            {
                if (speed < 0)
                {
                    if (speed > -0.05)
                    {
                        speed = 0;
                    }
                    else
                    {
                        speed += deceleration * .5f * deltaTime;
                    }
                    i = 0;
                }
            }

            if (turning == "right")
            {
                if (vehicletype != VehicleType.Tank)
                {
                    if (speed > 3)
                    {
                        drawInfo.angle += (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / (float)Math.Pow(speed, 2.25);
                    }
                    else
                    {
                        drawInfo.angle += (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / 15;
                    }
                }
                else
                {
                    drawInfo.angle += turnSpeed / (float)Math.Pow(speed, 1 / 5) * deltaTime;
                }
            }
            else if (turning == "left")
            {
                if (vehicletype != VehicleType.Tank)
                {
                    if (speed > 3)
                    {
                         drawInfo.angle-= (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / (float)Math.Pow(speed, 2.25);
                    }
                    else
                    {
                        drawInfo.angle -= (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / 15;
                    }
                }
                else
                {
                    drawInfo.angle -= turnSpeed / (float)Math.Pow(speed, 1 / 5) * deltaTime;
                }
            }
            else if (weapon.turning == "left")
            {
                WeaponDrawInfo.angle -= weapon.turnSpeed;
            }
            else if (weapon.turning == "right")
            {
                WeaponDrawInfo.angle += weapon.turnSpeed;
            }
            drawInfo.x += (float)(Math.Cos(drawInfo.angle * (Math.PI / 180)) * speed);
            drawInfo.y += (float)(Math.Cos((90 - drawInfo.angle) * (Math.PI / 180)) * speed);
            
            deltaTime = 1;
           // if (orig.GetPixel((int)BallPos.X - BallSizeW / 2, (int)BallPos.Y - BallSizeH / 2).G != 0)
           // {
           //     deltaTime = grassMultiplier;
         //   }


        }

    }





}
