using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.Mario.MarioPowerUp
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors")]
    public abstract class MarioPowerUpState : IMarioPowerUpState
    {
        public MarioEntity Mario { get; protected set; }
        protected IMarioPowerUpState CurrentState
        {
            get { return Mario.PowerState;}
            set { Mario.PowerState = value; }
        }
        protected MarioPowerUpState(MarioEntity mario)
        {
            Mario = mario;
        }

        public virtual void CollisionResponse(ICollision collidedObject) { }

        public void Enter(IMarioPowerUpState previousState)
        {
            CurrentState = this;
            Vector2 speed = Mario.SpriteVelocity;
            Mario.Sprite = Mario.Factory.MarioSprite(CurrentState, Mario.ActionState, Mario.Facing, Mario.SpritePosition);
            Mario.SpriteVelocity = speed;
        }

        public virtual void Damage(){}

        public virtual void DeadTransition() { }

        public virtual void FireTransition() { }

        public virtual void StandardTransition() { }

        public virtual void SuperTransition() { }

        public virtual void Update(GameTime gameTime) { }
        public virtual void InvincibleTransition() { }
        public virtual void ReturnToPreviousState() { }
    }
}
