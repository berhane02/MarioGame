using MarioGame.Collision;
using MarioGame.Interfaces;
using MarioGame.ItemStateMachine;
using MarioGame.ItemStateMachine.ItemStates;
using MarioGame.Levels;
using MarioGame.Mario;
using Microsoft.Xna.Framework;

namespace MarioGame.Entities
{
    public abstract class ItemEntity : Entity
    {
        public EventSoundEffects.EventSounds Sound;
        public Vector2 InitialPos { get; set; }
        public IItemState State { get; set; }
        protected ItemEntity(Vector2 loc, EventSoundEffects sounds) { }
        public override void Reveal(MarioEntity mario)
        {
            Mario = mario;
            State.RevealTransition();
            Sounds.PlaySound(EventSoundEffects.EventSounds.PowerUp);
        }

        public override void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphics)
        {
            Sprite.Update(gametime,SpriteVelocity,graphics);
            State.Update();
        }

        public override void CollisionResponse(ICollision collided)
        {
            State.CollisionResponse(collided);
        }
    }

    public class AxeEntity : ItemEntity
    {
        public AxeEntity(Vector2 loc, EventSoundEffects sounds) : base(loc, sounds)
        {
            Sounds = sounds;
            InitialPos = loc;
            Sprite = new Axe(loc);
            EntityCollision = new ItemCollision(this);
        }
        public override void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphics)
        {
            Sprite.Update(gametime, SpriteVelocity, graphics);
        }
        public override void CollisionResponse(ICollision collided)
        {
            if (collided is MarioCollision)
            {
                Dead = true;
            }
        }
    }
    public class FlowerEntity : ItemEntity
    {
        public FlowerEntity(Vector2 loc, EventSoundEffects sounds):base(loc, sounds)
        {
            Sounds = sounds;
            Sound = EventSoundEffects.EventSounds.CollectPowerUp;
            InitialPos = loc;
            Sprite = new Flower(loc);
            EntityCollision = new ItemCollision(this);
            State = new StandardItemState(this);
            State.NextState = new StandardItemState(this);
        }
    }
    public class CoinEntity: ItemEntity
    {
        public CoinEntity(Vector2 loc, EventSoundEffects sounds) : base(loc, sounds)
        {
            Sounds = sounds;
            Sound = EventSoundEffects.EventSounds.CollectCoin;
            InitialPos = loc;
            Sprite = new Coin(loc);
            EntityCollision = new ItemCollision(this);
            State = new StandardItemState(this);
            State.NextState = new StandardItemState(this);
        }
    }
    public class RedMushroomEntity: ItemEntity
    {
        public RedMushroomEntity(Vector2 loc, EventSoundEffects sounds) : base(loc, sounds)
        {
            Sounds = sounds;
            Sound = EventSoundEffects.EventSounds.CollectPowerUp;
            InitialPos = loc;
            Sprite = new RedMushroom(loc);
            EntityCollision = new ItemCollision(this);
            State = new StandardItemState(this);
            State.NextState = new MovingItemState(this);
        }
    }

    public class GreenMushroomEntity: ItemEntity
    {
        public GreenMushroomEntity(Vector2 loc, EventSoundEffects sounds) : base(loc, sounds)
        {
            Sounds = sounds;
            Sound = EventSoundEffects.EventSounds.Collect1Up;
            InitialPos = loc;
            Sprite = new GreenMushroom(loc);
            EntityCollision = new ItemCollision(this);
            State = new StandardItemState(this);
            State.NextState = new MovingItemState(this);
        }
    }
    
    public class StarEntity : ItemEntity
    {
        public StarEntity(Vector2 loc, EventSoundEffects sounds) : base(loc, sounds)
        {
            Sounds = sounds;
            Sound = EventSoundEffects.EventSounds.CollectPowerUp;
            InitialPos = loc;
            Sprite = new Star(loc);
            EntityCollision = new ItemCollision(this);
            State = new StandardItemState(this);
            State.NextState = new JumpingState(this);
        }
    }
}