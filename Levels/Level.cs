using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Block;
using MarioGame.Entities;
using MarioGame.Factory;
using MarioGame.Levels;
using MarioGame.Mario;
using Microsoft.Xna.Framework.Media;
using MarioGame.Mario.MarioPowerUp;

namespace MarioGame
{
    public class Tile
    {
        public Entity Entity { get; set; }
        public Vector2 Position { get; set; }
    }

    public class Level
    {
        private bool flag = false;
        private int totalExtraLives = 0;
        public int TotalExtraLives
        {
            get {return totalExtraLives;}
            set { totalExtraLives = value; }
        }
        UniformGrid Grid;
        bool toToggle = false;
        private const float Rewrite = 2000f;
        private List<Tile> tiles = new List<Tile>();
        private List<Entity> movingEntities = new List<Entity>();
        private GraphicsDeviceManager Graphics { get; set; }
        public static int ScreenWidth { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static int ScreenHeight { get; set; }

        public string Path { get; set; }
        public Song Song1 { get; set; }
        public Song Song2 { get; set; }
        public Song StarSong { get; set; }
        private bool Song1play;
        public EventSoundEffects Sounds { get; set; }
        public double Timer { get; set; }

        private Vector2 _pipePosition;
        private PipeBlockEntity _first;
        //mario
        public MarioEntity Mario { get; set; }
        public Level(GraphicsDeviceManager _graphics, string path, EventSoundEffects events)
        {
            Timer = 301f;
            Sounds = events;

            Graphics = _graphics;
            Path = path;
            using (var reader = new StreamReader(@"../../../../Levels/" + Path))
            {
                reader.ReadLine();
                string windowSize = reader.ReadLine();
                ScreenWidth = Convert.ToInt32(windowSize.Split(',')[1]);
                ScreenHeight = Convert.ToInt32(windowSize.Split(',')[2]);
                Grid = new UniformGrid(ScreenWidth, ScreenHeight);
                while (!reader.EndOfStream)
                {
                    AddingEntity(reader);
                }
            }
        }
        public void AddingEntity(StreamReader reader)
        {
            var line = reader.ReadLine();
            var values = line.Split(',');
            Tile tile = new Tile();
            string n = values[0];
            int x = Convert.ToInt32(values[1]);
            int y = Convert.ToInt32(values[2]);
            tile.Position = Grid.Position(x, y);
            string itemName = "";
            string pipeName = "";
            int itemNum = 0;
            if (values.Length > 3 && !string.IsNullOrEmpty(values[3]))
            {
                itemName = values[3];
                itemNum = Convert.ToInt32(values[4]);
            }
            if (values.Length > 7 && !string.IsNullOrEmpty(values[7]))
            {
                pipeName = values[5];
                _pipePosition = new Vector2(Convert.ToInt32(values[6]), Convert.ToInt32(values[7]));
            }
            else if (values.Length > 5 && !string.IsNullOrEmpty(values[5]))
            {
                pipeName = values[3];
                _pipePosition = new Vector2(Convert.ToInt32(values[4]), Convert.ToInt32(values[5]));
            }

            if (n.Equals("Mario"))
            {
                Mario = new MarioEntity(tile.Position, Sounds);
                movingEntities.Add(Mario);
            }
            else if (n.Equals("EnemyPipe"))
            {
                tile.Entity = new EnemyPipeEntity(tile.Position, tiles, movingEntities);
            }
            else if (n.Equals("Pole"))
            {
                tile.Entity = FlagFactory.BuildSprite(tile.Position);
            }
            else if (n.Equals("Boo"))
            {
                tile.Entity = BooFactory.BuildSprite(Mario, tile.Position);
            }
            else if (Enum.TryParse(n, out ItemCreation.ItemType itemType))
            {
                tile.Entity = ItemCreation.BuildItemEntity(itemType, tile.Position, Sounds);
            }
            else if (Enum.TryParse(n, out EnemyFactory.EnemyType enemyType))
            {
                tile.Entity = EnemyFactory.BuildEnemySprite(enemyType, tile.Position, Mario);
                movingEntities.Add(tile.Entity);
            }
            else if (Enum.TryParse(n, out BlockCreation.SceneBlock sceneBlock))
            {
                tile.Entity = BlockCreation.BuildSceneBlock(sceneBlock, tile.Position);
            }
            else if (Enum.TryParse(n, out BlockCreation.Blocks blockType))
            {
                tile.Entity = BlockCreation.BuildBlockEntity(blockType, tile.Position, Sounds);
                if ((tile.Entity is QuestionBlockEntity || tile.Entity is BrickBlockEntity)
                    && (Enum.TryParse(itemName, out ItemCreation.ItemType item)))//adding item to question block entity
                {
                    for (int i = 0; i < itemNum; i++)
                    {
                        Entity one = ItemCreation.BuildItemEntity(item, tile.Position, Sounds);
                        tile.Entity.AddItem(one, tiles, movingEntities, Mario);
                    }
                }
                else if (tile.Entity is BridgeBlockEntity && Enum.TryParse(itemName, out ItemCreation.ItemType axe))
                {
                    Entity _axe = ItemCreation.BuildItemEntity(axe, new Vector2(tile.Position.X + 32, tile.Position.Y - 64), Sounds);
                    Tile theAxe = new Tile { Entity = _axe };
                    tiles.Add(theAxe);
                    (tile.Entity as BridgeBlockEntity).AxeLink(_axe as AxeEntity);
                    for (int i = 0; i < itemNum; i++)//adding bridge blocks
                    {
                        Entity one = BlockCreation.BuildBlockEntity(BlockCreation.Blocks.BridgeBlock, new Vector2(tile.Position.X - i * 32, tile.Position.Y), Sounds);
                        (one as BridgeBlockEntity).AxeLink(_axe as AxeEntity);
                        tile.Entity.AddItem(one, tiles, movingEntities, Mario);
                    }
                }
                else if (tile.Entity is PipeBlockEntity && Enum.TryParse(itemName, out EnemyFactory.EnemyType enemy) && Enum.TryParse(pipeName, out BlockCreation.Blocks block))
                {
                    Entity plant = EnemyFactory.BuildEnemySprite(enemy, tile.Position, Mario);
                    tile.Entity.AddItem(plant, tiles, movingEntities, Mario);
                    _first = tile.Entity as PipeBlockEntity;
                    _pipePosition = Grid.Position((int)_pipePosition.X, (int)_pipePosition.Y);
                    Entity second = BlockCreation.BuildBlockEntity(block, _pipePosition, Sounds);
                    Tile two = new Tile { Entity = second };
                    _first.SetWarpDestination(second as PipeBlockEntity, Mario);
                    tiles.Add(two);
                    Grid.AddtoGrid(second);
                }
                else if (tile.Entity is PipeBlockEntity && Enum.TryParse(pipeName, out BlockCreation.Blocks pipe))
                {
                    _first = tile.Entity as PipeBlockEntity;
                    _pipePosition = Grid.Position((int)_pipePosition.X, (int)_pipePosition.Y);
                    Entity second = BlockCreation.BuildBlockEntity(pipe, _pipePosition, Sounds);
                    Tile two = new Tile { Entity = second };
                    _first.SetWarpDestination(second as PipeBlockEntity, Mario);
                    tiles.Add(two);
                    Grid.AddtoGrid(second);
                }

            }

            if (tile.Entity != null)
            {
                tiles.Add(tile);
                //Grid.AddtoGrid(tile.Entity);
            }
        }

        public void DifficultyMode(int difficulty)
        {
            StarSong = Game1.ContentLoad.Load<Song>("Music/Invincible");
            string ModeFile = "";
            if (difficulty == 1)
            {
                Song1 = Game1.ContentLoad.Load<Song>("Music/Overworld-slow");
                Song2 = Game1.ContentLoad.Load<Song>("Music/Underground-slow");
                ModeFile = "Easy.csv";
            }
            else if (difficulty == 2)
            {
                Song1 = Game1.ContentLoad.Load<Song>("Music/Overworld");
                Song2 = Game1.ContentLoad.Load<Song>("Music/Underground");
                ModeFile = "Normal.csv";
            }
            else if (difficulty == 3)
            {
                Song1 = Game1.ContentLoad.Load<Song>("Music/Overworld-fast");
                Song2 = Game1.ContentLoad.Load<Song>("Music/Underground-fast");
                ModeFile = "Hard.csv";
            }

            //Play music
            if (!Game1.hidden)
            {
                MediaPlayer.Play(Song1);
                Song1play = true;
            }
            else
            {
                MediaPlayer.Play(Song2);
                Song1play = false;
            }

            //Read entities
            using (var reader = new StreamReader(@"../../../../Levels/" + ModeFile))
            {
                while (!reader.EndOfStream)
                    AddingEntity(reader);
            }

        }


        public void Update(GameTime gameTime, bool hidden)
        {
            if (!hidden && !Song1play)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(Song1);
                Song1play = true;
            }
            else if (hidden && Song1play)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(Song2);
                Song1play = false;
            }
            //Timer
            if (Timer > 0)
            {
                Timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if ((int)Timer == 30)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Game1.ContentLoad.Load<Song>("Sounds/smb_warning"));
                    MediaPlayer.IsRepeating = false;
                }
                if (Mario.PowerState is MarioInvincibleState)
                {
                    if (!flag)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(StarSong);
                        flag = true;
                    }
                }
                else if (!(Mario.PowerState is MarioInvincibleState) && flag == true)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Song1);
                    flag = false;
                }



            }
            else if ((int)Timer == 0)
            {
                Mario.Die();
            }

            //Collision Detection
            for (int i = 0; i < movingEntities.Count; i++)
            {
                for (int j = 0; j < tiles.Count; j++)
                {
                    if (tiles[j].Entity.EntityCollision != null && movingEntities[i] != tiles[j].Entity)
                        movingEntities[i].EntityCollision.Detection(gameTime, tiles[j].Entity.EntityCollision);
                }
                movingEntities[i].EntityCollision.AfterAllDetection();
            }
            Mario.Update(gameTime, Graphics);
            for (int j = 0; j < tiles.Count; j++)//this will allow for adding a new item entity to the mix
            {
                if (tiles[j].Entity.Dead)
                {
                    if (tiles[j].Entity is EnemyEntity)
                    {
                        Mario.MarioScore += 100;
                    }

                    if (tiles[j].Entity is ItemEntity)
                    {
                        if (tiles[j].Entity is CoinEntity)
                        {
                            Mario.MarioScore += 200;
                            Mario.CoinCount += 1;
                            if (Mario.CoinCount is 100)
                            {
                                Mario.CoinCount = 0;
                                Mario.Live += 1;
                                totalExtraLives += 1;
                            }
                        }
                        else if

                       (tiles[j].Entity is GreenMushroomEntity)
                        {
                            Mario.Live += 1;
                            totalExtraLives += 1;
                        }
                        else
                        {
                            Mario.MarioScore += 1000;
                        }
                    }
                    if (movingEntities.Contains(tiles[j].Entity))
                        movingEntities.Remove(tiles[j].Entity);
                    //Grid.RemoveFromGrid(tiles[j].Entity);
                    tiles.RemoveAt(j);
                }
                else
                {
                    tiles[j].Entity.Update(gameTime, Vector2.Zero, Graphics);
                }

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Mario.Draw(spriteBatch);
            for (int j = 0; j < tiles.Count; j++)
                tiles[j].Entity.Draw(spriteBatch);

        }

        public void Mute()
        {
            if (!MediaPlayer.IsMuted)
                MediaPlayer.IsMuted = true;
            else
                MediaPlayer.IsMuted = false;
            Sounds.Mute(MediaPlayer.IsMuted);
        }
        public void ToggleCommand()
        {
            if (!toToggle)
            {
                toToggle = true;
                foreach (Tile tile in tiles)
                {
                    tile.Entity.Sprite.IsToggle = true;
                }
                Mario.Sprite.IsToggle = true;
            }
            else
            {
                toToggle = false;
                foreach (Tile tile in tiles)
                {
                    tile.Entity.Sprite.IsToggle = false;
                }
                Mario.Sprite.IsToggle = false;
            }
        }


    }

}
