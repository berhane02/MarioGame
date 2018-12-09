using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Levels;
using Microsoft.Xna.Framework;

namespace MarioGame.Block
{
    public class FloorBlockEntity: Entity
    {
        public FloorBlockEntity(Vector2 loc)
        {
            Sprite = new FloorBlock(loc);
            EntityCollision = new BlockCollision(this);
        }
    }
    public class StairBlockEntity : Entity
    {
        public StairBlockEntity(Vector2 loc)
        {
            Sprite = new StairBlock(loc);
            EntityCollision = new BlockCollision(this);
        }
    }

    public static class BlockCreation
    {
        public enum SceneBlock
        {
            FloorBlock, StairBlock
        }

        public enum Blocks
        {
            UsedBlock, QuestionBlock, BrickBlock, HiddenBlock, PipeBlock, BridgeBlock
        }
        public static Entity BuildSceneBlock(SceneBlock type, Vector2 loc)
        {
            switch (type)
            {
                case SceneBlock.FloorBlock:
                    return new FloorBlockEntity(loc);
                case SceneBlock.StairBlock:
                    return new StairBlockEntity(loc);
                default:
                    return null;
            }
        }
        public static BlockEntity BuildBlockEntity(Blocks type, Vector2 loc, EventSoundEffects sounds)
        {
            BlockEntity toReturn = null;
            switch (type)
            {
                case Blocks.UsedBlock:
                    toReturn = new UsedBlockEntity(loc,sounds);
                    break;
                case Blocks.QuestionBlock:
                    toReturn = new QuestionBlockEntity(loc,sounds);
                    break;
                case Blocks.BrickBlock:
                    toReturn = new BrickBlockEntity(loc,sounds);
                    break;
                case Blocks.HiddenBlock:
                    toReturn = new HiddenBlockEntity(loc,sounds);
                    break;
                case Blocks.PipeBlock:
                    toReturn = new PipeBlockEntity(loc, sounds);
                    break;
                case Blocks.BridgeBlock:
                    toReturn = new BridgeBlockEntity(loc, sounds);
                    break;
            }
            
            return toReturn;
        }
    }
}
