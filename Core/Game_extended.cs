using System;
using RaceGame.Pathfinding;
using System.Collections.Generic;
using System.Drawing;

namespace RaceGame
{
    public partial class Game
    {
        /*
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
                List<AstarObject> Neighbours = GetNeighbours(CurrentLocation, ref Set, NeighbourhoodType.Moore,MapsizeX,MapsizeY);
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
        List<Point> CreateSegments(List<Point> Points)
        {
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
            return Points;
        }
        List<Point> ReorderPoints(List<Point> Points, bool CopyEndPoint = false)
        {
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
            if (CopyEndPoint)
                PointsDone.Add(Points[0]);

            return PointsDone;
        }
        List<Point> AddPaths(List<Point> Points, RoadType rt)
        {
            List<Point> OriginalSet = new List<Point>();
            for (int i = 0; i < Points.Count; i++)
            {
                OriginalSet.Add(Points[i]);
            }
            OriginalSet.Add(OriginalSet[0]);
            for (int i = 0; i < OriginalSet.Count - 1; i++)
            {
                List<Point> Return = FindPath(OriginalSet[i], OriginalSet[i + 1], rt);
                for (int j = 0; j < Return.Count; j++)
                {
                    Points.Insert(i + 1 + j, Return[j]);
                }
                Console.WriteLine("Iteration " + i + "done, of " + (OriginalSet.Count - 1) + " iterations");
            }
            return Points;
        }
        void ProcessPointMap(List<Point> Points)
        {
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
        }
        */
        List<Point> FindRoadPath(Road a, Road b, RoadType T)
        {
            AstarObject[,] Set = new AstarObject[14, 9];
            for (int x = 0; x < 14; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Set[x, y] = new AstarObject(x, y, this);
                }
            }

            Heap<AstarObject> OpenSet = new Heap<AstarObject>(14 * 9);
            HashSet<AstarObject> ClosedSet = new HashSet<AstarObject>();
            AstarObject Start = Set[a.X, a.Y];
            AstarObject End = Set[b.X, b.Y];
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

