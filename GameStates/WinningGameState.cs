using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.GameStates
{
    public class WinningGameState : IGameState
    {
        Game1 game;
        private readonly SpriteFont font;
        private FeedbackLayer feed;
        public WinningGameState()
        {
            game = Game1.MarioGame1;
            game.gameOverState = true;
            font = game.Content.Load<SpriteFont>("File");
            feed = game.Feedback;
            MediaPlayer.Stop();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                                  null,
                                  SamplerState.LinearWrap,
                                  null, null, null,
                                  game.camera.GetViewMatrix(Vector2.One));
            spriteBatch.DrawString(font, "YOU WIN", new Vector2(game.Level0.Mario.SpritePosition.X-200, 225), Color.White);
            spriteBatch.DrawString(font, "Press R to restart", new Vector2(game.Level0.Mario.SpritePosition.X - 200, 300), Color.White);
            spriteBatch.DrawString(font, "Press Q to Quit", new Vector2(game.Level0.Mario.SpritePosition.X - 200, 400), Color.White);
            spriteBatch.End();
            feed.Draw(spriteBatch, game.camera);
        }


    }
}
