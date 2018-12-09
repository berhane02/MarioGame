using MarioGame.Entities;
using MarioGame.Interfaces;


namespace MarioGame.Collision
{
    public class PoleCollison : Collision
    {
        public PoleCollison(Entity Pole) : base(Pole) { }

        public override void Response(ICollision collided)
        {
            CurrentEntity.CollisionResponse(collided);
        }

    }
}
