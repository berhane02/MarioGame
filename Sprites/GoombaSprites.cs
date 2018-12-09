using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public class WalkingGoomba : EnemySprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Enemy/Goomba");

        public WalkingGoomba(Vector2 loc) : base(texture, true, 1, 3, loc) { }


        public override void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphics)
        {
            WhiteBox(graphics);
            double timePerFrame = 0.2;
            CurrentFrame = (int)(gametime.TotalGameTime.TotalSeconds / timePerFrame);
            CurrentFrame = CurrentFrame % TotalFrames;

            if (CurrentFrame == 1)
            {
                CurrentFrame = 0;
            }
            else
            {
                CurrentFrame = 1;
            }
            Velocity = velocity;
            Position += Velocity;
            BoundBox = new Rectangle((int)Position.X + 3, (int)Position.Y + 3, Width - 6, Height - 6);
        }

    }


}