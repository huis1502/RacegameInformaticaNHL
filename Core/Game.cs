using RaceGame.Pathfinding;
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
        int MapsizeY = 648;
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



            #region ProcessMap
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
            Base.drawInfos.Add(new DrawInfo(Background, MapsizeX / 2, MapsizeY / 2, MapsizeX, MapsizeY, 270, 0));
            this.Points = new System.Drawing.Point[Points.Count];
            for (int i = 0; i < Points.Count; i++)
            {
                System.Drawing.Point p = new System.Drawing.Point(Points[i].x, Points[i].y);
                this.Points[i] = p;
            }
            #endregion
        }
        public float CalculateDistance(Point a, Point b)
        {
            return (float)Math.Sqrt(Math.Pow(b.x -a.x, 2) + Math.Pow(b.y - a.y,2));
        }

        List<Point> FindPath(Point StartPosition, Point EndPosition, RoadType T)
        {
            AstarObject[,] Set = new AstarObject[MapsizeX, MapsizeY];
            for (int x = 0; x < MapsizeX; x++)
            {
                for (int y = 0; y < MapsizeY; y++)
                {
                    Set[x, y] = new AstarObject(x, y, this);
                }
            }

            Heap<AstarObject> OpenSet = new Heap<AstarObject>(MapsizeX * MapsizeY);
            HashSet<AstarObject> ClosedSet = new HashSet<AstarObject>();
            AstarObject Start = Set[StartPosition.x, StartPosition.y];
            AstarObject End = Set[EndPosition.x, EndPosition.y];
            OpenSet.Add(Start);

            while (OpenSet.Count > 0)
            {
                AstarObject CurrentLocation = OpenSet.RemoveFirst();

                ClosedSet.Add(CurrentLocation);

                if (CurrentLocation == End)
                {
                    return RetracePath(Start, End);
                    //Retracepath and stuff.
                }
                List<AstarObject> Neighbours = GetNeighbours(CurrentLocation, ref Set, NeighbourhoodType.Moore);
                foreach (AstarObject neighbour in Neighbours)
                {
                    if (neighbour.RType != T || ClosedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = CurrentLocation.gCost + GetDistance(CurrentLocation, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !OpenSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, End);
                        neighbour.parent = CurrentLocation;

                        if (!OpenSet.Contains(neighbour))
                        {
                            OpenSet.Add(neighbour);
                        }
                        else
                        {
                            OpenSet.UpdateItem(neighbour);
                        }

                    }

                }

            }
            return new List<Point>();
        }
        List<AstarObject> GetNeighbours(AstarObject A, ref AstarObject[,] Set, NeighbourhoodType NType)
        {
            if (NType == NeighbourhoodType.Moore)
            {
                List<AstarObject> Neighbours = new List<AstarObject>();
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0)
                            continue;

                        int CheckX = A.x + x;
                        int CheckY = A.y + y;

                        if (CheckX >= 0 && CheckX < MapsizeX && CheckY >= 0 && CheckY < MapsizeY)
                        {
                            Neighbours.Add(Set[CheckX, CheckY]);
                        }
                    }
                }
                return Neighbours;
            }
            else
            {
                if (NType == NeighbourhoodType.Neumann)
                {
                    List<AstarObject> Neighbours = new List<AstarObject>();
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0)
                                continue;

                            int CheckX = A.x + x;
                            int CheckY = A.y + y;

                            if (CheckX >= 0 && CheckX < MapsizeX && CheckY >= 0 && CheckY < MapsizeY)
                            {
                                Neighbours.Add(Set[CheckX, CheckY]);
                            }
                        }
                    }
                    return Neighbours;
                }
            }
            return null;
        }

        int GetDistance(AstarObject A, AstarObject B)
        {
            int dstX = Math.Abs(A.x - B.x);
            int dstY = Math.Abs(A.y - B.y);

            if (dstX > dstY)
                return 14 * dstY + 10 * dstX;
            return 14 * dstX + 10 * (dstY - dstX);

        }
        List<Point> RetracePath(AstarObject Start, AstarObject End)
        {
            List<Point> path = new List<Point>();

            AstarObject CurrentA = End;
            while (CurrentA != Start)
            {
                path.Add(CurrentA.ToPoint());
                CurrentA = CurrentA.parent;
            }
            path.Reverse();
            return path;
        }
    }
}