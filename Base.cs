using System.Windows.Forms;
using RaceGame.Structs;
using RaceGame.Delegates;
using System.Collections.Generic;
using System;

namespace RaceGame
{
    public static class Base
    {
        public static Window windowHandle;
        public static Game currentGame;
        public static List<GameTask> gameTasks;
        public static List<DrawInfo> drawInfos;

        [STAThread]
        public static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            windowHandle = new Window();
            Application.Run(windowHandle);
        }
    }
}
