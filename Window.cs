using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
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
            GameTimer.Interval = 1;
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
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Window_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Window_KeyUp_1);
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



      

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                Console.WriteLine("iets");
            }
            switch (e.KeyCode)
            {
                case Keys.W:
                    Base.currentGame.player1.vehicle.throttle = "go";
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
                case Keys.D2:
                    Base.currentGame.player1.vehicle.shooting = true;
                    break;
                case Keys.I:
                    Base.currentGame.player2.vehicle.throttle = "go";
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
                case Keys.D8:
                    Base.currentGame.player2.vehicle.shooting = true;
                    break;
            }
        }

        private void Window_KeyUp_1(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                //case Keys.W:
                //    Base.currentGame.player1.vehicle.throttle = "";
                //    break;
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
                case Keys.D2:
                    Base.currentGame.player1.vehicle.shooting = false;
                    break;
                //case Keys.I:
                //    Base.currentGame.player2.vehicle.throttle = "";
                //    break;
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
                case Keys.D8:
                    Base.currentGame.player2.vehicle.shooting = false;
                    break;
            }
        }
    }

        //xx anoniem
    
}
