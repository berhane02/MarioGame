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
    public class RevealedState:PiranhaState
    {
        private const float Delay = 1000f;//show for 10 seconds
        private int _elapsed = 0;
        public RevealedState(PiranhaEntity pir) : base(pir) { }

        public override void Enter()
        {
            Plant.SpriteVelocity = new Vector2(0, -1);
            Plant.EntityCollision = new EnemyCollision(Plant);//now collision with mario might kill mario
        }

        public override void ExitState()
        {
            Plant.SpriteVelocity = Vector2.Zero;
        }

        public override void Update(GameTime gametime)
        {
            if (Math.Abs(Plant.SpritePosition.Y - Plant._initalPos.Y) > Plant.Sprite.Texture.Width)
            {
                Plant.SpriteVelocity = Vector2.Zero;//completely revealed
                _elapsed += gametime.ElapsedGameTime.Milliseconds;
                if (_elapsed >= Delay)
                {
                    Plant.SpriteVelocity = new Vector2(0, 1);//make plant go down
                    _elapsed = 0;
                }
            }
            else if (Math.Abs(Plant.SpritePosition.Y - Plant._initalPos.Y) == 0 && Plant.SpriteVelocity.Y > 0)//stop descend
            {
                CurrentState.ExitState();
                CurrentState = new HiddenState(Plant);
                CurrentState.Enter();
            }
        }
    }
}
