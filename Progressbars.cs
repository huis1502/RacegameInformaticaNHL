using System;
using RaceGame.Enums;
using System.Windows.Forms;

namespace RaceGame
{

    public static class Progressbars
    {
        public static void Initialize()
        {
            Base.windowHandle.Player1Fuel.Maximum = Convert.ToInt32(Base.currentGame.player1.vehicle.fuelCapacity);
            Base.windowHandle.Player2Fuel.Maximum = Convert.ToInt32(Base.currentGame.player2.vehicle.fuelCapacity);
            Base.windowHandle.Player1Health.Maximum = Convert.ToInt32(Base.currentGame.player1.vehicle.maxHealth);
            Base.windowHandle.Player2Health.Maximum = Convert.ToInt32(Base.currentGame.player2.vehicle.maxHealth);
            Base.windowHandle.Player1Speed.Maximum = Convert.ToInt32(Base.currentGame.player1.vehicle.maxSpeed * 100);
            Base.windowHandle.Player2Speed.Maximum = Convert.ToInt32(Base.currentGame.player2.vehicle.maxSpeed * 100);
        }

        public static void Check()
        {
            Base.windowHandle.Player1Fuel.Value = Convert.ToInt32(Base.currentGame.player1.vehicle.fuel);
            Base.windowHandle.Player2Fuel.Value = Convert.ToInt32(Base.currentGame.player2.vehicle.fuel);
            Base.windowHandle.Player1Health.Value = Base.currentGame.player1.vehicle.health;
            Base.windowHandle.Player2Health.Value = Base.currentGame.player2.vehicle.health;
            if (Base.currentGame.player1.vehicle.speed > Base.currentGame.player1.vehicle.maxSpeed)
            {
                Base.windowHandle.Player1Speed.Value = Convert.ToInt32(Base.currentGame.player1.vehicle.maxSpeed * 100);
            }
            else
            {
                Base.windowHandle.Player1Speed.Value = Convert.ToInt32(Math.Abs(Base.currentGame.player1.vehicle.speed * 100));
            }
            if (Base.currentGame.player2.vehicle.speed > Base.currentGame.player2.vehicle.maxSpeed)
            {
                Base.windowHandle.Player2Speed.Value = Convert.ToInt32(Base.currentGame.player2.vehicle.maxSpeed * 100);
            }
            else
            {
                Base.windowHandle.Player2Speed.Value = Convert.ToInt32(Math.Abs(Base.currentGame.player2.vehicle.speed * 100));
            }


        }
    }

    public static class PitLabels
    {
        static string TIJDELIJKEPIT1COUNT = "0";
        static string TIJDELIJKEPIT2COUNT = "0";

        static void Check()
        {
            Base.windowHandle.Player1PitCount.Text = TIJDELIJKEPIT1COUNT;
            Base.windowHandle.Player2PitCount.Text = TIJDELIJKEPIT2COUNT;

        }
    }

    public static class LapLabels
    {
        static int TIJDELIJKELAPA1 = 1;
        static int TIJDELIJKELAPB1 = 5;

        static int TIJDELIJKELAPA2 = 1;
        static int TIJDELIJKELAPB2 = 5;

        static string TIJDELIJKELAPCOUNT1 = TIJDELIJKELAPA1 + "/" + TIJDELIJKELAPB1;
        static string TIJDELIJKELAPCOUNT2 = TIJDELIJKELAPA2 + "/" + TIJDELIJKELAPB2;

        static void Check()
        {
            Base.windowHandle.Player1LapCount.Text = TIJDELIJKELAPCOUNT1;
            Base.windowHandle.Player2LapCount.Text = TIJDELIJKELAPCOUNT2;

        }
    }

}
