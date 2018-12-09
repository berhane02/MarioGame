using MarioGame.Collision;
using MarioGame.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using MarioGame.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Block

{
    public class EnemyPipeEntity:Entity
    {
        const double TIMER = 3.0;
        private double timer = TIMER;

        public EnemyPipeEntity(Vector2 loc, List<Tile> tiles,List<Entity> movingEntities)
        {
            Sprite = new EnemyPipeBlock(loc);
            EntityCollision = new BlockCollision(this);

            CollideList = tiles;
            MovingEntities = movingEntities;
        }

        public override void Update(GameTime gametime, Vector2 Velocity, GraphicsDeviceManager graphics)
        {
            Sprite.Update(gametime, Velocity, graphics);
            timer -= gametime.ElapsedGameTime.TotalSeconds;
            if (timer<0)
            {
                Entity enemy;
                Random r = new Random();
                int num = r.Next(0, 3);
                switch (num % 3)
                {
                    case 0:
                        enemy = new GoombaEntity(new Vector2(Sprite.Position.X-32, Sprite.Position.Y));
                        break;
                    case 1:
                        enemy = new GreenKoopaEntity(new Vector2(Sprite.Position.X-32, Sprite.Position.Y));
                        break;
                    case 2:
                        enemy = new RedKoopaEntity(new Vector2(Sprite.Position.X-32, Sprite.Position.Y)) { SpriteVelocity = new Vector2(-1,0)};
                        break;
                    default:
                        enemy = new GoombaEntity(new Vector2(Sprite.Position.X-32, Sprite.Position.Y));
                        break;
                }
                enemy.SpriteVelocity = new Vector2(-1,0);
                MovingEntities.Add(enemy);
                Tile tile = new Tile();
                tile.Entity = enemy;
                tile.Position = enemy.SpritePosition;
                CollideList.Add(tile);
                timer = TIMER;
            }
        }

    }
}
