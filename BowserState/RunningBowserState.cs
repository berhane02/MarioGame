using MarioGame.Entities;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.BowserState
{
    public class RunningBowserState:BowserState
    {
        public RunningBowserState(BowserEntity bowser) : base(bowser) { }
        public override void Enter()
        {
            CurrentState = this;
            if (Bowser.Mario.SpritePosition.X > Bowser.SpritePosition.X)
            {
                Bowser.SpriteVelocity = new Vector2(2, 2);
            }
            else
            {
                Bowser.SpriteVelocity = new Vector2(-2, 2);
            }
        }

        public override void Update()
        {
            if (Bowser.SpriteVelocity.Y>0)//bowser is moving down
            {
                CurrentState.ExitState();
                CurrentState = new FallingBowserState(Bowser);
                CurrentState.Enter();
            }
            else if (Bowser.Mario.BoundBox.X > Bowser.SpritePosition.X && Bowser.SpriteVelocity.X <= 0)//mario is on the right
            {
                Bowser.SpriteVelocity = new Vector2(2, 2);
            }
            else if(Bowser.Mario.BoundBox.X < Bowser.SpritePosition.X && Bowser.SpriteVelocity.X >= 0)//mario on left
            {
                Bowser.SpriteVelocity = new Vector2(-2, 2);
            }
            if (Bowser.Mario.SpritePosition.Y < Bowser.SpritePosition.Y)//mario jumped so bowser will jump too
            {
                CurrentState.ExitState();
                CurrentState = new JumpingBowserState(Bowser);
                CurrentState.Enter();
            }
            else
            {
                Bowser.SpriteVelocity = new Vector2(Bowser.SpriteVelocity.X, 1);
            }
        }

        public override void CollisionResponse(ICollision collidedCollision)
        {
            if (Bowser.EntityCollision.TopCollision(collidedCollision))
            {
                Bowser.SpriteVelocity = new Vector2(Bowser.SpriteVelocity.X, 0);
            }

            if (Bowser.EntityCollision.SideCollision(collidedCollision))
            {
                Bowser.SpriteVelocity = new Vector2(0, Bowser.SpriteVelocity.Y);
            }
        }
    }
}
