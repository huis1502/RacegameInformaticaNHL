using System.Drawing;

namespace RaceGame.Delegates
{
    public delegate void GameTask();

}

namespace RaceGame.Enums
{
    
}

namespace RaceGame.Structs
{
    public struct DrawInfo
    {
        public Bitmap bitmapdata;
        public int x;
        public int y;
        public int width;
        public int height;
        public bool AutoRemove;
        public int Frames;

        public DrawInfo(Bitmap bitmap, int x, int y, int width, int height, bool AutoRemove = false, int Frames = 0)
        {
            bitmapdata = bitmap;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.AutoRemove = AutoRemove;
            this.Frames = Frames;
        }
        public void LowerFrameCount()
        {
            Frames = Frames - 1;
        }
    }
}