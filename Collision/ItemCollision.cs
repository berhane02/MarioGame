using MarioGame.Block;
using MarioGame.Entities;
using MarioGame.Interfaces;

namespace MarioGame.Collision
{
    class ItemCollision : Collision
    {
        public ItemCollision(Entity entity) : base(entity) { }

        public override void Response(ICollision collided)
        {
            if (collided.CurrentEntity is HiddenBlockEntity &&
                (collided.CurrentEntity as HiddenBlockEntity).BState is HiddenBlockState) { }//do nothing
            else{ CurrentEntity.CollisionResponse(collided); }
            
        }
        
    }
   
}
