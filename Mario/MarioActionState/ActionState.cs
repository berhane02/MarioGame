using System;
using MarioGame.Collision;
using MarioGame.Interfaces;
using MarioGame.MarioActionState;
using Microsoft.Xna.Framework;

namespace MarioGame.Mario.MarioActionState
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors")]
    public abstract class ActionState : IMarioActionState
    {
        public MarioEntity Mario { get; protected set; }
        protected Vector2 Velocity { get; set; }//this is to save the sprite velocity before a new sprite is created.
        protected IMarioActionState CurrentState
        {
            get { return Mario.ActionState; }
            set { Mario.ActionState = value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        protected IMarioActionState PreviousState;

        IMarioActionState IMarioActionState.PreviousState => PreviousState;


        public ActionState(MarioEntity mario)
        {
            Mario = mario;
        }

        public virtual void CollisionResponse(ICollision collidedObject)
        {
            if (!(collidedObject is ItemCollision)&&Mario.EntityCollision.TopCollision(collidedObject))
                Idle();
            
        }

        public virtual void ExitState(){}

        public virtual void Enter(IMarioActionState preState)
        {
            CurrentState = this;
            PreviousState = preState;
            Mario.Sprite = Mario.Factory.MarioSprite(Mario.PowerState, CurrentState, Mario.Facing, Mario.SpritePosition);
        }
        public virtual void Update(GameTime gameTime, GraphicsDeviceManager graphics) { }

        public virtual void Down(){}

        public virtual void GoLeft() { }

        public virtual void GoRight() { }

        public virtual void Up() { }
        public void Idle()
        {
                CurrentState.ExitState();
                CurrentState = new Idling(Mario);
                CurrentState.Enter(this);
        }

        public void Falling()
        {
            CurrentState.ExitState();
            CurrentState = new Falling(Mario);
            CurrentState.Enter(this);
        }


    }
}
