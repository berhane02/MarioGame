using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Interfaces
{
    public interface IGameState
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
