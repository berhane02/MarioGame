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
    public class GameOverState : IGameState
    {
        Game1 game;
        private readonly SpriteFont font;
        private FeedbackLayer feed;
        public GameOverState()
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
            spriteBatch.Begin(SpriteSortMode.Deferred,
                                null,
                                SamplerState.LinearWrap,
                                null, null, null,
                                game.camera.GetViewMatrix(Vector2.Zero));
            spriteBatch.DrawString(font, "GAME OVER", new Vector2(40, 225), Color.White);
            spriteBatch.DrawString(font, "Press R to restart", new Vector2(40, 300), Color.White);
            spriteBatch.DrawString(font, "Press Q to Quit", new Vector2(40, 400), Color.White);
            spriteBatch.End();
            feed.Draw(spriteBatch, game.camera);
        }
    }
}
