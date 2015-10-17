using RaceGame.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace RaceGame
{
    public class Game
    {
        public Player player1;
        public Player player2;
        public byte[,] GameField;
        int MapsizeX = 1008;
        int MapsizeY = 9*72;
        public Bitmap Background;
        protected Random random;
        public System.Drawing.Point[] Points;

        public Game()
        {
            GameField = new byte[MapsizeX, MapsizeY];
            random = new Random(DateTime.UtcNow.GetHashCode());
            CreateMap();
            CreatePlayers();
        }

        public void Escape()
        {
            Console.WriteLine("Pressed escape!");
        }

        public void CreatePlayers()
        {
            player1 = null;
            player2 = null;
            player1 = new Player(1);
            player1.CreateVehicle(Enums.VehicleType.Tank);
            player1.vehicle.StartDraw();
            player1.vehicle.StartWeaponDraw();
            player2 = new Player(2);
            player2.CreateVehicle(Enums.VehicleType.Tank);
            player2.vehicle.StartDraw();
            player2.vehicle.StartWeaponDraw();
            Base.gameTasks.Add(player1.vehicle.Appelnoot);
            Base.gameTasks.Add(player2.vehicle.Appelnoot);
        }

        public void CreateMap()
        {
            List<Point> Points = new List<Point>();
            int SegmentWidth = 72;
            for (int x = 0; x < (MapsizeX / SegmentWidth); x++)
            {
                for (int y = 0; y < (MapsizeY / SegmentWidth); y++)
                {   
                    int MidpointX = x * SegmentWidth + (SegmentWidth / 2);
                    int MidpointY = y * SegmentWidth + (SegmentWidth / 2);
                    Points.Add(new Point(MidpointX, MidpointY));
                }
            }

            for (int i = 0; i < Points.Count; i++)
            {
                int x = Points[i].x;
                int y = Points[i].y;

                if (x >= 0 && y >= 0 && x < MapsizeX && y < MapsizeY)
                {
                    GameField[Points[i].x, Points[i].y] = 255;
                }
            }
            Background = new Bitmap(MapsizeX, MapsizeY);
            for (int x = 0; x < MapsizeX; x++)
            {
                for (int y = 0; y < MapsizeY; y++)
                {
                    Color color;
                    if (GameField[x, y] == 0)
                    {
                        color = Color.Black;
                    }
                    else
                    {
                        color = Color.White;
                    }
                    Background.SetPixel(x, y, color);
                }
            }
            Base.drawInfos.Add(new Structs.DrawInfo(Background, MapsizeX / 2, MapsizeY / 2, MapsizeX, MapsizeY, 270, 0));
            this.Points = new System.Drawing.Point[Points.Count];
            for (int i = 0; i < Points.Count; i++)
            {
                System.Drawing.Point p = new System.Drawing.Point(Points[i].x, Points[i].y);
                this.Points[i] = p;
            }
        }
        public float CalculateDistance(Point a, Point b)
        {
            return (float)Math.Sqrt(Math.Pow(b.x -a.x, 2) + Math.Pow(b.y - a.y,2));
        }






    }
}
