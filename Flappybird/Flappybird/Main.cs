using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Flappybird
{
    public partial class Main : Form
    {

        int pipeSpeed = 5; 
        int gravity = 15; 
        int score = 0;
        int time = 0;
        int extratime = 0;
        public Main()
        {
            InitializeComponent();
            GameTime.Enabled = false;
        }

        private void GameTime_Tick(object sender, EventArgs e)
        {
            Flappybird.Top += gravity;
            PipeBottom.Left -= pipeSpeed;
            PipeTop.Left -= pipeSpeed;
            ScoreText.Text = "Score : " + score;

            if (PipeBottom.Left < -40)
            {
                PipeBottom.Left = 500;
                score++;
            }

            if (PipeTop.Left < -40)
            {
                PipeTop.Left = 400;
                score++;
            }

            if(Flappybird.Bounds.IntersectsWith(PipeTop.Bounds) || 
               Flappybird.Bounds.IntersectsWith(PipeBottom.Bounds) ||
               Flappybird.Bounds.IntersectsWith(Ground.Bounds))
            {
                SoundPlayer Sound = new SoundPlayer(Properties.Resources.GameOver);
                Sound.Play();
                GameOver();
            }
            time++;
            if(time == 500 + extratime)
            {
                pipeSpeed += 1;
                extratime += 100;
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            GameTime.Enabled = true;
            playText.Hide();
            
            if (e.KeyCode == Keys.Space)
                gravity = -7;
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            SoundPlayer Sound = new SoundPlayer(Properties.Resources.Fly);
            Sound.Play();
            if (e.KeyCode == Keys.Space)
                gravity = 7;
        }

        private void GameOver()
        {
            GameTime.Stop();
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show("Game Over!","Game Over", buttons);
            if(result == DialogResult.OK)
            {
                this.Controls.Clear();
                pipeSpeed = 5;
                gravity = 15;
                score = 0;
                time = 0;
                extratime = 0;
                this.InitializeComponent();
                GameTime.Enabled = false;
            }
        }
    }
}
