using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MarioGame.CommandHandling
{
    class KeyBoardControls : Controller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private Game1 _game;
        KeyboardState _previousKeyboardState;
        public KeyBoardControls(Game1 game)
        {
            _game = game;
            _previousKeyboardState = Keyboard.GetState();
            Command((int)Keys.Q, new ExitCommand(_game));
            Command((int)Keys.A, new LeftCommand(_game.Level0.Mario));
            Command((int)Keys.Left, new LeftCommand(_game.Level0.Mario));
            Command((int)Keys.D, new RightCommand(_game.Level0.Mario));
            Command((int)Keys.Right, new RightCommand(_game.Level0.Mario));
            Command((int)Keys.W, new UpCommand(_game.Level0.Mario));
            Command((int)Keys.Up, new UpCommand(_game.Level0.Mario));
            Command((int)Keys.S, new DownCommand(_game.Level0.Mario));
            Command((int)Keys.Down, new DownCommand(_game.Level0.Mario));
            Command((int)Keys.O, new DamageCommand(_game.Level0.Mario));
            Command((int)Keys.Y, new StandardCommand(_game.Level0.Mario));
            Command((int)Keys.U, new SuperCommand(_game.Level0.Mario));
            Command((int)Keys.I, new FireCommand(_game.Level0.Mario));
            Command((int)Keys.C, new CollisionCommand(_game.Level0));
            Command((int)Keys.R, new ResetCommand(_game));
            Command((int)Keys.M, new MuteCommand(_game.Level0));
            Command((int)Keys.P, new PauseCommand(_game));
            Command((int)Keys.T, new InvincibleCommand(_game.Level0.Mario));


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
            /*else
            {
                _game.Level0.Mario.ActionState.Idle();
            }*/
            // Update previous Keyboard state.
            _previousKeyboardState = currentState;
        }
    }
}
