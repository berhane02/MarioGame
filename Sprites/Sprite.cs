using System;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.CommandHandling;

namespace MarioGame
{
    public class Sprite : ISprite
    {
        public Texture2D Texture { get; set; }
        public Texture2D Rectangle { get; set; }
        public Rectangle BoundBox { get; set; }
        protected int Rows { get; set; }
        protected int Columns { get; set; }
        public int Width
        {
            get { return Texture.Width / Columns; }
        }

        public int Height
        {
            get { return Texture.Height / Rows; }
        }
        public int CurrentFrame { get; set; }
        protected int TotalFrames { get; set; }
        public float TimePerFrame { get; set; }
        protected float TotalElapsed { get; set; }
        protected bool SeriesPicture { get; set; }
        public bool IsToggle { get; set; }
        protected Color Box { get; set; }//this controls the color of the Bounding box
        protected Color Tint { get; set; }//this controls the collision response color
        public int Scale { get; set; }
        public bool Dead { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public Sprite(Texture2D texture, bool animated, int row, int column, Vector2 loc)
        {
            SeriesPicture = animated;
            Rows = row;
            Columns = column;
            CurrentFrame = 0;
            TotalFrames = Rows * Columns;
            Texture = texture;
            Position = loc;
            Velocity = Vector2.Zero;
            Scale = 1;
            Tint = Color.White;
            Box = Color.Beige;
            TimePerFrame = 0.2f;
            TotalElapsed = 0;
            Dead = false;
            BoundBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }

        public virtual void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphic)
        {
            WhiteBox(graphic);
            TotalElapsed += gametime.ElapsedGameTime.Milliseconds;
            if (SeriesPicture&&TotalElapsed>TimePerFrame)
            {
                CurrentFrame = (int)(gametime.TotalGameTime.TotalSeconds / TimePerFrame);
                CurrentFrame++;
                CurrentFrame = CurrentFrame % TotalFrames;

                //if (CurrentFrame == TotalFrames)
                //    CurrentFrame = 0;
                TotalElapsed -= TimePerFrame;
            }
            Velocity = velocity;
            Position += Velocity;//if static then velocity is 0;
            BoundBox = new Rectangle((int)Position.X, (int)Position.Y, Scale * Width, Scale * Height);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int row = CurrentFrame / Columns;
            int column = CurrentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Scale * Width, Scale * Height);
            if (IsToggle)
            {
                spriteBatch.Draw(Rectangle, BoundBox, Box); //draw rectangle as background for sprite
            }
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Tint);
        }

        public virtual Rectangle FutureBox(Vector2 Location)
        {
            return new Rectangle((int)Location.X, (int)Location.Y, BoundBox.Width, BoundBox.Height);
        }

        public virtual void CollisionResponse(bool hit)
        {
            //if (hit)
            //{
            //    Tint = Color.Black;
            //}
            //else
            //{
                Tint = Color.White;
            //}
        }
        public void WhiteBox(GraphicsDeviceManager graphic) //this is to give Boundbox a texture to be drawn
        {
            if (Rectangle == null)
            {
                Rectangle = new Texture2D(graphic.GraphicsDevice, 1, 1);
                Rectangle.SetData(new[] { Color.White });
            }
        }
        //public object Clone()
        //{
        //    return this.MemberwiseClone();
        //}
    }
}