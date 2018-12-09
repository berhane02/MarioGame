using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    public class EnemySprites:Sprite
    {
        public EnemySprites(Texture2D texture, bool animated, int row, int column, Vector2 loc) : base(texture, animated, row, column, loc)
        {
            TimePerFrame = 0.2f;
            Position = loc;
            Box = Color.Red;
            Dead = false;
            BoundBox = new Rectangle((int)Position.X+3,(int)Position.Y+3, Scale*Width-6,Scale*Height);//smaller bounding box
        }

        public override void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphic)
        {
            WhiteBox(graphic);
            CurrentFrame = (int)(gametime.TotalGameTime.TotalSeconds / TimePerFrame);
            CurrentFrame = CurrentFrame % TotalFrames;

            Velocity = velocity;
            Position += Velocity;
            BoundBox = new Rectangle((int)Position.X + 3, (int)Position.Y + 3, Scale*Width - 6, Scale*Height);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            int row = CurrentFrame / Columns;
            int column = CurrentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Scale * Width, Scale * Height);
            if (IsToggle)
            {
                spriteBatch.Draw(Rectangle, BoundBox, Box); //draw rectangle as background for sprite
            }
            if(Velocity.X<0)
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Tint, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            else
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Tint, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.5f);
        }
    }

    public class Piranha : EnemySprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Enemy/Piranha");

        public Piranha(Vector2 loc) : base(texture, true, 1, 2, loc)
        {
            Scale = 2;
        }
    }

    public class Bowser : EnemySprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Enemy/Bowser");
        public Bowser(Vector2 loc) : base(texture, true, 1, 4, loc)
        {
            Scale = 2;
        }
    }
}
