using RaceGame.Enums;
using System;
using System.Drawing;

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
            turnSpeed = 2;
            name = "Tank";
            bitmap = Bitmaps.Vehicles.TankBody;
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
            turnSpeed = 1.3f;
            bitmap = Bitmaps.Vehicles.Jackass;
            weapon = null;
            maxHealth = 75;
            health = maxHealth;
            ramDamage = 200;
            sideDamageMultiplier = 2f;
            grassMultiplier = 0.4f;
        }

        public override void StartWeaponDraw() { weaponDrawInfo = new DrawInfo(Bitmaps.Vehicles.Transparent,0,0,16,16); }
        public override void Appelnoot()
        {
            //Console.WriteLine(throttle);
            if (throttle)
            {
                fuel -= 0.04f;
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
                    }
                    else
                    {
                        float rotationPlus = (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / 15;
                        drawInfo.angle += rotationPlus;
                    }
                }
                else
                {
                    float rotationPlus = turnSpeed / (float)Math.Pow(speed, 1 / 5) * deltaTime;
                    drawInfo.angle += rotationPlus;
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
                    }
                    else
                    {
                        drawInfo.angle -= (turnSpeed * 3 / (float)Math.Pow(speed, 1 / 3)) * deltaTime * speed / 15;
                    }
                }
                else
                {
                    float rotationPlus = turnSpeed / (float)Math.Pow(speed, 1 / 5) * deltaTime;
                    drawInfo.angle -= rotationPlus;
                }
            }

            int NextX = (int)(drawInfo.x + (float)(Math.Cos(drawInfo.angle * (Math.PI / 180)) * speed));
            int NextY = (int)(drawInfo.y + (float)(Math.Cos((90 - drawInfo.angle) * (Math.PI / 180)) * speed));

            bool CanMove = true;

            for (int k = 0; k < Base.currentGame.ObstaclesList.Count; k++)
            {
                if (GetDistance(Base.currentGame.ObstaclesList[k].x, Base.currentGame.ObstaclesList[k].y, NextX, NextY) < 72)
                {
                    int MinXRange = Base.currentGame.ObstaclesList[k].x - Base.currentGame.ObstaclesList[k].range;
                    int MaxXRange = Base.currentGame.ObstaclesList[k].x + Base.currentGame.ObstaclesList[k].range;

                    int MinYRange = Base.currentGame.ObstaclesList[k].y - Base.currentGame.ObstaclesList[k].range;
                    int MaxYRange = Base.currentGame.ObstaclesList[k].y + Base.currentGame.ObstaclesList[k].range;

                    if (NextX > MinXRange && NextX < MaxXRange && NextY > MinYRange && NextY < MaxYRange)
                    {
                        CanMove = false;
                    }

                    float width = 20;
                    float length = 32;
                    float temgle = (float)(drawInfo.angle - (Math.Atan(((width / 2) / (length / 2))) * (180 / Math.PI)));
                    float topleftx = (float)(NextX + Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
                    float toplefty = (float)(NextY + Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

                    float toprightx = (float)(NextX + Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
                    float toprighty = (float)(NextY - Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

                    float backleftx = (float)(NextX - Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
                    float backlefty = (float)(NextY + Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

                    float backrightx = (float)(NextX - Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
                    float backrighty = (float)(NextY - Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

                    topleft = new PointF(topleftx, toplefty);
                    topright = new PointF(toprightx, toprighty);
                    backleft = new PointF(backleftx, backlefty);
                    backright = new PointF(backrightx, backrighty);

                    if (player == Base.currentGame.player1)
                    {
                        DrawInfo p2 = Base.currentGame.player2.vehicle.drawInfo;

                        if (Math.Abs(p2.x - topleft.X) < width && Math.Abs(p2.y - topleft.Y) < width || Math.Abs(p2.x - topright.X) < width && Math.Abs(p2.y - topright.Y) < width
                            || Math.Abs(p2.x - backleft.X) < width && Math.Abs(p2.y - backleft.Y) < width || Math.Abs(p2.x - backright.X) < width && Math.Abs(p2.y - backright.Y) < width
                           )
                        {
                            CanMove = false;
                        }
                    }

                    if (player == Base.currentGame.player2)
                    {
                        DrawInfo p1 = Base.currentGame.player1.vehicle.drawInfo;

                        if (Math.Abs(p1.x - topleft.X) < width && Math.Abs(p1.y - topleft.Y) < width || Math.Abs(p1.x - topright.X) < width && Math.Abs(p1.y - topright.Y) < width
                            || Math.Abs(p1.x - backleft.X) < width && Math.Abs(p1.y - backleft.Y) < width || Math.Abs(p1.x - backright.X) < width && Math.Abs(p1.y - backright.Y) < width
                           )
                        {
                            CanMove = false;
                        }
                    }
                }
            }
            if (CanMove)
            {
                if (NextX > 0 && NextX < Base.currentGame.MapsizeX && NextY > 0 && NextY < Base.currentGame.MapsizeY)
                {
                    drawInfo.x += (float)(Math.Cos(drawInfo.angle * (Math.PI / 180)) * speed);
                    drawInfo.y += (float)(Math.Cos((90 - drawInfo.angle) * (Math.PI / 180)) * speed);
                }
            }
            else
            {
                speed = 0f;
                i = 0;
            }

            deltaTime = 1;
            // if (orig.GetPixel((int)BallPos.X - BallSizeW / 2, (int)BallPos.Y - BallSizeH / 2).G != 0)
            // {
            //     deltaTime = grassMultiplier;
            //   }
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
            turnSpeed = 2.4f;
            bitmap = Bitmaps.Vehicles.LAPVBody;
            weapon = new LAPVWeapon(_player);
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 120;
            health = maxHealth;
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
            turnSpeed = 2.3f;
            bitmap = Bitmaps.Vehicles.HorsePowerBody;
            weapon = new HorsePowerWeapon(_player);
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 90;
            health = maxHealth;
            ramDamage = 40;
            sideDamageMultiplier = 1.5f;
            grassMultiplier = 0.6f;
        }

        public override void StartWeaponDraw() { weaponDrawInfo = new DrawInfo(Bitmaps.Vehicles.Transparent, 0, 0, 16, 16); }
        public override void Appelnoot()
        {
            //Console.WriteLine(throttle);
            if (throttle)
            {
                fuel -= 0.04f;
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

            int NextX = (int)(drawInfo.x + (float)(Math.Cos(drawInfo.angle * (Math.PI / 180)) * speed));
            int NextY = (int)(drawInfo.y + (float)(Math.Cos((90 - drawInfo.angle) * (Math.PI / 180)) * speed));

            bool CanMove = true;

            for (int k = 0; k < Base.currentGame.ObstaclesList.Count; k++)
            {
                if (GetDistance(Base.currentGame.ObstaclesList[k].x, Base.currentGame.ObstaclesList[k].y, NextX, NextY) < 72)
                {
                    int MinXRange = Base.currentGame.ObstaclesList[k].x - Base.currentGame.ObstaclesList[k].range;
                    int MaxXRange = Base.currentGame.ObstaclesList[k].x + Base.currentGame.ObstaclesList[k].range;

                    int MinYRange = Base.currentGame.ObstaclesList[k].y - Base.currentGame.ObstaclesList[k].range;
                    int MaxYRange = Base.currentGame.ObstaclesList[k].y + Base.currentGame.ObstaclesList[k].range;

                    if (NextX > MinXRange && NextX < MaxXRange && NextY > MinYRange && NextY < MaxYRange)
                    {
                        CanMove = false;
                    }

                    float width = 20;
                    float length = 32;
                    float temgle = (float)(drawInfo.angle - (Math.Atan(((width / 2) / (length / 2))) * (180 / Math.PI)));
                    float topleftx = (float)(NextX + Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
                    float toplefty = (float)(NextY + Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

                    float toprightx = (float)(NextX + Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
                    float toprighty = (float)(NextY - Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

                    float backleftx = (float)(NextX - Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
                    float backlefty = (float)(NextY + Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

                    float backrightx = (float)(NextX - Math.Cos(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));
                    float backrighty = (float)(NextY - Math.Sin(temgle * (Math.PI / 180)) * Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(length / 2, 2)));

                    topleft = new PointF(topleftx, toplefty);
                    topright = new PointF(toprightx, toprighty);
                    backleft = new PointF(backleftx, backlefty);
                    backright = new PointF(backrightx, backrighty);

                    if (player == Base.currentGame.player1)
                    {
                        DrawInfo p2 = Base.currentGame.player2.vehicle.drawInfo;

                        if (Math.Abs(p2.x - topleft.X) < width && Math.Abs(p2.y - topleft.Y) < width || Math.Abs(p2.x - topright.X) < width && Math.Abs(p2.y - topright.Y) < width
                            || Math.Abs(p2.x - backleft.X) < width && Math.Abs(p2.y - backleft.Y) < width || Math.Abs(p2.x - backright.X) < width && Math.Abs(p2.y - backright.Y) < width
                           )
                        {
                            CanMove = false;
                        }
                    }

                    if (player == Base.currentGame.player2)
                    {
                        DrawInfo p1 = Base.currentGame.player1.vehicle.drawInfo;

                        if (Math.Abs(p1.x - topleft.X) < width && Math.Abs(p1.y - topleft.Y) < width || Math.Abs(p1.x - topright.X) < width && Math.Abs(p1.y - topright.Y) < width
                            || Math.Abs(p1.x - backleft.X) < width && Math.Abs(p1.y - backleft.Y) < width || Math.Abs(p1.x - backright.X) < width && Math.Abs(p1.y - backright.Y) < width
                           )
                        {
                            CanMove = false;
                        }
                    }
                }
            }
            if (CanMove)
            {
                if (NextX > 0 && NextX < Base.currentGame.MapsizeX && NextY > 0 && NextY < Base.currentGame.MapsizeY)
                {
                    drawInfo.x += (float)(Math.Cos(drawInfo.angle * (Math.PI / 180)) * speed);
                    drawInfo.y += (float)(Math.Cos((90 - drawInfo.angle) * (Math.PI / 180)) * speed);
                }
            }
            else
            {
                speed = 0f;
                i = 0;
            }

            weaponDrawInfo.x = drawInfo.x;
            weaponDrawInfo.y = drawInfo.y;

            deltaTime = 1;
            // if (orig.GetPixel((int)BallPos.X - BallSizeW / 2, (int)BallPos.Y - BallSizeH / 2).G != 0)
            // {
            //     deltaTime = grassMultiplier;
            //   }
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
            turnSpeed = 3.4f;
            bitmap = Bitmaps.Vehicles.MotorfietsBody;
            weapon = new LAPVWeapon(_player);
            relativeWeaponPos.X = 0;
            relativeWeaponPos.Y = 0;
            maxHealth = 60;
            health = maxHealth;
            ramDamage = 30;
            sideDamageMultiplier = 2;
            grassMultiplier = 0.4f;
        }
    }
}
