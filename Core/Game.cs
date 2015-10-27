using RaceGame.Pathfinding;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace RaceGame
{
    public partial class Game
    {

        public Player player1;
        public Player player2;
        public byte[,] GameField;
        int MapsizeX = 1008; // Uit de Window.cs designer gehaald
        int MapsizeY = 648;
        //14x9 roads
        int MapsizeXR = 14;
        int MapsizeYR = 9;
        public Road[,] Roads;
        public Point[] CheckPoints;
        List<SpecRoad> SpecialRoadList = new List<SpecRoad>();
        public Point PitStopPoint;

        int FillPercentage = 50;
        int Iterations = 3;

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
            Roads = new Road[MapsizeXR, MapsizeYR];
            for (int x = 0; x < MapsizeXR; x++)
            {
                for (int y = 0; y < MapsizeYR; y++)
                {
                    Roads[x, y] = new Road(x,y, RoadType.NULL);
                }
            }
            Point[] _points = GeneratePointSet();
            CheckPoints = _points;

            for (int i = 0; i < _points.Length; i++)
            {
                int SX = 0;
                int SY = 0;
                if (i != _points.Length - 1)
                {
                    SX = _points[i + 1].x;
                    SY = _points[i + 1].y;
                }
                else
                {
                    SX = _points[0].x;
                    SY = _points[0].y;
                }
                List<Point> P = FindRoadPath(Roads[_points[i].x, _points[i].y], Roads[SX, SY], RoadType.NULL);
                List<Road> CurrentPath = new List<Road>();
                for (int j = 0; j < P.Count; j++)
                {
                    CurrentPath.Add(Roads[P[j].x, P[j].y]);
                }
                SetRoadType(P);
            }

            for (int i = 1; i < SpecialRoadList.Count; i += 2)
            {
                if (i == SpecialRoadList.Count - 1)
                {
                    SetSpecialRoadType(SpecialRoadList[i], SpecialRoadList[0]);
                    //ER MOET MINIMAAL 1 BLOK TUSSEN DE ROADS ZITTEN!!!
                }
                else
                {
                    SetSpecialRoadType(SpecialRoadList[i], SpecialRoadList[i + 1]);
                }
            }

            //Pitstopmagic
            for (int i = 0; i < PitstopLocations.Count; ++i)
            {
                Roads[PitstopLocations[i].x, PitstopLocations[i].y].roadType = RoadType.PitstopSpecial;
            }

            Background = new Bitmap(MapsizeX, MapsizeY);
            CellularAutomata();

            for (int x = 0; x < MapsizeX; ++x)
            {
                for (int y = 0; y < MapsizeY; ++y)
                {
                    Background.SetPixel(x,y, GameField[x,y] == 1 ? Color.Green : Color.FromArgb(255,93,171,67));
                }
            }
            for (int x = 0; x < MapsizeXR; x++)
            {
                for (int y = 0; y < MapsizeYR; y++)
                {
                    Bitmap T;
                    if (Roads[x, y].roadType != RoadType.PitstopSpecial)
                    {
                        switch (Roads[x, y].roadType)
                        {
                            case RoadType.horizontalStraight:
                                T = Bitmaps.Roads.HorizontalStraight;
                                break;
                            case RoadType.verticalStraight:
                                T = Bitmaps.Roads.VerticalStraight;
                                break;
                            case RoadType.bottomleftCorner:
                                T = Bitmaps.Roads.LeftBottom;
                                break;
                            case RoadType.bottomrightCorner:
                                T = Bitmaps.Roads.RightBottom;
                                break;
                            case RoadType.topleftCorner:
                                T = Bitmaps.Roads.LeftTop;
                                break;
                            case RoadType.toprightCorner:
                                T = Bitmaps.Roads.RightTop;
                                break;
                            default:
                                T = null;
                                break;
                        }
                        for (int x2 = 0; x2 < 72; x2++)
                        {
                            for (int y2 = 0; y2 < 72; y2++)
                            {
                                if (T != null && T.GetPixel(x2,y2).A != 0)
                                {
                                    Background.SetPixel(x * 72 + x2, y * 72 + y2, T.GetPixel(x2, y2));
                                }
                            }
                        }
                    }
                    else
                    {
                        //Pitstop code
                        if (pitstopType == PitStopType.Horizontal)
                        {
                            T = Bitmaps.Roads.HorizontalPitstop;
                            if (Roads[x,y].X == 6 && Roads[x,y].Y == 0)
                            {
                                for (int x2 = 0; x2 < 216; ++x2)
                                {
                                    for (int y2 = 0; y2 < 144; ++y2)
                                    {
                                        if (T.GetPixel(x2, y2).A != 0)
                                        Background.SetPixel(x * 72 + x2, y * 72 + y2, T.GetPixel(x2, y2));
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (pitstopType == PitStopType.Vertical)
                            {
                                T = Bitmaps.Roads.VerticalPitstop;
                                if (Roads[x, y].X == 0 && Roads[x, y].Y == 3)
                                {
                                    for (int x2 = 0; x2 < 144; ++x2)
                                    {
                                        for (int y2 = 0; y2 < 216; ++y2)
                                        {
                                            if (T.GetPixel(x2, y2).A != 0)
                                                Background.SetPixel(x * 72 + x2, y * 72 + y2, T.GetPixel(x2, y2));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("INVALID PITSTOPTYPE GAME.CS");
                            }
                        }
                    }
                }
            }
            for (int x = 0; x < 16; ++x)
            {
                for (int y = 0; y < 16; ++y)
                {
                    if(Bitmaps.Other.Wrench.GetPixel(x,y).A != 0)
                    Background.SetPixel(PitStopPoint.x - 8 + x, PitStopPoint.y - 8 + y, Bitmaps.Other.Wrench.GetPixel(x,y));
                }
            }
            for (int i = 0; i < CheckPoints.Length; ++i)
            {
                for (int x = 0; x < 16; ++x)
                {
                    for (int y = 0; y < 16; ++y)
                    {
                        if (Bitmaps.Other.GreenArrowUp.GetPixel(x, y).A != 0)
                            Background.SetPixel(CheckPoints[i].x * 72 - 8 + x + 36, CheckPoints[i].y * 72 - 8 + y + 36, Bitmaps.Other.BlueArrowUp.GetPixel(x, y));
                    }
                }
            }




            Base.drawInfos.Add(new DrawInfo(Background, MapsizeX / 2, MapsizeY / 2, MapsizeX, MapsizeY, 270, 0));
        }

        void CellularAutomata()
        {
            Random RND = new Random();
            for (int x = 0; x < MapsizeX; ++x)
            {
                for (int y = 0; y < MapsizeY; ++y)
                {
                    GameField[x, y] = RND.Next(0,101) < FillPercentage ? (byte)1 : (byte)0;
                }
            }

            for (int i = 0; i < Iterations; i++)
            {
                for (int x = 0; x < MapsizeX; ++x)
                {
                    for (int y = 0; y < MapsizeY; ++y)
                    {
                        int Count = GetNeighbours(x,y);
                        if (Count > 4)
                        {
                            GameField[x, y] = 1;
                        }
                        else
                        {
                            if (Count < 4)
                            {
                                GameField[x, y] = 0;
                            }
                            else
                            {
                                GameField[x,y] = (byte)RND.Next(0,2);
                            }
                        }
                    }
                }
            }
        }

        int GetNeighbours(int xCoord, int yCoord)
        {
            int Count = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int CheckX = xCoord + x;
                    int CheckY = yCoord + y;

                    if (CheckX >= 0 && CheckX < MapsizeX && CheckY >= 0 && CheckY < MapsizeY)
                    {
                        Count += GameField[CheckX, CheckY];
                    }
                }
            }
            return Count;
        }
    }
}