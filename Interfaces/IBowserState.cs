using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarioGame.Interfaces
{
    public interface IBowserState
    {
        void Enter();
        void ExitState();
        void Update();
        void CollisionResponse(ICollision collidedCollision);
    }
}
