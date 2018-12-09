using MarioGame.Collision;
using MarioGame.Interfaces;
using MarioGame.Mario;
using MarioGame.Sprites;
using Microsoft.Xna.Framework;

namespace MarioGame.Entities
{
    public class BooEntity : EnemyEntity
    {
       
        //public MarioEntity Mario { get; set; }
        private MarioEntity mario;

        public BooEntity(MarioEntity mario, Vector2 position):base(position)
        {
           
            Sprite = new BooSprite(mario, position);
            this.mario = mario;
            EntityCollision = new EnemyCollision(this);
        }

        public override void CollisionResponse(ICollision collided)
        {
            if (collided is MarioCollision)
            {
                mario.TakeDamage();
            }

        }



    }
}
