using System;
using System.Windows.Forms;

namespace RaceGame
{
    public partial class Window : Form
    {
        public Window()
        {
            
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Window
            // 
            this.ClientSize = new System.Drawing.Size(1008, 747);
            this.MaximumSize = new System.Drawing.Size(1024, 786);
            this.MinimumSize = new System.Drawing.Size(1024, 786);
            this.Name = "Window";
            this.Load += new System.EventHandler(this.Window_Load);
            this.ResumeLayout(false);

        }

        private void Window_Load(object sender, EventArgs e)
        {

        }
    }
}
