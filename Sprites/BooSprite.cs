
using MarioGame.Levels;
using MarioGame.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{

    public class BooSprite : EnemySprites
    {
        private bool makeSound = true;
        private bool BooFacing;
        private double disX, disY;
        private double viewPortPos = 400, viewPortNeg = -400;
        public MarioEntity Mario { get; set; }
        private static readonly Texture2D _left = Game1.ContentLoad.Load<Texture2D>("Boo/Left");
        private static readonly Texture2D _right = Game1.ContentLoad.Load<Texture2D>("Boo/Right");
        private static readonly Texture2D _leftMove = Game1.ContentLoad.Load<Texture2D>("Boo/LeftMove");
        private static readonly Texture2D _rightMove = Game1.ContentLoad.Load<Texture2D>("Boo/RightMove");
        private static Texture2D current = _left;
        public BooSprite(MarioEntity mario, Vector2 loc) : base(current, true, 1, 1, loc)
        {
            this.Mario = mario;
        }

        public override void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphic)
        {
            disX = Mario.SpritePosition.X - base.Position.X;
            disY = Mario.SpritePosition.Y - base.Position.Y;
            if (Mario.Facing && !BooFacing || !Mario.Facing && BooFacing)
            {
                velocity = new Vector2(0, 0);
                if (disX < 0)
                {
                    current = _left;
                    BooFacing = false;
                    makeSound = true;
                }
                else
                {
                    current = _right;
                    BooFacing = true;
                    makeSound = true;
                }

            }
            else if (disX < (viewPortPos) && disX > (viewPortNeg))
            {
                if (disX > 2 && disY > 0)
                {
                    velocity = new Vector2(0.5f, 0.5f);
                    current = _rightMove;
                    BooFacing = true;
                    if (makeSound)
                    {
                        Mario.Sounds.PlaySound(EventSoundEffects.EventSounds.BooSound);
                        makeSound = false;
                    }
                }

                if (disX > 2 && disY < 0)
                {
                    velocity = new Vector2(0.5f, -0.5f);
                    current = _rightMove;
                    BooFacing = true;
                    if (makeSound)
                    {
                        Mario.Sounds.PlaySound(EventSoundEffects.EventSounds.BooSound);
                        makeSound = false;
                    }
                }

                if (disX < -2 && disY < 0)
                {
                    velocity = new Vector2(-0.5f, -0.5f);
                    current = _leftMove;
                    BooFacing = false;
                    if (makeSound)
                    {
                        Mario.Sounds.PlaySound(EventSoundEffects.EventSounds.BooSound);
                        makeSound = false;
                    }
                }

                if (disX < -2 && disY > 0)
                {
                    velocity = new Vector2(-0.3f, 0.5f);
                    current = _leftMove;
                    BooFacing = false;
                    if (makeSound)
                    {
                        Mario.Sounds.PlaySound(EventSoundEffects.EventSounds.BooSound);
                        makeSound = false;
                    }
                }

                if (disX <= 1 && disX >= -1)
                {
                    if (disY > 0)
                    {
                        velocity = new Vector2(0, 0.5f);
                        current = _leftMove;
                    }
                    else
                    {
                        velocity = new Vector2(0, -0.5f);
                        current = _rightMove;
                    }
                }

                if (disY == 0)
                {
                    if (disX > 0)
                    {
                        velocity = new Vector2(0.5f, 0);
                        current = _rightMove;
                    }
                    else
                    {
                        velocity = new Vector2(-0.5f, 0);
                        current = _leftMove;
                    }
                }

            }
            base.Update(gametime, velocity, graphic);

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
            spriteBatch.Draw(current, destinationRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
