using System;
using RaceGame.Pathfinding;
using System.Collections.Generic;
using System.Drawing;

namespace RaceGame
{
    public partial class Game
    {

        protected List<Point> PitstopLocations = new List<Point>();
        protected PitStopType pitstopType;

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
            for (int i = 1; i < _roads.Count-1; i++)
            {
                SetSingleRoadType(_roads[i-1], _roads[i], _roads[i+1]);
            }
            SpecialRoadList.Add(new SpecRoad { Current =  _roads[0], Next = _roads[1]});
            SpecialRoadList.Add(new SpecRoad { Current = _roads[_roads.Count-1], Prev = _roads[_roads.Count-2]});
        }

        void SetSingleRoadType(Road Prev, Road Current, Road Next)
        {
            int X = Current.X;
            int Y = Current.Y;

            Direction PrevDir = Direction.NULL;
            Direction NextDir = Direction.NULL;

            #region Prev
            //Top
            if (Prev.X == X && Prev.Y < Y)
            {
                PrevDir = Direction.Top;
            }
            else
            {
                //Bottom
                if (Prev.X == X && Prev.Y > Y)
                {
                    PrevDir = Direction.Bottom;
                }
                else
                {
                    if (Prev.Y == Y && Prev.X < X)
                    {
                        PrevDir = Direction.Left;
                    }
                    else
                    {
                        if (Prev.Y == Y && Prev.X > X)
                        {
                            PrevDir = Direction.Right;
                        }
                        else
                        {
                            Console.WriteLine("INVALID DIRECTION IN SSRT()");
                        }
                    }
                }
            }
            #endregion
            #region NextDir
            //Top
            if (Next.X == X && Next.Y < Y)
            {
                NextDir = Direction.Top;
            }
            else
            {
                //Bottom
                if (Next.X == X && Next.Y > Y)
                {
                    NextDir = Direction.Bottom;
                }
                else
                {
                    if (Next.Y == Y && Next.X < X)
                    {
                        NextDir = Direction.Left;
                    }
                    else
                    {
                        if (Next.Y == Y && Next.X > X)
                        {
                            NextDir = Direction.Right;
                        }
                        else
                        {
                            Console.WriteLine("INVALID DIRECTION IN SSRT()");
                        }
                    }
                }
            }
            #endregion

            if ((PrevDir == Direction.Top && NextDir == Direction.Bottom) || (PrevDir == Direction.Bottom && NextDir == Direction.Top))
            {
                Current.roadType = RoadType.verticalStraight;
            }
            else
            {
                if ((PrevDir == Direction.Left && NextDir == Direction.Right) || (PrevDir == Direction.Right && NextDir == Direction.Left))
                {
                    Current.roadType = RoadType.horizontalStraight;
                }
                else
                {
                    if ((PrevDir == Direction.Top && NextDir == Direction.Left) || (PrevDir == Direction.Left && NextDir == Direction.Top))
                    {
                        Current.roadType = RoadType.topleftCorner;
                    }
                    else
                    {
                        if ((PrevDir == Direction.Top && NextDir == Direction.Right) || (PrevDir == Direction.Right && NextDir == Direction.Top))
                        {
                            Current.roadType = RoadType.toprightCorner;
                        }
                        else
                        {
                            if ((PrevDir == Direction.Bottom && NextDir == Direction.Left) || (PrevDir == Direction.Left && NextDir == Direction.Bottom))
                            {
                                Current.roadType = RoadType.bottomleftCorner;
                            }
                            else
                            {
                                if ((PrevDir == Direction.Bottom && NextDir == Direction.Right) || (PrevDir == Direction.Right && NextDir == Direction.Bottom))
                                {
                                    Current.roadType = RoadType.bottomrightCorner;
                                }
                                else
                                {
                                    Console.WriteLine("INVALID ROAD TYPE");
                                }
                            }
                        }
                    }
                }
            }
        }

