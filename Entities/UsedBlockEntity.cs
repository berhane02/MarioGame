using MarioGame.Block.BlockStates;
using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Factory;
using MarioGame.Levels;
using Microsoft.Xna.Framework;

namespace MarioGame.Block
{
    class UsedBlockEntity : BlockEntity
    {
        public UsedBlockEntity(Vector2 loc, EventSoundEffects sounds) : base(loc, sounds)//there are no sounds for used blocks
        {
            Factory = new BlockFactory();
            BState = new UsedBlockState(this);
            Sprite = Factory.BuildSprite(Factory.FindType(BState), loc);
            BState.Enter(null);
            SpritePosition = loc;
            InitialPos = loc;
            EntityCollision = new BlockCollision(this);
        }
    }
}
