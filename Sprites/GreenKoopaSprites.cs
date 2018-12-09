using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{

    public class GreenKoopa : EnemySprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Enemy/LeftgreenKoopa");

        public GreenKoopa(Vector2 loc) : base(texture, true, 1, 4, loc) { }

    }
    
}