        void SetSpecialRoadType(SpecRoad PrevR, SpecRoad NextR)
        {

            PrevR.Next = NextR.Current;
            NextR.Prev = PrevR.Current;

            SpecRoad[] _specs = new SpecRoad[2] { PrevR, NextR };

            for (int i = 0; i < 2; i++)
            {
                Road Prev = _specs[i].Prev;
                Road Next = _specs[i].Next;
                Road Current = _specs[i].Current;
                int X = Current.X;
                int Y = Current.Y;
                Direction PrevDir = Direction.NULL;
                Direction NextDir = Direction.NULL;

                #region Prev
                //Top
                if (Prev.X == X && Prev.Y < Y)
                {
                    PrevDir = Direction.Top;
                }
                else
                {
                    //Bottom
                    if (Prev.X == X && Prev.Y > Y)
                    {
                        PrevDir = Direction.Bottom;
                    }
                    else
                    {
                        if (Prev.Y == Y && Prev.X < X)
                        {
                            PrevDir = Direction.Left;
                        }
                        else
                        {
                            if (Prev.Y == Y && Prev.X > X)
                            {
                                PrevDir = Direction.Right;
                            }
                            else
                            {
                                Console.WriteLine("INVALID DIRECTION IN SSRT()");
                            }
                        }
                    }
                }
                #endregion
                #region NextDir
                //Top
                if (Next.X == X && Next.Y < Y)
                {
                    NextDir = Direction.Top;
                }
                else
                {
                    //Bottom
                    if (Next.X == X && Next.Y > Y)
                    {
                        NextDir = Direction.Bottom;
                    }
                    else
                    {
                        if (Next.Y == Y && Next.X < X)
                        {
                            NextDir = Direction.Left;
                        }
                        else
                        {
                            if (Next.Y == Y && Next.X > X)
                            {
                                NextDir = Direction.Right;
                            }
                            else
                            {
                                Console.WriteLine("INVALID DIRECTION IN SSRT()");
                            }
                        }
                    }
                }
                #endregion

                if ((PrevDir == Direction.Top && NextDir == Direction.Bottom) || (PrevDir == Direction.Bottom && NextDir == Direction.Top))
                {
                    Current.roadType = RoadType.verticalStraight;
                }
                else
                {
                    if ((PrevDir == Direction.Left && NextDir == Direction.Right) || (PrevDir == Direction.Right && NextDir == Direction.Left))
                    {
                        Current.roadType = RoadType.horizontalStraight;
                    }
                    else
                    {
                        if ((PrevDir == Direction.Top && NextDir == Direction.Left) || (PrevDir == Direction.Left && NextDir == Direction.Top))
                        {
                            Current.roadType = RoadType.topleftCorner;
                        }
                        else
                        {
                            if ((PrevDir == Direction.Top && NextDir == Direction.Right) || (PrevDir == Direction.Right && NextDir == Direction.Top))
                            {
                                Current.roadType = RoadType.toprightCorner;
                            }
                            else
                            {
                                if ((PrevDir == Direction.Bottom && NextDir == Direction.Left) || (PrevDir == Direction.Left && NextDir == Direction.Bottom))
                                {
                                    Current.roadType = RoadType.bottomleftCorner;
                                }
                                else
                                {
                                    if ((PrevDir == Direction.Bottom && NextDir == Direction.Right) || (PrevDir == Direction.Right && NextDir == Direction.Bottom))
                                    {
                                        Current.roadType = RoadType.bottomrightCorner;
                                    }
                                    else
                                    {
                                        Console.WriteLine("INVALID ROAD TYPE");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        struct SpecRoad
        {
            public Road Prev;
            public Road Current;
            public Road Next;

            public override string ToString()
            {
                string Text = "";
                if (Prev != null)
                    Text = Text + "Prev: " + Prev.X + "-" + Prev.Y + "\n";
                if (Current != null)
                    Text = Text + "Current: " + Current.X + "-" + Current.Y + "\n";
                if (Next != null)
                    Text = Text + "Next: " + Next.X + "-" + Next.Y + "\n";

                return Text;
            }
        }

        protected enum PitStopType
        {
            Vertical,
            Horizontal
        }

        Point[] GeneratePointSet()
        {
            Random RND = new Random(DateTime.Now.GetHashCode() + DateTime.UtcNow.GetHashCode() + DateTime.Now.Millisecond.GetHashCode());
            List<Point> Return = new List<Point>();
            int Direction = RND.Next(0, 2);
            Return.Add( new Point(1, 1));
            Direction = 1;
            if (Direction == 0)
            {
                //Pitstop toevoegen
                PitstopLocations.Add(new Point(0, 3));
                PitstopLocations.Add(new Point(0, 4));
                PitstopLocations.Add(new Point(0, 5));
                PitstopLocations.Add(new Point(1, 3));
                PitstopLocations.Add(new Point(1, 4));
                PitstopLocations.Add(new Point(1, 5));
                pitstopType = PitStopType.Vertical;

                Direction = RND.Next(0,2);

                if (Direction == 0)
                {
                    Return.Add(new Point(1, 3));
                    Return.Add(new Point(5,3));
                    Return.Add(new Point(5,6));
                    Return.Add(new Point(1,6));
                    Return.Add(new Point(1, 8));
                }
                else
                {
                    Return.Add(new Point(1,8));
                }

                Return.Add(new Point(12,8));

                Direction = RND.Next(0, 2);
                if (Direction == 0)
                {
                    Return.Add(new Point(12,6));
                    Return.Add(new Point(9, 6));
                    Return.Add(new Point(9, 3));
                    Return.Add(new Point(12, 3));
                }

                Return.Add(new Point(12,1));
            }
            else
            {
                Return.Add(new Point(12, 1));
                //Pitstop toevoegen
                PitstopLocations.Add(new Point(6, 0));
                PitstopLocations.Add(new Point(7, 0));
                PitstopLocations.Add(new Point(8, 0));
                PitstopLocations.Add(new Point(6, 1));
                PitstopLocations.Add(new Point(7, 1));
                PitstopLocations.Add(new Point(8, 1));
                pitstopType = PitStopType.Horizontal;

                Direction = RND.Next(0, 2);

                if (Direction == 0)
                {
                    Return.Add(new Point(12, 3));
                    Return.Add(new Point(7, 3));
                    Return.Add(new Point(7, 6));
                    Return.Add(new Point(12, 6));
                    Return.Add(new Point(12, 8));
                }
                else
                {
                    Return.Add(new Point(12, 8));
                }
                /*
                Direction = RND.Next(0,2);
                Direction = 0;
                if (Direction == 0)
                {
                    Return.Add(new Point(9, 8));
                    Return.Add(new Point(9, 7));
                    Return.Add(new Point(5, 7));
                    Return.Add(new Point(5, 8));
                    Return.Add(new Point(1, 8));
                }
                else
                {
                    Return.Add(new Point(1,8));
                } */
                Return.Add(new Point(1,8));
            }

            return Return.ToArray();
        }

    }
}
