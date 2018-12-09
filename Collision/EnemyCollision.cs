using MarioGame.Entities;
using Microsoft.Xna.Framework;
using MarioGame.Mario;
using MarioGame.Mario.MarioPowerUp;
using MarioGame.MarioActionState;
using MarioGame.Interfaces;
using MarioGame.Levels;

namespace MarioGame.Collision
{

    public class EnemyCollision : Collision
    {
        public EnemyCollision(Entity entity) : base(entity) { }
        public override void Response(ICollision collided)
        {
            if (collided is MarioCollision &&
                (collided.CurrentEntity as MarioEntity).PowerState is MarioInvincibleState)
            {
                CurrentEntity.Dead = true;
                (collided.CurrentEntity as MarioEntity).Sounds.PlaySound(EventSoundEffects.EventSounds.Stomp);
            }
            else
            {
                CurrentEntity.CollisionResponse(collided);
            }
        }

    }
}
