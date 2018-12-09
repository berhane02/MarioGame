using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.Levels;
using MarioGame.Mario;
using Microsoft.Xna.Framework;

namespace MarioGame.ItemStateMachine.ItemStates
{
    class JumpingState : ItemState
    {
        public JumpingState(ItemEntity entity) : base(entity) { }
        public override void Enter(IItemState state)
        {

            if (Item.Mario.SpritePosition.X > Item.SpritePosition.X)
            {
                Item.SpriteVelocity = new Vector2(-1, 1);
            }
            else
            {
                Item.SpriteVelocity = new Vector2(1, 1);
            }

        }

        public override void ExitState()
        {
            Item.SpriteVelocity = Vector2.Zero;
        }

        public override void Update()
        {
            Item.SpriteVelocity = new Vector2(Item.SpriteVelocity.X, 1);
        }


        public override void CollisionResponse(ICollision collidedObject)
        {
            if (collidedObject is BlockCollision)
            {
                Rectangle itemBox = Item.BoundBox;
                Rectangle brickBox = collidedObject.CurrentSprite.BoundBox;
                Rectangle intersection = Rectangle.Intersect(itemBox, brickBox);
                if (itemBox.Right > brickBox.Left && intersection.Height > intersection.Width
                    || itemBox.Left > brickBox.Right && intersection.Height > intersection.Width)
                {
                    Item.SpriteVelocity = new Vector2(Item.SpriteVelocity.X * -1, Item.SpriteVelocity.Y);
                }
                if (itemBox.Bottom > brickBox.Top && intersection.Height < intersection.Width)
                {
                    for (int i = 0; i < 20; i++)
                        Item.SpriteVelocity += new Vector2(0, -1);
                }

            }
            if (collidedObject is MarioCollision)
            {
                Item.Sounds.PlaySound(Item.Sound);
                Item.Dead = true;
            }


        }

    }
}
