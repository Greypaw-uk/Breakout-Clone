using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Breakout
{
    public partial class Form1 : Form
    {
        private bool goRight;
        private bool goLeft;
        private int speed = 10;

        private int ballx = 5;
        private int bally = 5;

        private int score = 0;

        private Random rnd = new Random(Guid.NewGuid().GetHashCode());

        public Form1()
        {
            InitializeComponent();

            SoundPlayer bgm = new SoundPlayer(Properties.Resources.bgm);
            bgm.Play();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "block")
                {
                    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    x.BackColor = randomColor;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.Left += ballx;
            ball.Top += bally;

            label1.Text = "Score: " + score;

            if (goLeft) 
            {
                player.Left -= speed;
            }

            if (goRight)
            {
                player.Left += speed;
            }

            if (player.Left < 1)
            {
                goLeft = false;
            }

            else if (player.Right > this.Width)
            {
                goRight = false;
            }

            if (ball.Left + ball.Width > ClientSize.Width || ball.Left < 0)
            {
                ballx = -ballx;
            }

            if (ball.Top < 0 || ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = -bally;
            }

            if (ball.Top + ball.Height > ClientSize.Height)
            {
                gameOver();
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "block")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        bally = -bally;
                        score++;
                        speed++;
                    }
                }
            }

            if (score > 29)
            {
                MessageBox.Show("You win!");
                gameOver();
            }
        }

        private void gameOver()
        {
            timer1.Stop();
            MessageBox.Show("You lost!  Press okay to restart!");
            Application.Restart();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && player.Left > 0)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.D && player.Right < this.Width)
            {
                goRight = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.D)
            {
                goRight = false;
            }
        }
    }
}
