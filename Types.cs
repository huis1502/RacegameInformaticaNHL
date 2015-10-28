using System.Drawing;

namespace RaceGame
{
    public class DrawInfo
    {
        public Bitmap bitmapdata;
        public float x;
        public float y;
        public int width;
        public int height;
        public bool AutoRemove;
        public int Frames;
        public float angle;
        public PointF rotatePoint;

        public DrawInfo(Bitmap bitmap, int x, int y, int width, int height, float _angle = 0, float RotateX = 0f, float RotateY = 0f, bool AutoRemove = false, int Frames = 0)
        {
            bitmapdata = bitmap;
            this.x = (int)x;
            this.y = (int)y;
            this.width = width;
            this.height = height;
            this.AutoRemove = AutoRemove;
            this.Frames = Frames;
            this.angle = _angle;
            rotatePoint = new PointF(RotateX, RotateY);
        }
        public void LowerFrameCount()
        {
            Frames -= 1;
        }
    }
}
