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
        public DrawInfo weaponDrawInfo;

        public DrawInfo testdraw;

        public Bitmap bitmap;
        public Bitmap weaponSprite;

        public Bitmap bm;

        public Player player;
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
        public bool shooting = false;

        float deltaTime = 1.00f;
        float prevDelta = 1.00f;
        float speed = 0f;
        float prevSpeed = 0f;
        float prevSpeedrev = 0f;
        public int i = 0;
        public int j = 0;
        private bool go = false;
        public string turning = "false";
        public string throttle = "";
        public bool brake = false;


        public Vehicle(int x, int y,VehicleType t, Player t2)
        {
            vehicletype = t;
            StartPositionX = x;
            StartPositionY = y;
            player = t2;
            Base.gameTasks.Add(CheckShooting);
            Base.gameTasks.Add(CheckCollision);
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

        public void startTestDraw()
        {
            bm = new Bitmap("vlam.png");
            testdraw = new DrawInfo(bm, 500, 500, 20,20, 0, 0,drawInfo.angle);
            //Base.drawInfos.Add(testdraw);
            Console.WriteLine("hier");
            Base.drawInfos.Add(testdraw);
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
            //Console.WriteLine(throttle);
            if (throttle == "go")
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
                        if (speed <= maxSpeed)
                        {
                            while (Math.Abs((((-(float)Math.Pow(acceleration * (i / 2) - Math.Sqrt(maxSpeed), 2)) + maxSpeed) * deltaTime) - speed) > .5)
                            {
                                i++;
                                if (Math.Abs((((-(float)Math.Pow(acceleration * (i / 2) - Math.Sqrt(maxSpeed), 2)) + maxSpeed) * deltaTime) - speed) <= .5)
                                {
                                    go = true;
                                    Console.WriteLine("na dit gaat hij stuk");
                                }
                            }
                        }
                        else
                        {
                            speed = maxSpeed;
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

            //Console.WriteLine(speed);

            if (turning == "right")
            {
                if (vehicletype != VehicleType.Tank)
                {
                    if (speed > 3)
                    {
                        float rotationPlus = (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / (float)Math.Pow(speed, 2.25);
                        drawInfo.angle += rotationPlus;
                        weaponDrawInfo.angle += rotationPlus;
                    }
                    else
                    {
                        float rotationPlus = (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / 15;
                        drawInfo.angle += rotationPlus;
                        weaponDrawInfo.angle += rotationPlus;
                    }
                }
                else
                {
                    float rotationPlus = turnSpeed / (float)Math.Pow(speed, 1 / 5) * deltaTime;
                    drawInfo.angle += rotationPlus;
                    weaponDrawInfo.angle += rotationPlus;
                }
            }
            else if (turning == "left")
            {
                if (vehicletype != VehicleType.Tank)
                {
                    if (speed > 3)
                    {
                        float rotationPlus = (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / (float)Math.Pow(speed, 2.25);
                        drawInfo.angle -= rotationPlus;
                        weaponDrawInfo.angle -= rotationPlus;
                    }
                    else
                    {
                        drawInfo.angle -= (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / 15;
                        weaponDrawInfo.angle -= (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / 15;
                    }
                }
                else
                {
                    float rotationPlus = turnSpeed / (float)Math.Pow(speed, 1 / 5) * deltaTime;
                    drawInfo.angle -= rotationPlus;
                    weaponDrawInfo.angle -= rotationPlus;
                }
            }
            else if (weapon.turning == "left")
            {
                weaponDrawInfo.angle -= weapon.turnSpeed;
            }
            else if (weapon.turning == "right")
            {
                weaponDrawInfo.angle += weapon.turnSpeed;
            }


            if (Math.Abs(180 - (drawInfo.angle - weaponDrawInfo.angle)) % 360 < 30 && shooting && throttle == "go" && speed >= maxSpeed)
            {
                speed *= 4;
            }

            drawInfo.x += (float)(Math.Cos(drawInfo.angle * (Math.PI / 180)) * speed);
            drawInfo.y += (float)(Math.Cos((90 - drawInfo.angle) * (Math.PI / 180)) * speed);

            weaponDrawInfo.x = drawInfo.x;
            weaponDrawInfo.y = drawInfo.y;

            deltaTime = 1;
            // if (orig.GetPixel((int)BallPos.X - BallSizeW / 2, (int)BallPos.Y - BallSizeH / 2).G != 0)
            // {
            //     deltaTime = grassMultiplier;
            //   }
        }

        public void CheckShooting()
        {
            if (shooting)
            {
                weapon.shoot();
            }
        }

        public void CheckCollision()
        {
            if (Base.currentGame.player1 == player) {
                //current player is player 1
                //  Point topleft=(drawInfo.x+16
                int width = 20;
                int length = 32;
                float topleftx = (float)(drawInfo.x + Math.Cos((drawInfo.angle % 360) * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
                float toplefty = (float)(drawInfo.y + Math.Sin((drawInfo.angle % 360) * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

                float backrightx = (float)(drawInfo.x - Math.Cos((drawInfo.angle % 360) * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
                float backrighty = (float)(drawInfo.y - Math.Sin((drawInfo.angle % 360) * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

                testdraw.x = backrightx;
                testdraw.y = backrighty;
                Console.WriteLine(testdraw.x);
                //Console.WriteLine("x van testdraw.x: " + testdraw.x + "\nWidth: " + width + "\nLength: " + length + "\nangle: " + drawInfo.angle % 360 + "\nDraw X: " + drawInfo.x);

                if (true)
                {
                    //collide op x of y as aan 1 kant
                    //Console.WriteLine("Collide");
                }
                else
                {
                    //Console.WriteLine("Niet collide");
                }
            }
        }
    }
}
