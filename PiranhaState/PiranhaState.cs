using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Entities;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework;

namespace MarioGame.PiranhaState
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors")]
    public abstract class PiranhaState: IPiranhaState
    {
        public PiranhaEntity Plant { get; set; }
        public IPiranhaState CurrentState
        {
            get { return Plant.State; }
            set { Plant.State = value; }
        }
        public PiranhaState(PiranhaEntity piranha)
        {
            Plant = piranha;
        }
        public virtual void Enter() { }

        public virtual void ExitState() { }

        public virtual void CollisionResponse(ICollision collidedCollision) { }

        public virtual void RevealTransition() { }

        public virtual void Update(GameTime gametime) { }
    }
}
