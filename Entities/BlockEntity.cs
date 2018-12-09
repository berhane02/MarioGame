using System.Collections.Generic;
using System.Collections.ObjectModel;
using MarioGame.Interfaces;
using MarioGame.Levels;
using MarioGame.Mario;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Entities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors")]
    public abstract class BlockEntity:Entity
    {
        public IBlockState BState { get; set; }
        public Vector2 InitialPos { get; set; }
        public IBlockFactory Factory { get; set; }
        public Collection<Entity> Items { get; set; }//hidden items in blocks
        protected bool Revealable;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId =
            "loc")]
        protected BlockEntity(Vector2 loc, EventSoundEffects eventSound){Revealable = false;
            Sounds = eventSound;
        }

        public override void Update(GameTime gametime, Vector2 Velocity, GraphicsDeviceManager graphics)
        {
            BState.Update(gametime, graphics);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BState.Draw(spriteBatch);
        }

        public void BumpTransition()
        {
            BState.BumpTransition();
        }
        public override void AddItem(Entity item, List<Tile> tiles,List<Entity> moving, MarioEntity mario)
        {
            Revealable = true;
            Mario = mario;
            MovingEntities = moving;
            CollideList = tiles;
            if(Items==null)
                Items = new Collection<Entity>();
            Items.Add(item);
        }

        public override void Reveal(MarioEntity mario)
        {
            if (Items != null && Items.Count > 0)//reveal item
            {
                Entity item = Items[0];
                Items.RemoveAt(0);
                Tile tile = new Tile
                {
                    Entity = item
                };
                CollideList.Add(tile);
                MovingEntities.Add(tile.Entity);
                //foreach (Cell cell in Cells)
                //{
                //    cell.AddEntity(item);
                //}
                //item.Cells = Cells;
                item.Reveal(mario);
            }
        }

        public override void CollisionResponse(ICollision collision)
        {
            BState.CollisionResponse(collision);
        }
    }
}
