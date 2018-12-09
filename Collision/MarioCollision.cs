using MarioGame.Block;
using MarioGame.Interfaces;
using MarioGame.Mario;
using MarioGame.Mario.MarioPowerUp;
using MarioGame.Entities;

namespace MarioGame.Collision
{
    class MarioCollision : Collision
    {
        private readonly MarioEntity Mario;

        public MarioCollision(MarioEntity mario) : base(mario)
        {
            CurrentEntity = mario;
            Mario = mario;
        }

        public override void Response(ICollision collided)//can collide with blocks and Goomba or Items
        {
            if (!BottomCollision(collided) && collided.CurrentEntity is HiddenBlockEntity &&
                (collided.CurrentEntity as HiddenBlockEntity).BState is HiddenBlockState)
            {
                //do nothing
            }
            else
            {
                CurrentEntity.CollisionResponse(collided);
            }
            
        }

        public override bool MarioState()
        {
            if (Mario.PowerState is MarioFireState || Mario.PowerState is MarioSuperState)
            {
                return true;
            }
            return false;
        }
    }
}
