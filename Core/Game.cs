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
        int MapsizeX = 1008;
        int MapsizeY = 648;
        //14x9 roads
        int MapsizeXR = 14;
        int MapsizeYR = 9;
        public Road[,] Roads;
        List<SpecRoad> SpecialRoadList = new List<SpecRoad>();

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
            for (int x = 0; x < MapsizeX; ++x)
            {
                for (int y = 0; y < MapsizeY; ++y)
                {
                    Background.SetPixel(x,y,Color.Green);
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
            Base.drawInfos.Add(new DrawInfo(Background, MapsizeX / 2, MapsizeY / 2, MapsizeX, MapsizeY, 270, 0));
        }
    }
}