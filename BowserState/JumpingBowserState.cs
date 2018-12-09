using MarioGame.Entities;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.BowserState
{
    public class JumpingBowserState: BowserState
    {
        private int JumpingHeight = 0;
        private static readonly int MaxJumpHeight = 200;
        private static readonly int HeightInc = 5;

        public JumpingBowserState(BowserEntity bowser) : base(bowser) { }

        public void Falling()
        {
            CurrentState.ExitState();
            CurrentState = new FallingBowserState(Bowser);
            CurrentState.Enter();
        }
        public override void Enter()
        {
            CurrentState = this;
            Bowser.SpriteVelocity = new Vector2(Bowser.SpriteVelocity.X, -4);
        }
       
        public override void Update()
        {
            JumpingHeight += HeightInc;
            if (JumpingHeight > MaxJumpHeight)
            {
                JumpingHeight = 0;
                Falling();
            }
        }

        public override void CollisionResponse(ICollision collidedCollision)
        {
            if (Bowser.EntityCollision.SideCollision(collidedCollision))//hitting side of something
            {
                Bowser.SpriteVelocity = new Vector2(0, Bowser.SpriteVelocity.Y);
                Falling();
            }else if (Bowser.EntityCollision.BottomCollision(collidedCollision))//hitting top of something
            {
                Falling();
            }
        }
    }
}
