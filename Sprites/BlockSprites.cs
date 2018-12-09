using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame
{
    public class BlockSprites : Sprite
    {
        protected BlockSprites(Texture2D texture, bool animated, int row, int column, Vector2 loc) : base(texture, animated, row, column, loc)
        {
            SeriesPicture = animated;
            Rows = row;
            Columns = column;
            CurrentFrame = 0;
            TotalFrames = Rows * Columns;
            Texture = texture;
            Position = loc;
            Velocity = Vector2.Zero;
            Scale = 2;
            Tint = Color.White;
            Box = Color.Blue;
            BoundBox = new Rectangle((int)loc.X, (int)loc.Y, Scale * Width , Scale * Height);
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
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Tint, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
            
        }

    }

    //public class EnemyPipeBlock : BlockSprites
    //{
    //    static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/pipe");

    //    public EnemyPipeBlock(Vector2 loc) : base(texture, false, 1, 1, loc) { Scale = 2; }
    //    public override void Draw(SpriteBatch spriteBatch)
    //    {
    //        int row = CurrentFrame / Columns;
    //        int column = CurrentFrame % Columns;

    //        Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);
    //        Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Scale * Width, Scale * Height);
    //        if (IsToggle)
    //        {
    //            spriteBatch.Draw(Rectangle, BoundBox, Box); //draw rectangle as background for sprite
    //        }
    //        spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Tint, -(float)Math.PI / 2.0f, Vector2.Zero, SpriteEffects.None, 0.1f);

    //    }
    //}

    public class PipeBlock : BlockSprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/pipe");
        
        public PipeBlock(Vector2 loc) : base(texture, false, 1, 1, loc) { Scale = 2;}
    }

    public class BridgeBlock : BlockSprites
    {
        private static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/Bridge");
        public BridgeBlock(Vector2 loc) : base(texture, false, 1, 1, loc) { Scale = 2;}
    }
    public class LongPipeBlock : BlockSprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/LongPipe");
        public LongPipeBlock(Vector2 loc) : base(texture, false, 1, 1, loc) { Scale = 2; }
    }
    public class EnemyPipeBlock : BlockSprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/EnemyPipe");
        public EnemyPipeBlock(Vector2 loc) : base(texture, false, 1, 1, loc) { Scale = 2; }
    }

    public class ExplodingBlock : BlockSprites//a shard
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/BrickBlock");

        public ExplodingBlock(Vector2 loc) : base(texture, false, 1, 2, loc)
        {
            Scale = 1;
        }

    }

    public class BrickBlock : BlockSprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/BrickBlock");
        public BrickBlock(Vector2 loc) : base(texture, false, 1, 2, loc) { }

    }
    public class HiddenBlock : BlockSprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/BrickBlock");

        public HiddenBlock(Vector2 loc) : base(texture, false, 1, 2, loc) { }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //if hidden we don't draw the sprite but we need the bounding box
            BoundBox = new Rectangle((int)Position.X, (int)Position.Y, Scale * Width , Scale * Height );
            if (IsToggle)
            {
                spriteBatch.Draw(Rectangle, BoundBox, Box);
            }
        }
    }
    public class FloorBlock : BlockSprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/FloorBlock");
        public FloorBlock(Vector2 loc) : base(texture, false, 1, 1, loc) { }
    }

    public class QuestionBlock : BlockSprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/QuestionBlocks");
        public QuestionBlock(Vector2 loc) : base(texture, true, 1, 3, loc) { }

    }

    public class StairBlock : BlockSprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/StairBlock");
        public StairBlock(Vector2 loc) : base(texture, false, 1, 1, loc) { }
    }

    public class UsedBlock : BlockSprites
    {
        static Texture2D texture = Game1.ContentLoad.Load<Texture2D>("Block/UsedBlock");
        public UsedBlock(Vector2 loc) : base(texture, false, 1, 1, loc) { }
    }


}