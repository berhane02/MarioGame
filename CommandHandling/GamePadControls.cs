using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.CommandHandling
{
    class GamePadControls : Controller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private Game1 _game;
        int _count = 0;
        private List<GamePadState> past, _current;
        GamePadState _pastGamePadState1, _pastGamePadState2, 
            _pastGamePadState3, _pastGamePadState4, _emptyInput;
        public GamePadControls(Game1 game)
            : base()
        {
            _game = game;
            Command((int)Buttons.Start, new ExitCommand(_game));
            Command((int)Buttons.DPadLeft, new LeftCommand(_game.Level0.Mario));
            Command((int)Buttons.DPadRight, new RightCommand(_game.Level0.Mario));
            Command((int)Buttons.DPadUp, new UpCommand(_game.Level0.Mario));
            Command((int)Buttons.DPadDown, new DownCommand(_game.Level0.Mario));
            past = new List<GamePadState>();
            _pastGamePadState1 = GamePad.GetState(PlayerIndex.One);
            _pastGamePadState2 = GamePad.GetState(PlayerIndex.Two);
            _pastGamePadState3 = GamePad.GetState(PlayerIndex.Three);
            _pastGamePadState4= GamePad.GetState(PlayerIndex.Four);
            past.Add(_pastGamePadState1);
            past.Add(_pastGamePadState2);
            past.Add(_pastGamePadState3);
            past.Add(_pastGamePadState4);
            _emptyInput = new GamePadState(Vector2.Zero, Vector2.Zero, 0, 0, new Buttons());
        }

        public override void UpdateInput()
        {
            // Get the current gamepad state for all players.
            GamePadState currentState1 = GamePad.GetState(PlayerIndex.One);
            GamePadState currentState2 = GamePad.GetState(PlayerIndex.Two);
            GamePadState currentState3 = GamePad.GetState(PlayerIndex.Three);
            GamePadState currentState4 = GamePad.GetState(PlayerIndex.Four);
            _current = new List<GamePadState> {currentState1, currentState2, currentState3, currentState4};

            // Process input only if connected.
            foreach (GamePadState player in _current)
            {
                GamePadState pastState = past[_count];
                if (player.IsConnected)
                {
                    if (player != _emptyInput) // Button Pressed
                    {
                        var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                        foreach (var button in buttonList)
                        {
                            if (player.IsButtonDown(button) &&
                                !pastState.IsButtonDown(button))
                                if (Commands.ContainsKey((int)button))
                                    Commands[(int)button].Execute();
                        }
                    }
                    // Update previous gamepad state.
                    past[_count] = player;
                }
                _count++;
                if (_count == 4)
                {
                    _count = 0;
                }
            }
           
        }
    }
    }

