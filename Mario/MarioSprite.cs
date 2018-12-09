using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Mario
{
    public class MarioSprite : Sprite
    {
        public MarioSprite(Texture2D texture, bool animated, int row, int column, Vector2 loc) : base(texture, animated, row, column, loc)
        {
            SeriesPicture = animated;
            Rows = row;
            Columns = column;
            CurrentFrame = 0;
            TotalFrames = Rows * Columns;
            Texture = texture;
            Position = new Vector2(loc.X, loc.Y);
            Velocity = new Vector2(0, 0);
            Box = Color.Yellow;
            Tint = Color.White;
            BoundBox = new Rectangle((int)Position.X - Width, (int)Position.Y - Height, Width, Height);
        }

        public override void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphic)
        {
            base.Update(gametime, velocity, graphic);
            BoundBox = new Rectangle((int)Position.X - Width, (int)Position.Y - Height, Width, Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            int row = CurrentFrame / Columns;
            int column = CurrentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X - Width, (int)Position.Y - Height, Width, Height);
            BoundBox = destinationRectangle;
            if (IsToggle)
            {
                spriteBatch.Draw(Rectangle, BoundBox, Box); //try to draw rectangle as background for sprite
            }
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White,0,Vector2.Zero,SpriteEffects.None,0.2f);

        }
        public override Rectangle FutureBox(Vector2 Location)
        {
            return new Rectangle((int)Location.X - Width, (int)Location.Y - Height, Width, Height);
        }
    }
}
