using MarioGame.CommandHandling;
using MarioGame.GameStates;
using MarioGame.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MarioGame
{
    /// <summary>
    /// This is the main type.
    /// </summary>
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private PauseState pause;
        private LevelRestartState rest;
        private MenuState menu;
        public static ContentManager ContentLoad { get; set; }
        private Level level;
        public Level Level0
        {
            get { return level; }
        }

        private int deadCount;
        private Background background;
        public FeedbackLayer Feedback { get; set; }
        public Controller KeyBoardControls { get; set; }
        public Controller GamePadControls { get; set; }
        public static Game1 MarioGame1 { get; set; }
        public bool pausedState { get; set; }
        public bool gameOverState { get; set; }
        public static bool menuState = true;
        public EventSoundEffects Sounds { get; set; }
        public Camera camera { get; set; }

        public static bool hidden;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ContentLoad = Content;
            IsMouseVisible = true;
            Sounds = new EventSoundEffects(Content);
            MediaPlayer.IsRepeating = true;

            MarioGame1 = this;
            deadCount = 0;
            hidden = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            KeyBoardControls = new KeyBoardControls(this);
            GamePadControls = new GamePadControls(this);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            level = new Level(_graphics, "level.csv", Sounds);
            menu = new MenuState(_graphics, level);
            background = new Background(_graphics);
            camera = new Camera(GraphicsDevice.Viewport);
            Feedback = new FeedbackLayer(_graphics);
            pause = new PauseState();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (menuState)
            {
                menu.Update();
            }
            else
            {
                if (!pausedState && !gameOverState)
                {
                    Feedback.Update(level, gameTime);
                    level.Update(gameTime, hidden);
                    GamePadControls.UpdateInput();
                    camera.Update(level.Mario.SpritePosition, Level.ScreenWidth, Level.ScreenHeight, hidden);
                    DeadRestartOrGameOver(gameTime);
                    WonGame();
                }
                KeyBoardControls.UpdateInput();
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (menuState)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                _spriteBatch.Begin();
                menu.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            else { 
                if (!pausedState && !gameOverState)
                {
                    if (hidden == true)
                    {
                        GraphicsDevice.Clear(Color.Black);
                    }
                    else
                    {
                        GraphicsDevice.Clear(Color.CornflowerBlue);
                        background.Draw(_spriteBatch, camera);
                    }

                    _spriteBatch.Begin(SpriteSortMode.BackToFront,
                                        null,
                                        SamplerState.LinearWrap,
                                        null, null, null,
                                        camera.GetViewMatrix(Vector2.One));
                    level.Draw(_spriteBatch);
                    _spriteBatch.End();
                    Feedback.Draw(_spriteBatch, camera);

                    base.Draw(gameTime);
                }
                else if (pausedState)
                {
                    _spriteBatch.Begin(SpriteSortMode.BackToFront,
                                        null,
                                        SamplerState.LinearWrap,
                                        null, null, null,
                                        camera.GetViewMatrix(Vector2.One));
                    pause.Draw(_spriteBatch);
                    _spriteBatch.End();
                }
            }
        }
        public void ExitCommand()
        {
            Exit();
        }

        public void Pause()
        {
            pausedState = !pausedState;
            if (pausedState)
            {
                MediaPlayer.Pause();
                KeyBoardControls = new PausedGameKeyboardControls(this);
            }
            else
            {
                MediaPlayer.Resume();
                KeyBoardControls = new KeyBoardControls(this);
            }


        }

        public void Reset()
        {
            level = new Level(_graphics, "level.csv", Sounds);
            menu = new MenuState(_graphics, level);
            menuState = true;
            MediaPlayer.IsRepeating = true;
            KeyBoardControls = new KeyBoardControls(this);
            GamePadControls = new GamePadControls(this);
            pausedState = false;
            gameOverState = false;
            deadCount = 0;
            level.TotalExtraLives = 0;
        }


        public void DeadRestartOrGameOver(GameTime gameTime)
        {
            if (level.Mario.HasDied)
            {
                deadCount++;
                deadCount -= level.TotalExtraLives;
                if (level.Mario.Live == 0)
                {
                    Sounds.PlaySound(EventSoundEffects.EventSounds.GameOver);
                    GameOverState over = new GameOverState();
                    gameOverState = true;
                    KeyBoardControls = new GameOverControls(this);
                    Feedback.Update(level, gameTime);
                    over.Draw(_spriteBatch);
                }
                else
                {
                    rest = new LevelRestartState();
                    rest.CheckProgress();
                    level = new Level(_graphics, "level.csv", Sounds);
                    level.DifficultyMode(MenuState.Difficulty);
                    KeyBoardControls = new KeyBoardControls(this);
                    GamePadControls = new GamePadControls(this);
                    level.Mario.Live -= deadCount;                    
                    rest.Draw(_spriteBatch);
                    MediaPlayer.Resume();
                }
            }
        }

        public void WonGame()
        {
            if (level.Mario.WonGame)
            {
                WinningGameState win = new WinningGameState();
                win.Draw(_spriteBatch);
                KeyBoardControls = new GameOverControls(this);
            }
        }



    }
}
