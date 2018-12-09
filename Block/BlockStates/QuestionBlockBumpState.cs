using System;
using MarioGame.Entities;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.Block.BlockStates
{
    class QuestionBlockBumpState: BlockState
    {
        public QuestionBlockBumpState(BlockEntity block) : base(block) { }

        public override void Enter(IBlockState previousState)
        {
            CurrentState = this;
            Block.SpriteVelocity = new Vector2(0, -2);
        }

        public override void ExitState()
        {
            Block.SpriteVelocity = Vector2.Zero;
        }

        public override void StandardTransition()
        {
            CurrentState.ExitState();
            CurrentState = new QuestionBlockState(Block);
            CurrentState.Enter(this);
        }

        public override void UsedTransition()
        {
            CurrentState.ExitState();
            CurrentState = new UsedBlockState(Block);
            CurrentState.Enter(this);
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            Block.Sprite.Update(gameTime, Block.SpriteVelocity, graphics);
            if (Math.Abs(Block.SpritePosition.Y - Block.InitialPos.Y) > 10)
                Block.SpriteVelocity = new Vector2(0, Block.SpriteVelocity.Y * -1);
            else if (Math.Abs(Block.InitialPos.Y - Block.SpritePosition.Y) == 0)
            {
                Block.Reveal(Block.Mario);
                if (Block.Items != null && Block.Items.Count > 0)
                    StandardTransition();
                else
                    UsedTransition(); 

            }

        }
    }
}
