using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.Block;
using Microsoft.Xna.Framework;

namespace MarioGame.Collision
{
    public class BlockCollision : Collision
    {
        public BlockCollision(Entity block) : base(block) { }

        public override void Response(ICollision collided)
        {
            CurrentEntity.CollisionResponse(collided);
        }
        
    }
}
