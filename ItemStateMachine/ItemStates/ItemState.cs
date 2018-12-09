using MarioGame.Entities;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.ItemStateMachine.ItemStates
{
    public abstract class ItemState:IItemState
    {
        public ItemEntity Item { get; set; }
        public IItemState CurrentState
        {
            get { return Item.State;}
            set { Item.State=value; }
        }
        protected IItemState NextState;

        IItemState IItemState.NextState
        {
            get { return NextState; }
            set { NextState = value; }
        }

        protected ItemState(ItemEntity entity)
        {
            Item = entity;
        }

        public virtual void Enter(IItemState state) { }

        public virtual void ExitState() {}
       
        public virtual void CollisionResponse(ICollision collidedCollision) { }

        public virtual void RevealTransition()
        {
            CurrentState.ExitState();
            CurrentState = new RevealItemState(Item);
            CurrentState.Enter(this);
        }

        public virtual void Update() { }
    }
}
