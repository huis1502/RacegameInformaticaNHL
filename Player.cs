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

        public void CreateVehicle(VehicleType vehicleType, int offset)
        {
            switch (vehicleType)
            {
                case VehicleType.Tank:
                    vehicle = new Tank(500, 500 + offset, this);
                    break;
                case VehicleType.Jackass:
                    vehicle = new Jackass(500, 500 + offset, this);
                    break;
                case VehicleType.LAPV:
                    vehicle = new LAPV(500, 500 + offset, this);
                    break;
                case VehicleType.HorsePower:
                    vehicle = new HorsePower(500, 500 + offset, this);
                    break;
                case VehicleType.Motorfiets:
                    vehicle = new Motorfiets(500, 500 + offset, this);
                    break;
            }
        }



    }
}
