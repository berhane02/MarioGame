using MarioGame.Factory;
using Microsoft.Xna.Framework;

namespace MarioGame.Interfaces
{
    public interface IMarioFactory
    {
        Sprite BuildSprite(MarioFactory.Mario sprite, Vector2 location);
    }
}
