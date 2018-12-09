using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Block.BlockStates
{
    public abstract class BlockState : IBlockState
    {
        public BlockEntity Block { get; protected set; }
        protected IBlockState CurrentState
        {
            get { return Block.BState; }
            set { Block.BState = value; }
        }

        protected BlockState(BlockEntity block)
        {
            Block = block;
        }

        public virtual void CollisionResponse(ICollision collidedObject)
        {
            if (collidedObject is MarioCollision && collidedObject.BottomCollision(Block.EntityCollision))
            {
                BumpTransition();
            }
        }

        public virtual void Enter(IBlockState previousState)
        {
            CurrentState = this;
            Block.Sprite = Block.Factory.BuildSprite(Block.Factory.FindType(this), Block.SpritePosition);
        }

        public virtual void ExitState() { }

        public virtual void BumpTransition() { }

        public virtual void StandardTransition() { }

        public virtual void UsedTransition()
        {
            CurrentState.ExitState();
            CurrentState = new UsedBlockState(Block);
            CurrentState.Enter(this);
        }
       
        public virtual void ExplodeTransition() { }

        public virtual void Update(GameTime gameTime,GraphicsDeviceManager graphics) {Block.Sprite.Update(gameTime,Block.SpriteVelocity,graphics); }
        public virtual void Draw(SpriteBatch spriteBatch) { Block.Sprite.Draw(spriteBatch);}
    }
}
