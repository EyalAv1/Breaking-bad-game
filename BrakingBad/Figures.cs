using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Media;

namespace Figures
{
    [Serializable]
    public abstract class Figures
    {
        //Coordinates - private
        float x;
        float y;

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public abstract void Draw(Graphics g);

        public abstract bool isTouching(float xPoint, float yPoint);
    }


    [Serializable]
    public class myCircle : Figures
    {
        float radius;  //private

        int ballMovePxlX = 3;
        int ballMovePxlY = 3;
        int score= 0;

        public int BallMovePxlX
        {
            get
            {
                return ballMovePxlX;
            }
            set
            {
                ballMovePxlX = value;
            }
        }

        public int BallMovePxlY
        {
            get
            {
                return ballMovePxlY;
            }
            set
            {
                ballMovePxlY = value;
            }
        }

        public myCircle()
            : this(400, 423, 15) //!!!!
        { }

        public myCircle(float xVal, float yVal, float rVal)
        {
            X = xVal;
            Y = yVal;
            radius = rVal;
        }


        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        public override void Draw(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.Black);
            Pen pen = new Pen(Color.Cyan, 2);
            Font font = new Font(
                new FontFamily("Arial"),
                16,
                FontStyle.Regular,
                GraphicsUnit.Pixel);
            g.FillEllipse(br, X - radius, Y - radius, 2 * radius, 2 * radius);
            g.DrawEllipse(pen, X - radius, Y - radius, 2 * radius, 2 * radius);
            g.DrawString("Score: " + score, font, new SolidBrush(Color.Red), 0, 0);
        }
        public override bool isTouching(float xPoint, float yPoint)
        {
            return Math.Sqrt((xPoint - X) * (xPoint - X) + (yPoint - Y) * (yPoint - Y)) < radius;
        }

        public void move_ball(int height, int width)
        {
            if ((this.X - this.radius) <= 0)
                ballMovePxlX = -ballMovePxlX;

            if ((this.X) >= width)
                ballMovePxlX = -ballMovePxlX;

            this.X += ballMovePxlX;

            if ((this.Y - this.radius) <= 0)
                ballMovePxlY = -ballMovePxlY;

            if ((this.Y - this.radius) >= height)
                ballMovePxlY = -ballMovePxlY;

            this.Y += ballMovePxlY;
        }

        public void IsTouchRacket(Plate p)
        {
            Rectangle ballBorder = new Rectangle((int)(this.X - this.Radius), (int)this.Y, (int)this.Radius * 2, (int)this.Radius);
            Rectangle racketBorder = new Rectangle((int)(p.X - p.Width / 2), (int)(p.Y - p.Height / 2), (int)p.Width, (int)p.Height);

            if (ballBorder.IntersectsWith(racketBorder))
                ballMovePxlY = -ballMovePxlY;
        }
        public void IsTouchBrick(bricksList l)
        {
            int i;
            SoundPlayer sound = new SoundPlayer(@"C:\Users\guyav\Desktop\BrakingBad\BrakingBad\PopSound.wav");
            //SoundPlayer sound = new SoundPlayer(@"\PopSound.wav");
            for (i = 0; i < l.NextIndex(); i++)
            {
                Rectangle ballBorder = new Rectangle((int)(this.X - this.Radius), (int)(this.Y - this.Radius), (int)(this.Radius * 2), (int)(this.Radius * 2));
                Rectangle racketBorder = new Rectangle((int)(l[i].X - l[i].Width / 2), (int)(l[i].Y - l[i].Height / 2), (int)l[i].Width, (int)l[i].Height);

                if (ballBorder.IntersectsWith(racketBorder))
                {
                    sound.Play();
                    ballMovePxlY = -ballMovePxlY;
                    l.RemoveBrick(i);
                    this.score++;   
                }

            }
        }
        public void setScore(int score)
        {
            this.score = score;
        }
    }

    [Serializable]
    public abstract class myRectangle : Figures
    {
        float width;
        float height; // Specified the class to rectangle

        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public abstract override void Draw(Graphics g);

        public abstract override bool isTouching(float xPoint, float yPoint);
    }

    [Serializable]
    public class Plate : myRectangle
    {

        public Plate(float xPoint, float yPoint)
        {
            X = xPoint;
            Y = yPoint;
            Width = 120;
            Height = 30;
        }
        public override void Draw(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.DarkRed);
            Pen pen = new Pen(Color.BlanchedAlmond, 2);
            g.FillRectangle(br, X - Width / 2, Y - Height / 2, Width, Height);
            g.DrawRectangle(pen, X - Width / 2, Y - Height / 2, Width, Height);
        }

        public override bool isTouching(float xPoint, float yPoint)
        {
            return Math.Abs(xPoint - X) <= Width / 2 && Math.Abs(yPoint - Y) <= Height / 2;
        }
    }

    [Serializable]
    public class Brick : myRectangle
    {
        Color brickColor;

        public Brick(float xPoint, float yPoint)
        {
            X = xPoint;
            Y = yPoint;
            Width = 160;
            Height = 40;
        }
        public override void Draw(Graphics g)
        {
            // Top left point
            SolidBrush br = new SolidBrush(Color.Black);
            Pen pen = new Pen(Color.White, 2);
            g.FillRectangle(br, X - Width / 2, Y - Height / 2, Width, Height);
            g.DrawRectangle(pen, X - Width / 2, Y - Height / 2, Width, Height);
        }

        public override bool isTouching(float xPoint, float yPoint)
        {
            return Math.Abs(xPoint - X) <= Width / 2 && Math.Abs(yPoint - Y) <= Height / 2;
        }

       
    }

    [Serializable]
    public class bricksList
    {
        protected List<Brick> bricks;

        // Constructor
        public bricksList()
        {
            bricks = new List<Brick>();
        }

        public int NextIndex()
        {
            return bricks.Count;
        }

        public Brick this[int index]
        {
            get
            {
                if (index >= bricks.Count)
                    return null;
                return bricks[index];
            }
            set
            {
                if (index == bricks.Count)
                    bricks.Add(value);
                else if (index < bricks.Count)
                    bricks[index] = value;
            }
        }
        public void RemoveBrick(int index)
        {
            this.bricks.RemoveAt(index);
        }

        public void DrawAll(Graphics g)
        {
            for (int i = 0; i < bricks.Count; i++)
                bricks[i].Draw(g);
        }
    }
}


    
