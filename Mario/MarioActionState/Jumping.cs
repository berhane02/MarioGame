using System;
using MarioGame.Collision;
using MarioGame.Interfaces;
using MarioGame.Levels;
using MarioGame.Mario.MarioPowerUp;
using MarioGame.MarioActionState;
using Microsoft.Xna.Framework;

namespace MarioGame.Mario.MarioActionState
{
    class Jumping : ActionState
    {
        private int JumpingHeight = 0;
        private static readonly int MaxJumpHeight = 200;

        private static readonly int HeightInc = 5;
        //private double MinJumpVelocity = -0.01;
        //private double MaxJumpVeclocity = -1;

        public Jumping(MarioEntity mario) : base(mario) { }

        public override void Enter(IMarioActionState preState)
        {
            Mario.Sounds.PlaySound(Mario.Jumping);
            CurrentState = this;
            PreviousState = preState;
            Vector2 velocity = Mario.SpriteVelocity;
            Mario.Sprite =
                Mario.Factory.MarioSprite(Mario.PowerState, CurrentState, Mario.Facing, Mario.SpritePosition);
            Mario.SpriteVelocity = new Vector2(velocity.X, -5);

        }

        public override void ExitState()
        {
            Mario.SpriteVelocity = new Vector2(Mario.SpriteVelocity.X, -Mario.SpriteVelocity.Y);
        }

        public override void GoLeft()
        {

            Mario.SpriteVelocity += new Vector2(-1, 0);

        }

        public override void GoRight()
        {
            Mario.SpriteVelocity += new Vector2(1, 0);
        }

        public override void Down()
        {
            CurrentState.ExitState();
            CurrentState = new Falling(Mario);
            CurrentState.Enter(this);
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {

            JumpingHeight += HeightInc;
            if (JumpingHeight > MaxJumpHeight)
            {
                JumpingHeight = 0;
                Down();
            }
            int MinX = Mario.Sprite.Width;
            if (MinX > Mario.SpritePosition.X)
            {
                Mario.SpriteVelocity = new Vector2(0, Mario.SpriteVelocity.Y);
                Down();
            }
        }

        public override void CollisionResponse(ICollision collided)
        {
            if (!(collided is ItemCollision))
            {
                if (Mario.EntityCollision.SideCollision(collided))
                    Mario.SpriteVelocity = new Vector2(0, Mario.SpriteVelocity.Y);
                if (Mario.EntityCollision.BottomCollision(collided))
                    Down();
            }
        }
    }
}
