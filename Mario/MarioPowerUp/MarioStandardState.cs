using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.Levels;
using MarioGame.Mario.MarioActionState;
using Microsoft.Xna.Framework;

namespace MarioGame.Mario.MarioPowerUp
{
    class MarioStandardState : MarioPowerUpState
    {
        private const float Delay = 600f;//stay for 5 seconds
        private int _elapsed;
        public MarioStandardState(MarioEntity mario) : base(mario)
        {
            _elapsed = 0;
            Mario.Jumping = EventSoundEffects.EventSounds.Jump;
        }

        public override void Update(GameTime gameTime)
        {
            _elapsed += gameTime.ElapsedGameTime.Milliseconds;
        }

        public override void Damage()
        {
            if (_elapsed >= Delay)
            {
                DeadTransition();
                _elapsed = 0;
            }
            
        }
        public override void DeadTransition()
        {
            CurrentState = new MarioDeadState(Mario);
            CurrentState.Enter(this);
            Mario.Live--;
        }

        public override void FireTransition()
        {
            CurrentState = new MarioFireState(Mario);
            CurrentState.Enter(this);
        }

        public override void SuperTransition()
        {
            CurrentState = new MarioSuperState(Mario);
            CurrentState.Enter(this);
        }
        public override void InvincibleTransition()
        {
            CurrentState = new MarioInvincibleState(Mario, this);
            CurrentState.Enter(this);
        }

        public override void CollisionResponse(ICollision collidedObject)
        {
            if (collidedObject is BlockCollision)
            {
                //do nothing
            }else if (collidedObject.CurrentEntity is RedMushroomEntity || collidedObject.CurrentEntity is FlowerEntity)
            {
                SuperTransition();
            }
            else if (collidedObject.CurrentEntity is StarEntity)
            {
                InvincibleTransition();
            }
            else if (collidedObject.CurrentEntity is PiranhaEntity || collidedObject.CurrentEntity is BowserEntity ||!Mario.EntityCollision.TopCollision(collidedObject) && !collidedObject.CurrentEntity.Dead && collidedObject is EnemyCollision)
            {
                    Damage();
            }
        }
    }
}
