using System.Runtime.InteropServices;
using MarioGame.Collision;
using MarioGame.Interfaces;
using MarioGame.Mario;
using MarioGame.Mario.MarioActionState;
using Microsoft.Xna.Framework;

namespace MarioGame.MarioActionState
{
    public class Running : ActionState
    {
  
        public Running(MarioEntity mario) : base(mario) { }

        public override void Enter(IMarioActionState preState)
        {
            CurrentState = this;
            PreviousState = preState;
            Mario.Sprite = Mario.Factory.MarioSprite(Mario.PowerState, CurrentState, Mario.Facing, Mario.SpritePosition);
 
            if(Mario.Facing)
                Mario.SpriteVelocity = new Vector2(3, 0);
            else
            {
                Mario.SpriteVelocity = new Vector2(-3, 0);
            }
            //if (Mario.Facing&&(int)velocity.X==0)//right
            //{
            //    Mario.SpriteVelocity = new Vector2(3, 0);
            //}
            //else if ((int) velocity.X == 0) //left
            //{
            //    Mario.SpriteVelocity = new Vector2(-3, 0);
            //}
            //else
            //{
            //    Mario.SpriteVelocity = new Vector2(velocity.X, 0);
            //}
        }

        public override void Down()
        {
            CurrentState.ExitState();
            CurrentState = new Crouching(Mario);
            CurrentState.Enter(this);
           
        }
        public override void GoLeft()
        {

            if (Mario.Facing) //right
            {
                Mario.Facing = false; //left
                CurrentState.Idle();
            }

        }

        public override void GoRight()
        {
            if (!Mario.Facing)//left
            {
                Mario.Facing = true;//right
                CurrentState.Idle();
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
            int MinX = Mario.Sprite.Texture.Width/4;
            if (MinX > Mario.SpritePosition.X)
                Idle();
            else if(Mario.SpriteVelocity.Y>0)
                Falling();
            else
            {
                Mario.SpriteVelocity = new Vector2(Mario.SpriteVelocity.X, 2);//gravity
            }
        }

        public override void CollisionResponse(ICollision collided)
        {
            if (!(collided is ItemCollision))
            {
                if (Mario.EntityCollision.TopCollision(collided))
                {
                    Mario.SpriteVelocity = new Vector2(Mario.SpriteVelocity.X, 0);
                }else if (Mario.EntityCollision.SideCollision(collided))
                {
                    Idle();
                }
                
            }
        }
    }
    }

