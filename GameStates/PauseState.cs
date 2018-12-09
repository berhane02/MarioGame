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
    public class PauseState : IGameState
    {
        Game1 game;
        private readonly SpriteFont font;
        public PauseState()
        {
            game = Game1.MarioGame1;
            font = game.Content.Load<SpriteFont>("File");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "PAUSED GAME", new Vector2(game.Level0.Mario.SpritePosition.X, 100), Color.Black);
            spriteBatch.DrawString(font, "Press P to unpause", new Vector2(game.Level0.Mario.SpritePosition.X, 150), Color.Black);
            spriteBatch.DrawString(font, "Press Q to Quit", new Vector2(game.Level0.Mario.SpritePosition.X, 250), Color.Black);
            spriteBatch.DrawString(font, "Press R to restart", new Vector2(game.Level0.Mario.SpritePosition.X, 200), Color.Black);
        }

    }
}
