using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Factory;
using static MarioGame.Factory.BlockFactory;

namespace MarioGame.Interfaces
{
    public interface IBlockFactory
    {
        BlockFactory.BlockType FindType(IBlockState bState);
        Sprite BuildSprite(BlockFactory.BlockType block, Vector2 loc);
    }
}
