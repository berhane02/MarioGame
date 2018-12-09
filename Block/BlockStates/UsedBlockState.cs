using MarioGame.Entities;
using MarioGame.Interfaces;

namespace MarioGame.Block.BlockStates
{
    public class UsedBlockState : BlockState
    {
        public UsedBlockState(BlockEntity block) : base(block){}//used state shouldn't do anything.

    }
}
