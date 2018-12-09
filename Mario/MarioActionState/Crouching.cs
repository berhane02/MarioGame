using System.Runtime.InteropServices;
using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.Mario;
using MarioGame.Mario.MarioActionState;
using MarioGame.Mario.MarioPowerUp;
using Microsoft.Xna.Framework;

namespace MarioGame.MarioActionState
{
    public class Crouching : ActionState
    { 
        public Crouching(MarioEntity mario) : base(mario) { }

        public override void Enter(IMarioActionState preState)
        {
            CurrentState = this;
            PreviousState = preState;
            if(!(Mario.PowerState is MarioStandardState))
                 Mario.Sprite = Mario.Factory.MarioSprite(Mario.PowerState, CurrentState, Mario.Facing, Mario.SpritePosition);
        }
       
        public override void Up()
        {
            Idle();
        }
        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            int MaxY = Level.ScreenHeight;
            if (MaxY < Mario.Sprite.Position.Y)
                Idle();
           Mario.SpriteVelocity=new Vector2(Mario.SpriteVelocity.X,2);
        }

        public override void CollisionResponse(ICollision collided)
        {
            if (!(collided is ItemCollision))
            {
                if(Mario.EntityCollision.TopCollision(collided)&& collided.CurrentEntity is PipeBlockEntity&&
                   (collided.CurrentEntity as PipeBlockEntity).Warpable)
                {
                    Mario.Pipe = collided.CurrentEntity as PipeBlockEntity;
                    CurrentState.ExitState();
                    CurrentState = new Warping(Mario);
                    CurrentState.Enter(this);
                }else if(Mario.EntityCollision.TopCollision(collided))
                    Mario.SpriteVelocity = new Vector2(Mario.SpriteVelocity.X, 0);
            }
        }
    }
}
