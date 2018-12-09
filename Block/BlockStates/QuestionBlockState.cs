using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.Levels;
using MarioGame.Mario;

namespace MarioGame.Block.BlockStates
{
    public class QuestionBlockState : BlockState
    {
        public QuestionBlockState(BlockEntity block) : base(block) { }
        public override void Enter(IBlockState previousState)
        {
            CurrentState = this;
            Block.Sprite = Block.Factory.BuildSprite(Block.Factory.FindType(this), Block.SpritePosition);
        }

        public override void CollisionResponse(ICollision collidedObject)
        {
            if (collidedObject is MarioCollision && collidedObject.BottomCollision(Block.EntityCollision))
            {
                Block.Mario = collidedObject.CurrentEntity as MarioEntity;
                BumpTransition();
            }
        }

        public override void BumpTransition()
        {
            CurrentState.ExitState();
            CurrentState = new QuestionBlockBumpState(Block);
            CurrentState.Enter(this);
            Block.Sounds.PlaySound(EventSoundEffects.EventSounds.BrickBump);
        }

    }
}
