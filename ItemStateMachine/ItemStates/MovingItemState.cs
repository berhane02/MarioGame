using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;
using MarioGame.ItemStateMachine.ItemStates;
using MarioGame.Mario;
using Microsoft.Xna.Framework;

namespace MarioGame.ItemStateMachine
{
    class MovingItemState : ItemState
    {
        public MovingItemState(ItemEntity entity) : base(entity) { }
        public override void Enter(IItemState state)
        {
            Vector2 RightVelocity = new Vector2(1, 1);
            Vector2 LeftVelocity = new Vector2(-1, 1);
            if (Item is RedMushroomEntity)
            {
                if (Item.Mario.SpritePosition.X > Item.SpritePosition.X)
                {
                    Item.SpriteVelocity = RightVelocity;
                }
                else
                {
                    Item.SpriteVelocity = LeftVelocity;
                }
            }
            else
            {
                if (Item.Mario.SpritePosition.X > Item.SpritePosition.X)
                {
                    Item.SpriteVelocity = LeftVelocity;
                }
                else
                {
                    Item.SpriteVelocity = RightVelocity;
                }
            }
            //if (Item is RedMushroomEntity)
            //{

            //}
            //else if (Item is GreenMushroomEntity)
            //{

            //}
        }

        public override void ExitState()
        {
            Item.SpriteVelocity = Vector2.Zero;
        }

        public override void Update()
        {

            Item.SpriteVelocity = new Vector2(Item.SpriteVelocity.X,1);
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
                    SideCollision();
                }
                if (itemBox.Bottom > brickBox.Top && intersection.Height < intersection.Width)
                {
                    TopCollision();
                }
                
            }
            if (collidedObject is MarioCollision)
            {
                Item.Sounds.PlaySound(Item.Sound);
                Item.Dead = true;
            }


        }

        private void SideCollision()
        {
            Item.SpriteVelocity = new Vector2(Item.SpriteVelocity.X * -1, Item.SpriteVelocity.Y);
        }

        private void TopCollision()
        {
            Item.SpriteVelocity = new Vector2(Item.SpriteVelocity.X, 0);
        }

    }
}
