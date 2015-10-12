using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using RaceGame.Structs;
using RaceGame.Delegates;

namespace RaceGame
{
    public partial class Window : Form
    {
        public Timer GameTimer;
        public Bitmap BackBuffer;
        public List<GameTask> GameTasks;
        public List<DrawInfo> DrawInfos;
        public Game currentGame;

        public Window()
        {
            InitializeComponent();
            GameTasks = new List<GameTask>();
            DrawInfos = new List<DrawInfo>();

            CreateGame();

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            GameTimer = new Timer();
            GameTimer.Interval = 16;
            GameTimer.Tick += new EventHandler(GameTimer_Tick);
            GameTimer.Start();
            Load += new EventHandler(CreateBackBuffer);
            Paint += new PaintEventHandler(PaintBackbuffer);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Window
            // 
            this.ClientSize = new System.Drawing.Size(1008, 747);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 786);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 786);
            this.Name = "Window";
            this.ShowIcon = false;
            this.Text = "Race Game";
            this.ResumeLayout(false);

        }
        void GameTimer_Tick(object sender, EventArgs e)
        {
            //Alle game tasks invoken
            for (int i = 0; i < GameTasks.Count; i++)
            {
                GameTasks[i].Invoke();
            }
            Draw();
        }
        void Draw()
        {
            if (BackBuffer != null)
            {
                using (var buffer = Graphics.FromImage(BackBuffer))
                {
                    buffer.Clear(Color.LightGray);
                    for (int i = 0; i < DrawInfos.Count;i++)
                    {
                        DrawInfo DR = DrawInfos[i];
                        buffer.DrawImage(DR.bitmapdata, DR.x, DR.y, DR.width, DR.height);
                        if (DR.AutoRemove)
                        {
                            if (DR.Frames != 0)
                            {
                                DrawInfos[i] = new DrawInfo(DR.bitmapdata, DR.x, DR.y,DR.width,DR.height,DR.AutoRemove,DR.Frames - 1);
                            }
                            else
                            {
                                DrawInfos.RemoveAt(i);
                            }
                        }
                    }                  
                }
                Invalidate();
            }
        }
        void CreateBackBuffer(object sender, EventArgs e)
        {
            if (BackBuffer != null)
            {
                BackBuffer.Dispose();
            }
            BackBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        }
        void PaintBackbuffer(object sender, PaintEventArgs e)
        {
            if (BackBuffer != null)
            {
                e.Graphics.DrawImageUnscaled(BackBuffer, new Point(0,0));
            }
        }

        void CreateGame()
        {
            Base.currentGame = null;
            currentGame = null;
            Base.currentGame = new Game();
            currentGame = Base.currentGame;
        }
    }
}
