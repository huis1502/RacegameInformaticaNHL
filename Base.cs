﻿using System;
using System.Windows.Forms;

namespace RaceGame
{
    public static class Base
    {
        public static Window windowHandle;

        public static void Main(string[] Args)
        {
            windowHandle = new Window();
            Application.Run(windowHandle);
        }
    }
}
