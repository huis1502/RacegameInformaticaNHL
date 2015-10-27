using System.Drawing;
using System.IO;

namespace RaceGame
{
    public static class Bitmaps
    {
        public static class Vehicles
        {
            public static Bitmap TankBody = new Bitmap("tankbody.png");
            public static Bitmap TankWeapon = new Bitmap("loop.png");

            public static Bitmap Jackass = new Bitmap("jackass.png");

            public static Bitmap LAPVBody = new Bitmap("lapv.png");
            public static Bitmap LAPVWeapon = new Bitmap("lapvweapon.png");

            public static Bitmap HorsePowerBody = new Bitmap("horsepower.png");

            public static Bitmap MotorfietsBody = new Bitmap("motorfiets.png");

        }

        public static class Roads
        {
            
            public static Bitmap HorizontalStraight = new Bitmap("hortrack.png");
            public static Bitmap VerticalStraight = new Bitmap("vertrack.png");

            public static Bitmap HorizontalPitstop = new Bitmap("PitHori.png");
            public static Bitmap VerticalPitstop = new Bitmap("PitVert.png");

            public static Bitmap LeftTop = new Bitmap("ulcorner.png");
            public static Bitmap LeftBottom = new Bitmap("dlcorner.png");
            
            public static Bitmap RightTop = new Bitmap("urcorner.png");
            public static Bitmap RightBottom = new Bitmap("drcorner.png");

        }

        public static class Other
        {
            public static Bitmap Wrench = new Bitmap("Wrench.png");
            public static Bitmap GreenArrowUp = new Bitmap("GreenArrowUp.png");
            public static Bitmap BlueArrowUp = new Bitmap("BlueArrowUp.png");
        }






    }
}
