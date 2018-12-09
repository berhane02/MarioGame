using System.Collections.ObjectModel;
using MarioGame.Entities;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Block.BlockStates
{
    public class ExplodingBlockState : BlockState
    {
        private Collection<Sprite> shards;
        public ExplodingBlockState(BlockEntity block) : base(block) { }
        public override void Enter(IBlockState previousState)
        {
            CurrentState = this;
            Sprite one = Block.Factory.BuildSprite(Block.Factory.FindType(this), Block.SpritePosition);
            one.Velocity = new Vector2(2, 3);
            Sprite two = Block.Factory.BuildSprite(Block.Factory.FindType(this), Block.SpritePosition);
            two.Velocity = new Vector2(1, 3);
            Sprite three = Block.Factory.BuildSprite(Block.Factory.FindType(this), Block.SpritePosition);
            three.Velocity = new Vector2(0, 3);
            Sprite four = Block.Factory.BuildSprite(Block.Factory.FindType(this), Block.SpritePosition);
            four.Velocity = new Vector2(-1, 3);
            shards = new Collection<Sprite>
            {
                one,
                two,
                three,
                four
            };
        }

        public override void ExitState()
        {
            foreach (Sprite shard in shards)
            {
                shard.Velocity = Vector2.Zero;
            }

            Block.Dead = true;
        }
        public override void CollisionResponse(ICollision collidedObject) { }//no collision response.

        public override void Update(GameTime game, GraphicsDeviceManager graphics)
        {
            foreach (Sprite shard in shards)
            {
                shard.Update(game, shard.Velocity, graphics);
            }
            if (shards[0].Position.Y>graphics.GraphicsDevice.Viewport.Height)
                CurrentState.ExitState();
           
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite shard in shards)
            {
                shard.Draw(spriteBatch);
            }
        }
    }
}
