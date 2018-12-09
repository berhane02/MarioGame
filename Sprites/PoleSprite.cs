using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    public class PoleSprite : Sprite
    {
        private static int n = 1;
        private static bool animated = false;
        public bool stopAnimating { get; set; }
        //private static Texture2D currentTexture;
        private static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Sprites/StaticFlag");
        private static Texture2D texture2 = Game1.ContentLoad.Load<Texture2D>("Sprites/MovingFlags");

        public PoleSprite(Vector2 loc, bool coll, int Colum) : base(texture, animated, 1, n, loc)
        {
            stopAnimating = false;
            animated = coll;
            if (animated)
            {
                texture = texture2;
                n = Colum;
            }
            else
            {
                texture = Game1.ContentLoad.Load<Texture2D>("Sprites/StaticFlag");
                n = 1;
            }
           
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            int row = CurrentFrame / Columns;
            int column = CurrentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X - Width, (int)Position.Y - Height, Width, Height);
            BoundBox = destinationRectangle;
            if (this.IsToggle)
            {
                spriteBatch.Draw(Rectangle, BoundBox, Box); //try to draw rectangle as background for sprite
            }
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphic)
        {
            WhiteBox(graphic);
            TotalElapsed += gametime.ElapsedGameTime.Milliseconds;
            if (!stopAnimating)
            {
                if (SeriesPicture && TotalElapsed > TimePerFrame)
                {
                    CurrentFrame = (int)(gametime.TotalGameTime.TotalSeconds / TimePerFrame);
                    CurrentFrame++;
                    CurrentFrame = CurrentFrame % TotalFrames;

                    if (CurrentFrame == 4)
                    {
                        stopAnimating = true;
                        animated = false;
                    }
                    TotalElapsed -= TimePerFrame;
                }
            }
           
        }
       

    }
}
