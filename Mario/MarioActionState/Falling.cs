using MarioGame.Collision;
using MarioGame.Interfaces;
using MarioGame.MarioActionState;
using Microsoft.Xna.Framework;


namespace MarioGame.Mario.MarioActionState
{
    public class Falling : ActionState
    {
        public Falling(MarioEntity mario) : base(mario) { }
        public override void Enter(IMarioActionState preState)
        {
            CurrentState = this;
            PreviousState = preState;
            if (preState is Jumping)
            {
                PreviousState = preState.PreviousState;
            }
            Velocity = Mario.SpriteVelocity;
            Mario.Sprite = Mario.Factory.MarioSprite(Mario.PowerState, CurrentState, Mario.Facing, Mario.SpritePosition);
            Mario.SpriteVelocity = new Vector2(Velocity.X, 5);
           
        }

        public override void ExitState()
        {
            Mario.SpriteVelocity = new Vector2(Mario.SpriteVelocity.X,0);
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            int MinX = Mario.Sprite.Width;
            if (MinX > Mario.SpritePosition.X)
                Mario.SpriteVelocity = new Vector2(0, Mario.SpriteVelocity.Y);
        }
        public override void GoLeft()
        {
            Mario.SpriteVelocity = new Vector2(Mario.SpriteVelocity.X-1, Mario.SpriteVelocity.Y);
        }

        public override void GoRight()
        {
            Mario.SpriteVelocity = new Vector2(Mario.SpriteVelocity.X+1, Mario.SpriteVelocity.Y);
        }

        public override void CollisionResponse(ICollision collided)
        {
            if (!(collided is ItemCollision))
            {
                if (Mario.EntityCollision.SideCollision(collided))
                    Mario.SpriteVelocity = new Vector2(0, Mario.SpriteVelocity.Y);
                else if (Mario.EntityCollision.TopCollision(collided))
                {
                    if ((int)Mario.SpriteVelocity.X != 0)
                    {
                        CurrentState.ExitState();
                        CurrentState = PreviousState;
                        CurrentState.Enter(this);
                    }
                    else { Idle();}
                }
                
            }
        }
    }
}
