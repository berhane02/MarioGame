using MarioGame.Block.BlockStates;
using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Factory;
using MarioGame.Levels;
using Microsoft.Xna.Framework;

namespace MarioGame.Block
{
    class QuestionBlockEntity : BlockEntity
    {
        public QuestionBlockEntity(Vector2 loc, EventSoundEffects sounds) : base(loc, sounds)
        {
            Sounds = sounds;
            Factory = new BlockFactory();
            BState = new QuestionBlockState(this);
            Sprite = Factory.BuildSprite(Factory.FindType(BState), loc);
            BState.Enter(null);
            SpritePosition = loc;
            InitialPos = loc;
            EntityCollision = new BlockCollision(this);
        }
    }
}
