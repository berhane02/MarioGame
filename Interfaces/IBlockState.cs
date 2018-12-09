using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Interfaces
{
    public interface IBlockState
    {
        void CollisionResponse(ICollision collidedCollision);
        void Enter(IBlockState previousState);
        void ExitState();
        void BumpTransition();
        void StandardTransition();
        void UsedTransition();
        void ExplodeTransition();
        void Update(GameTime gameTime, GraphicsDeviceManager graphics);
        void Draw(SpriteBatch spriteBatch);
    }
}
