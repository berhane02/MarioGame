using MarioGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.GameStates
{
    public class MenuState : IGameState
    {
        Button button1;
        Button button2;
        Button button3;

        Texture2D cover;

        GraphicsDeviceManager graphics;
        Level level;
        public static int Difficulty;

        public MenuState(GraphicsDeviceManager graphics, Level level)
        {
            this.graphics = graphics;
            this.level = level;
            //currentState = state;
            button1 = new Button(Game1.ContentLoad.Load<Texture2D>("Mode/Easy"), new Vector2(350, 230));
            button2 = new Button(Game1.ContentLoad.Load<Texture2D>("Mode/Normal"), new Vector2(350, 260));
            button3 = new Button(Game1.ContentLoad.Load<Texture2D>("Mode/Hard"),  new Vector2(350, 290));
            cover = Game1.ContentLoad.Load<Texture2D>("background/cover");
        }
        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            if (button1.clicked)
            {
                Game1.menuState = false;
                level.DifficultyMode(1);
                Difficulty = 1;
                //currentState.setState(currentState.getPlayingState(1));
            }
            else if (button2.clicked)
            {
                Game1.menuState = false;
                level.DifficultyMode(2);
                Difficulty = 2;
                //    currentState.setState(currentState.getPlayingState(2));
            }
            else if (button3.clicked)
            {
                Game1.menuState = false;
                level.DifficultyMode(3);
                Difficulty = 3;
                //    currentState.setState(currentState.getPlayingState(3));

            }
            button1.Update(mouse);
            button2.Update(mouse);
            button3.Update(mouse);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(cover, new Vector2(312, 80), Color.White);
            button1.Draw(spriteBatch);
            button2.Draw(spriteBatch);
            button3.Draw(spriteBatch);
        }
    }
}
