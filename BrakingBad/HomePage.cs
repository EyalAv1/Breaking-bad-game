using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Figures;


using System.IO;
using System.Runtime.Serialization;//!!!!!!
using System.Runtime.Serialization.Formatters.Binary;

namespace BrakingBad
{
    public partial class surface : Form
    {
        int playMode = 1; // edit mode = 0, game mode = 1
        int start = 0; // Flag to see if the user start playing
        int isClicking = 0;

        Plate userPlate = new Plate(506, 498);
        myCircle ball = new myCircle(506, 468, 15);
        bricksList bList = new bricksList();

        public surface()
        {
            InitializeComponent();
        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            DEBUG_TXT.Text = isClicking.ToString();
            if (start == 1)
            {
                ball.move_ball(gamePlate.Height, gamePlate.Width - 15);
                ball.IsTouchRacket(userPlate);
                ball.IsTouchBrick(bList);
                if (ball.Y > 536)
                {
                    userPlate = new Plate(506, 498);
                    ball = new myCircle(506, 468, 15);
                    bList = new bricksList();
                    start = 0;
                    MessageBox.Show("Game Over!");
                }
            }
            //MessageBox.Show("Game Over");
            gamePlate.Invalidate();
        }

        private void gamePlate_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //FontFamily = new FontFamily("Comic Sans MS");

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            userPlate.Draw(g);
            ball.Draw(g);
            bList.DrawAll(g);
            Invalidate();
        }


        //////////// Buttons Section ////////////
        private void editBtn_Click(object sender, EventArgs e)
        {
            playMode = 0;
        }

        private void gameBtn_Click(object sender, EventArgs e)
        {

            playMode = 1;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (playMode == 1)
                start = 1;
        }



        //////////// Plate Moving ////////////
        private void gamePlate_MouseDown(object sender, MouseEventArgs e)
        {
            if (userPlate.isTouching(e.X, e.Y) && playMode == 1)
            {
                userPlate.X = e.X;
                isClicking = 1;
            }
            gamePlate.Invalidate();
        }

        private void gamePlate_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClicking == 1 && start == 1) // isClicking == 1 && playMode == 1
            {
                if (e.X > (gamePlate.Size.Width - (userPlate.Width / 2)))
                    userPlate.X = (gamePlate.Size.Width - (userPlate.Width / 2));
                else if (e.X < (userPlate.Width / 2))
                    userPlate.X = userPlate.Width / 2;
                else
                    userPlate.X = e.X;
            }
            gamePlate.Invalidate();
        }

        private void gamePlate_MouseUp(object sender, MouseEventArgs e)
        {
            isClicking = 0;
        }


        //////////// Edit Mode ////////////
        private void gamePlate_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (playMode == 0 && start == 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    for (int i = 0; i < bList.NextIndex(); i++)
                    {
                        if (bList[i].isTouching(e.X, e.Y))
                        {
                            bList.RemoveBrick(i);
                        }
                    }
                }
                else
                    bList[bList.NextIndex()] = new Brick(e.X, e.Y);
            }
            gamePlate.Invalidate();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    //!!!!
                    formatter.Serialize(stream, bList);
                }
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream = File.Open(openFileDialog1.FileName, FileMode.Open);
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //!!!!
                bList = (bricksList)binaryFormatter.Deserialize(stream);
                gamePlate.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HomePage click = new HomePage();
            click.Show();
            Hide();
        }

        private void surface_Load(object sender, EventArgs e)
        {

        }
    }
}
