using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.GameStates
{
    class Button
    {
        Texture2D texture;
        Rectangle Rectangle;
        Vector2 Position;
        Color Color = new Color(255, 255, 255, 255);

        bool down;
        public bool clicked;

        public Button(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            Position = position;
        }

        public void Update(MouseState mouse)
        {
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 100, 20);
            Rectangle mouseArea = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseArea.Intersects(Rectangle))
            {
                //mouse visible
                if (Color.A == 255)
                    down = false;
                if (Color.A == 0)
                    down = true;

                if (down)
                    Color.A += 5;
                else
                    Color.A -= 5;

                if (mouse.LeftButton == ButtonState.Pressed)
                    clicked = true;
            }
            else if (Color.A < 255)
            {
                Color.A += 5;
                clicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color);
        }
    }
}
