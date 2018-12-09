using System;
using MarioGame.Block.BlockStates;
using MarioGame.Collision;
using MarioGame.Factory;
using MarioGame.Interfaces;
using MarioGame.Levels;
using MarioGame.Mario;
using MarioGame.Mario.MarioActionState;
using Microsoft.Xna.Framework;

namespace MarioGame.Entities
{
    public class PipeBlockEntity: BlockEntity
    {
        public Vector2 WarpVelocity { get; set; }
        public bool Warpable { get; set; }
        private PipeBlockEntity _exit;
        public PipeBlockEntity(Vector2 loc, EventSoundEffects eventSound) : base(loc, eventSound)
        {
            Sounds = eventSound;
            Factory = new BlockFactory();
            BState = new PipeBlockState(this);
            Sprite = Factory.BuildSprite(Factory.FindType(BState), loc);
            BState.Enter(null);
            SpritePosition = loc;
            InitialPos = loc;
            EntityCollision = new BlockCollision(this);
            Warpable = false;
            WarpVelocity = new Vector2(0, -1);//normally an outpipe
        }

        public void SetWarpDestination(PipeBlockEntity destination, MarioEntity mario)
        {
            Mario = mario;
            _exit = destination;
            WarpVelocity = new Vector2(0, 1);//into pipe
            Warpable = true;
        }
        public override void Reveal(MarioEntity mario)//continuous reveal is possible
        {
            if (Items != null&&!Items[0].Dead)//reveal piranha
            {
                Entity item = Items[0];                
                if (!MovingEntities.Contains(item))
                {
                    Tile tile = new Tile
                    {
                        Entity = item
                    };
                    //foreach (Cell cell in Cells)
                    //{
                    //    cell.AddEntity(item);
                    //}
                    //item.Cells = Cells;
                    CollideList.Add(tile);
                    MovingEntities.Add(tile.Entity);
                }
                item.Reveal(mario);
            }
        }

        public override void Update(GameTime gametime, Vector2 Velocity, GraphicsDeviceManager graphics)
        {
            Sprite.Update(gametime, Velocity, graphics);
            if (Revealable&&Math.Max(Mario.BoundBox.Center.X,Sprite.BoundBox.Center.X)-Math.Min(Mario.BoundBox.Center.X, Sprite.BoundBox.Center.X) > Sprite.Width+Mario.Sprite.Width*2)
            {
                Reveal(Mario);//reveal piranha
            }
        }

        public override void CollisionResponse(ICollision collision)
        {
            if (collision is MarioCollision && (collision.CurrentEntity as MarioEntity).ActionState is Warping)
            {
                if (collision.FutureBox.Top < BoundBox.Top&&Warpable)
                {
                    Mario.SpritePosition = _exit.SpritePosition+new Vector2(Mario.Sprite.Width+3,Mario.Sprite.Height);//warp
                    Mario.Pipe = _exit;
                }
            }
        }
    }
}
