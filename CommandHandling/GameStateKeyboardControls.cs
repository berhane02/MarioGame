using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MarioGame.CommandHandling
{
    public class GameOverControls : Controller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private Game1 _game;
        KeyboardState _previousKeyboardState;
        public GameOverControls(Game1 game)
        {
            _game = game;
            _previousKeyboardState = Keyboard.GetState();
            Command((int)Keys.Q, new ExitCommand(_game));
            Command((int)Keys.R, new ResetCommand(_game));
        }

        public override void UpdateInput()//Yemane, if mario is going up the idle state should be to fall.
        {
            // Get the current Keyboard state.
            KeyboardState currentState = Keyboard.GetState();
            if (_previousKeyboardState != currentState)//it was previous==current
            {
                foreach (Keys key in currentState.GetPressedKeys())
                {
                    if (Commands.ContainsKey((int)key))
                    {
                        Commands[((int)key)].Execute();
                    }
                }
            }
            //else
            //{
            //    _game.Level0.Mario.Idle();
            //}
            // Update previous Keyboard state.
            _previousKeyboardState = currentState;
        }
    }

    public class PausedGameKeyboardControls : Controller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private Game1 _game;
        KeyboardState _previousKeyboardState;
        public PausedGameKeyboardControls(Game1 game)
        {
            _game = game;
            _previousKeyboardState = Keyboard.GetState();
            Command((int)Keys.Q, new ExitCommand(_game));
            Command((int)Keys.R, new ResetCommand(_game));
            Command((int)Keys.P, new PauseCommand(_game));
        }

        public override void UpdateInput()//Yemane, if mario is going up the idle state should be to fall.
        {
            // Get the current Keyboard state.
            KeyboardState currentState = Keyboard.GetState();
            if (_previousKeyboardState != currentState)//it was previous==current
            {
                foreach (Keys key in currentState.GetPressedKeys())
                {
                    if (Commands.ContainsKey((int)key))
                    {
                        Commands[((int)key)].Execute();
                    }
                }
            }
            //else
            //{
            //    _game.Level0.Mario.Idle();
            //}
            // Update previous Keyboard state.
            _previousKeyboardState = currentState;
        }
    }


}
