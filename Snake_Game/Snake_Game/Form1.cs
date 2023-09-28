using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        PictureBox[] Snake = new PictureBox[400];
        PictureBox[] Apple = new PictureBox[5];
        List<PictureBox> App = new List<PictureBox>(5);
        PictureBox[] SnakeBot = new PictureBox[400];
        int dirX, dirY, dirXbot = 1, dirYbot = 0;
        int _width = 1050;
        int _height = 850;
        int sizeOfSides = 40;
        int score = 0;
        int scoreBot = 0;
        int scoreMax = 10;
        int scoreKillBot = 0;
        int fieldsCount = 20;
        int updateInterval = 200;
        Random Random = new Random();
        Label labelScore, labelScoreBot, labelScoreMax, labelScoreMaxBot, labelKillBot;
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {
                App.Add(new PictureBox());
                App[i].Location = new Point(-sizeOfSides, -sizeOfSides);
            }
            labelScore = new Label();
            labelScoreBot = new Label();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        bool ft = false, ft2 = false;
        private void WASD(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "D":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "A":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "W":
                    dirX = 0;
                    dirY = -1;
                    break;
                case "S":
                    dirX = 0;
                    dirY = 1;
                    break;
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirX = 0;
                    dirY = -1;
                    break;
                case "Down":
                    dirX = 0;
                    dirY = 1;
                    break;
            }
            if (e.KeyCode == Keys.Enter)
            {
                endGame();
                startGame();
            }
            if (e.KeyCode == Keys.Escape)
            {

                if (ft)
                {
                    Update.Start();
                    timerBot.Start();
                    ft = false;
                }
                else
                {
                    Update.Stop();
                    timerBot.Stop();
                    ft = true;
                }
            }
            /*if (e.KeyCode == Keys.Escape)
            {
                if (ft2)
                {
                    Update.Start();
                    ft2 = false;
                }
                else
                {
                    Update.Stop();
                    ft2 = true;
                }
            }*/
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            checkBorders(Snake, score);
            checkBorders(SnakeBot, scoreBot);
            eatItselfBot();
            Moving();
            MovingBot();
            eatApple();
            eatAppleBot();
        }
        int count = 0;
        private void timerBot_Tick(object sender, EventArgs e)
        {
            int x, y;
            Random rnd = new Random();

            do
            {
                x = rnd.Next(-1, 2);
                y = rnd.Next(-1, 2);
                if (x == 1 || x == -1)
                {
                    y = 0;
                }
                else if (x == 0)
                {
                    while (y == 0)
                    {
                        y = rnd.Next(-1, 1);
                    }
                }
            } 
            while (((dirXbot == 1 && x == -1) || (dirYbot == 1 && y == -1)) || ((dirXbot == -1 && x == 1) || (dirYbot == -1 && y == 1)));

            dirXbot = x;
            dirYbot = y;
            count++;

            if (count == 4)
            {
                labelScoreBot.Text = $"Рекорд бота: {++scoreBot}";
                SnakeBot[scoreBot] = new PictureBox();
                SnakeBot[scoreBot].Location = new Point(SnakeBot[scoreBot - 1].Location.X + sizeOfSides * dirX, SnakeBot[scoreBot - 1].Location.Y - sizeOfSides * dirY);
                SnakeBot[scoreBot].Size = new Size(sizeOfSides - 1, sizeOfSides - 1);
                SnakeBot[scoreBot].BackColor = Color.Yellow;
                this.Controls.Add(SnakeBot[scoreBot]);
                count = 0;
            }
        }

        void Moving()
        {
            for (int i = score; i >= 1; i--)
            {
                Snake[i].Location = Snake[i - 1].Location;
            }
            Snake[0].Location = new Point(Snake[0].Location.X + dirX * (sizeOfSides), Snake[0].Location.Y + dirY * (sizeOfSides));
            eatItself();
        }
        void MovingBot()
        {

            if (dirXbot == 1 && dirYbot == 0)
            {
                for (int k = 0; k <= score; k++)
                {
                    if (SnakeBot[0].Location.X + sizeOfSides * 2 >= Snake[k].Location.X && SnakeBot[0].Location.Y == Snake[k].Location.Y)
                    {
                        dirXbot = 0;
                        dirYbot = -1;
                        break;
                    }
                }
            }
            if (dirXbot == -1 && dirYbot == 0)
            {
                for (int k = 0; k <= score; k++)
                {
                    if (SnakeBot[0].Location.X - sizeOfSides * 2 <= Snake[k].Location.X && SnakeBot[0].Location.Y == Snake[k].Location.Y)
                    {
                        dirXbot = 0;
                        dirYbot = 1;
                        break;
                    }
                }
            }
            if (dirXbot == 0 && dirYbot == -1)
            {
                for (int k = 0; k <= score; k++)
                {
                    if (SnakeBot[0].Location.X == Snake[k].Location.X && SnakeBot[0].Location.Y - sizeOfSides * 2 <= Snake[k].Location.Y)
                    {
                        dirXbot = -1;
                        dirYbot = 0;
                        break;
                    }
                }
            }
            if (dirXbot == 0 && dirYbot == 1)
            {
                for (int k = 0; k <= score; k++)
                {
                    if (SnakeBot[0].Location.X == Snake[k].Location.X && SnakeBot[0].Location.Y + sizeOfSides * 2 >= Snake[k].Location.Y)
                    {
                        dirXbot = 1;
                        dirYbot = 0;
                        break;
                    }
                }
            }
            for (int i = scoreBot; i >= 1; i--)
            {
                SnakeBot[i].Location = SnakeBot[i - 1].Location;
            }
            SnakeBot[0].Location = new Point(SnakeBot[0].Location.X + dirXbot * (sizeOfSides), SnakeBot[0].Location.Y + dirYbot * (sizeOfSides));
            // eatItselfBot();
        }
        void generateMap()
        {
            for (int i = 0; i <= fieldsCount; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.SetBounds(0, sizeOfSides * i, sizeOfSides * fieldsCount, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= fieldsCount; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.SetBounds(sizeOfSides * i, 0, 1, sizeOfSides * fieldsCount);
                this.Controls.Add(pic);
            }
        }

        int countApple = 0;
        void generateApple()
        {
            bool FtPlayer = true, FtBot = true, FtFinal = true;
            int AppleX = Random.Next(1, fieldsCount);
            int AppleY = Random.Next(1, fieldsCount);
            while (FtFinal)
            {
                for (int k = 0; k < score; k++)
                {
                    if (Snake[k].Location.X == sizeOfSides * AppleX && Snake[k].Location.Y == sizeOfSides * AppleY)
                    {
                        FtPlayer = false;
                        break;
                    }
                    else
                    {
                        FtPlayer = true;
                    }
                }
                for (int k = 0; k < scoreBot; k++)
                {
                    if (SnakeBot[k].Location.X == sizeOfSides * AppleX && SnakeBot[k].Location.Y == sizeOfSides * AppleY)
                    {
                        FtBot = false;
                        break;
                    }
                    else
                    {
                        FtBot = true;
                    }
                }

                if (FtPlayer && FtBot)
                {
                    FtFinal = false;
                }
                else
                {
                    AppleX = Random.Next(1, fieldsCount);
                    AppleY = Random.Next(1, fieldsCount);
                    FtFinal = true;
                }

            }
            App[countApple].BackColor = Color.Red;
            App[countApple].Size = new Size(sizeOfSides, sizeOfSides);
            App[countApple].Location = new Point(sizeOfSides * AppleX, sizeOfSides * AppleY);
            this.Controls.Add(App[countApple]);

            if (countApple + 1 >= 5)
            {
                countApple = 0;
            }
            countApple++;

            if (score == scoreMax || scoreBot == scoreMax * 2)
            {
                updateInterval -= 40;
                Update.Interval = updateInterval;
                scoreMax += 10;
                generateApple();
            }

        }
        void eatApple()
        {
            for (int a = 0; a < App.Count; a++)
            {
                if (Snake[0].Location == App[a].Location)
                {
                    this.Controls.Remove(App[a]);
                    App[a].Location = new Point(-sizeOfSides, -sizeOfSides);
                    labelScore.Text = $"Рекорд: {++score}";
                    Snake[score] = new PictureBox();
                    Snake[score].Location = new Point(Snake[score - 1].Location.X + sizeOfSides * dirX, Snake[score - 1].Location.Y - sizeOfSides * dirY);
                    Snake[score].Size = new Size(sizeOfSides - 1, sizeOfSides - 1);
                    Snake[score].BackColor = Color.Green;
                    this.Controls.Add(Snake[score]);
                    generateApple();
                }
            }
        }
        void eatAppleBot()
        {
            for (int a = 0; a < App.Count; a++)
            {
                if (SnakeBot[0].Location == App[a].Location)
                {
                    this.Controls.Remove(App[a]);
                    App[a].Location = new Point(-sizeOfSides, -sizeOfSides);
                    labelScoreBot.Text = $"Рекорд бота: {++scoreBot}";
                    SnakeBot[scoreBot] = new PictureBox();
                    SnakeBot[scoreBot].Location = new Point(SnakeBot[scoreBot - 1].Location.X + sizeOfSides * dirX, SnakeBot[scoreBot - 1].Location.Y - sizeOfSides * dirY);
                    SnakeBot[scoreBot].Size = new Size(sizeOfSides - 1, sizeOfSides - 1);
                    SnakeBot[scoreBot].BackColor = Color.Yellow;
                    this.Controls.Add(SnakeBot[scoreBot]);
                    generateApple();
                }
            }
        }
        void eatItself()
        {
            for (int k = 1; k < score; k++)
            {
                if (Snake[0].Location == Snake[k].Location)
                {
                    endGame();
                }
            }

            for (int k = 0; k < scoreBot; k++)
            {
                if (Snake[0].Location == SnakeBot[k].Location)
                {
                    endGame();
                }
            }
        }
        void eatItselfBot()
        {
            for (int k = 2; k < scoreBot; k++)
            {
                if (SnakeBot[0].Location == SnakeBot[k].Location)
                {
                    snakeDelete(SnakeBot, scoreBot);
                    scoreBot = 0;
                    SnakeBot[0] = new PictureBox();
                    SnakeBot[0].BackColor = Color.Gold;
                    SnakeBot[0].SetBounds(1, 1, sizeOfSides - 1, sizeOfSides - 1);
                    this.Controls.Add(SnakeBot[0]);
                }
            }

            for (int k = 0; k <= score; k++)
            {
                if (SnakeBot[0].Location == Snake[k].Location)
                {
                    snakeDelete(SnakeBot, scoreBot);
                    scoreBot = 0;
                    labelKillBot.Text = $"Съедено ботов: {++scoreKillBot}";
                    SnakeBot[0] = new PictureBox();
                    SnakeBot[0].BackColor = Color.Gold;
                    SnakeBot[0].SetBounds(1, 1, sizeOfSides - 1, sizeOfSides - 1);
                    this.Controls.Add(SnakeBot[0]);
                }
            }

        }

        void checkBorders(PictureBox[] snack, int SCORE)
        {
            /*if (Snake[0].Location.X < 0 || Snake[0].Location.X >= sizeOfSides * fieldsCount 
                || Snake[0].Location.Y < 0 || Snake[0].Location.Y >= sizeOfSides * fieldsCount)
            {
                endGame();
            }*/
            if (snack[0].Location.X < 0)
            {
                snack[0].Location = new Point(sizeOfSides * fieldsCount - sizeOfSides, snack[0].Location.Y);
                for (int i = SCORE; i >= 1; i--)
                {
                    snack[i].Location = snack[i - 1].Location;
                }
            }
            if (snack[0].Location.X >= sizeOfSides * fieldsCount /*- sizeOfSides*/)
            {
                snack[0].Location = new Point(0, snack[0].Location.Y);
                for (int i = SCORE; i >= 1; i--)
                {
                    snack[i].Location = snack[i - 1].Location;
                }
            }
            if (snack[0].Location.Y < 0)
            {
                snack[0].Location = new Point(snack[0].Location.X, sizeOfSides * fieldsCount - sizeOfSides);
                for (int i = SCORE; i >= 1; i--)
                {
                    snack[i].Location = snack[i - 1].Location;
                }
            }
            if (snack[0].Location.Y >= sizeOfSides * fieldsCount /*- sizeOfSides*/)
            {
                snack[0].Location = new Point(snack[0].Location.X, 0);
                for (int i = SCORE; i >= 1; i--)
                {
                    snack[i].Location = snack[i - 1].Location;
                }
            }

        }
        void maxScore()
        {
            StreamReader red = new StreamReader("Score.txt");
            int maxP = Convert.ToInt32(red.ReadLine());
            int maxB = Convert.ToInt32(red.ReadLine());
            red.Close();
            if (maxP < score && maxB < scoreBot)
            {
                File.WriteAllText("Score.txt", $"{score}\n{scoreBot}");
            }
            else if (maxP < score)
            {
                File.WriteAllText("Score.txt", $"{score}\n{maxB}");
            }
            else if (maxB < scoreBot)
            {
                File.WriteAllText("Score.txt", $"{maxP}\n{scoreBot}");
            }
        }

        void snakeDelete(PictureBox[] snack, int SCORE)
        {
            for (int s = 0; s <= SCORE; s++)
            {
                this.Controls.Remove(snack[s]);
            }
            Array.Clear(snack, 1, snack.Length - 1);
            maxScore();
        }
        void startGame()
        {
            Random rnd = new Random();
            generateApple();
            generateApple();

            snakePicture.Visible = false;
            primaryGrupText.Visible = false;
            this.Width = _width;
            this.Height = _height;
            //Apple.Location = new Point(_sizeOfSides * fieldsCount / 2, _sizeOfSides * fieldsCount / 2);

            Snake[0] = new PictureBox();
            Snake[0].BackColor = Color.Lime;
            Snake[0].SetBounds(sizeOfSides * fieldsCount / 2, sizeOfSides * fieldsCount / 2, sizeOfSides - 1, sizeOfSides - 1);
            //Snake[0].SetBounds(-1,-1, sizeOfSides - 1, sizeOfSides - 1);
            this.Controls.Add(Snake[0]);

            SnakeBot[0] = new PictureBox();
            SnakeBot[0].BackColor = Color.Gold;
            SnakeBot[0].SetBounds(sizeOfSides, sizeOfSides, sizeOfSides - 1, sizeOfSides - 1);
            this.Controls.Add(SnakeBot[0]);

            generateMap();

            score = 0;
            scoreBot = 0;
            scoreMax = 10;
            updateInterval = 200;
            Update.Interval = updateInterval;

            labelScore.Text = $"Рекорд: {score}";
            labelScore.SetBounds(sizeOfSides * fieldsCount + 20, 10, sizeOfSides * 4, sizeOfSides - 10);
            labelScore.Font = new Font("Gill Sans Ultra", 14, FontStyle.Bold);
            labelScore.ForeColor = Color.Red;
            this.Controls.Add(labelScore);

            labelScoreBot.Text = $"Рекорд бота: {scoreBot}";
            labelScoreBot.SetBounds(sizeOfSides * fieldsCount + 20, sizeOfSides + 5, sizeOfSides * 5, sizeOfSides - 10);
            labelScoreBot.Font = new Font("Gill Sans Ultra", 14, FontStyle.Bold);
            labelScoreBot.ForeColor = Color.Red;
            this.Controls.Add(labelScoreBot);

            StreamReader red = new StreamReader("Score.txt");

            labelScoreMax = new Label();
            labelScoreMax.Text = $"Максимальный\nрекорд: {red.ReadLine()}";
            labelScoreMax.SetBounds(sizeOfSides * fieldsCount + 20, sizeOfSides * 2, sizeOfSides * 5, sizeOfSides + 10);
            labelScoreMax.Font = new Font("Gill Sans Ultra", 14, FontStyle.Bold);
            labelScoreMax.ForeColor = Color.Red;
            this.Controls.Add(labelScoreMax);

            labelScoreMaxBot = new Label();
            labelScoreMaxBot.Text = $"Максимальный\nрекорд бота: {red.ReadLine()}";
            labelScoreMaxBot.SetBounds(sizeOfSides * fieldsCount + 20, sizeOfSides * 3 + 20, sizeOfSides * 5, sizeOfSides + 10);
            labelScoreMaxBot.Font = new Font("Gill Sans Ultra", 14, FontStyle.Bold);
            labelScoreMaxBot.ForeColor = Color.Red;
            this.Controls.Add(labelScoreMaxBot);

            red.Close();

            labelKillBot = new Label();
            labelKillBot.Text = $"Съедено ботов: {scoreKillBot}";
            labelKillBot.SetBounds(sizeOfSides * fieldsCount + 20, sizeOfSides * 5, sizeOfSides * 5, sizeOfSides - 10);
            labelKillBot.Font = new Font("Gill Sans Ultra", 14, FontStyle.Bold);
            labelKillBot.ForeColor = Color.Red;
            this.Controls.Add(labelKillBot);

            Update.Start();
            timerBot.Start();
        }
        void endGame()
        {
            Update.Stop();
            timerBot.Stop();

            primaryGrupText.Visible = true;
            primaryGrupText.Location = new Point(Width / 2 - primaryGrupText.Width / 2, Height / 2 - primaryGrupText.Height / 2);
            snakeText.Text = "End game";
            snakeText.Location = new Point(toStartText.Location.X + 20, toStartText.Location.Y - 60);

            Thread.Sleep(1000);
            snakeDelete(Snake, score);
            snakeDelete(SnakeBot, scoreBot);
            score = 0;
            scoreBot = 0;
            scoreKillBot = 0;

            for (int a = 0; a < App.Count; a++)
            {
                this.Controls.Remove(App[a]);
                App[a].Location = new Point(-sizeOfSides, -sizeOfSides);
            }
            //labelScore.Text = "Проиграл";
        }
    }
}
