using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using MarioGame.Interfaces;
using MarioGame.Levels;
using MarioGame.Mario;
using MarioGame.Mario.MarioActionState;
using MarioGame.Mario.MarioPowerUp;

namespace MarioGame.MarioActionState
{
    public class Idling : ActionState
    {
       
        public Idling(MarioEntity mario) : base(mario) { }

        public override void Enter(IMarioActionState preState)
        {
            CurrentState = this;
            PreviousState = preState;
            Mario.Sprite = Mario.Factory.MarioSprite(Mario.PowerState, CurrentState, Mario.Facing, Mario.SpritePosition);
        }
        public override void Down()
        {
                CurrentState.ExitState();
                CurrentState = new Crouching(Mario);
                CurrentState.Enter(this); 
        }

        public override void GoLeft()
        {
            if (Mario.Facing)//right
            {
                Mario.Facing = false;//left
                CurrentState = new Idling(Mario);
                CurrentState.Enter(this);
                
            }
            else//left
            {
                CurrentState.ExitState();
                CurrentState = new Running(Mario);
                CurrentState.Enter(this);
            }
        }

        public override void GoRight()
        {
            if (Mario.Facing)//right
            {
                CurrentState.ExitState();
                CurrentState = new Running(Mario);
                CurrentState.Enter(this);
            }
            else//idle was facing left
            {
                Mario.Facing = true;//right
                CurrentState = new Idling(Mario);
                CurrentState.Enter(this);
            }
        }

        public override void Up()
        {
            CurrentState.ExitState();
            CurrentState = new Jumping(Mario);
            CurrentState.Enter(this);
        }
        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            if(Mario.SpriteVelocity.Y>0)
                Falling();
            else
            {
                Mario.SpriteVelocity = new Vector2(0, 1);
            }
        }
        public override void CollisionResponse(ICollision collided)
        {
          
            if (Mario.EntityCollision.TopCollision(collided))
            {
                Mario.SpriteVelocity = new Vector2(Mario.SpriteVelocity.X, 0);
            }

        }

    }
}
