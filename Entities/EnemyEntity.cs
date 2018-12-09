using System;
using System.Security.Permissions;
using MarioGame.BowserState;
using Microsoft.Xna.Framework;
using MarioGame.Collision;
using MarioGame.Interfaces;
using MarioGame.Levels;
using MarioGame.Mario;
using MarioGame.Mario.MarioPowerUp;
using MarioGame.PiranhaState;
using MarioGame.Sprites;

namespace MarioGame.Entities
{
    public abstract class EnemyEntity : Entity
    {

        public bool IsLeft { get; set; }
        private Rectangle camera;
        private bool started;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "loc")]
        protected EnemyEntity(Vector2 loc)
        {
            started = false;
        }

        public override void Update(GameTime gametime, Vector2 Velocity, GraphicsDeviceManager graphic)
        {
            camera = Camera.GetCamRec();
            Sprite.Update(gametime, SpriteVelocity, graphic);
            if (IsInCamera())
                SpriteVelocity = new Vector2(SpriteVelocity.X, 1);

        }

        protected void StartDirection()
        {
            if (IsLeft)
            {
                SpriteVelocity = new Vector2(-1, 1);

            }
            else
            {
                Sprite.Velocity = new Vector2(1, 1);
            }

        }
        
        private bool IsInCamera()
        {
            if (camera.Contains(SpritePosition))
            {
                if (!started)
                {
                    StartDirection();
                    started = true;
                }
                return true;
            }
            return false;
        }

        public override void CollisionResponse(ICollision collided)
        {
           
            if (collided is MarioCollision&&collided.TopCollision(EntityCollision))
            {
                Dead = true;
                (collided.CurrentEntity as MarioEntity).Sounds.PlaySound(EventSoundEffects.EventSounds.Stomp);
            }
            else if (collided is EnemyCollision)
            {
                if (EntityCollision.TopCollision(collided))
                {
                    SpriteVelocity = new Vector2(SpriteVelocity.X, 0);
                }
                else
                {
                    SpriteVelocity = new Vector2(SpriteVelocity.X * -1, SpriteVelocity.Y);
                    collided.CurrentEntity.SpriteVelocity = new Vector2(SpriteVelocity.X * -1, SpriteVelocity.Y);
                }
                
            }
            else if (collided is BlockCollision)
            {
                if (EntityCollision.SideCollision(collided))
                {
                    SpriteVelocity = new Vector2(SpriteVelocity.X * -1, SpriteVelocity.Y);
                }else if (EntityCollision.TopCollision(collided))
                {
                    SpriteVelocity = new Vector2(SpriteVelocity.X, 0);
                }
                

            }
        }
    }
    public class GoombaEntity : EnemyEntity
    {
        public GoombaEntity(Vector2 loc) : base(loc)
        {
            Sprite = new WalkingGoomba(loc);
            EntityCollision = new EnemyCollision(this);

            IsLeft = true;
            StartDirection();
        }
    }
    public class GreenKoopaEntity : EnemyEntity
    {
        public GreenKoopaEntity(Vector2 loc) : base(loc)
        {
            IsLeft = false;
            Sprite = new GreenKoopa(loc);
            EntityCollision = new EnemyCollision(this);
            StartDirection();

        }

    }

   

    public class RedKoopaEntity : EnemyEntity
    {

        public RedKoopaEntity(Vector2 loc) : base(loc)
        {
            IsLeft = true;
            Sprite = new RedKoopa(loc);
           
            EntityCollision = new EnemyCollision(this);
            StartDirection();

        }

    }
    public class PiranhaEntity : EnemyEntity
    {
        public Vector2 _initalPos;
        public IPiranhaState State { get; set; }
        public PiranhaEntity(Vector2 loc) : base(loc)
        {
            _initalPos = loc;
            EntityCollision = new BlockCollision(this);//not collidable
            Sprite = new Piranha(loc);
            State = new HiddenState(this);
        }
        public override void Reveal(MarioEntity mario)//for piranha plant to reveal when mario is close
        {
            State.RevealTransition();
        }

        public override void Update(GameTime gametime, Vector2 Velocity, GraphicsDeviceManager graphic)
        {
            Sprite.Update(gametime, SpriteVelocity, graphic);
            State.Update(gametime);
        }

        public override void CollisionResponse(ICollision collided)
        {
           //do nothing
        }
    }

    public class BowserEntity : EnemyEntity
    {
        public IBowserState State { get; set; }

        public BowserEntity(Vector2 loc, MarioEntity mario) : base(loc)
        {
            Mario = mario;
            EntityCollision = new EnemyCollision(this);
            Sprite = new Bowser(loc);
            State = new StandardBowserState(this);
        }
        public override void Update(GameTime gametime, Vector2 Velocity, GraphicsDeviceManager graphic)
        {
            Sprite.Update(gametime, SpriteVelocity, graphic);
            State.Update();
            if (BoundBox.Y > 510)
            {
                Dead = true;
            }
        }
        public override void CollisionResponse(ICollision collided)
        {
            State.CollisionResponse(collided);
        }
    }
}
