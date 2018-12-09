using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public interface ISprite
    {
        //Rectangle BoundBox { get; }
        //Texture2D Texture { get; set; }
        void Update(GameTime gametime, Vector2 velocity, GraphicsDeviceManager graphics);
        void Draw(SpriteBatch spriteBatch);
    }
}
