using MarioGame.CommandHandling;
using MarioGame.Interfaces;
using MarioGame.MarioActionState;
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
    public class LevelRestartState : IGameState
    {
        Game1 game;
        private bool checkpoint = false;
        public LevelRestartState()
        {
            game = Game1.MarioGame1;
            MediaPlayer.Pause();
        }

        public void CheckProgress()
        {
            if (game.Level0.Mario.SpritePosition.X >= 640)
            {
                checkpoint = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (checkpoint)
            {
                game.Level0.Mario.SpritePosition = new Vector2(640, 400);              
            }
            else
            {
                game.Level0.Mario.SpritePosition = new Vector2(64, 400);
            }
           
        }
    }
}
