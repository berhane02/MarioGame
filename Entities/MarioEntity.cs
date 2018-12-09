using System;
using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Factory;
using MarioGame.Interfaces;
using MarioGame.Levels;
using MarioGame.Mario.MarioActionState;
using MarioGame.Mario.MarioPowerUp;
using MarioGame.MarioActionState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Mario
{
    public class MarioEntity : Entity
    {
        public IMarioActionState ActionState { get; set; }
        public IMarioPowerUpState PowerState { get; set; }
        public MarioFactory Factory { get; set; }
        public bool Facing { get; set; } //left false; right true
        public bool WonGame { get; set; }
        public int Live { get; set; }
        public int MarioScore { get; set; }
        public int CoinCount { get; set; }
        public EventSoundEffects.EventSounds Jumping { get; set; }
        public bool HasDied { get; set; }
        public PipeBlockEntity Pipe { get; set; }
        public MarioEntity(Vector2 location, EventSoundEffects sounds)
        {
            Sounds = sounds;
            WonGame = false;
            Factory = new MarioFactory();
            PowerState = new MarioStandardState(this);
            ActionState = new Idling(this);
            Facing = true;
            Sprite = Factory.MarioSprite(PowerState, ActionState, Facing, location);
            SpritePosition = location;
            SpriteVelocity = new Vector2(0, 0);
            EntityCollision = new MarioCollision(this);
            Live = 3;
        }

        public override void CollisionResponse(ICollision collidedObject)
        {
            PowerState.CollisionResponse(collidedObject);
            ActionState.CollisionResponse(collidedObject);
        }

        public void Die()
        {
            PowerState.DeadTransition();
        }
        public void TakeDamage()
        {
            PowerState.Damage();
        }

        public void Standard()
        {
            PowerState.StandardTransition();
        }
        
        public void Super()
        {
            PowerState.SuperTransition();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void Fire()
        {
            PowerState.FireTransition();
        }
        public void Invincible()
        {
            PowerState.InvincibleTransition();
        }
        public void Up()
        {
            if(!HasDied)
                ActionState.Up();
        }
        public void Down()
        {
            if (!HasDied)
                ActionState.Down();
        }

        public void Left()
        {
            //Facing = false;
            if (!HasDied)
                ActionState.GoLeft();
        }
        public void Right()
        {
            //Facing = true;
            if (!HasDied)
                ActionState.GoRight();
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            Sprite.Update(gameTime, SpriteVelocity, graphics);
            ActionState.Update(gameTime,graphics);
            PowerState.Update(gameTime);
            if (SpritePosition.Y > 510)
            {
                PowerState.DeadTransition();
            }
                
        }



    }
}
