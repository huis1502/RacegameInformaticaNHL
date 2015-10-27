using System.Drawing;

namespace RaceGame.Delegates
{
    public delegate void GameTask();
}

namespace RaceGame.Enums
{
    public enum PlayerType
    {
        Human,
        AI
    }

    public enum VehicleType
    {
        Tank,
        Jackass,
        LAPV,
        HorsePower,
        Motorfiets,
    }

    public enum PointType
    {
        Inner,
        Corner,
    }
}