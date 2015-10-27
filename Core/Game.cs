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
            //Point[] _points = { new Point(0,0), new Point(13,0), new Point(13,8), new Point(0,8) };
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
                }
                else
                {
                    SetSpecialRoadType(SpecialRoadList[i], SpecialRoadList[i + 1]);
                }
            }
            
            Background = new Bitmap(MapsizeX, MapsizeY);
            for (int x = 0; x < MapsizeXR; x++)
            {
                for (int y = 0; y < MapsizeYR; y++)
                {
                    Bitmap T;
                    switch (Roads[x,y].roadType)
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
                            if(T != null)
                            Background.SetPixel(x * 72 + x2, y * 72 + y2, T.GetPixel(x2,y2));
                        }
                    }
                }
            }
            Base.drawInfos.Add(new DrawInfo(Background, MapsizeX / 2, MapsizeY / 2, MapsizeX, MapsizeY, 270, 0));
        }
    }
}