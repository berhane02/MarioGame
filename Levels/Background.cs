using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    public class Background
    {
        private GraphicsDeviceManager _graphics;
        private Texture2D background1;
        private Texture2D background2;
        private Point _dimension;

        public Background(GraphicsDeviceManager theGraphics)
        {
            _graphics = theGraphics;
            _dimension = new Point(Level.ScreenHeight*800/480,Level.ScreenHeight);

            background1 = Game1.ContentLoad.Load<Texture2D>("Background/background1");
            background2 = Game1.ContentLoad.Load<Texture2D>("Background/background2");
        }
        

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {

            // Now Tile RenderTarget across ViewPort
            Viewport _viewport = _graphics.GraphicsDevice.Viewport;
            int _tilesX = 1;
            while ((_dimension.X * _tilesX) <= _viewport.Width)
                _tilesX++;
            _tilesX++;

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                                null,
                                SamplerState.LinearWrap,
                                null, null, null,
                                camera.GetViewMatrix(new Vector2(0.2f)));
            for (int _tileX = 0; _tileX < _tilesX; _tileX++)
                spriteBatch.Draw(background1, new Vector2(_tileX * _dimension.X, 0), null, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.8f);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                                null,
                                SamplerState.LinearWrap,
                                null, null, null,
                                camera.GetViewMatrix(new Vector2(0.5f)));
            for (int _tileX = 0; _tileX < _tilesX; _tileX++)
                spriteBatch.Draw(background2, new Vector2(_tileX * _dimension.X, 0), null, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.9f);
            spriteBatch.End();
        }
    }
}
