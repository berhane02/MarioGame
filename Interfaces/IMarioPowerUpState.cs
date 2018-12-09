using MarioGame.Mario.MarioPowerUp;
using Microsoft.Xna.Framework;

namespace MarioGame.Interfaces
{
    public interface IMarioPowerUpState
    {
        void CollisionResponse(ICollision collidedObject);
        void Enter(IMarioPowerUpState previousState);
        void Damage();
        void StandardTransition();
        void SuperTransition();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        void FireTransition();
        void DeadTransition();
        void InvincibleTransition();
        void Update(GameTime gameTime);
        void ReturnToPreviousState();
    }
   
}
