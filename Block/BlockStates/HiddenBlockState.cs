using MarioGame.Block.BlockStates;
using MarioGame.Entities;
using MarioGame.Levels;
using Microsoft.Xna.Framework;


namespace MarioGame.Block
{
    public class HiddenBlockState : BlockState
    {

        public HiddenBlockState(BlockEntity block) : base(block) { }
      
        public override void BumpTransition()
        {
            CurrentState.ExitState();
            CurrentState = new BrickBlockState(Block);
            CurrentState.Enter(this);
            Block.Sounds.PlaySound(EventSoundEffects.EventSounds.BrickBump);
        }
    }
}
