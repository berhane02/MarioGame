using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MarioGame.Block;
using MarioGame.Block.BlockStates;
using MarioGame.Collision;
using MarioGame.Factory;
using MarioGame.Levels;
using MarioGame.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Entities
{
    public class BridgeBlockEntity : BlockEntity
    {
        private AxeEntity _axe;
        public BridgeBlockEntity(Vector2 loc, EventSoundEffects eventSound) : base(loc, eventSound)
        {
            Items = new Collection<Entity>
            {
                this//first is this one
            };
            Sounds = eventSound;
            Factory = new BlockFactory();
            BState = new BridgeBlockState(this);
            Sprite = Factory.BuildSprite(Factory.FindType(BState), loc);
            BState.Enter(null);
            SpritePosition = loc;
            InitialPos = loc;
            EntityCollision = new BlockCollision(this);
        }

        public override void Update(GameTime gametime, Vector2 Velocity, GraphicsDeviceManager graphics)
        {
            if (_axe.Dead && Items.Count > 1)//axe is collected
            {
                Entity remove = Items[1];
                remove.Dead = true;
                Items.RemoveAt(1);//remove right most
            }
            foreach (Entity part in Items)
            {
                part.Sprite.Update(gametime,SpriteVelocity, graphics);
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Entity part in Items)
            {
                part.Sprite.Draw(spriteBatch);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public override void AddItem(Entity item, List<Tile> tiles, List<Entity> moving, MarioEntity mario)
        {
            if (MovingEntities == null)
            {
                Mario = mario;
                MovingEntities = moving;
                CollideList = tiles;
            }
            Tile one = new Tile {Entity = item};
            CollideList.Add(one);
            Items.Add(item);
        }

        public void AxeLink(AxeEntity axe)//link to Axe
        {
            _axe = axe;
        }


    }
}
