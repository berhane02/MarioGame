using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Collision;
using MarioGame.Entities;
using Microsoft.Xna.Framework;

namespace MarioGame.PiranhaState
{
    public class HiddenState:PiranhaState
    {
        private const float Delay = 1000f;//show for 10 seconds
        private int _elapsed;
        public HiddenState(PiranhaEntity piranha) : base(piranha) { }

        public override void Enter()
        {
            Plant.EntityCollision = new BlockCollision(Plant);
        }

        public override void RevealTransition()
        {
            if (_elapsed > Delay)//to avoid coming out too often
            {
                CurrentState.ExitState();
                CurrentState = new RevealedState(Plant);
                CurrentState.Enter();
                _elapsed = 0;
            }
           
        }

        public override void Update(GameTime gametime)
        {
            _elapsed += gametime.ElapsedGameTime.Milliseconds;

        }
    }
}
