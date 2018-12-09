using MarioGame.Mario;
using MarioGame.GameStates;
using MarioGame.CommandHandling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame
{
    public class FeedbackLayer
    {
        private SpriteFont Font;
        private GameTime GameTime;
        private GraphicsDeviceManager Graphics;
        private string Player;
        private int Scores;
        private int Coins;
        private int Lives;
        private double Time;

        Sprite coinsprite; 
        Texture2D playertexture; 
        MarioSprite mario; 

        public FeedbackLayer(GraphicsDeviceManager graphics)
        {
            Player = "";
            Graphics = graphics;
            Font = Game1.ContentLoad.Load<SpriteFont>("File");
            Time = 400;
            Scores = 0;
            Coins = 0;
            Lives = 3;

            coinsprite = new Coin(new Vector2(360, 80));
            playertexture = Game1.ContentLoad.Load<Texture2D>("StandardMario/RightIdle");
            mario = new MarioSprite(playertexture, false, 1, 1, new Vector2(540, 115));
        }

        public void Update(Level level, GameTime gameTime)
        {
            GameTime = gameTime;

            //Player
            if (level.Mario is MarioEntity)
                Player = "MARIO";

            //SCORES
            Scores = level.Mario.MarioScore;

            //COINS
            coinsprite.Update(GameTime, Vector2.Zero, Graphics);
            Coins = level.Mario.CoinCount;

            //LIVES
            Lives = level.Mario.Live;

            //Times
            Time = level.Timer;

        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                                null,
                                SamplerState.LinearWrap,
                                null, null, null,
                                camera.GetViewMatrix(Vector2.Zero));
            
            //Player
            spriteBatch.DrawString(Font, "PLAYER", new Vector2(40, 40), Color.White);
            spriteBatch.DrawString(Font, Player, new Vector2(40,80), Color.MonoGameOrange);

            //Score
            spriteBatch.DrawString(Font, "SCORE", new Vector2(200, 40), Color.White);
            spriteBatch.DrawString(Font, Scores.ToString(), new Vector2(200, 80), Color.MonoGameOrange);
            
            //coins
            spriteBatch.DrawString(Font, "COINS", new Vector2(360, 40), Color.White);
            coinsprite.Draw(spriteBatch);
            spriteBatch.DrawString(Font, "X"+Coins.ToString(), new Vector2(400, 80), Color.MonoGameOrange);
            
            //LIVE
            spriteBatch.DrawString(Font, "LIVES", new Vector2(520, 40), Color.White);
            mario.Draw(spriteBatch);
            spriteBatch.DrawString(Font, "X"+Lives.ToString(), new Vector2(560,80), Color.MonoGameOrange);

            //TIME
            spriteBatch.DrawString(Font, "TIME", new Vector2(680,40), Color.White);
            spriteBatch.DrawString(Font, ((int)Time).ToString(), new Vector2(680,80), Color.MonoGameOrange);



            spriteBatch.End();
        }
 

    }
}
