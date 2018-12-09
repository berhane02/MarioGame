using System;
using MarioGame.Entities;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.ItemStateMachine.ItemStates
{
    public class RevealItemState : ItemState
    {
        public RevealItemState(ItemEntity item) : base(item) { }
        public override void Enter(IItemState state)
        {
            Item.SpriteVelocity = new Vector2(0, -1);
            NextState = state.NextState;
        }

        public override void ExitState()
        {
            Item.SpriteVelocity = Vector2.Zero;
        }

        public override void Update()
        {
            if (Math.Abs(Item.SpritePosition.Y - Item.InitialPos.Y) > Item.BoundBox.Height)
            {
                CurrentState.ExitState();
                CurrentState = NextState;
                CurrentState.Enter(this);
            }

        }
    }
}
