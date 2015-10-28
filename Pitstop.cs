using System.Collections.Generic;
using System;
using RaceGame;

namespace RaceGame
{
    class Pitstop
    {
        int PosX = Base.currentGame.PitStopPoint.x;
        int PosY = Base.currentGame.PitStopPoint.y;
        int Range = 36;

        public Pitstop()
        {
            Base.gameTasks.Add(CheckPitStop);
        }

        void CheckPitStop()
        {
            if (Base.currentGame.player1.vehicle.drawInfo.x <= PosX + Range && Base.currentGame.player1.vehicle.drawInfo.y <= PosY + Range && Base.currentGame.player1.vehicle.drawInfo.x >= PosX - Range && Base.currentGame.player1.vehicle.drawInfo.y >= PosY - Range)
            {
                Base.currentGame.player1.vehicle.inPitstop = true;
            }

            else if(Base.currentGame.player1.vehicle.inPitstop)
            {
                Base.currentGame.player1.vehicle.pitstopCounter--;
                Base.currentGame.player1.vehicle.inPitstop = false;
            }

            if (Base.currentGame.player2.vehicle.drawInfo.x <= PosX + Range && Base.currentGame.player2.vehicle.drawInfo.y <= PosY + Range && Base.currentGame.player2.vehicle.drawInfo.x >= PosX - Range && Base.currentGame.player2.vehicle.drawInfo.y >= PosY - Range)
            {
                Base.currentGame.player2.vehicle.inPitstop = true;
            }

            else if (Base.currentGame.player2.vehicle.inPitstop)
            {
                Base.currentGame.player2.vehicle.pitstopCounter--;
                Base.currentGame.player2.vehicle.inPitstop = false;
            }


            if ((Base.currentGame.player1.vehicle.drawInfo.x <= PosX + Range && Base.currentGame.player1.vehicle.drawInfo.y <= PosY + Range && Base.currentGame.player1.vehicle.drawInfo.x >= PosX - Range && Base.currentGame.player1.vehicle.drawInfo.y >= PosY - Range) && Math.Abs(0-Base.currentGame.player1.vehicle.speed) < 0.1)
            {
                Base.currentGame.player1.vehicle.inPitstop = true;
                if (Base.currentGame.player1.vehicle.fuel < Base.currentGame.player1.vehicle.fuelCapacity)
                {
                    Console.WriteLine("refilling fuel; now :" + Base.currentGame.player1.vehicle.fuel);
                    Base.currentGame.player1.vehicle.fuel += 4;
                    if (Base.currentGame.player1.vehicle.fuel > Base.currentGame.player1.vehicle.fuelCapacity)
                    {
                        Base.currentGame.player1.vehicle.fuel = Base.currentGame.player1.vehicle.fuelCapacity;
                    }
                }

                if (Base.currentGame.player1.vehicle.health < Base.currentGame.player1.vehicle.maxHealth)
                {
                    Base.currentGame.player1.vehicle.health += 1;
                    Console.WriteLine("le health iz " + Base.currentGame.player1.vehicle.health);
                }
            }

            if ((Base.currentGame.player2.vehicle.drawInfo.x <= PosX + Range && Base.currentGame.player2.vehicle.drawInfo.y <= PosY + Range && Base.currentGame.player2.vehicle.drawInfo.x >= PosX - Range && Base.currentGame.player2.vehicle.drawInfo.y >= PosY - Range) && Math.Abs(0 - Base.currentGame.player2.vehicle.speed) < 0.1)
            {
                Base.currentGame.player2.vehicle.inPitstop = true;
                if (Base.currentGame.player2.vehicle.fuel < Base.currentGame.player2.vehicle.fuelCapacity)
                {
                    Console.WriteLine("refilling fuel; now :" + Base.currentGame.player2.vehicle.fuel);
                    Base.currentGame.player2.vehicle.fuel += 4;
                    if (Base.currentGame.player2.vehicle.fuel > Base.currentGame.player2.vehicle.fuelCapacity)
                    {
                        Base.currentGame.player2.vehicle.fuel = Base.currentGame.player2.vehicle.fuelCapacity;
                    }
                }

                if (Base.currentGame.player2.vehicle.health < Base.currentGame.player2.vehicle.maxHealth)
                {
                    Base.currentGame.player2.vehicle.health += 1;
                }
            }
        }
    }
}