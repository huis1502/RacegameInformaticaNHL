

namespace RaceGame
{
    public class Point
    {
        public int x;
        public int y;

        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static implicit operator System.Drawing.Point(Point p)
        {
            return new System.Drawing.Point(p.x, p.y);
        }

    }
}
