using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARCANOID
{
    public partial class Form1 : Form
    {

        bool goRight;
        bool goLeft;
        int speed = 10;
        int lifes = 3;

        bool isGameOver;

        int ballx = 5;
        int bally = 5;

        int score = 0;

        Random rand = new Random();

        PictureBox[,] blocks;

        public Form1()
        {
            InitializeComponent();

            ball.Left = ClientSize.Width/2+ball.Width/2;
            ball.Top = 300;
            player.Left = ClientSize.Width / 2 + player.Width / 2;
            player.Top = 400;
            placeBlocks();
        }

        private void setupGame()
        {
            isGameOver = false;
            score = 0;
            lifes = 3;
            ballx = 5;
            bally = 5;
            speed = 12;
            score1.Text = "Score: " + score;
            lifes1.Text = "Lifes: " + lifes;

            ball.Left = ClientSize.Width / 2 + ball.Width / 2;
            ball.Top = 300;
            player.Left = ClientSize.Width / 2 + player.Width / 2;
            player.Top = 400;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    Color randomColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
                    x.BackColor = randomColor;
                }
            }

            gameTimer.Start();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gameOver()
        {
            isGameOver = true;
            gameTimer.Stop();
        }

        private void placeBlocks()
        {
            blocks = new PictureBox[7,9];

            int a = 0;

            int top = 15;
            int left = 15;

            for (int i = 0; i<5;i++)
            {
                for (int j = 0; j<9; j++)
                {
                    blocks[i,j] = new PictureBox();
                    blocks[i,j].Height = 30;
                    blocks[i,j].Width = 70;
                    blocks[i,j].Tag = "blocks";
                    blocks[i,j].BorderStyle = BorderStyle.Fixed3D;

                    blocks[i,j].Left = left;
                    blocks[i,j].Top = top;

                    this.Controls.Add(blocks[i,j]);

                    left = left + 70;
                }

                top = top + 30;
                left = 15;
            }

            setupGame();

        }

        private void removeBlocks()
        {
            foreach (PictureBox x in blocks)
            {
                this.Controls.Remove(x);
            }
        }



        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            

            ball.Left += ballx;
            ball.Top += bally;

            score1.Text = "Score: " + score;
            lifes1.Text = "Lifes: " + lifes;

            if (goLeft) { player.Left -= speed; }
            if (goRight) { player.Left += speed; }

            if (player.Left < 1)
            {
                goLeft = false; 
            }
            else if (player.Left + player.Width > ClientSize.Width)
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
                    lifes--;
                    bally = -bally;
                lifes1.Text = "Lifes: " + lifes;
            }

            if (lifes == 0)
            {
                gameOver();
                MessageBox.Show("You Loose! Press Enter to restart the game");
            }

            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                ball.Top = 380;

                bally = rand.Next(5, 12) * -1;

                if (ballx < 0)
                {
                    ballx = rand.Next(5, 12) * -1;
                }
                else
                {
                    ballx = rand.Next(5, 12);
                }

            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        bally = -bally;
                        score++;
                    }
                }
            }

            if (score == 45)
            {
                gameOver();
                MessageBox.Show("You Win! Press Enter to restart the game");
            }

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && player.Left > 0)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && player.Left + player.Width < ClientSize.Width)
            {
                goRight = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                removeBlocks();
                placeBlocks();
            }
        }

       
    }
}
