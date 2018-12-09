using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.Mario;

namespace MarioGame.Block.BlockStates
{
    public class PipeBlockState:BlockState
    {
        public PipeBlockState(BlockEntity block) : base(block) { }

        //public override void CollisionResponse(ICollision collidedObject)
        //{
        //    if (collidedObject is MarioCollision)
        //    {
        //        Block.Mario = collidedObject.CurrentEntity as MarioEntity;
        //        Block.Reveal(Block.Mario);
        //    }

        //}
    }
}
