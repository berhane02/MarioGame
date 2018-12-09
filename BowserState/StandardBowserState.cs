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
    public class StandardBowserState:BowserState
    {
        public StandardBowserState(BowserEntity bowser) : base(bowser) { }
        public override void Enter()
        {
            CurrentState = this;
            Bowser.SpriteVelocity = new Vector2(0, 2);
        }

        public override void Update()
        {
            if (Math.Abs(Bowser.Mario.SpritePosition.X - Bowser.SpritePosition.X) < 300)
            {
                CurrentState.ExitState();
                CurrentState = new RunningBowserState(Bowser);
                CurrentState.Enter();
            }
            else
            {
                Bowser.SpriteVelocity = new Vector2(0, 2);
            }
        }

        public override void CollisionResponse(ICollision collidedCollision)
        {
            if (Bowser.EntityCollision.TopCollision(collidedCollision))
            {
                Bowser.SpriteVelocity = new Vector2(0, 0);
            }
        }
    }
}
