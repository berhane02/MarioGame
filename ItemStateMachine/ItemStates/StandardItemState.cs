using MarioGame.Collision;
using MarioGame.Entities;
using MarioGame.Interfaces;

namespace MarioGame.ItemStateMachine.ItemStates
{
    class StandardItemState: ItemState
    {
        public StandardItemState(ItemEntity entity) : base(entity) { }

        public override void Enter(IItemState state)
        {
            NextState = state;
            if (NextState is RevealItemState && Item is CoinEntity)
            {
                Item.Dead = true;
            }
        }

        public override void CollisionResponse(ICollision collidedCollision)
        {
            if (collidedCollision is MarioCollision)
            {
                Item.Sounds.PlaySound(Item.Sound);
                Item.Dead = true;
            }
        }
    }
}
