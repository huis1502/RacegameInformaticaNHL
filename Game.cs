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
        int MapsizeY = 747;
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

            for (int i = 0; i < 5; i++)
            {
                PointsDone = CatMullRom(PointsDone);
            }

            for (int i = 0; i < PointsDone.Count; i++)
            {
                Console.WriteLine(PointsDone[i].x + "-" + PointsDone[i].y);
                int x = PointsDone[i].x;
                int y = PointsDone[i].y;

                if (x >= 0 && y >= 0 && x < MapsizeX && y < MapsizeY)
                {
                    GameField[PointsDone[i].x, PointsDone[i].y] = 255;
                }
            }

            Points = PointsDone;
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
                    Background.SetPixel(x,y, color);
                }
            }


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

        public List<Point> CatMullRom(List<Point> _points)
        {
            int OriginalPointsCount = _points.Count;

            for (int i = 0; i < OriginalPointsCount * 2; i += 2)
            {
                Point zero = _points[i];
                Point one;
                Point two;
                Point three;

                #region IFs
                if (i + 1 == _points.Count)
                {
                    //+1
                    one = _points[0];
                    two = _points[1];
                    three = _points[2];
                }
                else
                {
                    if (i + 2 == _points.Count)
                    {
                        one = _points[i + 1];
                        two = _points[0];
                        three = _points[1];
                    }
                    else
                    {
                        if (i + 3 == _points.Count)
                        {
                            one = _points[i + 1];
                            two = _points[i + 2];
                            three = _points[0];
                        }
                        else
                        {
                            one = _points[i + 1];
                            two = _points[i + 2];
                            three = _points[i + 3];
                        }
                    }
                }
                #endregion

                Point X;
                var u = 0.5f;
                var u3 = u * u * u;
                var u2 = u * u;
                var f1 = -0.5 * u3 + u2 - 0.5 * u;
                var f2 = 1.5 * u3 - 2.5 * u2 + 1.0;
                var f3 = -1.5 * u3 + 2.0 * u2 + 0.5 * u;
                var f4 = 0.5 * u3 - 0.5 * u2;
                var x = zero.x * f1 + one.x * f2 + two.x * f3 + three.x * f4;
                var y = zero.y * f1 + one.y * f2 + two.y * f3 + three.y * f4;


                X = new Point(PointType.Inner, (int)x,(int)y);
                _points.Insert(_points.IndexOf(two) + 1, X);
            }
            return _points; ;
        }

    }
}
