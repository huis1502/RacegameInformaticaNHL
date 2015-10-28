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
        public void GetKey(char input)
        {
            if (vehicle != null)
            {
                switch (input)
                {
                    case 'a':
                        vehicle.turning = "left";
                        break;
                    case 's':
                        vehicle.brake = true;
                        break;
                    case 'd':
                        vehicle.turning = "right";
                        break;
                    case 'w':
                        vehicle.throttle = true;
                        break;
                    case 'q':
                        vehicle.turning = "left";
                        break;
                    case 'e':
                        vehicle.turning = "right";
                        break;
                    case '2':
                        vehicle.Shoot();
                        break;

                    //Player 2
                    case 'j':
                        vehicle.turning = "left";
                        break;
                    case 'k':
                        vehicle.brake = true;
                        break;
                    case 'l':
                        vehicle.turning = "right";
                        break;
                    case 'i':
                        vehicle.throttle = true;
                        break;
                    case 'u':
                        vehicle.turning = "left";
                        break;
                    case 'o':
                        vehicle.turning = "right";
                        break;
                    case '8':
                        vehicle.Shoot();
                        break;

                }
            }
        }

        public void CreateVehicle(VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.Tank:
                    vehicle = new Tank(500,500);
                    break;
                case VehicleType.Jackass:
                    vehicle = new Jackass(500, 500);
                    break;
                case VehicleType.LAPV:
                    vehicle = new LAPV(500, 500);
                    break;
                case VehicleType.HorsePower:
                    vehicle = new HorsePower(500, 500);
                    break;
                case VehicleType.Motorfiets:
                    vehicle = new Motorfiets(500, 500);
                    break;
            }
        }



    }
}
