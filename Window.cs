using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using RaceGame.Structs;
using RaceGame.Delegates;
using System.Drawing.Drawing2D;

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
            Base.drawInfos = DrawInfos;
            Base.gameTasks = GameTasks;
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
            this.KeyPreview = true;
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
                    buffer.Clear(Color.White);
                    for (int i = 0; i < DrawInfos.Count;i++)
                    {
                        DrawInfo DR = DrawInfos[i];

                        PointF rotatePoint = new PointF(DR.x, DR.y);
                        Matrix myMatrix = new Matrix();
                        myMatrix.RotateAt(DR.angle + 90, rotatePoint, MatrixOrder.Append);
                        buffer.Transform = myMatrix;
                        buffer.DrawImage(DR.bitmapdata, DR.x - DR.width/2, DR.y - DR.height/2, DR.width, DR.height);

                        if (DR.AutoRemove)
                        {
                            if (DR.Frames != 0)
                            {
                                DR.LowerFrameCount();
                            }
                            else
                            {
                                DrawInfos.RemoveAt(i);
                            }
                        }
                    }
                    //buffer.DrawLines(new Pen(Color.White,10f), Base.currentGame.Points);
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
                e.Graphics.DrawImageUnscaled(BackBuffer, new System.Drawing.Point(0,0));
            }
        }

        void CreateGame()
        {
            Base.currentGame = null;
            currentGame = null;
            Base.currentGame = new Game();
            currentGame = Base.currentGame;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Base.currentGame.player1.vehicle.throttle = true;
                    break;
                case Keys.A:
                    Base.currentGame.player1.vehicle.turning = "left";
                    break;
                case Keys.S:
                    Base.currentGame.player1.vehicle.brake = true;
                    break;
                case Keys.D:
                    Base.currentGame.player1.vehicle.turning = "right";
                    break;
                case Keys.Q:
                    Base.currentGame.player1.vehicle.weapon.turning = "left";
                    break;
                case Keys.E:
                    Base.currentGame.player1.vehicle.weapon.turning = "right";
                    break;
                case Keys.I:
                    Base.currentGame.player2.vehicle.throttle = true;
                    break;
                case Keys.J:
                    Base.currentGame.player2.vehicle.turning = "left";
                    break;
                case Keys.K:
                    Base.currentGame.player2.vehicle.brake = true;
                    break;
                case Keys.L:
                    Base.currentGame.player2.vehicle.turning = "right";
                    break;
                case Keys.U:
                    Base.currentGame.player2.vehicle.weapon.turning = "left";
                    break;
                case Keys.O:
                    Base.currentGame.player2.vehicle.weapon.turning = "right";
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Base.currentGame.player1.vehicle.throttle = false;
                    break;
                case Keys.A:
                    Base.currentGame.player1.vehicle.turning = null;
                    break;
                case Keys.S:
                    Base.currentGame.player1.vehicle.brake = false;
                    break;
                case Keys.D:
                    Base.currentGame.player1.vehicle.turning = null;
                    break;
                case Keys.Q:
                    Base.currentGame.player1.vehicle.weapon.turning = "false";
                    break;
                case Keys.E:
                    Base.currentGame.player1.vehicle.weapon.turning = "false";
                    break;
                case Keys.I:
                    Base.currentGame.player2.vehicle.throttle = false;
                    break;
                case Keys.J:
                    Base.currentGame.player2.vehicle.turning = null;
                    break;
                case Keys.K:
                    Base.currentGame.player2.vehicle.brake = false;
                    break;
                case Keys.L:
                    Base.currentGame.player2.vehicle.turning = null;
                    break;
                case Keys.U:
                    Base.currentGame.player2.vehicle.weapon.turning = "false";
                    break;
                case Keys.O:
                    Base.currentGame.player2.vehicle.weapon.turning = "false";
                    break;
            }
        }
        

    }
}
