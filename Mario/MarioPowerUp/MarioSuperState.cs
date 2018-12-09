using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.Levels;

namespace MarioGame.Mario.MarioPowerUp
{
    class MarioSuperState : MarioPowerUpState
    {
        public MarioSuperState(MarioEntity mario) : base(mario)
        {
            Mario.Jumping = EventSoundEffects.EventSounds.SuperJump;
        }
        public override void Damage()
        {
            StandardTransition();
        }
        public override void StandardTransition()
        {
            CurrentState = new MarioStandardState(Mario);
            CurrentState.Enter(this);
        }

        public override void FireTransition()
        {
            CurrentState = new MarioFireState(Mario);
            CurrentState.Enter(this);
        }
        public override void InvincibleTransition()
        {
            CurrentState = new MarioInvincibleState(Mario, this);
            CurrentState.Enter(this);
        }
        public override void DeadTransition()
        {
            CurrentState = new MarioDeadState(Mario);
            CurrentState.Enter(this);
            Mario.Live--;
        }
        public override void CollisionResponse(ICollision collidedObject)
        {
            if (collidedObject is BlockCollision)
            {
                //do nothing
            }else if (collidedObject.CurrentEntity is FlowerEntity)
            {
                FireTransition();
            }
            else if (collidedObject.CurrentEntity is StarEntity)
            {
                InvincibleTransition();
            }
            else if (collidedObject.CurrentEntity is PiranhaEntity || collidedObject.CurrentEntity is BowserEntity || collidedObject is EnemyCollision && !Mario.EntityCollision.TopCollision(collidedObject)&&!collidedObject.CurrentEntity.Dead)
            {
                Damage();
            }
        }
    }
}
