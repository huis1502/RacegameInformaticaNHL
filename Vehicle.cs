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
        public int health;
        public int ramDamage;
        public float sideDamageMultiplier;
        public float grassMultiplier;
        public bool shooting = false;

        public PointF topleft;
        public PointF topright;
        public PointF backleft;
        public PointF backright;

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
                speed *= 2;
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
                Console.WriteLine("ratatata pew");
            }
        }

        public void CheckCollision()
        {
            //  Point topleft=(drawInfo.x+16
            float width = 20;
            float length = 32;
            float temgle = (float)(drawInfo.angle - (Math.Atan(((width / 2) / (length / 2))) * (180 / Math.PI)));
            float topleftx = (float)(drawInfo.x + Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
            float toplefty = (float)(drawInfo.y + Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

            float toprightx = (float)(drawInfo.x + Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
            float toprighty = (float)(drawInfo.y - Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

            float backleftx = (float)(drawInfo.x - Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
            float backlefty = (float)(drawInfo.y + Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

            float backrightx = (float)(drawInfo.x - Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
            float backrighty = (float)(drawInfo.y - Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

            topleft = new PointF(topleftx, toplefty);
            topright = new PointF(toprightx, toprighty);
            backleft = new PointF(backleftx, backlefty);
            backright = new PointF(backrightx, backrighty);

            testdraw.x = topleft.X;
            testdraw.y = topleft.Y;
            //Console.WriteLine("x van testdraw.x: " + testdraw.x + "\nWidth: " + width + "\nLength: " + length + "\nangle: " + drawInfo.angle % 360 + "\nDraw X: " + drawInfo.x);

            if (topleft.X > Base.currentGame.MapsizeX || topright.X > Base.currentGame.MapsizeX || backleft.X > Base.currentGame.MapsizeX || backright.X > Base.currentGame.MapsizeX
                || topleft.X<0||topright.X<0||backleft.X<0||backright.X<0
                || topleft.Y > Base.currentGame.MapsizeY || topright.Y > Base.currentGame.MapsizeY || backleft.Y > Base.currentGame.MapsizeY || backright.Y > Base.currentGame.MapsizeY
                || topleft.Y < 0 || topright.Y < 0 || backleft.Y < 0 || backright.Y < 0)
            {
                drawInfo.angle += 180;
                weaponDrawInfo.angle += 180;
            }

            if (player == Base.currentGame.player1)
            {
                DrawInfo p2 = Base.currentGame.player2.vehicle.drawInfo;

                if (Math.Abs(p2.x - topleft.X) < width && Math.Abs(p2.y - topleft.Y) < width || Math.Abs(p2.x - topright.X) < width && Math.Abs(p2.y - topright.Y) < width
                    || Math.Abs(p2.x - backleft.X) < width && Math.Abs(p2.y - backleft.Y) < width || Math.Abs(p2.x - backright.X) < width && Math.Abs(p2.y - backright.Y) < width
                   )
                {
                    drawInfo.angle += 180;
                    weaponDrawInfo.angle += 180;
                }
            }
        }
    }
}
