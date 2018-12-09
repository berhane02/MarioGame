using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{

    public class RedKoopa : EnemySprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Enemy/RightRedkoopa");

        public RedKoopa(Vector2 loc) : base(texture, true, 1, 4, loc) {}
        public override void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;

            int row;
            int column;
            if (CurrentFrame % 2 == 1)
            {
                row = 1 / Columns;
                column = 4 % Columns;
            }
            else
            {
                row = 3 / Columns;
                column = 3 % Columns;
            }

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);
            BoundBox = destinationRectangle;

            if (IsToggle)
            {
                spriteBatch.Draw(Rectangle, BoundBox, Box);
            }
            if (Velocity.X > 0)
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Tint, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            else
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Tint, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.5f);
        }

    }
    

}