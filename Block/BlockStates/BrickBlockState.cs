using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.ItemStateMachine.ItemStates;
using MarioGame.Levels;
using MarioGame.Mario;

namespace MarioGame.Block.BlockStates
{
    public class BrickBlockState : BlockState
    {
        public BrickBlockState(BlockEntity block) : base(block) { }
        public override void BumpTransition()
        {
            CurrentState.ExitState();
            CurrentState = new BrickBlockBumpState(Block);
            CurrentState.Enter(this);
            Block.Sounds.PlaySound(EventSoundEffects.EventSounds.BrickBump);
        }

        public override void ExplodeTransition()
        {
            CurrentState.ExitState();
            CurrentState = new ExplodingBlockState(Block);
            CurrentState.Enter(this);
            Block.Sounds.PlaySound(EventSoundEffects.EventSounds.BrickBreak);
        }

        public override void CollisionResponse(ICollision collidedObject)
        {
            if (collidedObject.MarioState()&&collidedObject.BottomCollision(Block.EntityCollision))
            {
                ExplodeTransition();
            }
            else if (collidedObject is MarioCollision && collidedObject.BottomCollision(Block.EntityCollision))
            {
                BumpTransition();
                Block.Mario = collidedObject.CurrentEntity as MarioEntity;
            }
        }

    }
}
