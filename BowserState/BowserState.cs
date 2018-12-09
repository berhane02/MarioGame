using MarioGame.Entities;
using MarioGame.Interfaces;

namespace MarioGame.BowserState
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors")]
    public abstract class BowserState : IBowserState
    {
        public BowserEntity Bowser { get; set; }
        public IBowserState CurrentState
        {
            get { return Bowser.State; }
            set { Bowser.State = value; }
        }
        public BowserState(BowserEntity bowser)
        {
            Bowser = bowser;
        }
        public virtual void Enter() { }

        public virtual void ExitState() { }

        public virtual void CollisionResponse(ICollision collidedCollision) { }

        public virtual void Update() { }
    }
}
