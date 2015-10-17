using RaceGame.Enums;

namespace RaceGame
{
    public class Point
    {
        PointType pointType;
        public int x;
        public int y;

        public Point(PointType p, int _x, int _y)
        {
            pointType = p;
            x = _x;
            y = _y;
        }

        public static implicit operator System.Drawing.Point(Point p)
        {
            return new System.Drawing.Point(p.x,p.y);
        }

    }
}