                List<AstarObject> Neighbours = GetNeighbours(CurrentLocation, ref Set, NeighbourhoodType.Neumann, MapsizeXR, MapsizeYR);
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
        List<AstarObject> GetNeighbours(AstarObject A, ref AstarObject[,] Set, NeighbourhoodType NType, int MapsizeX, int MapsizeY)
        {
            switch (NType)
            {
                case NeighbourhoodType.Moore:
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
                case NeighbourhoodType.Neumann:
                    if (NType == NeighbourhoodType.Neumann)
                    {
                        List<AstarObject> Neighbours2 = new List<AstarObject>();
                        int CheckX = A.x + 1;
                        int CheckY = A.y;
                        if (CheckX >= 0 && CheckX < MapsizeX && CheckY >= 0 && CheckY < MapsizeY)
                        {
                            Neighbours2.Add(Set[CheckX, CheckY]);
                        }
                        CheckX = A.x - 1;
                        if (CheckX >= 0 && CheckX < MapsizeX && CheckY >= 0 && CheckY < MapsizeY)
                        {
                            Neighbours2.Add(Set[CheckX, CheckY]);
                        }
                        CheckX = A.x;
                        CheckY = A.y - 1;
                        if (CheckX >= 0 && CheckX < MapsizeX && CheckY >= 0 && CheckY < MapsizeY)
                        {
                            Neighbours2.Add(Set[CheckX, CheckY]);
                        }
                        CheckX = A.x;
                        CheckY = A.y + 1;
                        if (CheckX >= 0 && CheckX < MapsizeX && CheckY >= 0 && CheckY < MapsizeY)
                        {
                            Neighbours2.Add(Set[CheckX, CheckY]);
                        }
                        return Neighbours2;
                    }
                    break;
                default:
                    return null;
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

        void SetRoadType(List<Point> Points)
        {
            //Create Road list
            List<Road> _roads = new List<Road>();
            for (int j = 0; j < Points.Count; j++)
            {
                _roads.Add(Roads[Points[j].x, Points[j].y]);                
            }
            //From a to b sorted
            //Setting first element
            for (int i = 0; i < _roads.Count; i++)
            {
                Direction Prev = GetRoadLocation(_roads[i], Points);
                if (Prev == Direction.NULL)
                {
                    Prev = Direction.Top;
                }
                Direction Next;
                if (i != _roads.Count - 1)
                {
                    Next = GetRoadLocation(_roads[i + 1], Points);
                    switch (Next)
                    {
                        case Direction.Right:
                            Next = Direction.Left;
                            break;
                        case Direction.Left:
                            Next = Direction.Right;
                            break;
                        case Direction.Top:
                            Next = Direction.Bottom;
                            break;
                        case Direction.Bottom:
                            Next = Direction.Top;
                            break;
                        default:
                            Next = Direction.NULL;
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
                else
                {
                    Next = Direction.Right;
                }

                if ((Prev == Direction.Bottom && Next == Direction.Top) || (Prev == Direction.Top && Next == Direction.Bottom))
                {
                    _roads[i].roadType = RoadType.verticalStraight;
                }
                else
                {
                    if ((Prev == Direction.Left && Next == Direction.Right) || (Prev == Direction.Right && Next == Direction.Left))
                    {
                        _roads[i].roadType = RoadType.horizontalStraight;
                    }
                    else
                    {
                        if ((Prev == Direction.Left && Next == Direction.Top) || (Prev == Direction.Top && Next == Direction.Left))
                        {
                            _roads[i].roadType = RoadType.topleftCorner;
                        }
                        else
                        {
                            if ((Prev == Direction.Right && Next == Direction.Top) || (Prev == Direction.Top && Next == Direction.Right))
                            {
                                _roads[i].roadType = RoadType.toprightCorner;
                            }
                            else
                            {
                                if ((Prev == Direction.Left && Next == Direction.Bottom) || (Prev == Direction.Bottom && Next == Direction.Left))
                                {
                                    _roads[i].roadType = RoadType.bottomleftCorner;
                                }
                                else
                                {
                                    if ((Prev == Direction.Right && Next == Direction.Bottom) || (Prev == Direction.Bottom && Next == Direction.Right))
                                    {
                                        _roads[i].roadType = RoadType.toprightCorner;
                                    }
                                    else
                                    {
                                        Console.WriteLine("ERROR INVALID ROAD TYPE");
                                        Console.WriteLine(Prev + "-" + Next + "" + _roads[i].X + "-" + _roads[i].Y);
                                    }
                                }
                            }
                        }
                    }
                } 
            }

        }

        Direction GetRoadLocation(Road R, List<Point> SET)
        {
            int CheckX = R.X - 1;
            int CheckY = R.Y;

            if (CheckX >= 0 && CheckX < MapsizeXR && CheckY >= 0 && CheckY < MapsizeYR)
            {
                if (CheckRotation(CheckX,CheckY, SET))
                {
                    return Direction.Right;
                }
            }
            CheckX = R.X + 1;
            if (CheckX >= 0 && CheckX < MapsizeXR && CheckY >= 0 && CheckY < MapsizeYR)
            {
                if (CheckRotation(CheckX, CheckY, SET))
                {
                    return Direction.Left;
                }
            }
            CheckX = R.X;
            CheckY = R.Y + 1;
            if (CheckX >= 0 && CheckX < MapsizeXR && CheckY >= 0 && CheckY < MapsizeYR)
            {
                if (CheckRotation(CheckX, CheckY, SET))
                {
                    return Direction.Top;
                }
            }
            CheckY = R.Y - 1;
            if (CheckX >= 0 && CheckX < MapsizeXR && CheckY >= 0 && CheckY < MapsizeYR)
            {
                if (CheckRotation(CheckX, CheckY, SET))
                {
                    return Direction.Bottom;
                }
            }
            return Direction.NULL;
        }


        bool CheckRotation(int x, int y, List<Point> ToCheck)
        {
            for (int i = 0; i < ToCheck.Count; i++)
            {
                if (ToCheck[i].x == x && ToCheck[i].y == y)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
