using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace MarioGame.Mario.MarioPowerUp
{
    public class MarioInvincibleState : MarioPowerUpState
    {
        private readonly MarioPowerUpState state;
        private bool starFlag = true;
        private int timer;

        public MarioInvincibleState(MarioEntity mario, MarioPowerUpState previous) : base(mario)
        {
            Mario.Jumping = EventSoundEffects.EventSounds.SuperJump;
            state = previous;

        }
        public override void StandardTransition()
        {
            CurrentState = new MarioStandardState(Mario);
            CurrentState.Enter(this);
        }

        public override void SuperTransition()
        {
            CurrentState = new MarioSuperState(Mario);
            CurrentState.Enter(this);
        }
        public override void FireTransition()
        {
            CurrentState = new MarioFireState(Mario);
            CurrentState.Enter(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (starFlag)
            {
                timer = 10 + (int)gameTime.TotalGameTime.TotalSeconds;
                starFlag = false;
            }
            else
            {
                if (timer == (int)gameTime.TotalGameTime.TotalSeconds)
                {
                    starFlag = true;
                    ReturnToPreviousState();
                }
            }        
        }
        public override void DeadTransition()
        {
            CurrentState = new MarioDeadState(Mario);
            CurrentState.Enter(this);
            Mario.Live--;
        }
        public override void CollisionResponse(ICollision collidedObject)
        {
            if (collidedObject is EnemyCollision)
            {
                EnemyEntity enemy = collidedObject.CurrentEntity as EnemyEntity;
                enemy.Dead = true;
            }
        }

        public override void ReturnToPreviousState()

        {
            if (state is MarioSuperState)
            {
                SuperTransition();
            }
            else if (state is MarioFireState)
            {
                FireTransition();
            }
            else
            {
                StandardTransition();
            }
        }

    }
}
