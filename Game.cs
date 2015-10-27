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
        public int MapsizeX = 1008;
        public int MapsizeY = 747;
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
            player1.CreateVehicle(Enums.VehicleType.Tank, -20);
            player1.vehicle.StartDraw();
            player1.vehicle.StartWeaponDraw();
            player1.vehicle.startTestDraw();
            player2 = new Player(2);
            player2.CreateVehicle(Enums.VehicleType.Tank, 20);
            player2.vehicle.StartDraw();
            player2.vehicle.StartWeaponDraw();
            player2.vehicle.startTestDraw();
            Base.gameTasks.Add(player1.vehicle.Appelnoot);
            Base.gameTasks.Add(player2.vehicle.Appelnoot);
        }

        public void CreateMap()
        {
            List<Point> Points = new List<Point>();
            int SegmentWidth = 100;
            for (int x = 0; x < (MapsizeX / SegmentWidth); x++ )
            {
                for (int y = 0; y < (MapsizeY / SegmentWidth); y++)
                {
                    if ( (x == 1 || y == 1 || x == (MapsizeX / SegmentWidth - 2) || y == (MapsizeY / SegmentWidth - 2) ) && x != 0 && y != 0 && x != (MapsizeX/SegmentWidth-1) && y != (MapsizeY/SegmentWidth-1) )
                    {
                        PointType pointtype;
                        if (x == 1 &&  y == 1 || x==1 && y == MapsizeY / SegmentWidth - 2 ||  x == MapsizeX / SegmentWidth - 2 && y == 1 || x == MapsizeX / SegmentWidth - 2 && y == MapsizeY / SegmentWidth - 2)
                        {
                            pointtype = PointType.Corner;
                        }
                        else
                        {
                            pointtype = PointType.Inner;
                        }

                        int MidpointX = x * SegmentWidth + (SegmentWidth / 2);
                        int MidpointY = y * SegmentWidth + (SegmentWidth / 2);

                        if (pointtype == PointType.Inner)
                        {
                            int xpos = random.Next(-10,10);
                            int ypos = random.Next(-10, 10);
                            MidpointX += xpos;
                            MidpointY += ypos;
                        }
                        Points.Add(new Point(pointtype, MidpointX, MidpointY));
                    }
                }
            }
            for (int i = 0; i < Points.Count; i++)
            {
                GameField[Points[i].x, Points[i].y] = 255;
            }
            Queue<Point> PointQueue = new Queue<Point>();
            PointQueue.Enqueue(Points[0]);
            List<Point> PointsDone = new List<Point>();
            while (PointQueue.Count != 0)
            {
                Point P = PointQueue.Dequeue();
                float MinDistance = -1;
                Point NextPoint = null;
                for (int j = 0; j < Points.Count; j++)
                {
                    if (P != Points[j] && !PointsDone.Contains(Points[j]))
                    {
                        float Distance = CalculateDistance(P, Points[j]);
                        if (MinDistance == -1 || MinDistance > Distance)
                        {
                            MinDistance = Distance;
                            NextPoint = Points[j];
                        }
                    }
                }
                if (NextPoint != null)
                {
                    PointsDone.Add(NextPoint);
                    PointQueue.Enqueue(NextPoint);
                }
            }
            PointsDone.Add(PointsDone[0]);
            Points = PointsDone;
            //Background = new Bitmap(MapsizeX, MapsizeY);
            Background = new Bitmap("track3.bmp");
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
                    //Background.SetPixel(x,y, color);
                }
            }
            CatMullRomSpline();


            //Base.drawInfos.Add(new Structs.DrawInfo(Background,MapsizeX/2,MapsizeY/2,MapsizeX,MapsizeY));

            Base.drawInfos.Add(new Structs.DrawInfo(Background, MapsizeX/2, MapsizeY/2, MapsizeX, MapsizeY, 270, 0));
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

        protected void CatMullRomSpline()
        {

        }

    }
}
