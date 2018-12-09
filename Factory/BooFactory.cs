
using MarioGame.Entities;
using MarioGame.Mario;
using Microsoft.Xna.Framework;

namespace MarioGame.Factory
{
    public static class BooFactory
    {
        public static Entity BuildSprite(MarioEntity mario, Vector2 loc)
        {
            return new BooEntity(mario,loc);
        }
    }
}
