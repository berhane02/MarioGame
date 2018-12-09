using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.Levels;
using Microsoft.Xna.Framework;

namespace MarioGame.Mario.MarioActionState
{
    public class Warping:ActionState
    {
        
        public Warping(MarioEntity mario) : base(mario) {}

        public override void Enter(IMarioActionState preState)
        {
            PreviousState = preState.PreviousState;//should be idle
            CurrentState = this;
            Mario.Sprite = Mario.Factory.MarioSprite(Mario.PowerState, PreviousState, Mario.Facing, Mario.SpritePosition);
            Mario.SpriteVelocity = Mario.Pipe.WarpVelocity;
            Mario.Sounds.PlaySound(EventSoundEffects.EventSounds.PipeTravel);
        }

        public override void CollisionResponse(ICollision collidedObject)
        {
            if (collidedObject.CurrentEntity is PipeBlockEntity)
            {
                Mario.SpriteVelocity = (collidedObject.CurrentEntity as PipeBlockEntity).WarpVelocity;
            }
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            if (Rectangle.Intersect(Mario.Pipe.BoundBox, Mario.BoundBox) == Rectangle.Empty)
            {
                Idle();
            }
        }
    }
}
