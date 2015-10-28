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

            public static Bitmap OneGreenTwoGreenUp = new Bitmap("OneGreenTwoGreenUp.png");
            public static Bitmap OneGreenTwoGreenRight = new Bitmap("OneGreenTwoGreenRight.png");
            public static Bitmap OneGreenTwoGreenDown = new Bitmap("OneGreenTwoGreenDown.png");
            public static Bitmap OneGreenTwoGreenLeft = new Bitmap("OneGreenTwoGreenLeft.png");

            public static Bitmap OneGreenTwoBlueUp = new Bitmap("OneGreenTwoBlueUp.png");
            public static Bitmap OneGreenTwoBlueRight = new Bitmap("OneGreenTwoBlueRight.png");
            public static Bitmap OneGreenTwoBlueDown = new Bitmap("OneGreenTwoBlueDown.png");
            public static Bitmap OneGreenTwoBlueLeft = new Bitmap("OneGreenTwoBlueLeft.png");

            public static Bitmap OneBlueTwoBlueUp = new Bitmap("OneBlueTwoBlueUp.png");
            public static Bitmap OneBlueTwoBlueRight = new Bitmap("OneBlueTwoBlueRight.png");
            public static Bitmap OneBlueTwoBlueDown = new Bitmap("OneBlueTwoBlueDown.png");
            public static Bitmap OneBlueTwoBlueLeft = new Bitmap("OneBlueTwoBlueLeft.png");

            public static Bitmap OneBlueTwoGreenUp = new Bitmap("OneBlueTwoGreenUp.png");
            public static Bitmap OneBlueTwoGreenRight = new Bitmap("OneBlueTwoGreenRight.png");
            public static Bitmap OneBlueTwoGreenDown = new Bitmap("OneBlueTwoGreenDown.png");
            public static Bitmap OneBlueTwoGreenLeft = new Bitmap("OneBlueTwoGreenLeft.png");

            public static Bitmap Finish = new Bitmap("finish.png");

            public static Bitmap Redpointer = new Bitmap("Redpointer.png");
            public static Bitmap Bluepointer = new Bitmap("Bluepointer.png");
        }

        public static class Obstacles
        {
            public static Bitmap Tree = new Bitmap("TreeTex.png");
            public static Bitmap Stone = new Bitmap("stone.png");
        }




    }
}
