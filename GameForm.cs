using System;
using System.Drawing;
using System.Windows.Forms;

namespace BreakoutPC
{
    public class GameForm : Form
    {
        Timer timer = new Timer();
        Rectangle ball = new Rectangle(100, 100, 30, 30);
        Rectangle paddle;

        double vx = 5;
        double vy = -10;
        double gravity = 0.5;

        bool moveLeft = false;
        bool moveRight = false;

        public GameForm()
        {
            this.Width = 800;
            this.Height = 600;
            this.DoubleBuffered = true;
            this.Text = "Breakout com Gravidade";

            paddle = new Rectangle((this.ClientSize.Width - 200) / 2, this.ClientSize.Height - 100, 200, 30);

            timer.Interval = 16; // ~60 FPS
            timer.Tick += Update;
            timer.Start();

            this.Paint += Draw;
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
        }

        private void Update(object sender, EventArgs e)
        {
            int step = 10;
            if (moveLeft && paddle.X > 0)
                paddle.X -= step;
            if (moveRight && paddle.X + paddle.Width < this.ClientSize.Width)
                paddle.X += step;

            vy += gravity;
            ball.X += (int)vx;
            ball.Y += (int)vy;

            // Colisão com paddle
            if (ball.IntersectsWith(paddle) && vy > 0)
            {
                ball.Y = paddle.Y - ball.Height;
                vy *= -1;
            }

            // Colisão com paredes
            if (ball.X <= 0 || ball.X + ball.Width >= this.ClientSize.Width)
                vx *= -1;

            if (ball.Y <= 0)
            {
                ball.Y = 0;
                vy *= -1;
            }

            // Colisão com chão (ajuste visual com DoEvents)
            if (ball.Y + ball.Height >= this.ClientSize.Height - 2)
            {
                ball.Y = this.ClientSize.Height - ball.Height - 2;

                Invalidate();            // Redesenha a tela
                Update();               // Força o update
                Application.DoEvents(); // Garante que o Draw() execute antes do MessageBox

                timer.Stop();
                MessageBox.Show("A bolinha caiu! Pressione OK para continuar.", "Pause");
                timer.Start();

                vy *= -1;
            }

            Invalidate();
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            g.FillRectangle(Brushes.Red, paddle);
            g.FillEllipse(Brushes.White, ball);

            // Linha amarela no limite inferior
            Pen yellowPen = new Pen(Color.Yellow, 2);
            g.DrawLine(yellowPen, 0, this.ClientSize.Height - 2, this.ClientSize.Width, this.ClientSize.Height - 2);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = true;
            if (e.KeyCode == Keys.Right) moveRight = true;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) moveLeft = false;
            if (e.KeyCode == Keys.Right) moveRight = false;
        }
    }
}
