using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame
{
    public class Item : Sprite
    {
        public Item(Texture2D texture, bool animated, int row, int column, Vector2 loc) : base(texture, animated, row, column, loc)
        {
            SeriesPicture = animated;
            Rows = row;
            Columns = column;
            CurrentFrame = 0;
            TotalFrames = Rows * Columns;
            Texture = texture;
            Position = loc;
            Velocity = Vector2.Zero;
            Scale = 1;
            Tint = Color.White;
            Box = Color.Green;
            BoundBox = new Rectangle((int)loc.X-5, (int)loc.Y-5, Scale*Width+10, Scale*Height+10);
        }
        public override void Update(GameTime gametime, Vector2 Velocity, GraphicsDeviceManager graphic)
        {
            WhiteBox(graphic);
            if (SeriesPicture)
            {
                double timePerFrame = 0.2;
                CurrentFrame = (int)(gametime.TotalGameTime.TotalSeconds / timePerFrame);
                CurrentFrame = CurrentFrame % TotalFrames;

                CurrentFrame++;
                if (CurrentFrame == TotalFrames)
                    CurrentFrame = 0;
            }

            base.Velocity = Velocity;
            Position += base.Velocity;//if static then velocity is 0;
            BoundBox = new Rectangle((int)Position.X-5, (int)Position.Y-5, Scale * Width+10, Scale * Height+10);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            int row = CurrentFrame / Columns;
            int column = CurrentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Scale * Width, Scale * Height);
            if (IsToggle)
            {
                spriteBatch.Draw(Rectangle, BoundBox, Box); //draw rectangle as background for sprite
            }
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Tint,0,Vector2.Zero,SpriteEffects.None,0.2f);
        }
    }

    public class Flower : Item
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Item/flower");
        public Flower(Vector2 loc) : base(texture, false, 1, 1, loc) { }
    }
    public class Coin : Item
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Item/coin");
        public Coin(Vector2 loc) : base(texture, true, 1, 4,loc) { }
    }
    public class RedMushroom : Item
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Item/redmushroom");
        public RedMushroom(Vector2 loc): base(texture, false, 1, 1,loc) {          
        }      
    }

    public class GreenMushroom : Item
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Item/greenmushroom");
        public GreenMushroom(Vector2 loc) : base(texture, false, 1, 1, loc) { }
    }


    public class Star : Item
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Item/star");
        public Star(Vector2 loc): base(texture, true, 1, 4, loc) { }

    }

    public class Axe : Item
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Item/Axe");
        public Axe(Vector2 loc) : base(texture, false, 1, 1, loc) { Scale = 2;}
       
    }


}