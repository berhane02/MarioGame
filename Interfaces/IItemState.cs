using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarioGame.Interfaces
{
    public interface IItemState
    {
        IItemState NextState{ get; set; }
        void Enter(IItemState state);
        void ExitState();
        void CollisionResponse(ICollision collidedCollision);
        void RevealTransition();
        void Update();
    }
}
