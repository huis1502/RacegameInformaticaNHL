using RaceGame.Enums;
using System;

namespace RaceGame
{
    public class Player
    {

        public int playerID;
        public PlayerType playerType;
        public Vehicle vehicle;
        public VehicleType vehicleType;
        public int LapCounter = 0;


        public Player(int i, PlayerType _playerType = PlayerType.Human, VehicleType _vehicleType = VehicleType.HorsePower)
        {
            playerID = i;
            playerType = _playerType;
            vehicleType = _vehicleType;
        }

        public void CreateVehicle(VehicleType vehicleType, Point Position)
        {
            switch (vehicleType)
            {
                case VehicleType.Tank:
                    vehicle = new Tank(Position.x, Position.y, this);
                    break;
                case VehicleType.Jackass:
                    vehicle = new Jackass(Position.x, Position.y, this);
                    break;
                case VehicleType.LAPV:
                    vehicle = new LAPV(Position.x, Position.y, this);
                    break;
                case VehicleType.HorsePower:
                    vehicle = new HorsePower(Position.x, Position.y, this);
                    break;
                case VehicleType.Motorfiets:
                    vehicle = new Motorfiets(Position.x, Position.y, this);
                    break;
            }
        }



    }
}
