using System.Xml;
using MarioGame.Block.BlockStates;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using MarioGame.Block;


namespace MarioGame.Factory
{
    public class BlockFactory : IBlockFactory
    {

        public enum BlockType
        {
            used, question, brick, hidden, exploded, pipe, bridge
        }
        public BlockType FindType(IBlockState bState)
        {
            BlockType type = BlockType.used;
            if (bState is BrickBlockState || bState is BrickBlockBumpState)
            {
                type = BlockType.brick;
            }else if (bState is ExplodingBlockState)
            {
                type = BlockType.exploded;
            }else if (bState is HiddenBlockState)
            {
                type = BlockType.hidden;
            }else if (bState is QuestionBlockState || bState is QuestionBlockBumpState)
            {
                type = BlockType.question;
            }else if (bState is PipeBlockState)
            {
                type = BlockType.pipe;
            }else if (bState is BridgeBlockState)
            {
                type = BlockType.bridge;
            }

            return type;
        }

        public Sprite BuildSprite(BlockType sprite, Vector2 loc)
        {
            Sprite toReturn = null;
            switch (sprite)
            {
                case BlockType.exploded:
                    {
                        toReturn = new ExplodingBlock(loc);
                        break;
                    }
                case BlockType.question:
                    {
                        toReturn = new QuestionBlock(loc);
                        break;
                    }
                case BlockType.used:
                    {
                        toReturn = new UsedBlock(loc);
                        break;
                    }
                case BlockType.brick:
                    {                        
                        toReturn = new BrickBlock(loc);
                        break;
                    }
                case BlockType.hidden:
                    {
                        toReturn = new HiddenBlock(loc);
                        break;
                    }
                case BlockType.pipe:
                    {
                        toReturn = new PipeBlock(loc);
                        break;
                    }
                case BlockType.bridge:
                    {
                        toReturn = new BridgeBlock(loc);
                        break;
                    }
            }
            return toReturn;
        }

    }
}
