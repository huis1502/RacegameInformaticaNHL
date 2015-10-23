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
            List<Point> P = FindRoadPath(Roads[0,0], Roads[MapsizeXR-1, MapsizeYR-1], RoadType.NULL);
            List<Road> CurrentPath = new List<Road>();
            for (int i = 0; i < P.Count; i++)
            {
                CurrentPath.Add(Roads[P[i].x, P[i].y]);
            }
            SetRoadType(P);
            Background = new Bitmap(MapsizeX, MapsizeY);
            for (int x = 0; x < MapsizeXR; x++)
            {
                for (int y = 0; y < MapsizeYR; y++)
                {
                    Color T = Color.Green;
                    if (Roads[x, y].roadType != RoadType.NULL)
                        T = Color.Black;
                    for (int x2 = 0; x2 < 72; x2++)
                    {
                        for (int y2 = 0; y2 < 72; y2++)
                        {
                            Background.SetPixel(x * 72 + x2, y * 72 + y2, T);
                        }
                    }
                }
            }
            Base.drawInfos.Add(new DrawInfo(Background, MapsizeX / 2, MapsizeY / 2, MapsizeX, MapsizeY, 270, 0));
        }
    }
}