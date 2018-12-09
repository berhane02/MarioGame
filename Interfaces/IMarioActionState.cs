using Microsoft.Xna.Framework;

namespace MarioGame.Interfaces
{
    public interface IMarioActionState
    {
        IMarioActionState PreviousState { get; }
        void CollisionResponse(ICollision collidedObject);
        void ExitState();
        void Enter(IMarioActionState preState);
        void GoLeft();
        void GoRight();
        void Up();
        void Down();
        void Idle();
        void Falling();
        void Update(GameTime gameTime, GraphicsDeviceManager graphics);
        
    }
}
