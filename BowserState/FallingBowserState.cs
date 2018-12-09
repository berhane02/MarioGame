using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Entities;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.BowserState
{
    public class FallingBowserState : BowserState
    {
        public FallingBowserState(BowserEntity bowser) : base(bowser) { }
        public override void Enter()
        {
            CurrentState = this;
            Bowser.SpriteVelocity = new Vector2(Bowser.SpriteVelocity.X, 4);
        }

        public override void CollisionResponse(ICollision collidedCollision)
        {
            if (Bowser.EntityCollision.SideCollision(collidedCollision))
            {
                Bowser.SpriteVelocity = new Vector2(0, Bowser.SpriteVelocity.Y);
            }

            if (Bowser.EntityCollision.TopCollision(collidedCollision))
            {
                CurrentState.ExitState();
                CurrentState = new RunningBowserState(Bowser);
                CurrentState.Enter();
            }
        }
    }
}
